using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OmIntLib.Data;

namespace OmIntLib.DataModels
{
    public class Contact
    {
        [JsonProperty("@type")]
        public string type => "contact";

        [JsonProperty("@collection")]
        public string collection { get; set; }

        public string id { get; set; }

        public string email { get; set; }

        [JsonConverter(typeof(JsonConverterObjectToString))]
        public string phone_number { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public marketing_optin_options? marketing_optin { get; set; }

        public string customer_id { get; set; }

        [JsonProperty("@add_to_lists")]
        public IEnumerable<int> add_to_lists { get; set; }

        [JsonProperty("@remove_from_lists")]
        public IEnumerable<int> remove_from_lists { get; set; }

        [JsonProperty("@force_optin")]
        public bool? force_optin { get; set; }

        [JsonProperty("@merge")]
        public bool? merge { get; set; } = true;

        public string prefix { get; set; }

        public string firstname { get; set; }

        public string middlename { get; set; }

        public string lastname { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public gender_options? gender { get; set; }

        [JsonConverter(typeof(LongDateConverter))]
        public DateTime? date_of_birth { get; set; }

        public string country_id { get; set; }

        public IEnumerable<string> store_ids { get; set; }

        public string timezone { get; set; }

        public Dictionary<string, string> properties { get; set; } = new Dictionary<string, string>();
        public bool ShouldSerializeproperties() => properties?.GetEnumerator().MoveNext() ?? false;

        public DateTime? timestamp_acquired { get; set; }

        public DateTime? timestamp_subscribed { get; set; }

        public DateTime? timestamp_unsubscribed { get; set; }

        [JsonProperty("@profile_id")]
        public string profile_id { get; set; }
    }
    public enum marketing_optin_options { EXPLICITLY_OPTEDOUT = -1, NOT_SPECIFIED = 0, EXPLICITLY_OPTEDIN = 1}
    public enum gender_options { m = 1, f = 2, o = 0}
}
