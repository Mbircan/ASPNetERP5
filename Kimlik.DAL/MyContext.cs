using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kimlik.Models.Entities;
using Kimlik.Models.IdentityModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Kimlik.DAL
{
    public class MyContext:IdentityDbContext<ApplicationUser>
    {
        public MyContext() : base("name=MyCon")
        {
        }
        public virtual DbSet<Message> Messages { get; set; }
    }
}
