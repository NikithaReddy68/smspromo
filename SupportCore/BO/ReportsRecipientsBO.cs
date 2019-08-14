using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDeskCore
{
    [Serializable]
    public class ReportsRecipientsBO
    {
        public int ReportID { get; set; }
        public int ConfigID { get; set; }
        public string ReportName { get; set; }
        public string Email_To { get; set; }
        public string Email_Cc { get; set; }
        public string Email_Bcc { get; set; }
        public int ConstraintTypeID { get; set; }
        public string ConstraintName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public string ConstraintNameText { get; set; }
        public string Email_Consolidated_To { get; set; }
        public string Email_Consolidated_Cc { get; set; }
        public string Email_Consolidated_Bcc { get; set; }
        public string Email_VivaKuwait_To { get; set; }
        public string Email_VivaKuwait_Cc { get; set; }
        public string Email_VivaKuwait_Bcc { get; set; }
    }

    [Serializable]
    public class ReportsBO
    {
        public int ReportID { get; set; }       
        public string ReportName { get; set; }       
    }

    [Serializable]
    public class ConstraintTypesBO
    {
        public int ConstraintTypeID { get; set; }
        public string ConstraintTypeName { get; set; }
    }

    [Serializable]
    public class FinanceCategoryBO
    {
        public int FinanceCategoryID { get; set; }
        public string FinanceCategoryName { get; set; }
    }
}
