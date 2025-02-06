using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Dto
{
    public  class ProductCreateDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        [Range(0.01, double.MaxValue)]
        public Decimal Price { get; set; }
    }
}
