using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Richie.NerdDinner.Models;
using System.Web.Mvc;

namespace Richie.NerdDinner.ViewModel
{
    public class DinnerFormViewModel
    {
        private static string[] _countries = new[] {
            "USA",
            "Albania",
            "Vietnam"
        };

        // Properies
        public Dinner2 Dinner { get; private set; }
        public SelectList Countries { get; private set; }

        // Constructor
        public DinnerFormViewModel(Dinner2 dinner) {
            Dinner = dinner;
            Countries = new SelectList(_countries, dinner.Country);

        }

    }
}