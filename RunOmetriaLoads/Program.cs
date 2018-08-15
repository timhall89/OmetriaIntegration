using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmIntLib;
using System.Reflection;

namespace RunOmetriaLoads
{
    class Program
    {
        static void Main(string[] args)
        {

            Settings settings;
            string SendMethodName = string.Empty;
            string GetMethodName = string.Empty;
            string SQLName = string.Empty;

            try { settings = new Settings(File.ReadAllText(args[0])); } catch { return; }
            Action<string> Log = s => File.AppendAllText(settings["LogFilePath"], $"{DateTime.Now:yyyy/MM/dd HH:mm:ss} || {s}{Environment.NewLine}");

            try {
                try { SendMethodName = args[1]; } catch { throw new ArgumentException("No SendMethodName found in argument list"); }
                try { GetMethodName = args[2]; } catch { throw new ArgumentException("No GetMethodName found in argument list"); }
                try { SQLName = args[3]; } catch { }

                new RootMethods(s => Log($"{SQLName} || {s}"), settings).RunFlow(SendMethodName, GetMethodName, SQLName);
            } catch (Exception ex) { Log(ex.Message); }
        }
    }
}
