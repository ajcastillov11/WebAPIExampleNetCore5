using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIExample.Models
{
    public class UserClaim
    {
        [Key]
        public int ClaimId { get; set; }
        public int UserId { get; set; }
        [StringLength(255)]
        [Required]
        public string ClaimType { get; set; }
        [Required]
        public bool ClaimValue { get; set; }
    }
}
