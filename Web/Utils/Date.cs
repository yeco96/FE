using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Utilidades
{
    public class Date : IEquatable<Date>, IEquatable<DateTime>
    {
        public Date(DateTime date)
        {
            value = date.Date;
        }

        public bool Equals(Date other)
        {
            return other != null && value.Equals(other.value);
        }

        public bool Equals(DateTime other)
        {
            return value.Equals(other);
        }

        public override String ToString()
        {
            return value.ToString();
        }

        public static implicit operator DateTime(Date date)
        {
            return date.value;
        }

        public static explicit operator Date(DateTime dateTime)
        {
            return new Date(dateTime);
        }

        private DateTime value;

        public static DateTime DateTimeNow()
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Central America Standard Time");
        }

        
    }
}
