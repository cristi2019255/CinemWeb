using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Actor
    {
        public int id { get; set; }

        public String name { get; set; }

        public String surname { get; set;}

        public List<ActorMovie> movies { get; set; }
    }
}
