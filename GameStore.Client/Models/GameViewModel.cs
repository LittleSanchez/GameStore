using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Client.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter game's name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter price of game")]
        [Range(1, 200, ErrorMessage = "Price is out of range [1; 200]")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Please enter year")]
        public int Year { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Developer { get; set; }
    }
}