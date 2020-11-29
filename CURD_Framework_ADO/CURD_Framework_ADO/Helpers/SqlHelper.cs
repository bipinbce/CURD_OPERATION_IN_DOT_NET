using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CURD_Framework_ADO.Helpers
{
    public static class SqlHelper
    {
        static string ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["db_connection"]);
        public static DataTable ExecuteQuery(string query, SqlParameter[] sqlParameters)
        {
            var dataTable = new DataTable();
            try
            {
                
                // create connection object
                SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                // check connection close/broken
                if (sqlConnection.State == ConnectionState.Closed || sqlConnection.State == ConnectionState.Broken)
                {
                    sqlConnection.Open();
                }
                try
                {
                    // set parameter to command
                    if (sqlParameters != null)
                    {
                        foreach (var param in sqlParameters)
                        {
                            sqlCommand.Parameters.Add(param);
                        }
                    }
                    // excute query
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(dataTable);
                    return dataTable;
                }
                catch (Exception ex)
                {
                    //
                }
                finally
                {
                    sqlCommand.Dispose();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dataTable;
        }
        public static string ExecuteNonQuery(string query, SqlParameter[] sqlParameters)
        {
            var result = "";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                if (sqlConnection.State == ConnectionState.Closed || sqlConnection.State == ConnectionState.Broken)
                    sqlConnection.Open();
                try
                {
                    if (sqlParameters != null)
                    {
                        foreach (var param in sqlParameters)
                        {
                            sqlCommand.Parameters.Add(param);
                        }
                    }
                    sqlCommand.ExecuteNonQuery();
                    result =  "execute successfully!";
                    return result;

                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message);
                }
                finally
                {
                    sqlCommand.Dispose();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;

        }
    }
}