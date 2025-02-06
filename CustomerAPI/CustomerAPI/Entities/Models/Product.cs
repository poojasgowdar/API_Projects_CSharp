using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="The Name field is Required.")]
        [StringLength(100,ErrorMessage = "The Name must be between 3 and 100 characters.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The Price must be greater than 0.")]
        public decimal Price { get; set; }
    }
}


