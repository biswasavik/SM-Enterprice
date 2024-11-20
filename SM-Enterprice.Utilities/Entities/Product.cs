using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM_Enterprice.Utilities.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product Name field is required")]
        public string Name { get; set; }
        public int ProductCode { get; set; }

        [Required(ErrorMessage = "Product Description field is required")]
        public string Description { get; set; }
        public double? Price { get; set; }

        public double? AmountPerBox { get; set; }

        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public ICollection<ProductGallery> ProductImages { get; set; }

        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
