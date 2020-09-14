using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using HBRTEST.Entities;
using HBRTEST.Utilities;

namespace HBRTEST.DAL
{
    public class UsersDAL
    {
        SqlDataReader sqlDataReader;
        DBConnection dbConnection = DBConnection.DbConnectionInstance();
        sqlCommand commandInstance = sqlCommand.InstanceCommand();
        public UsersDAL()
        {

        }
        private void CloseConnection(SqlConnection connection)
        {
            if (connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }
        public int CreateUser(UserEntity user)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                bool existsUser = ValidateIfUserExists(user.UserName);
                if (existsUser)
                {
                    return 0;
                }
                else
                {
                    sqlConnection.Open();
                    command.Connection = sqlConnection;
                    command.CommandText = "CreateUser";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                    command.Parameters.Add(new SqlParameter("@CellPhone", user.CellPhone));
                    command.Parameters.Add(new SqlParameter("@Genre", user.Genre));
                    command.Parameters.Add(new SqlParameter("@Email", user.Email));
                    command.Parameters.Add(new SqlParameter("@UserName", user.UserName));
                    command.Parameters.Add(new SqlParameter("@Password", PasswordEncrypt.Encrypt(user.Password)));
                    command.ExecuteNonQuery();
                    CloseConnection(sqlConnection);
                    return 1;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection(sqlConnection);
            }
        }

        private bool ValidateIfUserExists(string UserName)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "ValidateIfUserExists";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@UserName", UserName);
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if (!string.IsNullOrEmpty(sqlDataReader.GetString(0)))
                    {
                        sqlDataReader.Close();
                        CloseConnection(sqlConnection);
                        return true;
                    }
                }
                sqlDataReader.Close();
                CloseConnection(sqlConnection);
                return false;
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection(sqlConnection);
            }
        }
        public UserEntity SignIn(string UserName, string Password)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            UserEntity user = new UserEntity();

            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "GetUserByUserNameAndPassword";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@UserName", UserName));
                command.Parameters.Add(new SqlParameter("@Password", Password));
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    user.UserId = sqlDataReader.GetInt32(0);
                    user.FirstName = sqlDataReader.GetString(1);
                    user.LastName = sqlDataReader.GetString(2);
                    user.CellPhone = sqlDataReader.GetString(3);
                    user.Genre = sqlDataReader.GetString(4);
                    user.Email = sqlDataReader.GetString(5);
                    user.UserName = sqlDataReader.GetString(6);
                    user.Password = sqlDataReader.GetString(7);
                }
                sqlDataReader.Close();
                CloseConnection(sqlConnection);
                return user;
            }
            catch
            {
                throw;
            }
            finally
            {
                sqlDataReader.Close();
                CloseConnection(sqlConnection);
            }
        }
        public int UpdateProfile(UserEntity user)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();

            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "UpdateUser";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@UserId", user.UserId));
                command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                command.Parameters.Add(new SqlParameter("@CellPhone", user.CellPhone));
                command.Parameters.Add(new SqlParameter("@Genre", user.Genre));
                command.Parameters.Add(new SqlParameter("@Email", user.Email));
                command.Parameters.Add(new SqlParameter("@Password", PasswordEncrypt.Encrypt(user.Password)));
                command.ExecuteNonQuery();
                CloseConnection(sqlConnection);
                return 1;
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection(sqlConnection);
            }
        }
    }
}
