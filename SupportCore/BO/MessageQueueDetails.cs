using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDeskCore
{
    [Serializable]
    public class MessageQueueDetails
    {
       public string MSISDN { get; set; }
        public string CPNotificationURL { get; set; }
        public int ServiceID { get; set; }
        public string ServiceType { get; set; }
        public string Keyword { get; set; }
        
    }
}
