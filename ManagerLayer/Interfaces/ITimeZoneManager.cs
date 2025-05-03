using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace ManagerLayer.Interfaces
{
    public interface ITimeZoneManager
    {
        public TimeZoneResponseModel ConvertTime(TimeZoneRequestModel model);
    }
}
