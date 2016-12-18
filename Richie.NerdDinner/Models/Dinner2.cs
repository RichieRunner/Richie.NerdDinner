using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Richie.NerdDinner.Models
{
    public class Dinner2
    {
        public Dinner2() { RSVPs = new List<RSVP>(); }

        public int DinnerID { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
        public string HostedBy { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<RSVP> RSVPs { get; set; }
    }
}