using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BookMySeatDAL;
using AutoMapper;

namespace BookMySeat.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home\
        IBookMySeatRepository rep;
        public HomeController(IBookMySeatRepository repository)
        {
            rep = repository;
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.Movie, bookMySeat.Movie>();
                cfg.CreateMap<bookMySeat.Movie, Models.Movie>();
                cfg.CreateMap<Models.Theatre, bookMySeat.Theatre>();
                cfg.CreateMap<bookMySeat.Theatre, Models.Theatre>();
                cfg.CreateMap<Models.Booking, bookMySeat.Booking>();
                cfg.CreateMap<bookMySeat.Booking, Models.Booking>();
            });
        }

        public ActionResult Index()
        {
            Session["MovieName"] = "";
            Session["ShowTime"] = "";
            return View();            
        }

        public ActionResult DisplayMovies()
        {
            try
            { 
            List<BookMySeat.Models.Movie> mvs =Mapper.Map<List<bookMySeat.Movie>, List< Models.Movie >>((rep.GetMovies()));
            
            return View(mvs);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        public ActionResult DisplayTheatres(int movieID, string movieName, DateTime date)
        {            
            try
            {
                Session["MovieName"] = movieName;
                ViewBag.MovieID = movieID;
                
                var theatres =Mapper.Map<List<bookMySeat.Theatre>, List<Models.Theatre>>(rep.GetTheatres(movieID, date));
                
                return View(theatres);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        public ActionResult ShowTimes(int movieID, int theatreID, DateTime date)
        {
            try
            {
                Dictionary<int,DateTime> showTimes = rep.GetShowTimes(movieID, theatreID, date);
                return PartialView("_ShowTimes", showTimes);
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult DisplaySeats(int showID, DateTime showTime)
        {

            try
            {
                Session["ShowTime"] = showTime;
                var theatre = Mapper.Map<bookMySeat.Theatre, Models.Theatre>(rep.GetTheatre(showID));
                var bookedSeats = rep.GetBookedSeats(showID);

                ViewBag.ShowID = showID;
                ViewBag.Theatre = theatre;
                List<Models.SeatViewModel> seats = new List<Models.SeatViewModel>();

                char seatLetter = 'A';
                for (int i = 0; i < theatre.PremiumRows; i++)
                {
                    for (int j = 0; j < theatre.PremiumSeats / theatre.PremiumRows; j++)
                    {
                        Models.SeatViewModel seat = new Models.SeatViewModel();
                        seat.SeatNumber = seatLetter + j.ToString();
                        seat.SeatType = "Premium";
                        seats.Add(seat);

                    }
                    seatLetter++;
                }
                for (int i = 0; i < theatre.StandardRows; i++)
                {
                    for (int j = 0; j < theatre.StandardSeats / theatre.StandardRows; j++)
                    {
                        Models.SeatViewModel seat = new Models.SeatViewModel();
                        seat.SeatNumber = seatLetter + j.ToString();
                        seat.SeatType = "Standard";
                        seats.Add(seat);
                    }
                    seatLetter++;
                }

                foreach (var seat in seats)
                {
                    if (bookedSeats.Contains(seat.SeatNumber))
                    {
                        seat.IsBooked = true;
                    }
                }
                return View(seats);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }


        public ActionResult Booking(Models.Booking booking)
        {

            try
            {
                booking.BookingID = rep.AddBooking(Mapper.Map<Models.Booking, bookMySeat.Booking>(booking));
                if (booking.BookingID == "")
                    return View("Error");
                return View("BookingDetails",booking);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

    }
}