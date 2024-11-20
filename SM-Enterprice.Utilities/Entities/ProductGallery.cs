using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM_Enterprice.Utilities.Entities
{
    public class ProductGallery
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Image URL is required")]
        public string URL { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId{ get; set; }
        public Product Product { get; set; }
    }
}
