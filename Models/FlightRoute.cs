using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace web2020apr_p08_t5.Models
{
    public class FlightRoute
    {
        [Display(Name = "Route ID")]
        public int RouteID { get; set; }

        [Display(Name = "Departure City")]
        [StringLength(50, ErrorMessage =
            "Status cannot exceed 50 characters")]
        public string DepartureCity { get; set; }

        [Display(Name = "Departure Country")]
        [StringLength(50, ErrorMessage =
            "Status cannot exceed 50 characters")]
        public string DepartureCountry { get; set; }

        [Display(Name = "Arrival City")]
        [StringLength(50, ErrorMessage =
            "Status cannot exceed 50 characters")]
        public string ArrivalCity { get; set; }

        [Display(Name = "Arrival Country")]
        [StringLength(50, ErrorMessage =
            "Status cannot exceed 50 characters")]
        public string ArrivalCountry { get; set; }

        [Display(Name = "Flight Duration")]
        [Required(ErrorMessage = "Invalid duration! Please enter integer for duration.")]
        public int? FlightDuration { get; set; }
    }
}

