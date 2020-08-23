using System;
using Oracle.ManagedDataAccess.Client;

public class OracleDBclient
{
    private const string db_host = "localhost";
    private const int db_port = 1521;
    private const string db_srvname = "ORCLCDB.localdomain";
    private const string db_username = "testuser";
    private const string db_password = "T3stUs3r!";

    public static void Main(string[] args)
    {
        // Build the connection string
        OracleConnectionStringBuilder connBuilder = new OracleConnectionStringBuilder();
        connBuilder.DataSource = String.Format("{0}:{1}/{2}", db_host, db_port, db_srvname);
        connBuilder.UserID = db_username;
        connBuilder.Password = db_password;

        try
        {
            using (OracleConnection conn = new OracleConnection(connBuilder.ConnectionString))
            {
                // Connect the database
                conn.Open();

                Console.WriteLine("Data source: {0}", conn.DataSource);
                Console.WriteLine("Server version: {0}", conn.ServerVersion);
                Console.WriteLine("Connection timeout: {0}", conn.ConnectionTimeout);
                Console.WriteLine("State: {0}", conn.State);
                Console.WriteLine();

            }
        }
        catch (OracleException oex)
        {
            Console.WriteLine("Oracle database error: {0}", oex.Message);
        }

    }
}
