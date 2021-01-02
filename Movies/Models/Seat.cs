using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Seat
    {
        public int id { get; set; }
        public int price { get; set; }
        public int cinemaId { get; set; }        
        public int number { get; set; }
    }
}
