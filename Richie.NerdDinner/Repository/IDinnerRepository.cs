using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Richie.NerdDinner.Models;

namespace Richie.NerdDinner.Repository
{
    public interface IDinnerRepository
    {
        IEnumerable<Dinner> FindAllDinners();
        //IEnumerable<Dinner> FindByLocation(float latitude, float longitude);
        IEnumerable<Dinner> FindUpcomingDinners();
        Dinner GetDinner(int id);
        void AddDinner(Dinner dinner);
        void AddDinner2(Dinner dinner);
        void UpdateDinner(Dinner dinner);
        //void DeleteDinner(int id);
        //void AddDinnerRsvp(int dinnerID, RSVP rsvp);

        //void UpdateDinner2(Dinner dinner);
        IEnumerable<Country> getCountries();

        IEnumerable<Dinner> GetData(out int totalRecords, string globalSearch);
        IEnumerable<Dinner> GetData(out int totalRecords, string globalSearch, int? limitOffset, int? limitRowCount, string orderBy, bool desc);

    }
}