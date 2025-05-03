using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class TimeZoneRepo : ITimeZoneRepo
    {
        private readonly IConfiguration configuration;
        private readonly Dictionary<string, string> _countryToTimeZone = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "United States", "Eastern Standard Time" },
        { "India", "India Standard Time" },
        { "United Kingdom", "GMT Standard Time" },
        { "Germany", "W. Europe Standard Time" },
        { "Australia", "AUS Eastern Standard Time" },
        { "Japan", "Tokyo Standard Time" },
        { "China", "China Standard Time" },
    };
        public TimeZoneRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public TimeZoneResponseModel ConvertTime(TimeZoneRequestModel model)
        {
            TimeZoneResponseModel response = new TimeZoneResponseModel();
            if (_countryToTimeZone.TryGetValue(model.FromCountry, out string fromTimeZoneId) && _countryToTimeZone.TryGetValue(model.ToCountry, out string toTimeZoneId))
            {
                TimeZoneInfo fromTimeZone = TimeZoneInfo.FindSystemTimeZoneById(fromTimeZoneId);
                TimeZoneInfo toTimeZone = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);
                DateTime fromDateTime = TimeZoneInfo.ConvertTime(model.InputTime, fromTimeZone);
                DateTime toDateTime = TimeZoneInfo.ConvertTime(fromDateTime, toTimeZone);
                
                return new TimeZoneResponseModel
                {
                    OriginalTime = fromDateTime,
                    FromCountry = model.FromCountry,
                    ToCountry = model.ToCountry,
                    ConvertedTime = toDateTime,
                };
            }
            return null;
        }
        
    }
}
