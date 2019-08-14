using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntigralHelpDeskCore.BO
{
    [Serializable]
    public class ChargeLimit
    {
        #region "Private Variables"
        private long m_LimitID = 0;
        private string m_MSISDN = "";
        private int m_TelcoID = 0;
        private string m_TelcoName = "";
        private int m_LookupID = 0;
        private string m_ServiceSourceType = "";
        private string m_ServiceName = "";
        private string m_Shortcode = "";
        private string m_Keywords = "";
        private int m_ChargeLimitTypeID = 0; //     1-Single Tranaction/2-Daily/3-Monthly/4-DateRange
        private string m_ChargeLimitTypeCode = "";
        private decimal m_ChargeLimitAmount = 0;
        private decimal m_ChargeLimitAmount_Prepaid = 0;
        private decimal m_ChargeLimitAmount_Postpaid = 0;
        private DateTime m_CreateDateTime = new DateTime(1900, 1, 1);
        private int m_CreatedByLoginID = 0;
        private string m_CreatedByLoginName = "";
        private DateTime m_LastUpdatedDateTime = new DateTime(1900, 1, 1);
        private int m_LastUpdatedByLoginID = 0;
        private string m_LastUpdatedByLoginName = "";
        private bool m_Status = false;
        private string m_StatusText = "";
        private DateTime m_FromDate = new DateTime(1900, 1, 1);
        private DateTime m_ToDate = new DateTime(1900, 1, 1);
        private string m_LimitCategory = string.Empty; //   MSISDN wise/Service wise/Operator wise
        private string m_LimitRefID = string.Empty; //   
        private string m_DateRangeText = string.Empty;
        private string m_IsRecursiveText = string.Empty;
        private bool m_IsRecursive = false;
        private long m_BundleServiceId = 0;
        #endregion

        #region "Public Properties"
        public long LimitID { get { return m_LimitID; } set { m_LimitID = value; } }
        public string MSISDN { get { return m_MSISDN; } set { m_MSISDN = value; } }
        public int TelcoID { get { return m_TelcoID; } set { m_TelcoID = value; } }
        public string TelcoName { get { return m_TelcoName; } set { m_TelcoName = value; } }
        public int LookupID { get { return m_LookupID; } set { m_LookupID = value; } }
        public string ServiceSourceType { get { return m_ServiceSourceType; } set { m_ServiceSourceType = value; } }
        public string ServiceName { get { return m_ServiceName; } set { m_ServiceName = value; } }
        public string Shortcode { get { return m_Shortcode; } set { m_Shortcode = value; } }
        public string Keywords { get { return m_Keywords; } set { m_Keywords = value; } }
        public int ChargeLimitTypeID { get { return m_ChargeLimitTypeID; } set { m_ChargeLimitTypeID = value; } }
        public string ChargeLimitTypeCode { get { return m_ChargeLimitTypeCode; } set { m_ChargeLimitTypeCode = value; } }
        public decimal ChargeLimitAmount { get { return m_ChargeLimitAmount; } set { m_ChargeLimitAmount = value; } }
        public decimal ChargeLimitAmount_Prepaid { get { return m_ChargeLimitAmount_Prepaid; } set { m_ChargeLimitAmount_Prepaid = value; } }
        public decimal ChargeLimitAmount_Postpaid { get { return m_ChargeLimitAmount_Postpaid; } set { m_ChargeLimitAmount_Postpaid = value; } }
        public DateTime CreateDateTime { get { return m_CreateDateTime; } set { m_CreateDateTime = value; } }
        public int CreatedByLoginID { get { return m_CreatedByLoginID; } set { m_CreatedByLoginID = value; } }
        public string CreatedByLoginName { get { return m_CreatedByLoginName; } set { m_CreatedByLoginName = value; } }
        public DateTime LastUpdatedDateTime { get { return m_LastUpdatedDateTime; } set { m_LastUpdatedDateTime = value; } }
        public int LastUpdatedByLoginID { get { return m_LastUpdatedByLoginID; } set { m_LastUpdatedByLoginID = value; } }
        public string LastUpdatedByLoginName { get { return m_LastUpdatedByLoginName; } set { m_LastUpdatedByLoginName = value; } }
        public bool Status { get { return m_Status; } set { m_Status = value; } }
        public string StatusText { get { return m_StatusText; } set { m_StatusText = value; } }
        public DateTime FromDate { get { return m_FromDate; } set { m_FromDate = value; } }
        public DateTime ToDate { get { return m_ToDate; } set { m_ToDate = value; } }
        public string LimitCategory { get { return m_LimitCategory; } set { m_LimitCategory = value; } }
        public string LimitRefID { get { return m_LimitRefID; } set { m_LimitRefID = value; } }
        public string DateRangeText { get { return m_DateRangeText; } set { m_DateRangeText = value; } }
        public string IsRecursiveText { get { return m_IsRecursiveText; } set { m_IsRecursiveText = value; } }
        public bool IsRecursive { get { return m_IsRecursive; } set { m_IsRecursive = value; } }
        public long BundleServiceId { get { return m_BundleServiceId; } set { m_BundleServiceId = value; } }
        #endregion
    }
}
