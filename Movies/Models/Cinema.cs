using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Cinema
    {
        public int id { get; set; }

        [StringLength(30, ErrorMessage = "Name length can't be more than 30.")]
        public String name { get; set; }

        public List<Seat> seats { get; set; }

        public List<CinemaMovie> movies { get; set; }

        public Adress adress { get; set; }

    }
}
