using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Adress
    {
        public int id { get; set; }
        public String number { get; set; }
        public String street { get; set; }
        public String city { get; set; }
        public String country { get; set; }
        public int cinemaId { get; set; }
    }
}
