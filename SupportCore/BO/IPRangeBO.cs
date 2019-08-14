using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDeskCore
{
    [Serializable]
    public class IPRangeBO
    {
        public int SNo { get; set; }
        public int Telco_Id { get; set; }
        public string Telco_Name { get; set; }
        public string IPLowerbound { get; set; }
        public string IPUpperbound { get; set; }
        public int CreatedByLoginID { get; set; }
        public string CreatedByLoginName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int LastUpdatedByLoginID { get; set; }
        public string LastUpdatedByLoginName { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public bool IsActive { get; set; }
        public string IsActiveText { get; set; }
    }
}
