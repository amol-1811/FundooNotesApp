using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepo
    {
        public UserEntity Register(RegisterModel model);
        public bool CheckEmail(string email);

        public string Login(LoginModel loginModel);
        public ForgotPasswordModel ForgotPasswordModel(string Email);

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);
    }
}
