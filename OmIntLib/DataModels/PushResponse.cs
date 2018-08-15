using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OmIntLib.DataModels
{
    public class PushResponse
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public status_options? status { get; set; }

        public string request_id { get; set; }

        public int accepted { get; set; }

        public int rejected { get; set; }

        public int skipped { get; set; }
    }

    public enum status_options { OK = 0, ERROR = 1}
}
