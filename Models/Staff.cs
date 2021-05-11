using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace web2020apr_p08_t5.Models
{
    public class Staff
    {
        [Display(Name = "Staff ID")]
        [Required(ErrorMessage = "Staff ID is required")]
        public int StaffId { get; set; }

        [Display(Name = "Staff Name")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string StaffName { get; set; }

        public char? Gender { get; set; }

        [Display(Name = "Date Employed")]
        [DataType(DataType.Date)]
        public DateTime? DateEmployed{ get; set; }

        [Display(Name = "Vocation")]
        [StringLength(50, ErrorMessage ="Invalid Vocation")]
        public string Vocation { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "Invalid Email")]
        public string EmailAddr { get; set; }

        [Display(Name = "Password")]
        [StringLength(255, ErrorMessage = "Invalid Password")]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        [Display(Name = "Status")]
        [StringLength(50, ErrorMessage = "Invalid Status")]
        public string Status { get; set; }

       
    }
}
