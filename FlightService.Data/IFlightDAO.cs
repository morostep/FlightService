using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightService.Data
{
    public interface IFlightDAO
    {
        public IEnumerable<Flight> ViewFlights();
        public bool AddFlight(Flight flight);
        public bool DeleteFlight(int id);
        public Flight GetFlight(int id);
        public IEnumerable<string> ViewAirlines();
        public IEnumerable<string> ViewAirports();
        public bool UpdateFlight(Flight flight);

    }
}
