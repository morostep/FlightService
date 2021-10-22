using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Web.Models
{
    public class BookingPassengerViewModel
    {
        [Display(Name = "Booking Number")]
        public string bookingNumber { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastname { get; set; }

        [Required]
        [Display(Name = "Job")]
        public string job { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Age")]
        public int age { get; set; }
    }
}
