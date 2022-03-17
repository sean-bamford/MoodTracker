using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace MoodTracker
{
    public class EntryDAO
    {
        public readonly string connectionString;

        public EntryDAO(string connString)
        {
            connectionString = connString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string script = File.ReadAllText(Path.GetFullPath("MoodTrackerDatabase.sql")); //resets database only if it doesn't exist
                SqlCommand cmd = new SqlCommand(script, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void NewEntry(Entry entry)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand sqlEntry = new SqlCommand("INSERT INTO Entry([Date], [Level], Note) OUTPUT INSERTED.Id VALUES (GETDATE(), @Level, @Note); ", conn);
                sqlEntry.Parameters.AddWithValue("@Level", entry.Level);
                sqlEntry.Parameters.AddWithValue("@Note", entry.Note);
                int entryId = Convert.ToInt32(sqlEntry.ExecuteScalar()); //add parameters from Entry, take returned scalar and use it to add to FactorEntry
                if (entry.Factors != null)
                {
                    for (int i = 0; i < entry.Factors.Count; i++)
                    {
                        SqlCommand sqlFactors = new SqlCommand("INSERT INTO FactorEntry(EntryId, FactorId, [Value]) VALUES (@EntryId, @FactorId, @Value);", conn);
                        sqlFactors.Parameters.AddWithValue("@EntryId", entryId);
                        sqlFactors.Parameters.AddWithValue("@FactorId", entry.Factors[i].Id);
                        sqlFactors.Parameters.AddWithValue("@Value", entry.Factors[i].Value);
                        sqlFactors.ExecuteNonQuery();
                    }
                }
            }

        }

        public List<Entry> GetEntries(int timeFrame) //1 is last week, 2 last month, 3 last year
        {
            string interval;
            int length;
            if (timeFrame == 1) { interval = "DAY"; length = -7; }
            else if (timeFrame == 2) { interval = "MONTH"; length = -1; }
            else if (timeFrame == 3) { interval = "YEAR"; length = -1; }
            else { return null; }
            List<Entry> entries = new List<Entry>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand sqlEntry = new SqlCommand("SELECT * FROM Entry e LEFT JOIN FactorEntry fe ON e.Id = fe.EntryId LEFT JOIN Factor f ON f.Id = fe.FactorId WHERE e.Date BETWEEN DATEADD(@Interval, @Length, GETDATE()) AND GETDATE() ORDER BY e.Id;", conn);
                sqlEntry.Parameters.AddWithValue("@Interval", interval);
                sqlEntry.Parameters.AddWithValue("@Length", length);
                                
                SqlDataReader reader = sqlEntry.ExecuteReader(); //read each line. If the entry exists, just add the Factor info. If not, add the entry info also.
                while (reader.Read())
                {
                    Entry entry = new Entry();
                    if (entries[entry][Convert.ToInt32(reader["EntryId"])] == null) //if there is no entry for the ID, add one
                    {
                        
                        entry.Id = Convert.ToInt32(reader["EntryId"]);
                        entry.Level = Convert.ToInt32(reader["Level"]);
                        entry.DateTime = Convert.ToDateTime(reader["Date"]);
                        entry.Note = Convert.ToString(reader["Note"]);
                        entries.Add(entry);
                    } 
                    entries[]
                }


                return entries;

            }

        }
    }

}


