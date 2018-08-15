using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib
{
    /// <summary>
    /// Class to manage properties.
    /// </summary>
    public class Settings
    {
        public Settings() { }
        public Settings(string JSon) => this.JSon = JSon;
        
        public string JSon
        {
            get => Properties.ToString();
            set { Properties = JObject.Parse(value); }
        }

        private JObject Properties { get; set; }
        
        public string this[string key]
        {
            get { try { return Properties[key].ToString(); } catch { throw new ArgumentException($"Could not find property value for {key}"); } }
            set { try { Properties[key] = value; } catch { } }
        }
    }
}
