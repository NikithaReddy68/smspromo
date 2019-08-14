using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace HelpDeskCore
{
    [Serializable]
    public class LookupKeywordBO
    {
        public int LookupID { get; set; }
        public int TelcoID { get; set; }
        public string Shortcode { get; set; }
        public string Keyword { get; set; }
        public string SourceName { get; set; }
        public string SourceType { get; set; }
        public string LookupIDKeyword { get; set; }
        public string ServiceName { get; set; }
        public int ServiceId { get; set; }
        private BillingInfo[] m_BillingInfoList = new BillingInfo[0];
        public BillingInfo[] BillingInfoList
        {
            get { return m_BillingInfoList; }
            set { m_BillingInfoList = value; }
        }
       

    }
}
