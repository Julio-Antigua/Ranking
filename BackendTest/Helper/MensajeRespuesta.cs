using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTest.Helper
{
    public class MensajeRespuesta
    {
        public static string RespuestaOk => "La Solicitud Se Realizo Correctamente";
        public static string RespuestaBadRequest => "Se Produjo Un Error Al Realizar La Solicitud";
        public static string RespuestaPost => "Solicitud Creada Correctamente";
        public static string RespuestaPostError => "Ocurrio un error al intentar guardar la solicitud";
        public static string RespuestaPut => "Solicitud Actualizada Correctamente";
        public static string RespuestaPutError => "Ocurrio un error al intentar actualizar la solicitud";
        public static string RespuestaDelete => "Solicitud Eliminada Correctamente";
        public static string RespuestaDeleteError => "Ocurrio un error al intentar eliminar la solicitud";
        public static string Me_Gusta => "Me Gusta";
        public static string No_Me_Gusta => "No Me Gusta";


    }
}
