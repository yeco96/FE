using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Class.Utilidades
{
    class FormatString
    {
        // NO CURRENCY
        public static String GetCurrencyNoFormat(String pValor)
        {
            double value = double.Parse(pValor, NumberStyles.Currency, CultureInfo.CurrentCulture);
            return Convert.ToString(value);
        }
        // CURRENCY
        public static String SetCurrencyFormat(double pValor)
        {
            return String.Format(CultureInfo.CurrentCulture, "{0:C2}", pValor);
        }
        // NO NUMERIC
        public static String GetNumericNoFormat(String pValor)
        {
            double value = double.Parse(pValor, NumberStyles.Number, CultureInfo.CurrentCulture);
            return Convert.ToString(value);
        }
        // NUMERIC
        public static String SetNumericFormat(double pValor)
        {
            return String.Format(CultureInfo.CurrentCulture, "{0:N}", pValor);
        }
        // NO PHONE NUMBER
        public static String GetPhoneNumberNoFormat(String pValor)
        {
            String phoneNumber = pValor;
            phoneNumber = Regex.Replace(phoneNumber, @"[^\d]", "");
            return phoneNumber;
        }
        // PHONE NUMBER
        public static String GetDNINoFormat(String pValor)
        {
            String dni = pValor;
            dni = Regex.Replace(dni, @"[^\d]", "");
            return dni;
        }
    }
}
