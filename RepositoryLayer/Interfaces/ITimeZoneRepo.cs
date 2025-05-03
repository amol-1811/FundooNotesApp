using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface ITimeZoneRepo
    {
        public TimeZoneResponseModel ConvertTime(TimeZoneRequestModel model);
    }
}
