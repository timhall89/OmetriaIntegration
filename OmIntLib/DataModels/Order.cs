using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OmIntLib.DataModels
{
    public class Order
    {
        [JsonProperty("@type")]
        public string type => "order";

        public string id { get; set; }

        public DateTime timestamp { get; set; }

        public float? grand_total
        {
            get
            {
                float? val = 0;
                foreach (OrderLineItem ol in lineitems) val += ol.total;
                return val;
            }
        }

        public float? subtotal { get; set; }

        [JsonProperty("@discount")]
        public float? total_discount
        {
            get
            {
                float? val = 0;
                foreach (OrderLineItem ol in lineitems) val += ol.discount;
                return val;
            }
        }

        public float? shipping { get; set; }

        public float? tax { get; set; }

        public string currency { get; set; }

        public OrderExtendedTotals totals { get; set; }

        public string web_id { get; set; }

        public string status { get; set; }

        public bool? is_valid => (grand_total != 0);

        public OrderCustomer customer { get; set; }

        public List<OrderLineItem> lineitems { get; set; } = new List<OrderLineItem>();
        public bool ShouldSerializelineitems() => lineitems?.GetEnumerator().MoveNext() ?? false;

        public string ip_address { get; set; }

        public string channel { get; set; }

        public string store { get; set; }

        public string payment_method { get; set; }

        public string shipping_method { get; set; }

        public OrderAdress shipping_address { get; set; }

        public OrderAdress billing_address { get; set; }

        public string coupon_code { get; set; }

        public Dictionary<string, string> properties { get; set; } = new Dictionary<string, string>();
        public bool ShouldSerializeproperties() => properties?.GetEnumerator().MoveNext() ?? false;

    }
}
