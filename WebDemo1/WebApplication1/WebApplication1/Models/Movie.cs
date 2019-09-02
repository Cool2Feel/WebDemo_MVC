﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Movie
    {
        public int ID
        {
            get; set;
        }
        [Required]
        [StringLength(200)]
        public string Title
        {
            get; set;
        }

        public DateTime ReleaseDate
        {
            get; set;
        }

        public string Genre
        {
            get; set;
        }

        public decimal Price
        {
            get; set;
        }
    }

    public class MovieDBContext :DbContext
    {
        public DbSet<Movie> Movies
        {
            get; set;
        }
    }

}