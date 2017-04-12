using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMySeat.Models
{
    public class SeatViewModel
    {
        public string SeatNumber { get; set; }

        public string SeatType { get; set; }

        public bool IsBooked { get; set; }
    }
}