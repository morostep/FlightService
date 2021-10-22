using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightService.Data
{
    public interface IPassengerDAO
    {
        public IEnumerable<Passenger> ViewPassengers();
        public bool AddPassenger(Passenger passenger);
        public bool DeletePassenger(int Id);
        public Passenger GetPassenger(int Id);
        public bool UpdatePassenger(Passenger passenger);


    }
}
