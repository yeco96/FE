using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Web.Models;

namespace Class.Seguridad
{
    [Table("security_user")]
    public class Usuario : MembershipProvider
    {
        [NotMapped]
        public static string USUARIO_AUTOMATICO = "123456789";
        [NotMapped]
        public static string USUARIO_TOKEN = "603540974";

        [Key]
        [Required]
        [MaxLength(12, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Código")]
        public string codigo { set; get; }


        [Required]
        [MaxLength(100, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Nombre")]
        public String nombre { set; get; }

        [Required]
        [MaxLength(100, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Contraseña")]
        public String contrasena { set; get; }

        [Required]
        [Display(Name = "Intentos")]
        public int intentos { set; get; }

        [Required]
        [MaxLength(5, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Rol")]
        [ForeignKey("Rol")]
        public string rol { set; get; }

        [Required]
        [MaxLength(12, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Emisor")]
        public string emisor { set; get; }



        /// <summary>
        /// AUDIOTORIA
        /// </summary>
        public string estado { set; get; }


        public string usuarioCreacion { set; get; }

        public DateTime? fechaCreacion { set; get; }


        public string usuarioModificacion { set; get; }

        public DateTime? fechaModificacion { set; get; }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Usuario()
        {
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.nombre, this.codigo);
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            using (var conexion = new DataModelFE())
            {
                Usuario usuario = conexion.Usuario.Where(x => x.codigo == username && x.contrasena == password).FirstOrDefault();
                if (usuario != null)
                    return true;
                else
                    return false;
            }
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// OBJETOS COMPUESTOS x
        /// </summary>
        public virtual Rol Rol { get; set; }

        [NotMapped]
        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [NotMapped]
        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [NotMapped]
        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [NotMapped]
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        [NotMapped]
        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [NotMapped]
        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [NotMapped]
        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [NotMapped]
        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [NotMapped]
        public override int MinRequiredPasswordLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [NotMapped]
        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [NotMapped]
        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
