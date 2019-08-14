using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDeskCore
{
    [Serializable]
    public class ContentBO
    {
        public string ContentTypeID { get; set; }
        public string ContentTypeName { get; set; }
        public long ContentId { get; set; }
        public string LoginName { get; set; }
        
    }
}
