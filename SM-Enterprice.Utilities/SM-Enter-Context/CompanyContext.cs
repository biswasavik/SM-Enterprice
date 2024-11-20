using Microsoft.EntityFrameworkCore;
using SM_Enterprice.Utilities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM_Enterprice.Utilities.SM_Enter_Context
{
    public class CompanyContext: DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        { }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductGallery> ProductGalleries { get; set; }
    }
}
