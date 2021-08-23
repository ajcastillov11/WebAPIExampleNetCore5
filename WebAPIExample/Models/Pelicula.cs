using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIExample.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string NombrePelicula { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        [StringLength(255)]
        public string Director { get; set; }
    }
}
