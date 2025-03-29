using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entity;

namespace ManagerLayer.Interfaces
{
    public interface IUserManager
    {
        public UserEntity Register(RegisterModel model);
        public bool CheckEmail(string email);
        public string Login(LoginModel loginModel);
        public ForgotPasswordModel ForgotPasswordModel(string Email);

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);
        public List<UserEntity> GetAllUsers();
        public UserEntity GetUserById(int userId);
        public List<UserEntity> GetUserByFirstLetter(string letter);
        public int CountUsers();
        public List<UserEntity> GetUsersByOrder(bool ascending);
        public double GetAverageAge();
        public int GetYoungestAge();
        public int GetOldestAge();
    }
}
