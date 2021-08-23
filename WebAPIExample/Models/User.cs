using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIExample.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Required()]
        public string UserName { get; set; }

        [StringLength(255)]
        [Required()]
        public string Password { get; set; }
    }
}
