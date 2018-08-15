using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib.DataModels
{
    public class PushError
    {
        public string request_id { get; set; }

        public string record_type { get; set; }

        public string record_id { get; set; }

        public object record { get; set; }

        public DateTime? timestamp { get; set; }

        public PushErrorMessage message { get; set; }
    }
}
