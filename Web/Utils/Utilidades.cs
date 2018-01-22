using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;  
namespace Class.Utilidades
{
    public class Utilidades
    {
        #region VALIDACIONES
        // Numero: parametro String
        public static bool esNumero(String miTextBox)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            return regex.IsMatch(miTextBox);
        }
        // Cadena: parametro String
        public static bool esCadena(String pValor)
        {
            Regex regex = new Regex(@"^[^ ][a-zA-Z ]+[^ ]$");
            return regex.IsMatch(pValor);
        }
        //Es mayuscula
        public static bool esMayuscula(String pValor)
        {
            Regex regex = new Regex(@"^[A-Z]+$");
            return regex.IsMatch(pValor);
        }
        //Es minuscula
        public static bool esMinuscula(String pValor)
        {
            Regex regex = new Regex(@"^[a-z]+$");
            return regex.IsMatch(pValor);
        }
        // Telefono
        public static bool esTelefono(String miTextBox)
        {
            if (miTextBox.Trim().Equals("-")) {
                return true;
            } 
            else
            { 
                Regex regex = new Regex(@"^([0-9]{4})[\-]([0-9]{4})?$");
                return regex.IsMatch(miTextBox);
            }
        }

        /// <summary>
        /// veirifica que el telefono no sea solo maskara 
        /// </summary>
        /// <param name="telefono"></param>
        /// <returns></returns>
        public static String verificaTelefono(String telefono){
            if (telefono.Trim().Equals("-"))
            {
                return "";
            }
            else
            {
                return telefono;
            }
        }



        // Decimal: parametro String
        public static bool esDecimal(String pValor)
        {  
            Regex regex = new Regex(@"^[0-9]{1,9}([\.][0-9]{1,3})?$");
            return regex.IsMatch(pValor);
        }
        // URL                                       
        public static bool esUrl(String miTextBox)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\-\.]+\.(com|org|net|mil|edu|es|COM|ORG|NET|MIL|EDU|ES)$");
            return regex.IsMatch(miTextBox);
        }
        // Email
        public static bool esEmail(String miTextBox)
        {                              // \w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*
            Regex regex = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");

            // Resultado: 
            //       Valid: david.jones@proseware.com 
            //       Valid: d.j@server1.proseware.com 
            //       Valid: jones@ms1.proseware.com 
            //       Invalid: j.@server1.proseware.com 
            //       Invalid: j@proseware.com9 
            //       Valid: js#internal@proseware.com 
            //       Valid: j_9@[129.126.118.1] 
            //       Invalid: j..s@proseware.com 
            //       Invalid: js*@proseware.com 
            //       Invalid: js@proseware..com 
            //       Invalid: js@proseware.com9 
            //       Valid: j.s@server1.proseware.com

            return regex.IsMatch(miTextBox);
        }


        public static String validarExepcionSQL(String message)  {
            if(message.ToUpper().Contains("DUPLICATE ENTRY") ){
                return "El registro a insertar ya se encuentra en el sistema. " +  
                        "Cambiar el o los códigos que desea guardar por otros que no existan en el sistema.";
            }else{ 
                if(message.ToUpper().Contains("CANNOT DELETE OR UPDATE A PARENT ROW") ){
                    return "El registro está siendo utilizado por otras funcionalidades del sistema. " + 
                            "Para eliminar el registro primero se deben eliminar los registros asociados en los respectivos mantenimientos o modificar estos para que no se relacionen con este registro.";
                }else{ 
                    if (message.ToUpper().Contains("FOREIGN KEY")) {
                        if(message.ToUpper().Contains("ADD") ){
                            return "El registro a insertar contiene datos asociados que aún no están almacenados en el sistema. " + "\n" +
                                                "Para insertar el registro primero debe verificar los registros asociados, esto porque algún registro asociado no existe en el sistema.";
                        }else{
                            return "El registro está siendo utilizado por otras funcionalidades del sistema. " +  
                                                "Para eliminar el registro primero se deben eliminar los registros asociados en los respectivos mantenimientos o modificar estos para que no se relacionen con este registro.";
                        }
                    }else{
                        return "Error no controlado." + "\n" +
                              "Favor comunicarse con el administrador del sistema. \n"  + message;
                    }
                }
           }
        }
         
        #endregion

        #region CULTURE

        public static CultureInfo getCulture()
        {
            CultureInfo Micultura = new CultureInfo("es-CR", false);
            Micultura.NumberFormat.CurrencySymbol = "₵";
            Micultura.NumberFormat.CurrencyDecimalDigits = 2;
            Micultura.NumberFormat.CurrencyDecimalSeparator = ".";
            Micultura.NumberFormat.CurrencyGroupSeparator = ",";
            int[] grupo = new int[] { 3, 3, 3 };
            Micultura.NumberFormat.CurrencyGroupSizes = grupo;
            Micultura.NumberFormat.NumberDecimalDigits = 2;
            Micultura.NumberFormat.NumberGroupSeparator = ",";
            Micultura.NumberFormat.NumberDecimalSeparator = ".";
            Micultura.NumberFormat.NumberGroupSizes = grupo;
            return Micultura;
        }
        #endregion

       public static CultureInfo getCultureCaja()
        {
            CultureInfo Micultura = new CultureInfo("es-CR", false);
            Micultura.NumberFormat.CurrencySymbol = "₵";
            Micultura.NumberFormat.CurrencyDecimalDigits = 2;
            //Micultura.NumberFormat.CurrencyDecimalSeparator = ".";
            //Micultura.NumberFormat.CurrencyGroupSeparator = ",";
            int[] grupo = new int[] { 3, 3, 3 };
            Micultura.NumberFormat.CurrencyGroupSizes = grupo;
            Micultura.NumberFormat.NumberDecimalDigits = 2;
            //Micultura.NumberFormat.NumberGroupSeparator = ",";
            //Micultura.NumberFormat.NumberDecimalSeparator = ".";
            Micultura.NumberFormat.NumberGroupSizes = grupo;
            return Micultura;
            
        }
    }
}
