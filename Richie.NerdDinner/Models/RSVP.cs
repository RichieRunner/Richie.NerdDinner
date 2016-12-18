using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Richie.NerdDinner.Models
{
    public class RSVP
    {
        public int RsvpID { get; set; }
        public int DinnerID { get; set; }
        public string AttendeeName { get; set; }
    }
}