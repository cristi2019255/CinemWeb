using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Movie
    {
        public int id { get; set; }

        [Range(0,10)]
        public float rank { get; set; }
        public String name { get; set; }

        public List<ActorMovie> actors { get; set; }

        public List<CinemaMovie> cinemas { get; set; }

        [Range(0,2000)]
        public float price { get; set; }

        public String Image { get; set; }

        public String ganre { get; set; }

        public String descr { get; set; }
    }
}
