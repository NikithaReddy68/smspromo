using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntigralHelpDeskCore.BO
{
    [Serializable]
    public class ChargeLimitType
    {
        #region "Private Variables"
        int m_LimitTypeID = 0;
        string m_LimitTypeCode = "";
        string m_LimitTypeDescription = "";
        #endregion

        #region "Public Variables"
        public int LimitTypeID { get { return m_LimitTypeID; } set { m_LimitTypeID = value; } }
        public string LimitTypeCode { get { return m_LimitTypeCode; } set { m_LimitTypeCode = value; } }
        public string LimitTypeDescription { get { return m_LimitTypeDescription; } set { m_LimitTypeDescription = value; } }
        #endregion
    }
}
