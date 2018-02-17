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
    [Table("security_rol")]
    public class Rol  : RoleProvider
    {    
        public const String ADMINISTRADOR = "ADMIN"; 
        public const String FACTURADOR = "FACT"; 
     
        [Key]
        [Required]
        [MaxLength(5, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Código")]
        public string codigo { set; get; }
         
        [Required]
        [MaxLength(50, ErrorMessage = "La propiedad {0} no puede tener más de {1} elementos")]
        [Display(Name = "Descripcion")]
        public String descripcion { set; get; }
        
        [NotMapped]
        public String nameAPP { set; get; }

        /// <summary>
        /// AUDIOTORIA
        /// </summary>
        public string estado { set; get; }

        [ForeignKey("UsuarioCreacion")]
        public string usuarioCreacion { set; get; }

        public DateTime? fechaCreacion { set; get; }

        [ForeignKey("UsuarioModificacion")]
        public string usuarioModificacion { set; get; }

        public DateTime? fechaModificacion { set; get; }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Rol(string descripcion)
        {
            this.codigo = descripcion.Substring(1,5).ToUpper();
            this.descripcion = descripcion.ToUpper();
        }

        public Rol()
        { 
        }
        

        /// <summary>
        /// OBJETOS COMPUESTOS x
        /// </summary>
        public virtual Usuario UsuarioCreacion { get; set; }
        public virtual Usuario UsuarioModificacion { get; set; }

        [NotMapped]
        public override string ApplicationName
        {
            get
            {
                return nameAPP; 
            }

            set
            {
                nameAPP = value;
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var conexion = new DataModelFE())
            {
                Usuario usuario = conexion.Usuario.Where(x => x.rol == roleName && (x.codigo == username || x.nombre == username)).FirstOrDefault();
                if (usuario != null)
                    return true;
                else
                    return false;
            } 
        }

        public override string[] GetRolesForUser(string username)
        { 
            using (var conexion = new DataModelFE())
            {
                Usuario usuario = conexion.Usuario.Where(x=>x.codigo == username || x.nombre == username).FirstOrDefault();
                if (usuario != null) 
                    return new[] { usuario.rol };
                else
                    return new string[] { };
            }
        }

        public override void CreateRole(string roleName)
        {
            using (var conexion = new DataModelFE())
            { 
               conexion.Rol.Add(new Rol(roleName));
               conexion.SaveChanges();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            using (var conexion = new DataModelFE())
            {
                conexion.Rol.Remove(new Rol(roleName));
                conexion.SaveChanges();
                return true;
            }
        }

        public override bool RoleExists(string roleName)
        {
            using (var conexion = new DataModelFE())
            {
                Rol rol = conexion.Rol.Find(roleName);
                if (rol != null)
                    return true;
                else
                    return false;
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            using (var conexion = new DataModelFE())
            {
                string[] users = conexion.Usuario.Where(x => x.rol == roleName).Select(x => x.codigo).ToArray();

                if (users != null)
                    return users;
                else
                    return new string[] { };
            }
        }

        public override string[] GetAllRoles()
        {
            using (var conexion = new DataModelFE())
            { 
                string[] roles = conexion.Rol.Select(x=>x.codigo).ToArray();
                
                if (roles != null)
                    return roles;
                else
                    return new string[] { };
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            using (var conexion = new DataModelFE())
            {
                string[] users = conexion.Usuario.Where(x=>x.rol == roleName && (x.codigo == usernameToMatch || x.nombre == usernameToMatch)).Select(x => x.codigo).ToArray();

                if (users != null)
                    return users;
                else
                    return new string[] { };
            }
        }
    }
}
