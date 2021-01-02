using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class ActorMovie
    {
 
        public int actorId { get; set; }
        public Actor actor { get; set; }
        public int movieId { get; set; }
        public Movie movie { get; set; }
        public String role { get; set; }
    }
}
