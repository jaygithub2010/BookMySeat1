using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookMySeatDAL;
using AutoMapper;

namespace BookMySeat.Controllers
{
    public class BookMySeatController : ApiController
    {
        private IBookMySeatRepository bookingRep;

        public BookMySeatController(IBookMySeatRepository repository)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BookMySeat.Models.Booking, bookMySeat.Booking>());
            bookingRep = repository;
        }

        public List<bookMySeat.Movie> GetMovies()
        {
            try
            {
                return bookingRep.GetMovies();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<bookMySeat.Theatre> GetTheatres(int movieID, DateTime date)
        {
            try
            {
                return bookingRep.GetTheatres(movieID, date);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public bookMySeat.Theatre GetTheatre(int theatreID)
        {
            try
            {
                return bookingRep.GetTheatre(theatreID);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public Dictionary<int, DateTime> GetShowTimes(int movieID, int theatreID, DateTime date)
        {
            try
            {
                return bookingRep.GetShowTimes(movieID, theatreID, date);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<string> GetBookedTickets(int showID)
        {
            try
            {
                return bookingRep.GetBookedSeats(showID);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        [HttpPost]
        public BookMySeat.Models.Booking PostBooking(BookMySeat.Models.Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bookingID = bookingRep.AddBooking(Mapper.Map<BookMySeat.Models.Booking, bookMySeat.Booking>(booking));
                    if (bookingID != "")
                    {
                        booking.BookingID = bookingID;
                        return booking;
                    }
                    return null;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
