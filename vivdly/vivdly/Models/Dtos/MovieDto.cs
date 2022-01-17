using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vivdly.Models.Dtos
{
    public class MovieDto
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DateAdded { get; set; }

        [Range(1, 20)]
        public byte NumberOfStock { get; set; }

        public DateTime ReleaseDAte { get; set; }

        public GenreDto Genre { get; set; }

        public byte GenreID { get; set; }
    }
}