using Microsoft.AspNetCore.Http;
using SM_Enterprice.Utilities.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM_Enterprice.Utilities.Entities
{
    public class ProductModel
    {
        [Required(ErrorMessage = "Product Name field is required")]
        public string Name { get; set; }
        public int ProductCode { get; set; }
        public Guid CompanyId { get; set; }
        [Required(ErrorMessage = "Product Description field is required")]
        public string Description { get; set; }
        public double? Price { get; set; }

        public double? AmountPerBox { get; set; }

        public bool IsActive { get; set; }

        public IFormFile Image { get; set; }
    }
}
