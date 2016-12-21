using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

using Richie.NerdDinner.Models;
using Richie.NerdDinner.Repository;
using Richie.NerdDinner.ViewModel;
using Richie.NerdDinner.Helpers;    

namespace Richie.NerdDinner.Controllers
{
    public class DinnerController : Controller
    {

        IDinnerRepository dinnerRepo = new DinnerRepository();
        ILookUpDataRepository lookupRepo = new LookUpDataRepository();

        // GET: Dinner
        public ActionResult Index(int? page)
        {
            const int pageSize = 2;
            //var dinners = dinnerRepo.FindUpcomingDinners().ToList();

            var upcomingDinners = dinnerRepo.FindUpcomingDinners().ToList();

            //var paginatedDinners = upcomingDinners.OrderBy(d => d.EventDate)
            //                                        .Skip(page * pageSize)
            //                                        .Take(pageSize)
            //                                        .ToList();

            var paginatedDinners = new PaginatedList<Dinner>(upcomingDinners, page ?? 0, pageSize);
            
            return View(paginatedDinners);
        }

        // GET: Dinners/Details/1
        public ActionResult Details(int id)
        {
            Dinner dinner = dinnerRepo.GetDinner(id);
            if (dinner == null)
            {
                return View("NotFound");
            }
            return View("Details", dinner);
        }

// EDIT-------------------------------------

        [Authorize]
        public ActionResult Edit(int id)
        {
            Dinner dinner = dinnerRepo.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }

            LookUpItem lookupItem = lookupRepo.getCountries();

            var modelVM = new CountryViewModel(dinner) { Countries = lookupItem.DropDownList };

            return View(modelVM);
        }

        [HttpPost, Authorize]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            Dinner dinner = dinnerRepo.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }

            LookUpItem lookupItem = lookupRepo.getCountries();

            var modelVM = new CountryViewModel(dinner) { Countries = lookupItem.DropDownList };

            if (TryUpdateModel(modelVM))
            {
                // Persist changes to DB
                dinnerRepo.UpdateDinner(modelVM.Dinner);

                // Perform HTTP redirect to details page for saved Dinner
                return RedirectToAction("Details", new { id = dinner.DinnerID });
            }

            return View(modelVM);
        }

// CREATE-------------------------------------
        
        [Authorize]
        public ActionResult Create()
        {
            Dinner dinner = new Dinner() { EventDate = DateTime.Now, HostedBy = User.Identity.Name};
            LookUpItem lookupItem = lookupRepo.getCountries();

            var modelVM = new CountryViewModel(dinner) { Countries = lookupItem.DropDownList };

            return View(modelVM);

        }

        [HttpPost, Authorize]
        public ActionResult Create(Dinner dinner)
        {

            //dinnerRepo.AddDinner2(dinner);
            //return RedirectToAction("Details", new { id = dinner.DinnerID });


            if (ModelState.IsValid)
            {
                dinnerRepo.AddDinner2(dinner);
                return RedirectToAction("Details", new { id = dinner.DinnerID });
            }
            return View(dinner);
        }

    }
}