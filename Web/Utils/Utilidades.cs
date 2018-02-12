using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Web.Models;
using Web.Models.Catalogos;
using XMLDomain;

namespace Class.Utilidades
{
    public class Utilidades
    {
        #region VALIDACIONES
        // Numero: parametro string
        public static bool esNumero(string miTextBox)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            return regex.IsMatch(miTextBox);
        }
        // Cadena: parametro string
        public static bool esCadena(string pValor)
        {
            Regex regex = new Regex(@"^[^ ][a-zA-Z ]+[^ ]$");
            return regex.IsMatch(pValor);
        }
        //Es mayuscula
        public static bool esMayuscula(string pValor)
        {
            Regex regex = new Regex(@"^[A-Z]+$");
            return regex.IsMatch(pValor);
        }
        //Es minuscula
        public static bool esMinuscula(string pValor)
        {
            Regex regex = new Regex(@"^[a-z]+$");
            return regex.IsMatch(pValor);
        }
        // Telefono
        public static bool esTelefono(string miTextBox)
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
        public static string verificaTelefono(string telefono){
            if (telefono.Trim().Equals("-"))
            {
                return "";
            }
            else
            {
                return telefono;
            }
        }



        // Decimal: parametro string
        public static bool esDecimal(string pValor)
        {  
            Regex regex = new Regex(@"^[0-9]{1,9}([\.][0-9]{1,3})?$");
            return regex.IsMatch(pValor);
        }
        // URL                                       
        public static bool esUrl(string miTextBox)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9\-\.]+\.(com|org|net|mil|edu|es|COM|ORG|NET|MIL|EDU|ES)$");
            return regex.IsMatch(miTextBox);
        }
        // Email
        public static bool esEmail(string miTextBox)
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


        public static string validarExepcionSQL(string message)  {
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



        #region sendMail 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinatario">direccion de correo</param>
        /// <param name="asunto">asunto de correo</param>
        /// <param name="mensaje">contenido del correo</param>
        /// /// <param name="alias">nombre para enmascar el correo</param>
        /// <returns></returns>
        public static bool sendMail(string destinatario, string asunto, string mensaje, string alias, string xml, string consecutivo, string clave)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    ConfiguracionCorreo mailConfig = conexion.ConfiguracionCorreo.Where(x => x.estado == Estado.ACTIVO.ToString()).FirstOrDefault();
                     
                    MailMessage correo = new MailMessage();
                    SmtpClient smtp = new SmtpClient(); 
                    correo.From = new MailAddress(mailConfig.user, alias);
                    correo.To.Add(destinatario);
                    correo.Subject = String.Format("{0}", asunto);
                    correo.Body = mensaje;

                    if (xml != null)
                    {
                        correo.Attachments.Add(new Attachment(GenerateStreamFromMemoryStream(UtilidadesReporte.generarPDF(clave)), string.Format("{0}.pdf", consecutivo)));
                        correo.Attachments.Add(new Attachment(GenerateStreamFromString(xml), string.Format("{0}.xml", consecutivo)));
                    }
                    correo.Priority = MailPriority.Normal;
                    correo.IsBodyHtml = true;
                    smtp.Credentials = new NetworkCredential(mailConfig.user, mailConfig.password);
                    smtp.Host = mailConfig.host;
                    smtp.Port = int.Parse(mailConfig.port);
                    smtp.EnableSsl = true;
                    smtp.Send(correo);
                    correo.Dispose();
                } 
                return true;
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }
        }
        #endregion

        
         public static string mensageGenerico()
        {
            String mensaje = "";
            mensaje += "<p>Estimado Cliente:</p>";
            mensaje += "<p>Adjunto a este correo encontrará un Comprobante Electrónico en formato XML y su correspondiente representación en formato PDF. Lo anterior con base en las especificaciones del Ministerio de Hacienda.</p>";
            mensaje += "<p></p>";
            mensaje += "<p>**** Este mensaje se ha generado automáticamente.</p>";
            mensaje += "<p>**** Por Favor No conteste a este mensaje ya que no recibirá ninguna respuesta..</p>";
            
            return mensaje; 
        }

   

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static Stream GenerateStreamFromMemoryStream(MemoryStream stream)
        { 
            StreamWriter writer = new StreamWriter(stream); 
            writer.Flush();
            stream.Position = 0;
            return stream;
        }




    }
}
