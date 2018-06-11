using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessMonitor.Models;
using ProcessMonitor.DAL;
using System.Data.SqlClient;

namespace ProcessMonitor.DAL
{
    public class DataAccess
    {
        public static List<Entries> inregistrari = new List<Entries>();
        SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\III\SEM. II\PS- IERCAN\TCPCommunication\ProcessMonitor\App_Data\Entries.mdf;Integrated Security=True;Connect Timeout=30");
        public DataAccess()
        {
            connection.Open();
        }

        public static void LoadData()
        {
            try
            {
                SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\III\SEM. II\PS- IERCAN\TCPCommunication\ProcessMonitor\App_Data\Entries.mdf;Integrated Security=True;Connect Timeout=30");
                SqlCommand command = new SqlCommand("SELECT * FROM Entries", conn);
                SqlDataReader reader = command.ExecuteReader();
                inregistrari.Clear();
                while (reader.Read())
                {
                    inregistrari.Add(new Entries(Convert.ToInt32(reader.GetString(1)), reader.GetDateTime(2), reader.GetString(3)));
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception a) { }
        }

        public IList<Entries> GetAllLogs()
        {
            List<Entries> logs = new List<Entries>();
            SqlCommand getAllCommand = new SqlCommand("SELECT * FROM Entries", connection);
            SqlDataReader reader = getAllCommand.ExecuteReader();
            while (reader.Read())
            {
                logs.Add(new Entries((int)reader[0], (DateTime)reader[1], (string)reader[2]));
            }
            return logs;
        }

        public IList<Entries> GetLogById(int Id)
        {
            List<Entries> logs = new List<Entries>();
            SqlCommand getAllCommand = new SqlCommand("SELECT * FROM Entries where Id LIKE '" + Id.ToString() + "%'", connection);
            SqlDataReader reader = getAllCommand.ExecuteReader();
            while (reader.Read())
            {
                logs.Add(new Entries((int)reader[0], (DateTime)reader[1], (string)reader[2]));
            }
            return logs;
        }
        public async void Insert(Entries log)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\III\SEM. II\PS- IERCAN\TCPCommunication\ProcessMonitor\App_Data\Entries.mdf;Integrated Security=True;Connect Timeout=30");
            await connection.OpenAsync();
            SqlCommand insertCommand = new SqlCommand("INSERT INTO Entries(Timespan,Stare) VALUES(@Timespan,@Stare)", connection);
            insertCommand.Parameters.Add("@Timespan", System.Data.SqlDbType.DateTime).Value = log.Timespan;
            insertCommand.Parameters.Add("@Stare", System.Data.SqlDbType.VarChar).Value = log.Stare;
            await insertCommand.ExecuteNonQueryAsync();
        }

    }
    }
