using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib.DataModels
{
    public class OrderExtendedTotals
    {
        [JsonProperty("base")]
        public OrderExtendedBaseTotal Base { get; set; }

        [JsonProperty("local")]
        public OrderExtendedLocalTotal Local { get; set; }
    }
}
