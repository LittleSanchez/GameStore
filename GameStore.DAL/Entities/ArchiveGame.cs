using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class ArchiveGame
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Year { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }


        // Foreign keys
        public int? GenreId { get; set; }
        public int? DeveloperId { get; set; }


        public virtual Developer Developer { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
