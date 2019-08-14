using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDeskCore
{
    [Serializable]
    public class ChangeSubscriberServicesBO
    {
        public string TelcoID { get; set; }
        public string TelcoName { get; set; }
        public string Shortcode { get; set; }
        public string Subscription_Id { get; set; }
        public string Subscription_Name { get; set; }
        public string Keyword_Id { get; set; }
        public string Keyword { get; set; }
        public int BillServiceId { get; set; }
    }
}
