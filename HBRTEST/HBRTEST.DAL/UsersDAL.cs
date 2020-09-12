using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using HBRTEST.Entities;
using HBRTEST.Utilities;
using System.ComponentModel;

namespace HBRTEST.DAL
{
    public class UsersDAL: IDisposable
    {
        #region Definiciones
        SqlDataReader sqlDataReader;
        private Component components = new Component();
        private bool _disposed = false;
        #endregion
        #region Constructor
        public UsersDAL()
        {

        }
        #endregion
        #region Propiedades
        #endregion
        #region Métodos
        #region Métodos Públicos


        public string CreateUser(UserEntity user)
        {
            #region Definiciones
            SqlConnection sqlConnection = DBConnection.DbInstance();
            SqlCommand command = sqlCommand.InstanceCommand();
            #endregion
            #region Proceso

            try
            {
                bool existsUser = ValidateIfUserExists(user.UserName);
                if (existsUser)
                {
                    return "Error: El usuario ya existe, intente con uno nuevo";
                }
                else
                {
                    sqlConnection.Open();
                    command.Connection = sqlConnection;
                    command.CommandText = "exec CreateUser";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                    command.Parameters.Add(new SqlParameter("@CellPhone", user.CellPhone));
                    command.Parameters.Add(new SqlParameter("@Genre", user.Genre));
                    command.Parameters.Add(new SqlParameter("@Email", user.Email));
                    command.Parameters.Add(new SqlParameter("@UserName", user.UserName));
                    command.Parameters.Add(new SqlParameter("@Password", PasswordEncrypt.Encrypt(user.Password)));
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                    return "Usuario registrado correctamente";
                }
            }
            catch (Exception exception)
            {
                sqlConnection.Close();
                return exception.Message;
            }
            #endregion
        }
        public UserEntity SignIn(string UserName, string Password)
        {
            #region Definiciones
            SqlConnection sqlConnection = DBConnection.DbInstance();
            SqlCommand command = sqlCommand.InstanceCommand();
            UserEntity user = new UserEntity();
            #endregion
            #region Proceso
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "exec GetUserByUserNameAndPassword";
                command.CommandType = System.Data.CommandType.StoredProcedure;
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
                if (user != null)
                {
                    sqlDataReader.Close();
                    sqlConnection.Close();
                    return user;
                }
                sqlDataReader.Close();
                sqlConnection.Close();
                return null;
            }
            catch
            {
                sqlDataReader.Close();
                sqlConnection.Close();
                throw new Exception();
            }
            #endregion
        }
        public UserEntity UpdateProfile(UserEntity user)
        {
            #region Definiciones
            SqlConnection sqlConnection = DBConnection.DbInstance();
            SqlCommand command = sqlCommand.InstanceCommand();
            #endregion
            #region Proceso
            try
            {
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "exec UpdateUser";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserId", user.UserId));
                command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                command.Parameters.Add(new SqlParameter("@CellPhone", user.CellPhone));
                command.Parameters.Add(new SqlParameter("@Genre", user.Genre));
                command.Parameters.Add(new SqlParameter("@Email", user.Email));
                command.Parameters.Add(new SqlParameter("@Password", PasswordEncrypt.Encrypt(user.Password)));
                command.ExecuteNonQuery();
                sqlConnection.Close();
                return user;
            }
            catch
            {
                sqlConnection.Close();
                throw new Exception();
            }
            #endregion
        }
        #endregion


        #region Métodos Privados
        private bool ValidateIfUserExists(string UserName)
        {
            SqlConnection sqlConnection = DBConnection.DbInstance();
            SqlCommand command = sqlCommand.InstanceCommand();
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    return false;
                }
                sqlConnection.Open();
                command.Connection = sqlConnection;
                command.CommandText = "exec ValidateIfUserExists";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@UserName", UserName));
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if (!string.IsNullOrEmpty(sqlDataReader.GetString(0)))
                    {
                        sqlDataReader.Close();
                        sqlConnection.Close();
                        return true;
                    }
                }
                sqlDataReader.Close();
                sqlConnection.Close();
                return false;
            }
            catch
            {
                sqlDataReader.Close();
                sqlConnection.Close();
                throw new Exception();
            }
        }
        #endregion
        #endregion

        #region Destructor
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this.components.Dispose();
                    this.components = null;
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~UsersDAL() => Dispose(false);
        #endregion
    }
}
