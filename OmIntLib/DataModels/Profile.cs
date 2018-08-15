using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib.DataModels
{
    public class Profile
    {
        [JsonProperty("@type")]
        public string type => "profile";

        public string id { get; set; }

        public string email { get; set; }

        public string name { get; set; }

        public string lifecycle_status { get; set; }

        public bool? seen { get; set; }

        public string customer_id { get; set; }

        public string firstname { get; set; }

        public string middlename { get; set; }

        public string lastname { get; set; }

        public string prefix { get; set; }

        public string suffix { get; set; }

        public string country_id { get; set; }

        public string gender { get; set; }

        public IEnumerable<string> tags { get; set; }

        public DateTime? date_of_birth { get; set; }

        public bool? marketing_optin { get; set; }

        public IEnumerable<ProfileList> lists { get; set; }


    }
}
