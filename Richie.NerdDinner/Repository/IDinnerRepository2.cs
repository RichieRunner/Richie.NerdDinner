using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Richie.NerdDinner.Models;

namespace Richie.NerdDinner.Repository
{
    public interface IDinnerRepository2
    {
        //IEnumerable<Dinner2> FindAllDinners();
        //IEnumerable<Dinner> FindByLocation(float latitude, float longitude);
        IEnumerable<Dinner2> FindUpcomingDinners();
        Dinner2 GetDinner(int id);
        void UpdateDinner(Dinner2 dinner);



    }
}
