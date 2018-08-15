using Newtonsoft.Json;
using OmIntLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OmIntLib.DataModels
{
    public class Product
    {
        [JsonProperty("@type")]
        public string type => "product";

        public string id { get; set; }

        public string title { get; set; }

        public bool? is_variant { get; set; }

        public float? price { get; set; }

        public string sku { get; set; }

        public DateTime? special_price_dt_from { get; set; }

        public DateTime? special_price_dt_to { get; set; }

        public float? special_price { get; set; }

        public bool? is_active { get; set; }

        public bool? is_in_stock { get; set; }

        public string url { get; set; }

        public string image_url { get; set; }

        public List<OmertiraAttribute> attributes { get; set; } = new List<OmertiraAttribute>();
        public bool ShouldSerializeattributes() => attributes?.GetEnumerator().MoveNext() ?? false;

        public IEnumerable<ProductListing> listings { get; set; }

        public Dictionary<string, string> properties { get; set; } = new Dictionary<string, string>();
        public bool ShouldSerializeproperties() => properties?.GetEnumerator().MoveNext() ?? false;
    }
}
