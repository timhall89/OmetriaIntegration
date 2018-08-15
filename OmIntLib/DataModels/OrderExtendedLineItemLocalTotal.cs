using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib.DataModels
{
    public class OrderExtendedLineItemLocalTotal
    {
        public string currency { get; set; }

        public float? unit_price { get; set; }

        public float? discount { get; set; }

        public float? refunded { get; set; }

        public float? subtotal { get; set; }

        public float? tax { get; set; }

        public float? total { get; set; }
    }
}
