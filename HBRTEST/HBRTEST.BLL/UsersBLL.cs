using System;
using System.Collections.Generic;
using System.Text;
using HBRTEST.DAL;
using HBRTEST.Entities;

namespace HBRTEST.BLL
{
    public class UsersBLL
    {
        private UsersDAL _usersRepository = new UsersDAL();
        public UsersBLL()
        {

        }

        public UserEntity SignIn(string UserName, string Password)
        {
            UserEntity user = _usersRepository.SignIn(UserName, Password);
            return user;
        }
        public int CreateUser(UserEntity user)
        {
            int creationResult = _usersRepository.CreateUser(user);
            return creationResult;
        }

        public int UpdateProfile(UserEntity user)
        {
            int updateResult = _usersRepository.UpdateProfile(user);
            return updateResult;
        }
    }
}
