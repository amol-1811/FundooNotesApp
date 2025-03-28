using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CommonLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooDBContext context;
        private readonly IConfiguration configuration;
        public UserRepo(FundooDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public UserEntity Register(RegisterModel model)
        {
            UserEntity user = new UserEntity();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DOB = model.DOB;
            user.Gender = model.Gender;
            user.Email = model.Email;
            user.Password = EncodePasswordToBase64(model.Password);
            this.context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        private string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }
        public bool CheckEmail(string email)
        {
            var result = this.context.Users.FirstOrDefault(x => x.Email == email);
            if(result == null)
            {
                return false;
            }
            return true;
        }

        public string Login(LoginModel login)
        {
            var checkUser = this.context.Users.FirstOrDefault(x => x.Email == login.Email && x.Password == EncodePasswordToBase64(login.Password));
            if (checkUser != null) 
            {
                var token = GenerateToken(checkUser.Email, checkUser.UserId);
                return token;
            }
            
            return null;
        }

        private string GenerateToken(string email, int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("EmailId", email),
                new Claim("UserID", userId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public ForgotPasswordModel ForgotPasswordModel(string Email)
        {
            UserEntity user = context.Users.ToList().Find(user => user.Email == Email);
            ForgotPasswordModel forgotPass = new ForgotPasswordModel();
            forgotPass.Email = user.Email;
            forgotPass.UserId = user.UserId;
            forgotPass.Token = GenerateToken(user.Email, user.UserId);
            return forgotPass;
        }

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            UserEntity user = context.Users.ToList().Find(user => user.Email == Email);

            if (CheckEmail(user.Email))
            {
                user.Password = EncodePasswordToBase64(resetPasswordModel.ConfirmPassword);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
