using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class MovieView
    {
        public int id { get; set; }

        public int rank { get; set; }
        public String name { get; set; }

        public List<ActorMovie> actors { get; set; }

        public int price { get; set; }


        public String ganre { get; set; }

        public String descr { get; set; }

        public IFormFile Image { get; set; }
    }
}
