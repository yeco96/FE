using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Class.Utilidades
{
    class Formatstring
    {
        // NO CURRENCY
        public static string GetCurrencyNoFormat(string pValor)
        {
            double value = double.Parse(pValor, NumberStyles.Currency, CultureInfo.CurrentCulture);
            return Convert.ToString(value);
        }
        // CURRENCY
        public static string SetCurrencyFormat(double pValor)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:C2}", pValor);
        }
        // NO NUMERIC
        public static string GetNumericNoFormat(string pValor)
        {
            double value = double.Parse(pValor, NumberStyles.Number, CultureInfo.CurrentCulture);
            return Convert.ToString(value);
        }
        // NUMERIC
        public static string SetNumericFormat(double pValor)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0:N}", pValor);
        }
        // NO PHONE NUMBER
        public static string GetPhoneNumberNoFormat(string pValor)
        {
            string phoneNumber = pValor;
            phoneNumber = Regex.Replace(phoneNumber, @"[^\d]", "");
            return phoneNumber;
        }
        // PHONE NUMBER
        public static string GetDNINoFormat(string pValor)
        {
            string dni = pValor;
            dni = Regex.Replace(dni, @"[^\d]", "");
            return dni;
        }
    }
}
