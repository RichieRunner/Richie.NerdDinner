using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

using Richie.NerdDinner.Models;


namespace Richie.NerdDinner.Repository
{
    public class LookUpDataRepository : ILookUpDataRepository
    {
        private string connectionString;

        public LookUpDataRepository()
        {
            const string connectionStringKey = "RichieConnection";
            this.connectionString = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
        }

        public LookUpItem getCountries()
        {
            LookUpItem lookUpItem = new LookUpItem();

            using(var connection = new SqlConnection(this.connectionString))
	        {
                var commandText = @"SELECT CountryId, CountryName FROM Country";

                var command = new SqlCommand(commandText, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    lookUpItem.DropDownList = new List<SelectListItem>();

                    while (reader.Read())
                    {
                        //lookUpItem.DropDownList.Add(new SelectListItem { Text = "User", Value = "0" });
                        //lookUpItem.DropDownList.Add(new SelectListItem { Text = (string)reader["CountryName"], Value = reader["CountryID"].ToString() });
                        lookUpItem.DropDownList.Add(new SelectListItem { Text = (string)reader["CountryName"], Value = reader.GetInt32(0).ToString() });
                    }
                }
	        }

            return lookUpItem;
        }
    }
}