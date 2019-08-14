using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDeskCore
{
    [Serializable]
    public class BillingInfo
    {
        private string m_PrepaidCode = string.Empty;
        private string m_PrepaidDescription = string.Empty;
        private string m_PostpaidCode = string.Empty;
        private string m_PostpaidDescription = string.Empty;
        private decimal m_Price = 0;
        private string m_PriceCode = string.Empty;

        public decimal Price
        {
            get { return m_Price; }
            set { m_Price = value; }
        }
        public string PriceCode
        {
            get { if (m_PriceCode == null) m_PriceCode = string.Empty; return m_PriceCode; }
            set { m_PriceCode = value; }
        }
        public string PrepaidCode
        {
            get { if (m_PrepaidCode == null) m_PrepaidCode = string.Empty; return m_PrepaidCode; }
            set { m_PrepaidCode = value; }
        }
        public string PrepaidDescription
        {
            get { if (m_PrepaidDescription == null) m_PrepaidDescription = string.Empty; return m_PrepaidDescription; }
            set { m_PrepaidDescription = value; }
        }
        public string PostpaidCode
        {
            get { if (m_PostpaidCode == null) m_PostpaidCode = string.Empty; return m_PostpaidCode; }
            set { m_PostpaidCode = value; }
        }
        public string PostpaidDescription
        {
            get { if (m_PostpaidDescription == null) m_PostpaidDescription = string.Empty; return m_PostpaidDescription; }
            set { m_PostpaidDescription = value; }
        }

        public BillingInfo()
        {
            m_PrepaidCode = string.Empty;
            m_PrepaidDescription = string.Empty;
            m_PostpaidCode = string.Empty;
            m_PostpaidDescription = string.Empty;
            m_Price = 0;
            m_PriceCode = string.Empty;
        }


    }
}
