using emiranda4BAIS3150Project.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace emiranda4BAIS3150Project.TechnicalServices
{
    public class Items
    {
        private string ConnectionString = Startup.ConnectionString;
        public bool AddItem(Item newItem)
        {
            bool success = false;

            SqlConnection ABCHardwareConnection = new SqlConnection();
            ABCHardwareConnection.ConnectionString = ConnectionString;

            ABCHardwareConnection.Open();

            SqlCommand ABCAddAnItemCommand = CreateSqlCommandSP(ABCHardwareConnection, "AddAnItem");

            SqlParameter ABCAddAnItemParameter;

            ABCAddAnItemParameter = CreateSqlParameter("@ItemCode", SqlDbType.Char, newItem.ItemCode);
            ABCAddAnItemCommand.Parameters.Add(ABCAddAnItemParameter);

            ABCAddAnItemParameter = CreateSqlParameter("@Description", SqlDbType.VarChar, newItem.Description);
            ABCAddAnItemCommand.Parameters.Add(ABCAddAnItemParameter);

            ABCAddAnItemParameter = CreateSqlParameter("@UnitPrice", SqlDbType.Money,
                newItem.UnitPrice.ToString());
            ABCAddAnItemCommand.Parameters.Add(ABCAddAnItemParameter);

            ABCAddAnItemParameter = CreateSqlParameter("@Status", SqlDbType.VarChar, newItem.Status);
            ABCAddAnItemCommand.Parameters.Add(ABCAddAnItemParameter);

            ABCAddAnItemParameter = CreateSqlParameter("@QuantityOnHand", SqlDbType.Int, newItem.QuantityOnHand.ToString());
            ABCAddAnItemCommand.Parameters.Add(ABCAddAnItemParameter);

            ABCAddAnItemCommand.ExecuteNonQuery();

            ABCHardwareConnection.Close();

            success = true;

            return success;
        }

        public Item GetItem(string itemCode)
        {
            SqlConnection ABCHardwareConnection = new SqlConnection();
            ABCHardwareConnection.ConnectionString = ConnectionString;

            ABCHardwareConnection.Open();

            SqlCommand ABCGetAnItemCommand = CreateSqlCommandSP(ABCHardwareConnection, "GetAnItem");

            SqlParameter ABCGetAnItemParameter = CreateSqlParameter("@ItemCode", SqlDbType.Char, itemCode);
            ABCGetAnItemCommand.Parameters.Add(ABCGetAnItemParameter);

            SqlDataReader DataReader = ABCGetAnItemCommand.ExecuteReader();

            Item existingItem = new Item();
            if (DataReader.HasRows)
            {
                DataReader.Read();
                existingItem = new Item
                {
                    ItemCode = DataReader["ItemCode"].ToString(),
                    Description = DataReader["Description"].ToString(),
                    UnitPrice = double.Parse(DataReader["UnitPrice"].ToString()),
                    Status = DataReader["Status"].ToString(),
                    QuantityOnHand = int.Parse(DataReader["QuantityOnHand"].ToString())
                };
            }
            ABCHardwareConnection.Close();
            return existingItem;
        }
        public Item GetItemByDescription(string description)
        {
            SqlConnection ABCHardwareConnection = new SqlConnection();
            ABCHardwareConnection.ConnectionString = ConnectionString;

            ABCHardwareConnection.Open();

            SqlCommand ABCGetAnItemByDescriptionCommand = CreateSqlCommandSP(ABCHardwareConnection, "GetAnItemByDescription");

            SqlParameter ABCGetAnItemByDescriptionParameter = CreateSqlParameter("@Description", SqlDbType.VarChar, description);
            ABCGetAnItemByDescriptionCommand.Parameters.Add(ABCGetAnItemByDescriptionParameter);

            SqlDataReader DataReader = ABCGetAnItemByDescriptionCommand.ExecuteReader();

            Item existingItem = new Item();
            if (DataReader.HasRows)
            {
                DataReader.Read();
                existingItem = new Item
                {
                    ItemCode = DataReader["ItemCode"].ToString(),
                    Description = DataReader["Description"].ToString(),
                    UnitPrice = double.Parse(DataReader["UnitPrice"].ToString()),
                    Status = DataReader["Status"].ToString(),
                    QuantityOnHand = int.Parse(DataReader["QuantityOnHand"].ToString())
                };
            }
            ABCHardwareConnection.Close();
            return existingItem;
        }

        public bool DeleteItem(string itemCode)
        {
            bool success = false;

            SqlConnection ABCHardwareConnection = new SqlConnection();
            ABCHardwareConnection.ConnectionString = ConnectionString;

            ABCHardwareConnection.Open();

            SqlCommand ABCDeleteAnItemCommand = CreateSqlCommandSP(ABCHardwareConnection, "DeleteAnItem");

            SqlParameter ABCDeleteAnItemParameter = CreateSqlParameter("@ItemCode", SqlDbType.Char, itemCode);
            ABCDeleteAnItemCommand.Parameters.Add(ABCDeleteAnItemParameter);

            ABCDeleteAnItemCommand.ExecuteNonQuery();
            ABCHardwareConnection.Close();

            success = true;

            return success;
        }

        public bool UpdateItem(Item updatedItem)
        {
            bool success = false;

            SqlConnection ABCHardwareConnection = new SqlConnection();
            ABCHardwareConnection.ConnectionString = ConnectionString;

            ABCHardwareConnection.Open();

            SqlCommand ABCUpdateAnItemCommand = CreateSqlCommandSP(ABCHardwareConnection, "UpdateAnItem");

            SqlParameter ABCUpdateAnItemParameter;

            ABCUpdateAnItemParameter = CreateSqlParameter("@ItemCode", SqlDbType.Char, updatedItem.ItemCode);
            ABCUpdateAnItemCommand.Parameters.Add(ABCUpdateAnItemParameter);

            ABCUpdateAnItemParameter = CreateSqlParameter("@Description", SqlDbType.VarChar, updatedItem.Description);
            ABCUpdateAnItemCommand.Parameters.Add(ABCUpdateAnItemParameter);

            ABCUpdateAnItemParameter = CreateSqlParameter("@UnitPrice", SqlDbType.Money, updatedItem.UnitPrice.ToString());
            ABCUpdateAnItemCommand.Parameters.Add(ABCUpdateAnItemParameter);

            ABCUpdateAnItemParameter = CreateSqlParameter("@Status", SqlDbType.VarChar, updatedItem.Status);
            ABCUpdateAnItemCommand.Parameters.Add(ABCUpdateAnItemParameter);

            ABCUpdateAnItemParameter = CreateSqlParameter("@QuantityOnHand", SqlDbType.Int, updatedItem.QuantityOnHand.ToString());
            ABCUpdateAnItemCommand.Parameters.Add(ABCUpdateAnItemParameter);

            ABCUpdateAnItemCommand.ExecuteNonQuery();
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
