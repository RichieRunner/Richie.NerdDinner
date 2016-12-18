using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Richie.NerdDinner.Models
{
    public class LookUpItem
    {
        public int LookUpId { get; set; }
        public string LookUpValue { get; set; }

        public List<SelectListItem> DropDownList { get; set; }
    }
}