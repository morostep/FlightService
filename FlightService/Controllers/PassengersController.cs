using FlightService.Data;
using FlightService.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Web.Controllers
{
    public class PassengersController : Controller
    {
        private readonly IPassengerDAO passengerDAO;
        public PassengersController(IPassengerDAO passengerDAO)
        {
            this.passengerDAO = passengerDAO;
        }

        public IActionResult Index()
        {
            IEnumerable<Passenger> passenger = passengerDAO.ViewPassengers();
            List<PassengerViewModel> model = new List<PassengerViewModel>();

            foreach (var home in passenger)
            {
                PassengerViewModel temp = new PassengerViewModel
                {
                    Id = home.Id,
                    age = home.age,
                    email = home.email,
                    firstname = home.firstname,
                    lastname = home.lastname,
                    job = home.job

                };

                model.Add(temp);
            }

            return View(model);
        }

        public IActionResult Details(int Id)
        {
            Passenger model = passengerDAO.GetPassenger(Id);

            PassengerViewModel temp = new PassengerViewModel
            {
                Id = model.Id,
                age = model.age,
                email = model.email,
                firstname = model.firstname,
                lastname = model.lastname,
                job = model.job

            };

            return View(temp);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Passenger model = passengerDAO.GetPassenger(Id);

            PassengerViewModel temp = new PassengerViewModel
            {
                Id = model.Id,
                age = model.age,
                email = model.email,
                firstname = model.firstname,
                lastname = model.lastname,
                job = model.job

            };

            return View(temp);


        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Passenger model = passengerDAO.GetPassenger(Id);

            
                PassengerViewModel temp = new PassengerViewModel
                {
                    Id = model.Id,
                    age = model.age,
                    email = model.email,
                    firstname = model.firstname,
                    lastname = model.lastname,
                    job = model.job

                };

            

            return View(temp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] PassengerViewModel passenger)
        {

            Passenger newPassenger = new Passenger();

            newPassenger.firstname = passenger.firstname;
            newPassenger.lastname = passenger.lastname;
            newPassenger.job = passenger.job;
            newPassenger.email = passenger.email;
            newPassenger.age = passenger.age;
            newPassenger.Id = passenger.Id;

            passengerDAO.UpdatePassenger(newPassenger);

            return RedirectToAction("Index");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(PassengerViewModel passenger)
        {
            passengerDAO.DeletePassenger(passenger.Id);
            return RedirectToAction("Index");


        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] PassengerViewModel passenger)
        {
          
                Passenger newPassenger = new Passenger();

                newPassenger.firstname = passenger.firstname;
                newPassenger.lastname = passenger.lastname;
                newPassenger.job = passenger.job;
                newPassenger.email = passenger.email;
                newPassenger.age = passenger.age;

            passengerDAO.AddPassenger(newPassenger);

                return RedirectToAction("Index");
           

        }

        [HttpGet]
        public IActionResult BookFlight(int id)
        {
            FlightDAO flightDAO = new FlightDAO();
            BookingDAO bookingDAO = new BookingDAO();

            IEnumerable<Flight> flight = flightDAO.ViewFlights();
            
            List<FlightViewModel> model = new List<FlightViewModel>();

            foreach (var book in flight)
            {
                int passengerAmount = bookingDAO.CheckPassengersAmount(book.Id);
                int bkcheck = bookingDAO.CheckIfBooked(id, book.Id);
                if (bkcheck==0)
                {



                    FlightViewModel temp = new FlightViewModel
                    {
                        Id = book.Id,

                        flightNumber = book.flightNumber,
                        Airline = book.Airline,
                        departureDate = DateTime.Parse(book.departureDate).ToString("MMMM, dd yyyy"),
                        arrivalDate = DateTime.Parse(book.arrivalDate).ToString("MMMM, dd yyyy"),
                        departureTime = DateTime.Parse(book.departureTime).ToString("h:mm tt", CultureInfo.CurrentCulture),
                        arrivalTime = DateTime.Parse(book.arrivalTime).ToString("h:mm tt", CultureInfo.CurrentCulture),
                        arrivalAirport = book.arrivalAirport,
                        departureAirport = book.departureAirport,
                        passengerLimit = book.passengerLimit,
                        passengerAmount = passengerAmount
                    };
                    model.Add(temp);

                }
            }

            ViewBag.Id = id;

            return View(model);
        }

        public IActionResult BookFlights([Bind] int FlightId, [Bind] int PassengerId)
        {

            BookingDAO bookingDAO = new BookingDAO();
        
            Booking b = new Booking("", 
                                    FlightId, 
                                    PassengerId);

            bookingDAO.BookFlight(b);
         

            return RedirectToAction("Index");


        }

        public IActionResult CheckFlight(int Id)
        {
            BookingDAO bookingDAO = new BookingDAO();
            IEnumerable <BookingFlight> bookings = bookingDAO.GetFlightBookings(Id);

            List<BookingFlightViewModel> model = new List<BookingFlightViewModel>();

            foreach (var book in bookings)
            {
                BookingFlightViewModel temp = new BookingFlightViewModel
                {
                    bookingNumber = book.bookingNumber.ToUpper(),
                    flightNumber = book.flightNumber,
                    Airline = book.Airline,
                    departureDate = DateTime.Parse(book.departureDate).ToString("MMMM, dd yyyy"),
                    arrivalDate = DateTime.Parse(book.arrivalDate).ToString("MMMM, dd yyyy"),
                    departureTime = DateTime.Parse(book.departureTime).ToString("h:mm tt", CultureInfo.CurrentCulture),
                    arrivalTime = DateTime.Parse(book.arrivalTime).ToString("h:mm tt", CultureInfo.CurrentCulture),
                    arrivalAirport = book.arrivalAirport,
                    departureAirport = book.departureAirport,
                    passengerLimit = book.passengerLimit
                };

                model.Add(temp);
            }
            ViewBag.Id = Id;


            return View(model);

        }

        public IActionResult DeleteBooking(string id)
        {
            BookingDAO bookingDAO = new BookingDAO();
            bookingDAO.DeleteBooking(id);

            return RedirectToAction("Index");

        }
    }
}
