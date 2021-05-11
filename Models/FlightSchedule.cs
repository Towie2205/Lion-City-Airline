using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace web2020apr_p08_t5.Models
{
    public class FlightSchedule
    {
        [Display(Name = "ScheduleID")]
        public int ScheduleID { get; set; }

        [Display(Name = "Flight Number")]
        [StringLength(20, ErrorMessage =
            "Flight Number cannot exceed 20 characters")]

        public string FlightNumber { get; set; }
        [Display(Name = "Route ID")]
        public int RouteID { get; set; }

        [Display(Name = "Aircraft ID")]
        public int? AircraftID { get; set; }

        [Display(Name = "Departure Date & Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd 0: HH:mm}")]
        public DateTime? DepartureDateTime { get; set; }

        [Display(Name = "Arrival Date & Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd 0: HH:mm}")]
        public DateTime? ArrivalDateTime { get; set; }

        [Display(Name = "Economy Class Price")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal EconomyClassPrice { get; set; }

        [Display(Name = "Business Class Price")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal BusinessClassPrice { get; set; }

        [Display(Name = "Status")]
        [StringLength(20, ErrorMessage =
            "Status cannot exceed 20 characters")]
        public string Status { get; set; }
    }
}

