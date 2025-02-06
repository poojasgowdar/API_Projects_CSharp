using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.dto
{
    public class ProductUpdateDto
    {
        [Required(ErrorMessage ="The Name Field is Required")]
        [StringLength(100,ErrorMessage = "The Name should be in between 3 and 100 characters", MinimumLength=3)]
        public string Name { get; set; }

        [Required(ErrorMessage ="The Price Field is Required")]
        [Range(0.01,double.MaxValue,ErrorMessage="The Price must be greater than zero")]
        public decimal Price { get; set; }

    }
}
