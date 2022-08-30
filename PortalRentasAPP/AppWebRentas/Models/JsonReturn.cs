using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebRentas.Models
{
    public class JsonReturn
    {
        //0 - Todo ok. 
        //1 - Error.
        //2 - Error (redireccionar)
        //3 - Alerta
        //4 - Info

        public short TipoError
        {
            get;
            set;
        }

        public bool Success
        {
            get;
            set;
        }

        public string MensajeError
        {
            get;
            set;
        }

        public object InnerObject
        {
            get;
            set;
        }

        public long TotalCount
        {
            get;
            set;
        }

        public string UrlImagen
        {
            get;
            set;
        }

        public static JsonReturn SuccessSinRetorno()
        {
            return new JsonReturn
            {
                InnerObject = null,
                MensajeError = "",
                Success = true,
                TipoError = 0,
                TotalCount = 0,
                UrlImagen = ""
            };
        }

        public static JsonReturn SuccessConRetorno(object obj)
        {
            obj.ToString();
            return new JsonReturn
            {
                InnerObject = obj,
                MensajeError = "",
                Success = true,
                TipoError = 0,
                TotalCount = 0,
                UrlImagen = ""
            };
        }

        public static JsonReturn ErrorConMsjSimple(AppWebRentas.Util.ModeloMensaje modeloMensaje)
        {
            return new JsonReturn
            {
                InnerObject = null,
                MensajeError = modeloMensaje != null ? modeloMensaje.Mensaje : "Se generó un error. Disculpe las molestias",
                Success = false,
                TipoError = 1,
                TotalCount = 0,
                UrlImagen = modeloMensaje != null ? modeloMensaje.UrlImagen : "ImagenesMensaje/error.png"
            };
        }

        public static JsonReturn AlertaConMsjSimple(AppWebRentas.Util.ModeloMensaje modeloMensaje)
        {
            return new JsonReturn
            {
                InnerObject = null,
                MensajeError = modeloMensaje != null ? modeloMensaje.Mensaje : "",
                Success = false,
                TipoError = 3,
                TotalCount = 0,
                UrlImagen = modeloMensaje != null ? modeloMensaje.UrlImagen : "ImagenesMensaje/alerta.png"
            };
        }

        public static JsonReturn InfoConMsjSimple(AppWebRentas.Util.ModeloMensaje modeloMensaje)
        {
            return new JsonReturn
            {
                InnerObject = null,
                MensajeError = modeloMensaje != null ? modeloMensaje.Mensaje : "",
                Success = false,
                TipoError = 4,
                TotalCount = 0,
                UrlImagen = modeloMensaje != null ? modeloMensaje.UrlImagen : "ImagenesMensaje/info.png"
            };
        }

        public static JsonReturn Redireccionar(string url)
        {
            return new JsonReturn
            {
                InnerObject = null,
                MensajeError = url,
                Success = false,
                TipoError = 2,
                TotalCount = 0
            };
        }

        public static JsonReturn RedireccionarIndex()
        {
            return new JsonReturn
            {
                InnerObject = null,
                MensajeError = "",
                Success = false,
                TipoError = 2,
                TotalCount = 0
            };
        }
    }
}