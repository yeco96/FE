using DevExpress.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;

public enum Estado
{
    ACTIVO,
    INACTIVO
} 
public enum Confirmacion
{
    SI,
    NO
}


public enum Sexo
{
    FEMENINO,
    MASCULINO
}


public enum EstadoMensajeHacienda { 

 
    [Display(Name = "ENVIADO")]
    ENVIADO = 0,

    [Display(Name = "ACEPTADO")]
    ACEPTADO = 1,

    [Display(Name = "ACEPTADO PARCIALMENTE")] 
    ACEPTADO_PARCIALMENTE = 2,

    [Display(Name = "RECHAZADO")]
    RECHAZADO = 3
}

public class EstadoMensajeHaciendaClass
{

    public static List<ListEditItem> values()
    { 
        List<ListEditItem> lista = new List<ListEditItem>();
        lista.Add(new ListEditItem("ENVIADO",0));
        lista.Add(new ListEditItem("ACEPTADO", 1));
        lista.Add(new ListEditItem("ACEPTADO PARCIALMENTE", 2));
        lista.Add(new ListEditItem("RECHAZADO", 3));

        return lista;
       
    }
     
}




