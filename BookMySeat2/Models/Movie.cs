using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMySeat.Models
{
    public class Movie    
    {
        public int MovieID { get; set; }

        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public TimeSpan RunningTime { get; set; }

    }
}