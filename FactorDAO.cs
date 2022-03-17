using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace MoodTracker
{
    public class FactorDAO
    {
        public readonly string connectionString;

        public FactorDAO(string connString)
        {
            connectionString = connString;            
        }

        public List<Factor> GetFactors()
        {
            List<Factor> factors = new List<Factor>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Factor;", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Factor factor = new Factor();
                    factor.Id = Convert.ToInt32(reader["Id"]);
                    factor.Name = Convert.ToString(reader["Name"]);
                    factor.IsDefault = Convert.ToBoolean(reader["IsDefault"]);
                    factor.QuestionText = Convert.ToString(reader["QuestionText"]);
                    factor.ValueType = Convert.ToInt32(reader["ValueType"]);
                    factor.Value = 0; //0 is our default, indicates the user has not added a value yet
                    factors.Add(factor);
                }

            }
            return factors;
        }

       

    }
}

