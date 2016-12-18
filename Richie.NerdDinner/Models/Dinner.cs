using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Richie.NerdDinner.Models
{
    public class Dinner
    {
        /*
        public Dinner() { RSVPs = new List<RSVP>(); }
        public int DinnerID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title may not be longer than 50 characters")]
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }

        [StringLength(256, ErrorMessage = "Hosted By name may not be longer than 20 characters")]
        public string HostedBy { get; set; }

        [Required(ErrorMessage = "Contact phone is required")]
        [StringLength(20, ErrorMessage = "Contact phone may not be longer than 20 characters")]
        public string ContactPhone { get; set; }

        //[Required(ErrorMessage = "Address is required")]
        //[StringLength(50, ErrorMessage = "Address may not be longer than 50 characters")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "Country is required")]
        //[StringLength(30, ErrorMessage = "Country may not be longer than 30 characters")]
        //[UIHint("CountryDropDown")]
        public string Country { get; set; }
        //public int? CountryID { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public List<RSVP> RSVPs { get; set; }
        */

        public Dinner() { RSVPs = new List<RSVP>(); }

        public int DinnerID { get; set; }
        public string Title { get; set; }
        public DateTime? EventDate { get; set; }
        public string Description { get; set; }
        public string HostedBy { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public int? Country { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public List<RSVP> RSVPs { get; set; }



    }
}