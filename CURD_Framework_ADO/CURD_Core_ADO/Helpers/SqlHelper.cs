using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CURD_Core_ADO.Helpers
{
    public static class SqlHelper
    {
        static string ConnectionString = "data source =DESKTOP-8A8TA50;Initial Catalog = POC_DB; User ID = sa; Password=Mssqlserver@8083;Integrated Security = true; MultipleActiveResultSets=True";
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
                    result = "execute successfully!";
                    return result;

                }
                catch (Exception ex)
                {
                    result = ex.Message;
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
