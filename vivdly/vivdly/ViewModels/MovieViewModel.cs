using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using vivdly.Models;

namespace vivdly.ViewModels
{
    public class MovieViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        //public Movie Movie { get; set; }


        public int? ID { get; set; }

        [Required]
        public string Name { get; set; }

        
        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        [Required]
        public byte? NumberOfStock { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte? GenreID { get; set; }

        public MovieViewModel()
        {
            ID = 0;
        }

        public MovieViewModel(Movie movie)
        {
            ID = movie.ID;
            Name = movie.Name;
            NumberOfStock = movie.NumberOfStock;
            ReleaseDate = movie.ReleaseDAte;
            GenreID = movie.GenreID;

        }

    }
}