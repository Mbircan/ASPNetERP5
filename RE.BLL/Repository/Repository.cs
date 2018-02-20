using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RE.Models.Entities;

namespace RE.BLL.Repository
{
    public class Repository
    {
        public class ProductRepo : RepositoryBase<Product, int> { }
        public class CategoryRepo : RepositoryBase<Category, int> { }
        public class EmployeeRepo : RepositoryBase<Employee, int> { }
        public class ShipperRepo : RepositoryBase<Shipper, int> { }
        public class SupplierRepo : RepositoryBase<Supplier, int> { }
        public class CustomerRepo : RepositoryBase<Customer, string> { }
        public class OrderDetailRepo : RepositoryBase<Order_Detail, int> { }
    }
}
