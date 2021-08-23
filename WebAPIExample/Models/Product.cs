using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIExample.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [StringLength(255)]
        public string ProductName { get; set; }
        public DateTime IntroductionDate { get; set; }
        public float Price { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
