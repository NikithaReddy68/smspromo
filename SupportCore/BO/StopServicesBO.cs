using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDeskCore
{
    public class StopServicesBO
    {
        public int TelcoID { get; set; }
        public string TelcoName { get; set; }
        public string Shortcode { get; set; }
        public int Subscription_Id { get; set; }
        public string Subscription_Name { get; set; }
        public string Keyword { get; set; }
        public int BillServiceId { get; set; }
        public decimal price { get; set; }
        public string SrvSourceType { get; set; }
        public string SrvSourceDesc { get; set; }
        public int cpid { get; set; }
        public string cpName { get; set; }
        public string formatKwd { get; set; }
        public string clpStatus { get; set; }
        public int ServicesCount { get; set; }

    }
}
