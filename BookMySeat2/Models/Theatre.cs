using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMySeat.Models
{
    public class Theatre
    {
        public int TheatreID { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public int PremiumSeats { get; set; }

        public int StandardSeats { get; set; }

        public int PremiumRows { get; set; }

        public int StandardRows { get; set; }

        public decimal PremiumSeatPrice { get; set; }

        public decimal StandardSeatPrice { get; set; }
    }
}