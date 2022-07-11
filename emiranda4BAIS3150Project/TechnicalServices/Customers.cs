using emiranda4BAIS3150Project.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace emiranda4BAIS3150Project.TechnicalServices
{
    public class Customers
    {
        private string ConnectionString = Startup.ConnectionString;
        public bool AddCustomer(Customer newCustomer)
        {
            bool success = false;

            SqlConnection ABCHardwareConnection = new SqlConnection();
            ABCHardwareConnection.ConnectionString = ConnectionString;

            ABCHardwareConnection.Open();

            SqlCommand ABCAddACustomerCommand = CreateSqlCommandSP(ABCHardwareConnection, "AddACustomer");

            SqlParameter ABCAddACustomerParameter;

            ABCAddACustomerParameter = CreateSqlParameter("@CustomerFirstName", SqlDbType.VarChar, newCustomer.CustomerFirstName);
            ABCAddACustomerCommand.Parameters.Add(ABCAddACustomerParameter);

            ABCAddACustomerParameter = CreateSqlParameter("@CustomerLastName", SqlDbType.VarChar, newCustomer.CustomerLastName);
            ABCAddACustomerCommand.Parameters.Add(ABCAddACustomerParameter);

            ABCAddACustomerParameter = CreateSqlParameter("@Address", SqlDbType.VarChar, newCustomer.Address);
            ABCAddACustomerCommand.Parameters.Add(ABCAddACustomerParameter);

            ABCAddACustomerParameter = CreateSqlParameter("@City", SqlDbType.VarChar, newCustomer.City);
            ABCAddACustomerCommand.Parameters.Add(ABCAddACustomerParameter);

            ABCAddACustomerParameter = CreateSqlParameter("@Province", SqlDbType.VarChar, newCustomer.Province);
            ABCAddACustomerCommand.Parameters.Add(ABCAddACustomerParameter);

            ABCAddACustomerParameter = CreateSqlParameter("@PostalCode", SqlDbType.VarChar, newCustomer.PostalCode);
            ABCAddACustomerCommand.Parameters.Add(ABCAddACustomerParameter);

            ABCAddACustomerParameter = CreateSqlParameter("@Status", SqlDbType.VarChar, newCustomer.Status);
            ABCAddACustomerCommand.Parameters.Add(ABCAddACustomerParameter);

            ABCAddACustomerCommand.ExecuteNonQuery();

            ABCHardwareConnection.Close();

            success = true;

            return success;
        }

        public Customer GetCustomer(int customerId)
        {
            SqlConnection ABCHardwareConnection = new SqlConnection();
            ABCHardwareConnection.ConnectionString = ConnectionString;

            ABCHardwareConnection.Open();

            SqlCommand ABCGetACustomerCommand = CreateSqlCommandSP(ABCHardwareConnection, "GetACustomer");

            SqlParameter ABCGetACustomerParameter = CreateSqlParameter("@CustomerId", SqlDbType.Int, customerId.ToString());
            ABCGetACustomerCommand.Parameters.Add(ABCGetACustomerParameter);

            SqlDataReader DataReader = ABCGetACustomerCommand.ExecuteReader();

            Customer existingCustomer = new Customer();
            if (DataReader.HasRows)
            {
                DataReader.Read();
                existingCustomer = new Customer
                {
                    CustomerId = int.Parse(DataReader["CustomerId"].ToString()),
                    CustomerFirstName = DataReader["CustomerFirstName"].ToString(),
                    CustomerLastName = DataReader["CustomerLastName"].ToString(),
                    Address = DataReader["Address"].ToString(),
                    City = DataReader["City"].ToString(),
                    Province = DataReader["Province"].ToString(),
                    PostalCode = DataReader["PostalCode"].ToString(),
                    Status = DataReader["Status"].ToString(),
                };
            }

            ABCHardwareConnection.Close();

            return existingCustomer;
        }

        public bool DeleteCustomer(int customerId)
        {
            bool success = false;

            SqlConnection ABCHardwareConnection = new SqlConnection();
            ABCHardwareConnection.ConnectionString = ConnectionString;

            ABCHardwareConnection.Open();

            SqlCommand ABCDeleteACustomerCommand = CreateSqlCommandSP(ABCHardwareConnection, "DeleteACustomer");

            SqlParameter ABCDeleteACustomerParameter = CreateSqlParameter("@CustomerId", SqlDbType.Int, customerId.ToString());
            ABCDeleteACustomerCommand.Parameters.Add(ABCDeleteACustomerParameter);

            ABCDeleteACustomerCommand.ExecuteNonQuery();
            ABCHardwareConnection.Close();

            success = true;

            return success;
        }

        public bool UpdateCustomer(Customer existingCustomer)
        {
            bool success = false;

            SqlConnection ABCHardwareConnection = new SqlConnection();
            ABCHardwareConnection.ConnectionString = ConnectionString;

            ABCHardwareConnection.Open();

            SqlCommand ABCUpdateACustomerCommand = CreateSqlCommandSP(ABCHardwareConnection, "UpdateACustomer");

            SqlParameter ABCUpdateACustomerParameter;

            ABCUpdateACustomerParameter = CreateSqlParameter("@CustomerId", SqlDbType.Int, existingCustomer.CustomerId.ToString());
            ABCUpdateACustomerCommand.Parameters.Add(ABCUpdateACustomerParameter);

            ABCUpdateACustomerParameter = CreateSqlParameter("@CustomerFirstName", SqlDbType.VarChar, existingCustomer.CustomerFirstName);
            ABCUpdateACustomerCommand.Parameters.Add(ABCUpdateACustomerParameter);

            ABCUpdateACustomerParameter = CreateSqlParameter("@CustomerLastName", SqlDbType.VarChar, existingCustomer.CustomerLastName);
            ABCUpdateACustomerCommand.Parameters.Add(ABCUpdateACustomerParameter);

            ABCUpdateACustomerParameter = CreateSqlParameter("@Address", SqlDbType.VarChar, existingCustomer.Address);
            ABCUpdateACustomerCommand.Parameters.Add(ABCUpdateACustomerParameter);

            ABCUpdateACustomerParameter = CreateSqlParameter("@City", SqlDbType.VarChar, existingCustomer.City);
            ABCUpdateACustomerCommand.Parameters.Add(ABCUpdateACustomerParameter);

            ABCUpdateACustomerParameter = CreateSqlParameter("@Province", SqlDbType.VarChar, existingCustomer.Province);
            ABCUpdateACustomerCommand.Parameters.Add(ABCUpdateACustomerParameter);

            ABCUpdateACustomerParameter = CreateSqlParameter("@PostalCode", SqlDbType.VarChar, existingCustomer.PostalCode);
            ABCUpdateACustomerCommand.Parameters.Add(ABCUpdateACustomerParameter);

            ABCUpdateACustomerCommand.ExecuteNonQuery();

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
