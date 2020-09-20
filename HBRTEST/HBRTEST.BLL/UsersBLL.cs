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
        public UserEntity GetUserById(int UserId)
        {
            UserEntity user = _usersRepository.GetUserById(UserId);
            return user;
        }
        public UserEntity SignIn(string UserName, string Password)
        {
            UserEntity user = _usersRepository.SignIn(UserName, Password);
            return user;
        }
        public void CreateUser(UserEntity user)
        {
            _usersRepository.CreateUser(user);
        }

        public void UpdateProfile(UserEntity user)
        {
            _usersRepository.UpdateProfile(user);
        }
    }
}
