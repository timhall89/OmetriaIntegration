using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib.DataModels
{
    public class OrderExtendedLineItemTotals
    {
        [JsonProperty("base")]
        public OrderExtendedLineItemBaseTotal Base { get; set; }

        [JsonProperty("local")]
        public OrderExtendedLineItemLocalTotal Local { get; set; }
    }
}
