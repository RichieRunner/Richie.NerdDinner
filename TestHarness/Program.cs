using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Richie.NerdDinner.Models;
using Richie.NerdDinner.Repository;
using Richie.NerdDinner.ViewModel;
using Richie.NerdDinner.Helpers;

namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSkipTake();
        }

        static void TestGet()
        {
            IDinnerRepository2 dinnerRepo = new DinnerRepository2();
            Dinner2 dinner = dinnerRepo.GetDinner(1);
            Console.WriteLine(dinner.Title.ToString());
        }


        static void TestUpdate()
        {
            IDinnerRepository2 dinnerRepo = new DinnerRepository2();
            Dinner2 dinner = dinnerRepo.GetDinner(1);
            dinnerRepo.UpdateDinner(dinner);
        }

        static void TestPaginated()
        {
            IDinnerRepository dinnerRepo = new DinnerRepository();

            var upcomingDinners = dinnerRepo.FindUpcomingDinners();

            var paginatedDiners = new PaginatedList<Dinner>(upcomingDinners, 1, 2);

            Console.WriteLine(String.Format("{0}, {1})","Total Count:", paginatedDiners.TotalCount));
            Console.WriteLine(String.Format("{0}, {1})", "Total Pages:", paginatedDiners.TotalPages));
            Console.WriteLine(String.Format("{0}, {1})", "Has Previous Page: ", paginatedDiners.HasPreviousPage));
            Console.WriteLine(String.Format("{0}, {1})", "Has Next Page:", paginatedDiners.HasNextPage));
        }

        static void TestSkipTake()
        {
            var list = new[] { 1, 2, 3, 4, 5, 6 };
            var afterSecond = list.Take(3);
            Console.WriteLine(string.Join(",", afterSecond));
        }
    }
}
