using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntigralHelpDeskCore.BO
{
    [Serializable]
    public class BlockedServiceMsisdnsBO
    {
        public long BlockedID { get; set; }
        public long LookupID { get; set; }
        public string Msisdn { get; set; }
        public DateTime BlockedDateTime { get; set; }
        public DateTime UnblockedDateTime { get; set; }
        public string BlockedSource { get; set; }
        public int UnblockedBy { get; set; }
        public string Remarks { get; set; }
        public bool IsBlocked { get; set; }
        public int SubscriptionID { get; set; }
        public string LoginName { get; set; }
        public string ServiceName { get; set; }
        public string BillingType { get; set; }
        public decimal Price { get; set; }
        public string TelcoName { get; set; }
        public string Shortcode { get; set; }
        public string Keywords { get; set; }
    }    
}
