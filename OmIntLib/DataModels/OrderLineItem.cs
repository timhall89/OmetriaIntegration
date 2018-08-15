using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib.DataModels
{
    public class OrderLineItem
    {
        public string product_id { get; set; }

        public string variant_id { get; set; }

        public int? quantity { get; set; }

        public string sku { get; set; }

        public float? unit_price { get; set; }

        public int quantity_refunded { get; set; }

        public float? refunded { get; set; }

        public float? subtotal { get; set; }

        public float? tax { get; set; }

        public float? total { get; set; }

        public float? discount { get; set; }

        public bool? is_on_sale { get; set; }

        public List<OmertiraAttribute> variant_options { get; set; } = new List<OmertiraAttribute>();
        public bool ShouldSerializevariant_options() => variant_options?.GetEnumerator().MoveNext() ?? false;

        public OrderExtendedLineItemTotals totals { get; set; }

        public Dictionary<string, string> properties { get; set; } = new Dictionary<string, string>();
        public bool ShouldSerializeproperties() => properties?.GetEnumerator().MoveNext() ?? false;
    }
}
