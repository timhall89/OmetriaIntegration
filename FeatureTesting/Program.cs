using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace FeatureTesting
{
    using OmIntLib;
    using OmIntLib.Systems;
    using System.IO;
    using System.Runtime.InteropServices;

    class Program
    {
       static void Main(string[] args)
        {
            Settings settings = new Settings(File.ReadAllText(args[0]));

            DateTime? dtr = int.TryParse(settings["ProfileFromDate"], out int i) ?
            DateTime.Now.AddDays(i) :
            (DateTime.TryParse(settings["ProfileFromDate"], out DateTime dt) ? dt : (DateTime?)null);

            Console.WriteLine(dtr);
    
            return;
           

            ////new RootMethods(msg => Console.WriteLine(msg), settings).ContactsTestToOmetria();
            ////Console.WriteLine(settings["Prop"]);
            ////Console.WriteLine(settings.JSon);
            ////Properties["DailyProductLoadSQL"] = @"SELECT * FROM ""V66LWHIU#"".OMTR_PROD WHERE ""is_in_stock"" = 1";
            ////File.WriteAllText(args[0], Properties.JSon);
            //string method = "WriteS";
            TestMethods.ometria = new Ometria(settings);
            TestMethods.merretConnString = settings["DBConnString"];
            TestMethods.MainMethod();
        }
    }
}
