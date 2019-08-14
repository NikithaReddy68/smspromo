using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelpDeskCore
{
    [Serializable]
 public   class PriceListBO
    {
        public int Price_Id { get; set; }
        public double Price { get; set; }
        public string PrepaidCode { get; set; }
        public string PrepaidDescription { get; set; }
        public string PostpaidCode { get; set; }
        public string PostpaidDescription { get; set; }
    }
}
