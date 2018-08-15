using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib
{
    using DataModels;
    using Data;
    using Systems;
    using System.Reflection;

    public class RootMethods
    {
        public Action<string> Log { get; set; } = s => { };
        public Settings settings { get; }

        public RootMethods(Action<string> Log, Settings settings)
        {
            this.settings = settings;
            this.Log = Log;
        }

        private string GetSQLFromSettings(string SQLName) => File.ReadAllText($"{settings["SQLFolder"]}{SQLName}");
        private DateTime? ProfileFromDate => 
            int.TryParse(settings["ProfileFromDate"], out int i) ? 
            DateTime.Now.AddDays(i) :
            (DateTime.TryParse(settings["ProfileFromDate"], out DateTime dt) ? dt : (DateTime?)null) ;

        [GetMethod]
        IEnumerable<Contact> GetContatcsFromMerret(string SQLName) =>  
            DataParser.DataTableToObject<Contact>(GetSQLFromSettings(SQLName), settings["DBConnString"], DataParser.CustomReadMethods.GetContactProperties);

        [GetMethod]
        IEnumerable<Product> GetProductsFromMerret(string SQLName) => 
            DataParser.DataTableToObject<Product>(GetSQLFromSettings(SQLName), settings["DBConnString"], DataParser.CustomReadMethods.GetProductProperties);

        [GetMethod]
        IEnumerable<Order> GetTransactionsFromMerret(string SQLName) => 
            DataParser.CustomReadMethods.GetOrders(GetSQLFromSettings(SQLName), settings["DBConnString"]);

        [GetMethod]
        IEnumerable<Profile> GetAllProfiles(string s)
        {
            int offset = 0;
            int n = 250;
            DateTime? dt = ProfileFromDate;
            Ometria ometria = new Ometria(settings);
            bool first = true;
            bool some = false;
            do
            {
                some = false;
                if (!first) System.Threading.Thread.Sleep(300);
                first = false;
                foreach (var profile in DataParser.JSonToObject<IEnumerable<Profile>>(ometria.ListProfiles(n, offset, dt)))
                {
                    some = true;
                    yield return profile;
                }

                offset += n;

            } while (some);
        }

        [SendMethod]
        void PushToOmetria(IEnumerable<object> col) => Log($"{new PushRequest(col, (o, e) => { }).Push(new Ometria(settings))} records pushed");

        [SendMethod]
        void WriteJSonToFile(IEnumerable<object> col) => File.WriteAllText(settings["OutputFilePath"], DataParser.ObjectToJSon(col));

        [SendMethod]
        void WriteStringToFile(string s) => File.WriteAllText(settings["OutputFilePath"], s);

        [SendMethod]
        void WriteProfileCustomToFile(IEnumerable<Profile> col) => File.WriteAllLines(settings["ProfileOutFile"], col.Select(p => $"{p.id},{p.email},{((p.marketing_optin ?? false) ? "Y" : "N")}")); 

        public void RunFlow(string SendMethodName, string GetMethodName, string SQLName)
        {
            Type thisType = GetType();

            MethodInfo SendMethod = thisType.GetMethod(SendMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if ((SendMethod == null) || SendMethod.GetCustomAttribute(typeof(SendMethodAttribute)) == null)
                throw new ArgumentException($"Send Method {SendMethodName} could not be found");
      
            MethodInfo GetMethod = thisType.GetMethod(GetMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if ((GetMethod == null) || GetMethod.GetCustomAttribute(typeof(GetMethodAttribute)) == null)
                throw new ArgumentException($"Get Method {GetMethodName} could not be found");

            try { SendMethod?.Invoke(this, new object[] { GetMethod.Invoke(this, new object[] { SQLName }) }); }
            catch (TargetInvocationException ex) { throw ex.InnerException; }
        }

        public static IEnumerable<string> GetMethodNames
            => typeof(RootMethods).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => m.GetCustomAttribute(typeof(GetMethodAttribute)) != null)
            .Select(m => m.Name);

        public static IEnumerable<string> SendMethodNames
            => typeof(RootMethods).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => m.GetCustomAttribute(typeof(SendMethodAttribute)) != null)
            .Select(m => m.Name);
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class GetMethodAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class SendMethodAttribute : Attribute { }
}
