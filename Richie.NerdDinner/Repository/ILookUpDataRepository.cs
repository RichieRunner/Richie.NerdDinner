using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Richie.NerdDinner.Models;

namespace Richie.NerdDinner.Repository
{
    public interface ILookUpDataRepository
    {
        LookUpItem getCountries();
    }
}