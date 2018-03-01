using System.ComponentModel.DataAnnotations;

namespace NorthRestAPI.Models.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        public string ProductName { get; set; }
        public int? CategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
    }
}