using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class CinemaMovie
    {
       
        public int cinemaId { get; set; }
        public Cinema cinema { get; set; }
        public int movieId { get; set; }
        public Movie movie { get; set; }  
        
        public DateTime dateIn { get; set; }
        public DateTime dateOut { get; set; }
    }
}
