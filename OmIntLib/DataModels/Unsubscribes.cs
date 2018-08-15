using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib.DataModels
{
    class Unsubscribes
    {
        public string profile_id { get; set; }

        public string email_address { get; set; }

        public string type { get; set; }

        public DateTime timestamp { get; set; }

        public string reason { get; set; }
    }
}
