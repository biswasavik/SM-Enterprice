using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM_Enterprice.Utilities.Entities
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Cpmpany Name field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description field is required")]
        public string Description { get; set; }
        public int RegistrationCode {  get; set; }  
        [Required(ErrorMessage = "Contact Number field is required")]
        //[MinLength(10)]
        [Phone(ErrorMessage = "Company Phone Number is Invalid!")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Address field is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Type field is required")]
        public string Type {  get; set; }
        [Required(ErrorMessage = "Manager field is required")]
        public string ManagerName {  get; set; }
        [Required(ErrorMessage = "Type field is required")]
        [Phone(ErrorMessage = "Manager Phone Number is Invalid!")]
        public string ManagerContactNo {  get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}
