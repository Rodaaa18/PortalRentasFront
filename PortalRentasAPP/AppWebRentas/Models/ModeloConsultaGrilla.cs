using System;
using System.Collections.Generic;

namespace AppWebRentas.Models
{
    public class ModeloConsultaGrilla
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public ModeloBusquedaPorColumna search { get; set; }
        public List<ModeloOrderBy> order { get; set; }
        public List<ModeloColumna> columns { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }

    public enum ModeloDireccion
    {
        asc, desc
    }

    public class ModeloOrderBy
    {
        public int? column { get; set; }
        public ModeloDireccion dir { get; set; }
    }

    public class ModeloBusquedaPorColumna
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class ModeloColumna
    {
        public string data { get; set; }
        public string name { get; set; }
        public Boolean searchable { get; set; }
        public Boolean orderable { get; set; }
        public ModeloBusquedaPorColumna search { get; set; }
    }
}