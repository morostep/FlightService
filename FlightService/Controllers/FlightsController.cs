using FlightService.Data;
using FlightService.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Web.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightDAO flightDAO;
        public FlightsController(IFlightDAO flightDAO)
        {
            this.flightDAO = flightDAO;
        }

        public IActionResult Index() //gets all the flights and places them into a list of FlightViewModel
        {
            IEnumerable<Flight> flight = flightDAO.ViewFlights();
            List<FlightViewModel> model = new List<FlightViewModel>();
            foreach (var home in flight)
            {
           
                FlightViewModel temp = new FlightViewModel
                {
                    Id = home.Id,

                    flightNumber = home.flightNumber,
                    Airline = home.Airline,
                    departureDate = DateTime.Parse(home.departureDate).ToString("MMMM, dd yyyy"),
                    arrivalDate = DateTime.Parse(home.arrivalDate).ToString("MMMM, dd yyyy"),
                    departureTime = DateTime.Parse(home.departureTime).ToString("h:mm tt", CultureInfo.CurrentCulture),
                    arrivalTime = DateTime.Parse(home.arrivalTime).ToString("h:mm tt", CultureInfo.CurrentCulture),
                    arrivalAirport = home.arrivalAirport,
                    departureAirport = home.departureAirport,
                    passengerLimit = home.passengerLimit
                };

                model.Add(temp);
            }

            return View(model);
        }

        public IActionResult Details(int id) //gets one flight and displays the details of it
        {
            Flight model = flightDAO.GetFlight(id);
            FlightViewModel temp = new FlightViewModel
            {
                Id = model.Id,
                flightNumber = model.flightNumber,
                Airline = model.Airline,
                departureDate = DateTime.Parse(model.departureDate).ToString("MMMM, dd yyyy"),
                arrivalDate = DateTime.Parse(model.arrivalDate).ToString("MMMM, dd yyyy"),
                departureTime = DateTime.Parse(model.departureTime).ToString("h:mm tt", CultureInfo.CurrentCulture),
                arrivalTime = DateTime.Parse(model.arrivalTime).ToString("h:mm tt", CultureInfo.CurrentCulture),
                arrivalAirport = model.arrivalAirport,
                departureAirport = model.departureAirport,
                passengerLimit = model.passengerLimit
            };

            return View(temp);
        }

        [HttpGet]
        public IActionResult Delete(int id) //gets one flight and gives the user the option to delete it
        {
            Flight model = flightDAO.GetFlight(id);

            FlightViewModel temp = new FlightViewModel
            {
                Id = model.Id,
                flightNumber = model.flightNumber,
                Airline = model.Airline,
                departureDate = DateTime.Parse(model.departureDate).ToString("MMMM, dd yyyy"),
                arrivalDate = DateTime.Parse(model.arrivalDate).ToString("MMMM, dd yyyy"),
                departureTime = DateTime.Parse(model.departureTime).ToString("h:mm tt", CultureInfo.CurrentCulture),
                arrivalTime = DateTime.Parse(model.arrivalTime).ToString("h:mm tt", CultureInfo.CurrentCulture),
                arrivalAirport = model.arrivalAirport,
                departureAirport = model.departureAirport,
                passengerLimit = model.passengerLimit
            };

            return View(temp);


        }

        [HttpGet]
        public IActionResult Edit(int id) //get req of the edit page so that the user will be able to edit it
        {
            Flight model = flightDAO.GetFlight(id);

            FlightViewModel temp = new FlightViewModel
            {
                Id = model.Id,
                flightNumber = model.flightNumber,
                Airline = model.Airline,
                departureDate = DateTime.Parse(model.departureDate).ToString("yyyy-MM-dd"),
                arrivalDate = DateTime.Parse(model.arrivalDate).ToString("yyyy-MM-dd"),
                departureTime = model.departureTime,
                arrivalTime = model.arrivalTime,
                arrivalAirport = model.arrivalAirport,
                departureAirport = model.departureAirport,
                passengerLimit = model.passengerLimit
            };

            IEnumerable<string> airlineList = flightDAO.ViewAirlines();

            List<string> airlineModel = new List<string>();

            foreach (var airline in airlineList)
            {
                airlineModel.Add(airline);
            }

            IEnumerable<string> airportList = flightDAO.ViewAirports();

            List<string> airportModel = new List<string>();

            foreach (var airport in airportList)
            {
                airportModel.Add(airport);
            }
            ViewBag.Airline = new SelectList(airlineModel);
            ViewBag.Airport = new SelectList(airportModel);


            return View(temp);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] FlightViewModel flight) //post req of the edit page so that the users edits can be saved and applied
        {

            Flight newFlight = new Flight();
            newFlight.Id = flight.Id;
            newFlight.Airline = flight.Airline;
            newFlight.departureDate = flight.departureDate;
            newFlight.arrivalDate = flight.arrivalDate;
            newFlight.departureTime = flight.departureTime;
            newFlight.arrivalTime = flight.arrivalTime;
            newFlight.arrivalAirport = flight.arrivalAirport;
            newFlight.departureAirport = flight.departureAirport;
            newFlight.passengerLimit = flight.passengerLimit;

           flightDAO.UpdateFlight(newFlight);

            return RedirectToAction("Index");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(FlightViewModel flight) //confirmation that the user wants to delete the flight and goes through with it
        {
            flightDAO.DeleteFlight(flight.Id);
            return RedirectToAction("Index");


        }

        [HttpGet]
        public IActionResult Create() //creates a form page to give the user the ability to create a page
        {
            IEnumerable<string> airlineList = flightDAO.ViewAirlines();

            List<string> airlineModel = new List<string>();

            foreach (var airline in airlineList)
            {
                airlineModel.Add(airline);
            }

            IEnumerable<string> airportList = flightDAO.ViewAirports();

            List<string> airportModel = new List<string>();

            foreach (var airport in airportList)
            {
                airportModel.Add(airport);
            }
            ViewBag.Airline = new SelectList(airlineModel);
            ViewBag.Airport = new SelectList(airportModel);


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] FlightViewModel flight) //post req of the create page so that the users creation can be saved and applied
        {

                Random rand = new Random();
                Flight newFlight = new Flight();
                newFlight.Id = flight.Id;
                    
                newFlight.flightNumber = rand.Next(99, 9999).ToString();
                newFlight.Airline = flight.Airline;
                newFlight.departureDate = flight.departureDate;
                newFlight.arrivalDate = flight.arrivalDate;
                newFlight.departureTime = flight.departureTime;
                newFlight.arrivalTime = flight.arrivalTime;
                newFlight.arrivalAirport = flight.arrivalAirport;
                newFlight.departureAirport = flight.departureAirport;
                newFlight.passengerLimit = flight.passengerLimit;


            flightDAO.AddFlight(newFlight);

                return RedirectToAction("Index");
            

        }

        public IActionResult CheckPassengers(int Id) //check amount of the passengers booked to a flight to know if other passengers can or cannot book on this flight 
        {
            BookingDAO bookingDAO = new BookingDAO();
            IEnumerable<BookingPassenger> bookings = bookingDAO.GetPassengerBookings(Id);

            List<BookingPassengerViewModel> model = new List<BookingPassengerViewModel>();

            foreach (var book in bookings)
            {
                BookingPassengerViewModel temp = new BookingPassengerViewModel
                {
                    bookingNumber = book.bookingNumber.ToUpper(),
                    age = book.age,
                    email = book.email,
                    firstname = book.firstname,
                    lastname = book.lastname,
                    job = book.job
                };

                model.Add(temp);
            }
            ViewBag.Id = Id;

            return View(model);

        }
    }
}
