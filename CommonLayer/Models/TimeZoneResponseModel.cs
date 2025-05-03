using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class TimeZoneResponseModel
    {
        public DateTime OriginalTime { get; set; }
        public string FromCountry { get; set; }
        public string ToCountry { get; set; }
        public DateTime ConvertedTime { get; set; }
    }
}
