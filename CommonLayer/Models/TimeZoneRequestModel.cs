using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class TimeZoneRequestModel
    {
        public DateTime InputTime { get; set; }
        public string FromCountry { get; set; }
        public string ToCountry { get; set; }
    }
}
