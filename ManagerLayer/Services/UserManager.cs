using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace ManagerLayer.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepo userRepo;

        public UserManager(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        public UserEntity Register(RegisterModel model)
        {
            return userRepo.Register(model);
        }
        public bool CheckEmail(string email)
        {
            return userRepo.CheckEmail(email);
        }

        public string Login(LoginModel loginModel)
        {
            return userRepo.Login(loginModel);
        }
        public ForgotPasswordModel ForgotPasswordModel(string Email)
        {
            return userRepo.ForgotPasswordModel(Email);
        }

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            return userRepo.ResetPassword(Email, resetPasswordModel);
        }

    }
}
