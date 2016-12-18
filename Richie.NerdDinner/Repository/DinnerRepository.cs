using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

using Richie.NerdDinner.Models;
using Richie.NerdDinner.Extensions;

namespace Richie.NerdDinner.Repository
{
    public class DinnerRepository : IDinnerRepository
    {
        private string connectionString;

        public DinnerRepository()
        {
            const string connectionStringKey = "RichieConnection";
            this.connectionString = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
        }

        public IEnumerable<Dinner> FindAllDinners()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText = @"SELECT * FROM Dinner";

                var command = new SqlCommand(commandText, connection);
                connection.Open();
                return GetDinners(command);
            }
        }
        public IEnumerable<Dinner> FindUpcomingDinners()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText = @"SELECT * FROM Dinner";

                var command = new SqlCommand(commandText, connection);
                //command.Parameters.Add(
                //    new SqlParameter { ParameterName = "CurrentDate", DbType = DbType.DateTime, Value = DateTime.Now });

                connection.Open();
                return GetDinners(command);
            }
        }

        public Dinner GetDinner(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText = @"SELECT * FROM Dinner WHERE DinnerID = @DinnerID";

                var command = new SqlCommand(commandText, connection);
                command.Parameters.Add(
                    new SqlParameter { ParameterName = "DinnerID", DbType = DbType.Int32, Value = id });

                connection.Open();
                var dinners = GetDinners(command);

                if (dinners.Count() < 1)
                {
                    return null;
                }
                return dinners[0];
            }
        }

        public void UpdateDinner(Dinner dinner)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText = @"exec NerdDinner_Dinner_Update ";
                commandText += @"@Title = '" + dinner.Title + "' ,";
                commandText += @"@EventDate = '" + dinner.EventDate + "' ,";
                commandText += @"@Country = '" + dinner.Country + "' ,";
                commandText += @"@Description = '" + dinner.Description + "' ,";
                commandText += @"@HostedBy = '" + dinner.HostedBy + "' ,";
                commandText += @"@ContactPhone = '" + dinner.ContactPhone + "' ,";
                commandText += @"@Address = '" + dinner.ContactPhone + "' ,";
                commandText += @"@Latitude = " + dinner.Latitude + " ,";
                commandText += @"@Longitude = " + dinner.Longitude + " ,";
                
                commandText += @"@DinnerID = " + dinner.DinnerID;
                
                var command = new SqlCommand(commandText, connection);

                //command.Parameters.Add(
                //    new SqlParameter { ParameterName = "DinnerID", DbType = DbType.Int32, Value = dinner.DinnerID });
                //command.Parameters.Add(
                //    new SqlParameter { ParameterName = "Country", DbType = DbType.Int32, Value = dinner.Country });
                //command.Parameters.Add(
                //    new SqlParameter { ParameterName = "Title", DbType = DbType.String, Value = dinner.Title});

                var parameters = GetDinnerObjectParameters(dinner);
                command.Parameters.AddRange(parameters);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddDinner2(Dinner dinner)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText = @"
                    insert into Dinner
                    (
                        Title,
                        Description,
                        Address,
                        Country,
                        ContactPhone,
                        Latitude,
                        Longitude,
                        EventDate,
                        HostedBy
                    )
                    values
                    (
                        @Title,
                        @Description,
                        @Address,
                        @Country,
                        @ContactPhone,
                        @Latitude,
                        @Longitude,
                        @EventDate,
                        @HostedBy
                    )
                    
                    select cast(SCOPE_IDENTITY() as Integer) as DinnerID   
                ";

                var command = new SqlCommand(commandText, connection);
                var parameters = GetDinnerObjectParameters(dinner);
                command.Parameters.AddRange(parameters);

                connection.Open();
                dinner.DinnerID = (int)command.ExecuteScalar();
            }
        }

        public void AddDinner(Dinner dinner)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText = @"exec NerdDinner_Dinner_Add ";
                commandText += @"@Title = '" + dinner.Title + "' ,";
                commandText += @"@EventDate = '" + dinner.EventDate + "' ,";
                commandText += @"@Country = '" + dinner.Country + "' ,";
                commandText += @"@Description = '" + dinner.Description + "' ,";
                commandText += @"@HostedBy = '" + dinner.HostedBy + "' ,";
                commandText += @"@ContactPhone = '" + dinner.ContactPhone + "' ,";
                commandText += @"@Address = '" + dinner.ContactPhone + "' ,";
                commandText += @"@Latitude = " + dinner.Latitude + " ,";
                commandText += @"@Longitude = " + dinner.Longitude;

                var command = new SqlCommand(commandText, connection);
                
                //command.Parameters.Add(
                //    new SqlParameter { ParameterName = "Country", DbType = DbType.Int32, Value = dinner.Country });
                //command.Parameters.Add(
                //    new SqlParameter { ParameterName = "Title", DbType = DbType.String, Value = dinner.Title });
                //command.Parameters.Add(
                //    new SqlParameter { ParameterName = "EventDate", DbType = DbType.Date, Value = dinner.EventDate });

                var parameters = GetDinnerObjectParameters(dinner);
                command.Parameters.AddRange(parameters);

                connection.Open();
                dinner.DinnerID = (int)command.ExecuteScalar();

            }
        }

        private List<Dinner> GetDinners(SqlCommand command)
        {
            var returnDinners = new List<Dinner>();
            using (var reader = command.ExecuteReader())
            {
                //Project first result set into a collection of Dinner Objects

                while (reader.Read())
                {
                    var dinner = new Dinner()
                    {
                        DinnerID = (int)reader["DinnerID"],
                        Title = reader["Title"] as string ?? default(string),
                        Description = reader["Description"] as string ?? default(string),
                        Address = reader["Address"] as string ?? default(string),
                        ContactPhone = reader["ContactPhone"] as string ?? default(string),
                        Country = reader["Country"] as int? ?? default(int),
                        HostedBy = reader["HostedBy"] as string ?? default(string),
                        EventDate = reader["EventDate"] as DateTime? ?? default(DateTime),
                        Latitude = reader["Latitude"] as int? ?? default(int),
                        Longitude = reader["Longitude"] as int? ?? default(int),
                    };
                    returnDinners.Add(dinner);
                }

                reader.NextResult();

                int? dinnerID = null;
                Dinner parentDinner = null;

                while (reader.Read())
                {
                    var rsvp = new RSVP()
                    {
                        RsvpID = (int)reader["RsvpID"],
                        DinnerID = (int)reader["DinnerID"],
                        AttendeeName = (string)reader["AttendeeName"]
                    };

                    if (dinnerID != rsvp.DinnerID)
                    {
                        dinnerID = rsvp.DinnerID;
                        parentDinner = returnDinners.Where(dinner => dinner.DinnerID == dinnerID).First();
                    }

                    parentDinner.RSVPs.Add(rsvp);
                }


                return returnDinners;
            }
        }

        private SqlParameter[] GetDinnerObjectParameters(Dinner dinner)
        {
            return new[]{
                new SqlParameter{ParameterName="Title", DbType=DbType.String, Value=dinner.Title},
                new SqlParameter{ParameterName="Description", DbType=DbType.String, Value=dinner.Description},
                new SqlParameter{ParameterName="EventDate", DbType=DbType.DateTime, Value=dinner.EventDate},
                new SqlParameter{ParameterName="Address", DbType=DbType.String, Value=dinner.Address},
                new SqlParameter{ParameterName="ContactPhone", DbType=DbType.String, Value=dinner.ContactPhone},
                new SqlParameter{ParameterName="Country", DbType=DbType.Int32, Value=dinner.Country},
                new SqlParameter{ParameterName="Latitude", DbType=DbType.Double, Value=dinner.Latitude},
                new SqlParameter{ParameterName="Longitude", DbType=DbType.Double, Value=dinner.Longitude},
                new SqlParameter{ParameterName="HostedBy", DbType=DbType.String, Value=dinner.HostedBy}};
        }

        public IEnumerable<Country> getCountries()
        {
            var returnCountries = new List<Country>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText = @"SELECT * FROM Country";

                var command = new SqlCommand(commandText, connection);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new Country()
                        {
                            CountryID = (int)reader["CountryID"],
                            CountryName = (string)reader["CountryName"]
                        };
                        returnCountries.Add(country);
                    }
                }
            }
            return returnCountries;
        }


    }
}