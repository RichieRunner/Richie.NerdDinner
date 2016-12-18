using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Richie.NerdDinner.Models;

namespace Richie.NerdDinner.ViewModel
{
    public class CountryViewModel
    {
        public Dinner Dinner { get; set; }
        public Country Country { get; set; }
        public List<SelectListItem> Countries { get; set; }


        // Constructor
        public CountryViewModel(Dinner dinner)
        {
            Dinner = dinner;
        }
    }
}