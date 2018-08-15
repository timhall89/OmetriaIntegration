using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib.DataModels
{
    public class OrderExtendedLocalTotal
    {
        public string currency { get; set; }

        public float? shipping { get; set; }

        public float? tax { get; set; }

        public float? discount { get; set; }

        public float? subtotal { get; set; }

        public float? grand_total { get; set; }
    }
}
