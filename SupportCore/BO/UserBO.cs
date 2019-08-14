using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDeskCore
{
    [Serializable]
    public class UserBO
    {
        public int LoginID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public string RoleDescription { get; set; }
        public string Name { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UserStatus { get; set; }
        public string PageNames_ReadWrite { get; set; }
        public string PageNames_ReadOnly { get; set; }
        public string PageNames_NoAccess { get; set; }
    }
}
