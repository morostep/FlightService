using FlightService.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Web.Models
{
    public class BookingFlightViewModel
    {
        [Display(Name = "Booking Number")]
        public string bookingNumber { get; set; }

        [Display(Name = "Flight Number")]
        public string flightNumber { get; set; }

        [Display(Name = "Airline")]
        public string Airline { get; set; }

        [Display(Name = "Departure Date")]
        public string departureDate { get; set; }

        [Display(Name = "Departure Time")]
        public string departureTime { get; set; }

        [Display(Name = "Arrival Date")]
        public string arrivalDate { get; set; }

        [Display(Name = "Arrival Time")]
        public string arrivalTime { get; set; }

        [Display(Name = "Departure Airport")]
        public string departureAirport { get; set; }

        [Display(Name = "Arrival Airport")]
        public string arrivalAirport { get; set; }

        [Display(Name = "Passenger Limit")]
        public int passengerLimit { get; set; }
    }
}
