using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Web.Models
{
    public class FlightViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Flight Number")]
        public string flightNumber { get; set; }

        [Required]
        [Display(Name = "Airline")]
        public string Airline { get; set; }

        [Required]
        [Display(Name = "Departure Date")]
        public string departureDate { get; set; }

        [Required]
        [Display(Name = "Departure Time")]
        public string departureTime { get; set; }

        [Required]
        [Display(Name = "Arrival Date")]
        public string arrivalDate { get; set; }

        [Required]
        [Display(Name = "Arrival Time")]
        public string arrivalTime { get; set; }

        [Required]
        [Display(Name = "Departure Airport")]
        public string departureAirport { get; set; }

        [Required]
        [Display(Name = "Arrival Airport")]
        public string arrivalAirport { get; set; }

        [Required]
        [Display(Name = "Passenger Limit")]
        public int passengerLimit { get; set; }


        public int passengerAmount { get; set; }
    }
}
