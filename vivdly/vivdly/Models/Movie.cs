using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vivdly.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(1,20)]
        public byte NumberOfStock { get; set; }

        [Display(Name="Release Date")]
        public DateTime ReleaseDAte { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        public byte GenreID { get; set; }
    }
}