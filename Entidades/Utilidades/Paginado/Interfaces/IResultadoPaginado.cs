using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.Utilidades.Paginado.Interfaces
{
    public interface IResultadoPaginado<T> : IResultadoPaginadoBase
    {
        IEnumerable<T> Resultados { get; set; }
    }
}
