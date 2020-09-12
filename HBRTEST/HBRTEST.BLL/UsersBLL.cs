using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using HBRTEST.DAL;
using HBRTEST.Entities;

namespace HBRTEST.BLL
{
    class UsersBLL: IDisposable
    {
        #region Definiciones
        private Component components = new Component();
        private bool _disposed = false;
        #endregion

        #region Constructor
        public UsersBLL()
        {

        }
        #endregion

        #region Métodos
        #region Métodos Públicos
        public UserEntity SignIn(string UserName, string Password)
        {
            try
            {
                using (UsersDAL _usersRepository = new UsersDAL())
                {
                    UserEntity user = _usersRepository.SignIn(UserName, Password);
                    if(user != null)
                    {
                        return user;
                    }
                    return null;
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        public string CreateUser(UserEntity user)
        {
            try
            {
                using (UsersDAL _usersRepository = new UsersDAL())
                {
                    string creationResult = _usersRepository.CreateUser(user);
                    return creationResult;
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public UserEntity UpdateProfile(UserEntity user)
        {
            try
            {
                using(UsersDAL _userRepository = new UsersDAL())
                {
                    UserEntity newUser = _userRepository.UpdateProfile(user);
                    if(newUser != null)
                    {
                        return newUser;
                    }
                    return null;
                }
            }
            catch
            {
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
        ~UsersBLL() => Dispose(false);
        #endregion
    }
}
