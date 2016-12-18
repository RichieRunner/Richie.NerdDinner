using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Richie.NerdDinner.Models;
using System.Web.Mvc;

namespace Richie.NerdDinner.ViewModel
{
    public class DinnerModel
    {
        public Dinner Dinner { get; set; }

        public Country Country { get; set; }

        public List<SelectListItem> MyListItems { get; set; }

        public SelectList Countriess { get; set; }
        // Constructor
        public DinnerModel(Dinner dinner)
        {
            Dinner = dinner;
            //Countriess = new SelectList(_countriess, dinner.Country);
            //dinner.Countries = Countriess;
        }



    }
}