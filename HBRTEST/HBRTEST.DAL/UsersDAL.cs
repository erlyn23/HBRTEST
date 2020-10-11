using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using HBRTEST.Domain;
using HBRTEST.Core.Utilities;
using HBRTEST.ErrorHandling;
using HBRTEST.Core.DBUtilities;

namespace HBRTEST.DAL
{
    public class UsersDAL
    {
        SqlDataReader sqlDataReader;
        DBConnection dbConnection = DBConnection.DbConnectionInstance();
        Command commandInstance = Command.InstanceCommand();
        public UsersDAL()
        {

        }

        public UserEntity GetUserById(int UserId)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            UserEntity user = null;
            try
            {
                if(UserId <= 0)
                {
                    throw new PersonalizedException("El id del usuario debe ser mayor o igual a 1");
                }
                else
                {
                    sqlConnection.Open();
                    command.Connection = sqlConnection;
                    command.CommandText = "GetUserById";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@UserId", UserId));
                    sqlDataReader = command.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        user = new UserEntity {
                            UserId = sqlDataReader.GetInt32(0),
                            FirstName = sqlDataReader.GetString(1),
                            LastName = sqlDataReader.GetString(2),
                            CellPhone = sqlDataReader.GetString(3),
                            Genre = sqlDataReader.GetString(4),
                            Email = sqlDataReader.GetString(5),
                            UserName = sqlDataReader.GetString(6),
                            Password = sqlDataReader.GetString(7),
                            CreationDate = sqlDataReader.GetDateTime(8),
                            LastModificationDate = sqlDataReader.GetDateTime(9),
                            Active = sqlDataReader.GetBoolean(10)
                       };
                    }
                    sqlDataReader.Close();
                    DBConnection.CloseConnection(sqlConnection);
                    if (user == null)
                    {
                        throw new PersonalizedException("Usuario no encontrado");
                        
                    }
                    return user;
                }
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
            finally
            {
                DBConnection.CloseConnection(sqlConnection);
            }
        }
        public void CreateUser(UserEntity user)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                bool hasUserEmptyFields = ValidateNullOrEmptyFields(user);
                bool isUserNameExists = ValidateIfUserNameExists(user.UserName);
                if (user == null)
                {
                    throw new PersonalizedException("El usuario no puede ser nulo");
                }
                else if (hasUserEmptyFields)
                {
                    throw new PersonalizedException("No puedes dejar campos vacíos");
                }
                else if (isUserNameExists)
                {
                    throw new PersonalizedException("El nombre de usuario ya existe, intente con uno nuevo");
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
                    command.Parameters.Add(new SqlParameter("@CreationDate", DateTime.Today));
                    command.Parameters.Add(new SqlParameter("@LastModificationDate", DateTime.Today));
                    command.Parameters.Add(new SqlParameter("@Active", true));
                    command.ExecuteNonQuery();
                    DBConnection.CloseConnection(sqlConnection);
                }
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
            finally
            {
                DBConnection.CloseConnection(sqlConnection);
            }
        }
        private bool ValidateNullOrEmptyFields(UserEntity user)
        {
            if(user.UserId > 0)
            {
                if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.CellPhone)
                || string.IsNullOrEmpty(user.Genre) || string.IsNullOrEmpty(user.Email))
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.CellPhone)
                || string.IsNullOrEmpty(user.Genre) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserName)
                || string.IsNullOrEmpty(user.Password))
                {
                    return true;
                }
                return false;
            }
        }
        private bool ValidateIfUserNameExists(string UserName)
        {

            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            try
            {
                if (string.IsNullOrEmpty(UserName))
                    throw new PersonalizedException("El campo nombre de usuario no puede estar vacío");
                
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "ValidateIfUserExists";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.Add(new SqlParameter("@UserName", UserName));
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if (!string.IsNullOrEmpty(sqlDataReader.GetString(0)))
                    {
                        sqlDataReader.Close();
                        DBConnection.CloseConnection(sqlConnection);
                        return true;
                    }
                }
                sqlDataReader.Close();
                DBConnection.CloseConnection(sqlConnection);
                return false;
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
            finally
            {
               DBConnection.CloseConnection(sqlConnection);
            }
        }
        public UserEntity SignIn(string UserName, string Password)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            UserEntity user = new UserEntity();

            try
            {
                if(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    throw new PersonalizedException("Debes ingresar un nombre de usuario y una contraseña");
                }
                else
                {
                    sqlConnection.Open();
                    command.Connection = sqlConnection;
                    command.CommandText = "GetUserByUserNameAndPassword";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@UserName", UserName));
                    command.Parameters.Add(new SqlParameter("@Password", PasswordEncrypt.Encrypt(Password)));
                    sqlDataReader = command.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        user = new UserEntity
                        {
                            UserId = sqlDataReader.GetInt32(0),
                            UserName = sqlDataReader.GetString(6),
                            Active = sqlDataReader.GetBoolean(10)
                        };
                    }
                    sqlDataReader.Close();
                    DBConnection.CloseConnection(sqlConnection);
                    if(user == null)
                    {
                        throw new PersonalizedException("Nombre de usuario o contraseña incorrecta");
                    }
                    return user;
                }
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
            finally
            {
                DBConnection.CloseConnection(sqlConnection);
            }
        }
        public void UpdateProfile(UserEntity user)
        {
            SqlConnection sqlConnection = dbConnection.GetDbConnection();
            SqlCommand command = commandInstance.GetSqlCommand();
            bool newPassword = false;
            UserEntity oldUser = null;
            try
            {
                bool hasUserEmptyFields = ValidateNullOrEmptyFields(user);
                if(user == null)
                {
                    throw new PersonalizedException("El usuario no puede ser nulo o vacío");
                }
                else if (hasUserEmptyFields)
                {
                    throw new PersonalizedException("No puedes dejar campos vacíos");
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    oldUser = GetUserById(user.UserId);
                    newPassword = false;
                }
               
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
                if (newPassword)
                {
                    command.Parameters.Add(new SqlParameter("@Password", PasswordEncrypt.Encrypt(user.Password)));
                }
                else
                {
                    command.Parameters.Add(new SqlParameter("@Password", oldUser.Password));
                }
                command.Parameters.Add(new SqlParameter("@LastModificationDate", DateTime.Today));
                command.Parameters.Add(new SqlParameter("@Active", user.Active));
                command.ExecuteNonQuery();
                DBConnection.CloseConnection(sqlConnection);
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
            finally
            {
                DBConnection.CloseConnection(sqlConnection);
            }
        }
    }
}
