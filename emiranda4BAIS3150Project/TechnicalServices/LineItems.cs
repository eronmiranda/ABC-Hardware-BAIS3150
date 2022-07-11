using emiranda4BAIS3150Project.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace emiranda4BAIS3150Project.TechnicalServices
{
    public class LineItems
    {
        private string ConnectionString = Startup.ConnectionString;
        public bool AddLineItem(LineItem newLineItem)
        {
            bool success = false;

            SqlConnection ABCHardwareConnection = new SqlConnection();
            ABCHardwareConnection.ConnectionString = ConnectionString;

            ABCHardwareConnection.Open();

            SqlCommand ABCAddLineItemCommand = CreateSqlCommandSP(ABCHardwareConnection, "AddLineItem");

            SqlParameter ABCAddLineItemParameter;

            ABCAddLineItemParameter = CreateSqlParameter("@Quantity", SqlDbType.Int, newLineItem.Quantity.ToString());
            ABCAddLineItemCommand.Parameters.Add(ABCAddLineItemParameter);

            ABCAddLineItemParameter = CreateSqlParameter("@ItemCode", SqlDbType.Char, newLineItem.ItemCode);
            ABCAddLineItemCommand.Parameters.Add(ABCAddLineItemParameter);

            ABCAddLineItemParameter = CreateSqlParameter("@SaleNumber", SqlDbType.Money,
                newLineItem.SaleNumber.ToString());
            ABCAddLineItemCommand.Parameters.Add(ABCAddLineItemParameter);

            ABCAddLineItemCommand.ExecuteNonQuery();

            ABCHardwareConnection.Close();

            success = true;

            return success;
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
