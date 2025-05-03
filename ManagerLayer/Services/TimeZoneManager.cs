using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using ManagerLayer.Interfaces;
using RepositoryLayer.Interfaces;

namespace ManagerLayer.Services
{
    public class TimeZoneManager : ITimeZoneManager
    {
        private readonly ITimeZoneRepo timeZoneRepo;
        public TimeZoneManager(ITimeZoneRepo timeZoneRepo)
        {
            this.timeZoneRepo = timeZoneRepo;
        }


        public TimeZoneResponseModel ConvertTime(TimeZoneRequestModel model)
        {
            try
            {
                return this.timeZoneRepo.ConvertTime(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
