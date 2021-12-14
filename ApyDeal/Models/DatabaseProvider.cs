using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApyDeal.Models
{
    public class DatabaseProvider
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["Todo"].ConnectionString;
        public DataTable GetAllDeals()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetAllDeals", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            return dt;
        }
        public DataTable GetDealsByDate(DateTime date)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetDealsByDate", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter paramsDate = new SqlParameter
                {
                    ParameterName = "@Date",
                    Value = date
                };

                command.Parameters.Add(paramsDate);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            return dt;
        }

        public int CreateDeal(Deal deal)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("CreateDeal", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDescription = new SqlParameter
                {
                    ParameterName = "@Description",
                    Value = deal.Description
                };
                SqlParameter paramsStatus = new SqlParameter
                {
                    ParameterName = "@Status",
                    Value = deal.Status
                };
                SqlParameter paramsDate = new SqlParameter
                {
                    ParameterName = "@Date",
                    Value = deal.Date
                };
                SqlParameter paramsUrgency = new SqlParameter
                {
                    ParameterName = "@Urgency",
                    Value = deal.Urgency
                };
               
                command.Parameters.Add(paramDescription);
                command.Parameters.Add(paramsStatus);
                command.Parameters.Add(paramsDate);
                command.Parameters.Add(paramsUrgency);
                
                int newID = Convert.ToInt32(command.ExecuteScalar());

                return newID;
            }
        }

        public void UpdateDeal(Deal deal)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UpdateDeal", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramID = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = deal.Id
                };
                SqlParameter paramDescription = new SqlParameter
                {
                    ParameterName = "@Description",
                    Value = deal.Description
                };
                SqlParameter paramsStatus = new SqlParameter
                {
                    ParameterName = "@Status",
                    Value = deal.Status
                };
                SqlParameter paramsUrgency = new SqlParameter
                {
                    ParameterName = "@Urgency",
                    Value = deal.Urgency
                };

                command.Parameters.Add(paramID);
                command.Parameters.Add(paramDescription);
                command.Parameters.Add(paramsStatus);
                command.Parameters.Add(paramsUrgency);

                command.ExecuteNonQuery();
            }
        }
        public void DeleteDeal(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DeleteDeal", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter paramUID = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(paramUID);
                command.ExecuteNonQuery();
            }
        }

        public DataTable GetDeal(int id)
        {
            DataTable dr = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetDealById", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                SqlParameter paramUID = new SqlParameter
                {
                    ParameterName = "@id",
                    Value = id
                };
                command.Parameters.Add(paramUID);
                adapter.Fill(dr);
            }
            return dr;
        }


    }
}