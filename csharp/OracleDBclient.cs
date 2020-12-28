/*
1) Install 'Oracle Data Provider for .NET'

2) Check the right Oracle.ManagedDataAccess.dll from Oracle Data Provider for .NET in the bin subdirectory
*/

using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

public class OracleDBclient
{
    private const string db_host = "localhost";
    private const int db_port = 1521;
    private const string db_srvname = "ORCLCDB.localdomain";
    private const string db_username = "testuser";
    private const string db_password = "T3stUs3r!";

    private const string db_table = "cars";

    private const string db_update_column = "remark";
    private const string db_update_column_variable = "updatevar";

    private const string db_column = "id";
    private const string db_column_variable = "var";
    private const int db_column_value = 1;

    private const string db_factorial_variable = "n";
    private const int db_factorial_value = 4;

    private const string db_add_and_subtract_a_variable = "a";
    private const int db_add_and_subtract_a_value = 12;
    private const string db_add_and_subtract_b_variable = "b";
    private const int db_add_and_subtract_b_value = 5;

    private const string db_add_and_subtract_x_variable = "x";
    private const string db_add_and_subtract_y_variable = "y";

    private static string GetNow()
    {
        DateTime dateTimeNow = DateTime.Now;
        return dateTimeNow.ToString();
    }

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

                // UPDATE statement
                string new_comment = "C# " + GetNow();

                string sql0 = String.Format("update {0} set {1}=:{2} where {3}!=:{4}", db_table, db_update_column, db_update_column_variable, db_column, db_column_variable);
                Console.WriteLine(sql0);

                using (var cmd = new OracleCommand(sql0, conn))
                {
                    OracleParameter par1 = new OracleParameter(db_update_column_variable, new_comment);
                    cmd.Parameters.Add(par1);

                    OracleParameter par2 = new OracleParameter(db_column_variable, db_column_value);
                    cmd.Parameters.Add(par2);

                    int updatedRows = cmd.ExecuteNonQuery();
                    Console.WriteLine("Total updated rows: {0}", updatedRows);
                }

                Console.WriteLine();

                // Full SELECT statement
                string sql1 = String.Format("select * from {0}", db_table);
                Console.WriteLine(sql1);

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

                // SELECT WHERE statement
                string sql2 = String.Format("select count(*) from {0} where {1}!=:{2}", db_table, db_column, db_column_variable);
                Console.WriteLine(sql2);

                using (var cmd = new OracleCommand(sql2, conn))
                {
                    OracleParameter par1 = new OracleParameter(db_column_variable, db_column_value);
                    cmd.Parameters.Add(par1);

                    Object result = cmd.ExecuteScalar();
                    Console.WriteLine("Result: {0}", result);
                }

                Console.WriteLine();

                // SELECT package function statement
                string sql3 = String.Format("select calculator.factorial(:{0}) from dual", db_factorial_variable);
                Console.WriteLine(sql3);

                using (var cmd = new OracleCommand(sql3, conn))
                {
                    OracleParameter par1 = new OracleParameter(db_factorial_variable, db_factorial_value);
                    cmd.Parameters.Add(par1);

                    Object result = cmd.ExecuteScalar();
                    Console.WriteLine("Result: {0}", result);
                }

                Console.WriteLine();

                // CALL package procedure statement
                string sql4 = String.Format("calculator.add_and_subtract");
                Console.WriteLine(sql4);

                using (var cmd = new OracleCommand(sql4, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter par1 = new OracleParameter(db_add_and_subtract_a_variable, db_add_and_subtract_a_value);
                    cmd.Parameters.Add(par1);
                    OracleParameter par2 = new OracleParameter(db_add_and_subtract_b_variable, db_add_and_subtract_b_value);
                    cmd.Parameters.Add(par2);
                    OracleParameter par3 = new OracleParameter(db_add_and_subtract_x_variable, OracleDbType.Int32, ParameterDirection.Output);
                    cmd.Parameters.Add(par3);
                    OracleParameter par4 = new OracleParameter(db_add_and_subtract_y_variable, OracleDbType.Int32, ParameterDirection.Output);
                    cmd.Parameters.Add(par4);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Result x: {0}", cmd.Parameters[db_add_and_subtract_x_variable].Value);
                    Console.WriteLine("Result y: {0}", cmd.Parameters[db_add_and_subtract_y_variable].Value);
                }
            }
        }
        catch (OracleException oex)
        {
            Console.WriteLine("Oracle database error: {0}", oex.Message);
        }

    }
}
