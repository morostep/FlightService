using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlightService.Data
{
    public class FlightDAO : IFlightDAO
    {
        private string connString = "Data Source=DESKTOP-2A9J2RD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=FlightService";

        public bool AddFlight(Flight flight)
        {
            string[] DA = flight.departureAirport.Split(",");
            string[] AA = flight.arrivalAirport.Split(",");

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("[FlightService].[dbo].[AddFlight]", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FlightNumber", flight.flightNumber);
                command.Parameters.AddWithValue("@IdAirline", flight.Airline);
                command.Parameters.AddWithValue("@DepartureDate", DateTime.Parse(flight.departureDate));
                command.Parameters.AddWithValue("@ArrivalDate", DateTime.Parse(flight.arrivalDate));
                command.Parameters.AddWithValue("@DepartureTime", flight.departureTime);
                command.Parameters.AddWithValue("@ArrivalTime", flight.arrivalTime);
                command.Parameters.AddWithValue("@DepartureAirportId", DA[0]);
                command.Parameters.AddWithValue("@ArrivalAirportId", AA[0]);
                command.Parameters.AddWithValue("@PassengerLimit", flight.passengerLimit);

                try
                {
                    connection.Open();
                    command.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not add the Flight " + ex);
                    return false;
                }
                finally
                {
                    connection.Close();

                }
                return true;

            }
        }

        public bool UpdateFlight(Flight flight)
        {
            string[] DA = flight.departureAirport.Split(",");
            string[] AA = flight.arrivalAirport.Split(",");
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("[FlightService].[dbo].[UpdateFlight]", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", flight.Id);
                command.Parameters.AddWithValue("@IdAirline", flight.Airline);
                command.Parameters.AddWithValue("@DepartureDate",flight.departureDate);
                command.Parameters.AddWithValue("@ArrivalDate", flight.arrivalDate);
                command.Parameters.AddWithValue("@DepartureTime", flight.departureTime);
                command.Parameters.AddWithValue("@ArrivalTime", flight.arrivalTime);
                command.Parameters.AddWithValue("@DepartureAirportId", DA[0]);
                command.Parameters.AddWithValue("@ArrivalAirportId", AA[0]);
                command.Parameters.AddWithValue("@PassengerLimit", flight.passengerLimit);

                try
                {
                    connection.Open();
                    command.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not update the flight " + ex);
                    return false;
                }
                finally
                {
                    connection.Close();

                }
                return true;

            }
        }

        public bool DeleteFlight(int id)
        {
           
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("[FlightService].[dbo].[DeleteFlight]", connection);
                command.CommandType = CommandType.StoredProcedure; 
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    command.ExecuteScalar();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not delete flight " + ex);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
                return true;

            }
        }

        public Flight GetFlight(int id)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("dbo.GetFlight", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                Flight flight = new Flight();
                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            flight = new Flight(int.Parse(reader["Id"].ToString()),
                                                    reader["DesignatorCode"].ToString() + reader["FlightNumber"].ToString(),
                                                     reader["Name"].ToString(),
                                                     reader["DepartureDate"].ToString().Substring(0, reader["DepartureDate"].ToString().Length - 11),
                                                     reader["DepartureTime"].ToString(),
                                                     reader["ArrivalDate"].ToString().Substring(0, reader["ArrivalDate"].ToString().Length - 11),
                                                     reader["ArrivalTime"].ToString(),
                                                     reader["DepartureAirport"].ToString(),
                                                     reader["ArrivalAirport"].ToString(),
                                                     int.Parse(reader["PassengerLimit"].ToString()));

                        }
                        connection.Close();

                        return flight;

                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get the Flight " + ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return null;

        }

        public IEnumerable<string> ViewAirlines()
        {
            List<string> airlineList = new List<string>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("dbo.GetAirlines", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            airlineList.Add(reader["Name"].ToString());


                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get the Flight " + ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return airlineList;

        }

        public IEnumerable<Flight> ViewFlights()
        {
            List<Flight> flightList = new List<Flight>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("[FlightService].[dbo].[ViewFlights]", connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Flight temp = new Flight(int.Parse(reader["Id"].ToString()), 
                                                     reader["DesignatorCode"].ToString() + reader["FlightNumber"].ToString(),
                                                     reader["Name"].ToString(),
                                                     reader["DepartureDate"].ToString().Substring(0, reader["DepartureDate"].ToString().Length-11), 
                                                     reader["DepartureTime"].ToString(),
                                                     reader["ArrivalDate"].ToString().Substring(0, reader["ArrivalDate"].ToString().Length - 11),
                                                     reader["ArrivalTime"].ToString(),
                                                     reader["DepartureAirport"].ToString(),
                                                     reader["ArrivalAirport"].ToString(),
                                                     int.Parse(reader["PassengerLimit"].ToString()));

                            flightList.Add(temp);


                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get the Flight " + ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return flightList;


        }

        public IEnumerable<string> ViewAirports()
        {
            List<string> airportList = new List<string>();

            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand command = new SqlCommand("dbo.GetAirports", connection);
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            airportList.Add(reader["Airport"].ToString());
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get the Flight " + ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return airportList;


        }
    }
}
