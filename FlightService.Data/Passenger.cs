using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightService.Data
{
    public class Passenger
    {
        public int Id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string job { get; set; }
        public string email { get; set; }
        public int age { get; set; }

        public Passenger() { }

        public Passenger(int Id, string firstname, string lastname, string job, string email, int age)
        {
            this.Id = Id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.job = job;
            this.email = email;
            this.age = age;
        }
    }
}
