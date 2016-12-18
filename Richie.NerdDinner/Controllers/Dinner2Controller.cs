using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Richie.NerdDinner.Models;
using Richie.NerdDinner.Repository;
using Richie.NerdDinner.ViewModel;

namespace Richie.NerdDinner.Controllers
{
    public class Dinner2Controller : Controller
    {

        IDinnerRepository2 dinnerRepository2 = new DinnerRepository2();


        // GET: Dinner2
        public ActionResult Index()
        {
            var dinners = dinnerRepository2.FindUpcomingDinners().ToList();
            return View("Index", dinners);
        }

        // GET: /Dinners/Details/2
        public ActionResult Details(int id)
        {
            Dinner2 dinner = dinnerRepository2.GetDinner(id);
            return View("Details", dinner);
        }

        // GET: /Dinners/Edit/1
        public ActionResult Edit(int id)
        {
            Dinner2 dinner = dinnerRepository2.GetDinner(id);
            return View(new DinnerFormViewModel(dinner));
        }

        // POST: /Dinners/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Dinner2 dinner = dinnerRepository2.GetDinner(id);
            var modelVM = new DinnerFormViewModel(dinner);
            if (TryUpdateModel(modelVM))
            {
                dinnerRepository2.UpdateDinner(modelVM.Dinner);
                return RedirectToAction("Details", new { id = dinner.DinnerID });
            }
            return View(modelVM);
        }

        /*
        ////////////// VM2

        public ActionResult EditVM2(int Id)
        {
            Dinner dinner = dinnerRepo.GetDinner(Id);

            var countries = GetAllCountries();

            var modelVM = new DinnerModel(dinner);

            modelVM.Dinner.Countries = GetSelectListItems(countries);

            return View(modelVM);

        }

        [HttpPost]
        public ActionResult EditVM2(int Id, FormCollection formValues)
        {
            Dinner dinner = dinnerRepo.GetDinner(Id);

            var countries = GetAllCountries();

            var modelVM = new DinnerModel(dinner);

            modelVM.Dinner.Countries = GetSelectListItems(countries);

            try
            {
                //modelVM.Dinner.Title = Request.Form["Title"];
                //modelVM.Dinner.Country = Request.Form["Country"];
                UpdateModel(dinner, "Dinner");
                dinnerRepo.UpdateDinner(modelVM.Dinner);
                return RedirectToAction("Details", new { id = dinner.DinnerID });
            }
            catch
            {

                throw;
            }

            //if (TryUpdateModel(modelVM, "Dinner"))
            //{
            //    // Persist changes to DB
            //    dinnerRepo.UpdateDinner(modelVM.Dinner);

            //    // Perform HTTP redirect to details page for saved Dinner
            //    return RedirectToAction("Details", new { id = dinner.DinnerID });
            //}
            //return View(modelVM);

        }

        ////////////// VM3

        public ActionResult EditVM3(int Id)
        {
            Dinner dinner = dinnerRepo.GetDinner(Id);

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Item1",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "Item2",
                Value = "2"
            });
            items.Add(new SelectListItem
            {
                Text = "Item3",
                Value = "3"
            });
            items.Add(new SelectListItem
            {
                Text = "Item4",
                Value = "4"
            });

            var modelVM = new DinnerModel(dinner) { MyListItems = items };
            //modelVM.Country.ListItems = items;

            return View(modelVM);

        }

        [HttpPost]
        public ActionResult EditVM3(int Id, FormCollection formValues)
        {
            Dinner dinner = dinnerRepo.GetDinner(Id);

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Item1",
                Value = "1"
            });
            items.Add(new SelectListItem
            {
                Text = "Item2",
                Value = "2"
            });
            items.Add(new SelectListItem
            {
                Text = "Item3",
                Value = "3"
            });
            items.Add(new SelectListItem
            {
                Text = "Item4",
                Value = "4"
            });

            var modelVM = new DinnerModel(dinner) { MyListItems = items };

            try
            {
                UpdateModel(dinner, "Dinner");
                dinnerRepo.UpdateDinner(modelVM.Dinner);
                return RedirectToAction("Details", new { id = dinner.DinnerID });
            }
            catch
            {

                throw;
            }

            //if (TryUpdateModel(modelVM, "Dinner"))
            //{
            //    // Persist changes to DB
            //    dinnerRepo.UpdateDinner(modelVM.Dinner);

            //    // Perform HTTP redirect to details page for saved Dinner
            //    return RedirectToAction("Details", new { id = dinner.DinnerID });
            //}
            //return View(modelVM);

        }
        private IEnumerable<string> GetAllCountries()
        {
            return new List<string>
            {
                "ACT",
                "New South Wales",
                "Northern Territories",
                "Queensland",
                "South Australia",
                "Victoria",
                "Western Australia",
            };
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            var selectList = new List<SelectListItem>();
            int elementId = 1;
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = elementId.ToString(),
                    Text = element
                });

                elementId++;
            }

            return selectList;
        }

        */
    }
}