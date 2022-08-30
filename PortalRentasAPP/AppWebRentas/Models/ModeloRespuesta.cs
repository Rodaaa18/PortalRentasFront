using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppWebRentas.Models
{
    public class ModeloRespuesta
    {
        public object result { get; set; }
        public Value value { get; set; }
    }
    public class Value
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
}