using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightService.Data
{
    public class PassengerDAO : IPassengerDAO
    {
        private string connString = "Data Source=DESKTOP-2A9J2RD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=FlightService";

        public bool AddPassenger(Passenger passenger)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {

                SqlCommand command = new SqlCommand("dbo.AddPassenger", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", passenger.firstname);
                command.Parameters.AddWithValue("@LastName", passenger.lastname);
                command.Parameters.AddWithValue("@Job", passenger.job);
                command.Parameters.AddWithValue("@Email", passenger.email);
                command.Parameters.AddWithValue("@Age", passenger.age);
                try
                {
                    connection.Open();
                    command.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not add passenger " + ex);
                    return false;
                }
                finally
                {
                    connection.Close();

                }
                return true;

            }
        }

        public bool DeletePassenger(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("dbo.DeletePassenger", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", Id);
                try
                {
                    connection.Open();
                    command.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not delete passenger " + ex);
                    return false;
                }
                finally
                {
                    connection.Close();

                }
                return true;

            }
        }

        public Passenger GetPassenger(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {

                SqlCommand command = new SqlCommand("dbo.GetPassenger", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", Id);

                Passenger passenger = new Passenger();

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            passenger = new Passenger(int.Parse(reader["Id"].ToString()), 
                                                     reader["FirstName"].ToString(),
                                                     reader["LastName"].ToString(),
                                                     reader["Job"].ToString(),
                                                     reader["Email"].ToString(),
                                                     int.Parse(reader["Age"].ToString()));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get passenger " + ex);
                }
                finally
                {
                    connection.Close();

                }
                return passenger;

            }
        }

        public bool UpdatePassenger(Passenger passenger)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {

                SqlCommand command = new SqlCommand("dbo.UpdatePassenger", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", passenger.firstname);
                command.Parameters.AddWithValue("@LastName", passenger.lastname);
                command.Parameters.AddWithValue("@Job", passenger.job);
                command.Parameters.AddWithValue("@Email", passenger.email);
                command.Parameters.AddWithValue("@Age", passenger.age);
                command.Parameters.AddWithValue("@Id", passenger.Id);

                try
                {
                    connection.Open();
                    command.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not update passenger " + ex);
                    return false;
                }
                finally
                {
                    connection.Close();

                }
                return true;

            }
        }

        public IEnumerable<Passenger> ViewPassengers()
        {
            List<Passenger> passengerList = new List<Passenger>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("dbo.ViewPassengers", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Passenger temp = new Passenger(int.Parse(reader["Id"].ToString()),
                                                     reader["FirstName"].ToString(),
                                                     reader["LastName"].ToString(),
                                                     reader["Job"].ToString(),
                                                     reader["Email"].ToString(),
                                                     int.Parse(reader["Age"].ToString())) ;

                           
                            passengerList.Add(temp);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not view passengers " + ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return passengerList;

        }
    }
}
