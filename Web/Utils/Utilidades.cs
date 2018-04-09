using Class.Seguridad;
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
using Web.Models.Facturacion;
using Web.Utils;
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


        public static string validarExepcionSQL(Exception exception) { 

            string message = "";

            if (exception.InnerException != null)
            {
                message += exception.InnerException.Message;
                if (exception.InnerException.InnerException != null)
                {
                    message += exception.InnerException.InnerException.Message;
                }
            }
            else
            {
                message += exception.Message;
            }

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
            Micultura.DateTimeFormat.ShortTimePattern= "HH:mm:ss";
            Micultura.DateTimeFormat.ShortDatePattern= "yyyy-MM-dd";
            Micultura.DateTimeFormat.FullDateTimePattern = "yyyy-MM-dd HH:mm:ss";
            Micultura.DateTimeFormat.TimeSeparator = ":";
            Micultura.DateTimeFormat.DateSeparator = "-";

            return Micultura;
            
        }


        #region sendMail 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emisor">pasara buscar el proveedor de correo</param>
        /// <param name="destinatario">direccion de correo</param>
        /// <param name="asunto">asunto de correo</param>
        /// <param name="mensaje">contenido del correo</param>
        /// <param name="alias">nombre para enmascar el correo</param>
        /// <param name="xml">XML para adjunto</param>
        /// <param name="consecutivo">numero de consecutivo</param>
        /// <param name="clave"></param>
        /// <returns>TRUE envaido FALSE no eviado</returns>
        public static bool sendMail(string emisor, string destinatario, string asunto, string mensaje, string alias, string xml, string consecutivo, string clave, List<string> cc)
        {
            try
            {
                using (var conexion = new DataModelFE())
                {
                    ConfiguracionCorreo mailConfig = conexion.ConfiguracionCorreo.Where(x => x.estado == Estado.ACTIVO.ToString() && x.codigo == emisor).FirstOrDefault();
                    if (mailConfig == null)
                    { 
                        mailConfig = conexion.ConfiguracionCorreo.Where(x => x.estado == Estado.ACTIVO.ToString() && x.codigo == Usuario.USUARIO_AUTOMATICO).FirstOrDefault();
                    }
                    if (mailConfig != null)
                    {
                        MailMessage correo = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        correo.From = new MailAddress(mailConfig.user, alias);

                        if (string.IsNullOrWhiteSpace(destinatario))
                        {
                            correo.To.Add(cc[0]);
                        }
                        else
                        {
                            correo.To.Add(destinatario);
                        }

                        if (cc != null)
                        {
                            foreach (var item in cc)
                            {
                                correo.CC.Add(item);
                            }
                        }
                         
                        //correo.Subject = String.Format("SPAM-LOW: {0}", asunto);
                        correo.Subject =  asunto;
                        correo.Body = mensaje;

                        if (xml != null)
                        {
                            correo.Attachments.Add(new Attachment(GenerateStreamFromMemoryStream(UtilidadesReporte.generarPDF(clave)), string.Format("{0}.pdf", consecutivo)));
                            correo.Attachments.Add(new Attachment(GenerateStreamFromString(xml), string.Format("{0}.xml", consecutivo)));
                        }
                        correo.Priority = MailPriority.Normal;
                        correo.IsBodyHtml = true;
                        smtp.Credentials = new NetworkCredential(mailConfig.user, Ale5Util.DesEncriptar(mailConfig.password));
                        smtp.Host = mailConfig.host;
                        smtp.Port = int.Parse(mailConfig.port);

                        if (Confirmacion.SI.ToString().Equals(mailConfig.ssl))
                        {
                            smtp.EnableSsl = true;
                        }
                        else
                        {
                            smtp.EnableSsl = false;
                        }

                        smtp.Send(correo);
                        correo.Dispose();
                    }
                    else
                    {
                        return false;
                    }
                } 
                return true;
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="emisor">pasara buscar el proveedor de correo</param>
        /// <param name="destinatario">direccion de correo</param>
        /// <param name="asunto">asunto de correo</param>
        /// <param name="mensaje">contenido del correo</param>
        /// <param name="alias">nombre para enmascar el correo</param> 
        /// <returns>TRUE envaido FALSE no eviado</returns>
        public static bool sendMail(string emisor, string destinatario, string asunto, string mensaje, string alias, List<string> cc)
        {
            try
            {
                using (var conexion = new DataModelFE())
                { 
                    ConfiguracionCorreo mailConfig = conexion.ConfiguracionCorreo.Where(x => x.estado == Estado.ACTIVO.ToString() && x.codigo==emisor).FirstOrDefault();
                    if (mailConfig == null)
                    { 
                        mailConfig = conexion.ConfiguracionCorreo.Where(x => x.estado == Estado.ACTIVO.ToString() && x.codigo == Usuario.USUARIO_AUTOMATICO).FirstOrDefault();
                    }
                    
                    MailMessage correo = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    correo.From = new MailAddress(mailConfig.user, alias);


                    if (string.IsNullOrWhiteSpace(destinatario))
                    {
                        correo.To.Add(cc[0]);
                    }
                    else
                    {
                        correo.To.Add(destinatario);
                    }

                    if (cc != null) {
                        foreach (var item in cc)
                        {
                            correo.CC.Add(item);
                        }
                    }
                    //correo.Subject = String.Format("SPAM-LOW: {0}", asunto);
                    correo.Subject =  asunto ;
                    correo.Body = mensaje;
                    correo.Priority = MailPriority.Normal;
                    correo.IsBodyHtml = true;
                    smtp.Credentials = new NetworkCredential(mailConfig.user, Ale5Util.DesEncriptar(mailConfig.password));
                    smtp.Host = mailConfig.host;
                    smtp.Port = int.Parse(mailConfig.port);

                    if (Confirmacion.SI.ToString().Equals(mailConfig.ssl))
                    {
                        smtp.EnableSsl = true;
                    }
                    else
                    {
                        smtp.EnableSsl = false;
                    }

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

        /// <summary>
        /// texto generico para el envio de correos
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// texto generico para el envio de correos
        /// </summary>
        /// <returns></returns>
        public static string mensageGenericoPruebaCorreo()
        {
            String mensaje = ""; 
            mensaje += "<p>**** Este mensaje se ha generado automáticamente.</p>";
            mensaje += "<p>**** Por Favor No conteste a este mensaje ya que no recibirá ninguna respuesta..</p>";

            return mensaje;
        }


        /// <summary>
        /// transforma un string a bytes en memoria
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// genera un Stream a partir de un MemoryStream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Stream GenerateStreamFromMemoryStream(MemoryStream stream)
        { 
            StreamWriter writer = new StreamWriter(stream); 
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        public static string getCorreoPrincipal(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
            {
                return null;
            }
            else
            {
               string [] lista = correo.Split(',');
                if (lista.Length == 0)
                    return correo;
                else
                    return lista[0];

            }
        }


        /// <summary>
        /// Genera una contraseña de valores de a-z A-Z  0-0 !@#$%*_+-/
        /// </summary>
        /// <param name="longitud">Cantidad de digitos que va a tener la contraseña</param>
        /// <returns></returns>
        public static string generarContrasena(int longitud)
        {
            const string valido = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%*_+-/";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(valido[rnd.Next(valido.Length)]);
            }
            return res.ToString();
        }
        
        
        /// <summary>
        /// verifica que existan los datos de la conexionn con hacienda
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        public static bool verificaDatosHacienda(EmisorReceptorIMEC dato)
        {
            bool valido = true;
            if (dato.llaveCriptografica == null)
            {
                valido = false;
            }
            if (string.IsNullOrWhiteSpace(dato.claveLlaveCriptografica))
            {
                valido = false;
            }
            if (string.IsNullOrWhiteSpace(dato.usernameOAuth2))
            {
                valido = false;
            }
            if (string.IsNullOrWhiteSpace(dato.passwordOAuth2))
            {
                valido = false;
            }
            return valido;
        }  
    }
}
