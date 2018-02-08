using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCFundamentals.Models;

namespace MVCFundamentals.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Product Product { get; set; }
    }
}