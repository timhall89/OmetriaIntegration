using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib.DataModels
{
    public class ProductListing
    {
        public string title { get; set; }

        public float? price { get; set; }

        public string store { get; set; }

        public string currency { get; set; }

        public bool? is_active { get; set; }

        public float? special_price { get; set; }

        public string url { get; set; }

        public string image_url { get; set; }

      
    }
}
