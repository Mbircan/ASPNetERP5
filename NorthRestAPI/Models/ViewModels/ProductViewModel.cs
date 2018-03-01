using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthRestAPI.Models.ViewModels
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
    }
}