using System;
using Oracle.ManagedDataAccess.Client;

public class OracleDBclient
{
    private const string db_host = "localhost";
    private const int db_port = 1521;
    private const string db_srvname = "ORCLCDB.localdomain";
    private const string db_username = "testuser";
    private const string db_password = "T3stUs3r!";

    private const string db_table = "cars";

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

                // Full SELECT statement
                string sql1 = String.Format("select * from {0}", db_table);
                using (var cmd = new OracleCommand(sql1, conn))
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        int columns = reader.FieldCount;
                        Console.WriteLine("Total columns: {0}", columns);

                        for (int ii = 0; ii < columns; ii++)
                        {
                            Console.WriteLine(" - {0} {1}", reader.GetName(ii), reader.GetDataTypeName(ii));
                        }

                        int number = 0;
                        while (reader.Read())
                        {
                            number++;
                            Console.Write(number);

                            for (int ii = 0; ii < columns; ii++)
                            {
                                string type = reader.GetDataTypeName(ii);

                                string value = "?";
                                if (!reader.IsDBNull(ii))
                                {
                                    if (type.EndsWith("Varchar2"))
                                    {
                                        value = reader.GetString(ii);
                                    }
                                    else if (type.Equals("TimeStamp"))
                                    {
                                        value = reader.GetDateTime(ii).ToString();
                                    }
                                    else if (type.Equals("Int16"))
                                    {
                                        value = reader.GetInt16(ii).ToString();
                                    }
                                    else if (type.Equals("Int32"))
                                    {
                                        value = reader.GetInt32(ii).ToString();
                                    }
                                }
                                else
                                {
                                    value = "(null)";
                                }

                                Console.Write(" '{0}'", value);
                            }

                            Console.WriteLine();
                        }
                    }

                Console.WriteLine();
            }
        }
        catch (OracleException oex)
        {
            Console.WriteLine("Oracle database error: {0}", oex.Message);
        }

    }
}
