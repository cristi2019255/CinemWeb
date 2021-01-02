using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class SeatDate
    {
        [Key]
        public int id { get; set; }
        public int seatId { get; set; }
        public Seat seat { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public String userId { get; set; }
    }
}
