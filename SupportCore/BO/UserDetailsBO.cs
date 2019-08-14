using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDeskCore
{
    [Serializable]
    public class UserDetailsBO
    {
        public string LoginName { get; set; }
        public int  CPGLoginID { get; set; }
        public int VaultLoginID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public int UPM_AppID { get; set; }
        public string APIPassword { get; set; }
        public string EmailID { get; set; }
        public int RoleID { get; set; }
        public string UserRole { get; set; }
        public string WFStatus { get; set; }
        public string Contact_Mobile { get; set; }
        public int CEAdmin_ID { get; set; }
        public string CEAdmin { get; set; }
        public int AccManager_ID { get; set; }
        public string AcctManager { get; set; }
        public int LockId { get; set; }
        public string LockStatus { get; set; }
        public bool SendPinBySMS { get; set; }
        public bool SendPinByEmail { get; set; }
    }
}
