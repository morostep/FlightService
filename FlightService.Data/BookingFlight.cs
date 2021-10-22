using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightService.Data
{
    public class BookingFlight
    {

        public string bookingNumber { get; set; }

        public string flightNumber { get; set; }
        public string Airline { get; set; }

        public string departureDate { get; set; }
        public string departureTime { get; set; }

        public string arrivalDate { get; set; }
        public string arrivalTime { get; set; }

        public string departureAirport { get; set; }
        public string arrivalAirport { get; set; }
        public int passengerLimit { get; set; }

        public BookingFlight(string bookingNumber, 
            string flightNumber, 
            string Airline, 
            string departureDate, 
            string departureTime, 
            
            string arrivalDate, 
            string arrivalTime, 
            string departureAirport, 
            string arrivalAirport, 
            int passengerLimit)
        {
            this.bookingNumber = bookingNumber;
            this.flightNumber = flightNumber;
            this.Airline = Airline;
            this.departureAirport = departureAirport;
            this.departureDate = departureDate;
            this.departureTime = departureTime;
            this.arrivalAirport = arrivalAirport;
            this.arrivalDate = arrivalDate;
            this.arrivalTime = arrivalTime;
            this.passengerLimit = passengerLimit;
        }
    }
}
