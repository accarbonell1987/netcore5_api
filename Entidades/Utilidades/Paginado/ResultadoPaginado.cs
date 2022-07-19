using Entidades.Utilidades.Paginado.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.Utilidades.Paginado
{
    public class ResultadoPaginado<T> : ResultadoPaginadoBase, IResultadoPaginado<T> where T : class
    {
        public IEnumerable<T> Resultados { get; set; }

        public ResultadoPaginado()
        {
            this.Resultados = new List<T>();
        }
    }
}
