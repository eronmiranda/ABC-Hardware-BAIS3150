using emiranda4BAIS3150Project.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace emiranda4BAIS3150Project.TechnicalServices
{
    public class Sales
    {
        private string ConnectionString = Startup.ConnectionString;
        public int AddSale(Sale ABCSale)
        {
            int saleNumber = 0;

            SqlConnection ABCHardwareConnection = new SqlConnection();
            ABCHardwareConnection.ConnectionString = ConnectionString;

            ABCHardwareConnection.Open();

            SqlCommand ABCAddSaleCommand = CreateSqlCommandSP(ABCHardwareConnection, "AddSale");

            SqlParameter ABCAddSaleParameter;
            ABCAddSaleParameter = CreateSqlParameter("@SaleDate", SqlDbType.Date, ABCSale.SaleDate.ToString());
            ABCAddSaleCommand.Parameters.Add(ABCAddSaleParameter);

            ABCAddSaleParameter = CreateSqlParameter("@SalespersonId", SqlDbType.Int, ABCSale.SalespersonId.ToString());
            ABCAddSaleCommand.Parameters.Add(ABCAddSaleParameter);

            ABCAddSaleParameter = CreateSqlParameter("@CustomerId", SqlDbType.Int, ABCSale.CustomerId.ToString());
            ABCAddSaleCommand.Parameters.Add(ABCAddSaleParameter);


            SqlDataReader DataReader = ABCAddSaleCommand.ExecuteReader();

            if (DataReader.HasRows)
            {
                DataReader.Read();

                saleNumber = int.Parse(DataReader[0].ToString());
            }

            ABCHardwareConnection.Close();

            return saleNumber;
        }
        private static SqlCommand CreateSqlCommandSP(SqlConnection sqlConnection, string storedProcedureName)
        {
            return new SqlCommand
            {
                Connection = sqlConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = storedProcedureName
            };
        }

        private static SqlParameter CreateSqlParameter(string parameterName, SqlDbType sqlDbType, string sqlValue)
        {
            return new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = sqlDbType,
                Direction = ParameterDirection.Input,
                SqlValue = sqlValue
            };
        }
    }
}
