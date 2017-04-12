using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BookMySeat.Models
{
    public class Booking
    {
        public string BookingID { get; set; }

        [Required]
        public string Seats { get; set; }

        [Required]
        public int ShowID { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string BookedBy { get; set; }

        [Required]
        public double Price { get; set; }
    }
}

