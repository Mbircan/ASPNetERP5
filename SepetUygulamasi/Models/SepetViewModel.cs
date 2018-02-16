using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SepetUygulamasi.Models
{
    public class SepetViewModel
    {
        public int ProductID { get; set; }
        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    }
}