using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib.DataModels
{
    public class OrderCustomer
    {
        [JsonProperty("id")]
        public string customer_id { get; set; }

        public string email { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }
    }
}
