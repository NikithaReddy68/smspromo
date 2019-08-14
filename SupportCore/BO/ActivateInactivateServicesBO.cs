using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDeskCore
{
    [Serializable]
    public class ActivateInactivateServicesBO
    {
        public int TelcoID { get; set; }
        public string TelcoName { get; set; }
        public string Shortcode { get; set; }
        public int ShortcodeID { get; set; }
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Keyword { get; set; }
        public int BillServiceId { get; set; }
        public decimal Price { get; set; }
        public string ServiceSourceType { get; set; }
        public string ServiceSourceDesc { get; set; }
        public int cpid { get; set; }
        public string cpName { get; set; }        
        public string ServiceStatus { get; set; }
        public int ServiceSourceTypeID { get; set; }
        public bool ToActivateServices { get; set; }
        public string LookUpIDs { get; set; }
    }
}
