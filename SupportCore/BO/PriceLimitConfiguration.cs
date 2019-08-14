using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntigralHelpDeskCore.BO
{
    [Serializable]
    public class PriceLimitConfiguration
    {
        #region "Private Variables"

        private long m_ID = 0;
        private string m_MSISDN = string.Empty;
        private int m_Telco_ID = 0;
        private string m_Telco_Name = string.Empty;
        private decimal m_Daily_Price_Limit = 0;
        private decimal m_Daily_Available_Price = 0;
        private DateTime m_Daily_Available_Price_LastUpdatedDateTime = DateTime.Now;
        private decimal m_Monthly_Price_Limit = 0;
        private decimal m_Monthly_Available_Price = 0;
        private DateTime m_Monthly_Available_Price_LastUpdatedDateTime = DateTime.Now;
        private int m_CreatedByLoginID = 0;
        private string m_CreatedByLoginName = string.Empty;
        private DateTime m_CreatedDate = DateTime.Now;
        private int m_LastUpdatedByLoginID = 0;
        private string m_LastUpdatedByLoginName = string.Empty;
        private DateTime m_LastUpdatedDate = DateTime.Now;
        private int m_Status = 1;
        private string m_StatusText = string.Empty;
        private bool m_IsChargeLimitAllowed = false;
        private string m_IsChargeLimitAppliedText = string.Empty;
        private bool m_IsActive = false;
        private bool m_IsInsert = false;
        #endregion

        #region "Public Properties"

        public long ID { get { return m_ID; } set { m_ID = value; } }
        public string MSISDN { get { return m_MSISDN; } set { m_MSISDN = value; } }
        public int Telco_ID { get { return m_Telco_ID; } set { m_Telco_ID = value; } }
        public string Telco_Name { get { return m_Telco_Name; } set { m_Telco_Name = value; } }
        public decimal Daily_Price_Limit { get { return m_Daily_Price_Limit; } set { m_Daily_Price_Limit = value; } }
        public decimal Daily_Available_Price { get { return m_Daily_Available_Price; } set { m_Daily_Available_Price = value; } }
        public DateTime Daily_Available_Price_LastUpdatedDateTime { get { return m_Daily_Available_Price_LastUpdatedDateTime; } set { m_Daily_Available_Price_LastUpdatedDateTime = value; } }
        public decimal Monthly_Price_Limit { get { return m_Monthly_Price_Limit; } set { m_Monthly_Price_Limit = value; } }
        public decimal Monthly_Available_Price { get { return m_Monthly_Available_Price; } set { m_Monthly_Available_Price = value; } }
        public DateTime Monthly_Available_Price_LastUpdatedDateTime { get { return m_Monthly_Available_Price_LastUpdatedDateTime; } set { m_Monthly_Available_Price_LastUpdatedDateTime = value; } }
        public int CreatedByLoginID { get { return m_CreatedByLoginID; } set { m_CreatedByLoginID = value; } }
        public string CreatedByLoginName { get { return m_CreatedByLoginName; } set { m_CreatedByLoginName = value; } }
        public DateTime CreatedDate { get { return m_CreatedDate; } set { m_CreatedDate = value; } }
        public int LastUpdatedByLoginID { get { return m_LastUpdatedByLoginID; } set { m_LastUpdatedByLoginID = value; } }
        public string LastUpdatedByLoginName { get { return m_LastUpdatedByLoginName; } set { m_LastUpdatedByLoginName = value; } }
        public DateTime LastUpdatedDate { get { return m_LastUpdatedDate; } set { m_LastUpdatedDate = value; } }
        public int Status { get { return m_Status; } set { m_Status = value; } }
        public string StatusText { get { return m_StatusText; } set { m_StatusText = value; } }
        public bool IsChargeLimitAllowed { get { return m_IsChargeLimitAllowed; } set { m_IsChargeLimitAllowed = value; } }
        public string IsChargeLimitAppliedText { get { return m_IsChargeLimitAppliedText; } set { m_IsChargeLimitAppliedText = value; } }
        public bool IsActive { get { return m_IsActive; } set { m_IsActive = value; } }
        public bool IsInsert { get { return m_IsInsert; } set { m_IsInsert = value; } }
        #endregion
    }
}
