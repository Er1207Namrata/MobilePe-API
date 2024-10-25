using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MobileAPI_V2.Model
{
    public class Connection
    {
        public static string connectionString = string.Empty;


        static Connection()
        {
            try
            {
                #if DEBUG              
                    connectionString = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("ConnectionStrings").GetSection("DefaultConnectionDev").Value;
                #else
                   connectionString = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
                #endif


            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
{
    int k = 0;
    try
    {
        using (var connection = new SqlConnection(connectionString))
        using (var command = new SqlCommand(commandText, connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(commandParameters);
            connection.Open();
            k = command.ExecuteNonQuery();
        }
        return k;
    }
    catch
    {
        return k;
    }
}

public static DataSet ExecuteQuery(string commandText, params SqlParameter[] parameters)
{
    DataSet ds = new DataSet();
    try
    {


        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand(commandText, connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(parameters);
        SqlDataAdapter da = new SqlDataAdapter(command);
        da.Fill(ds);
        connection.Close();



    }
    catch (Exception ex)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Flag");
        dt.Columns.Add("Message");

        DataRow dr = dt.NewRow();
        dr["Flag"] = "0";
        dr["Message"] = ex.Message;
        dt.Rows.Add(dr);
        ds.Tables.Add(dt);

    }
    return ds;
}

    
    }
}
