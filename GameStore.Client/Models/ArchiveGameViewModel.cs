using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Client.Models
{
    public class ArchiveGameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Genre { get; set; }
    }
}