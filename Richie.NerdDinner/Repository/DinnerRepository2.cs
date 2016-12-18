using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

using Richie.NerdDinner.Models;


namespace Richie.NerdDinner.Repository
{
    public class DinnerRepository2 : IDinnerRepository2
    {
        private string connectionString; 

        public DinnerRepository2()
        {
            const string connectionStringKey = "RichieConnection";
            this.connectionString = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
        }

        public IEnumerable<Dinner2> FindAllDinners()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText = 
                @"SELECT * FROM Dinner2
                ";

                var command = new SqlCommand(commandText, connection);
                connection.Open();
                return GetDinners(command);
            }
        }

        public IEnumerable<Dinner2> FindUpcomingDinners()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText =
                @"SELECT * FROM Dinner2 WHERE @CurrentDate <= EventDate
                ";

                var command = new SqlCommand(commandText, connection);
                command.Parameters.Add(
                    new SqlParameter{ParameterName="CurrentDate", DbType = DbType.DateTime, Value = DateTime.Now});

                connection.Open();
                return GetDinners(command);
            }
        }

        public Dinner2 GetDinner(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText =
                @"SELECT DinnerID, Title, Description, Address, HostedBy, Country, ContactPhone, EventDate FROM Dinner2 WHERE DinnerID = @DinnerID
                ";

                var command = new SqlCommand(commandText, connection);
                command.Parameters.Add(
                    new SqlParameter { ParameterName = "DinnerID", DbType = DbType.Int32, Value = id });

                connection.Open();
                return GetDinners(command)[0];
            }
        }

        public void AddDinner(Dinner2 dinner)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText = @"
                    insert into Dinner2
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

        public void UpdateDinner(Dinner2 dinner)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText = @"
                    update Dinner2
                    set Title = @Title, Country = @Country
                    where DinnerID = @DinnerID
                ";
                var command = new SqlCommand(commandText, connection);

                command.Parameters.Add(
                    new SqlParameter { ParameterName = "DinnerID", DbType = DbType.Int32, Value = dinner.DinnerID });
                command.Parameters.Add(
                    new SqlParameter { ParameterName = "Title", DbType = DbType.String, Value = dinner.Title});
                command.Parameters.Add(
                    new SqlParameter { ParameterName = "Country", DbType = DbType.String, Value = dinner.Country });

                //var dinnerObjectParameters = GetDinnerObjectParameters(dinner);
                //command.Parameters.AddRange(dinnerObjectParameters);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteDinner(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText =
                @"
                    delete from Rsvp where DinnerID = @DinnerID
                    delete from Dinner2 where DinnerID = @DinnerID
                ";

                var command = new SqlCommand(commandText, connection);
                command.Parameters.Add(
                    new SqlParameter { ParameterName = "DinnerID", DbType = DbType.Int32, Value = id });

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddDinnerRsvp(int dinnerID, RSVP rsvp)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var commandText =
                @"
                    insert into Rsvp
                    (
                        DinnerID,
                        AttendeeName
                    )
                    values
                    (
                        @DinnerID,
                        @AttendeeName
                    )

                    select cast(SCOPE_IDENTITY() as Integer) as RsvpID 
                ";

                var command = new SqlCommand(commandText, connection);
                var parameters = new[]{
                    new SqlParameter{ParameterName="DinnerID", DbType=DbType.Int32, Value=dinnerID},
                    new SqlParameter{ParameterName="AttendeeName", DbType=DbType.String, Value=rsvp.AttendeeName}};

                command.Parameters.AddRange(parameters);
                connection.Open();
                rsvp.RsvpID = (int)command.ExecuteScalar();
            }
        }


        private SqlParameter[] GetDinnerObjectParameters(Dinner2 dinner)
        {
            return new[]{
                new SqlParameter{ParameterName="Title", DbType=DbType.String, Value=dinner.Title},
                new SqlParameter{ParameterName="Description", DbType=DbType.String, Value=dinner.Description},
                new SqlParameter{ParameterName="EventDate", DbType=DbType.DateTime, Value=dinner.EventDate},
                new SqlParameter{ParameterName="Address", DbType=DbType.String, Value=dinner.Address},
                new SqlParameter{ParameterName="ContactPhone", DbType=DbType.String, Value=dinner.ContactPhone},
                new SqlParameter{ParameterName="Country", DbType=DbType.String, Value=dinner.Country},
                new SqlParameter{ParameterName="Latitude", DbType=DbType.Double, Value=dinner.Latitude},
                new SqlParameter{ParameterName="Longitude", DbType=DbType.Double, Value=dinner.Longitude},
                new SqlParameter{ParameterName="HostedBy", DbType=DbType.String, Value=dinner.HostedBy}};
        }


        private List<Dinner2> GetDinners(SqlCommand command)
        {
            var returnDinners = new List<Dinner2>();
            using (var reader = command.ExecuteReader())
            {
                //Project first result set into a collection of Dinner Objects

                while (reader.Read())
                {
                    var dinner = new Dinner2()
                    {
                        DinnerID = (int)reader["DinnerID"],
                        Title = (string)reader["Title"],
                        Description = (string)reader["Description"],
                        Address = (string)reader["Address"],
                        ContactPhone = (string)reader["ContactPhone"],
                        Country = (string)reader["Country"],
                        HostedBy = (string)reader["HostedBy"],
                        EventDate = (DateTime)reader["EventDate"]
                        //Latitude = (double)reader["Latitude"],
                        //Longitude = (double)reader["Longitude"]
                    };
                    returnDinners.Add(dinner);
                }

                //Project second result set into Rsvp objects. Associate them with 
                //their parent dinner object.

                reader.NextResult();

                int? dinnerID = null;
                Dinner2 parentDinner = null;

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
            }

            return returnDinners;
        }

    }

}