using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.Utilidades.Paginado.Interfaces
{
    public interface IResultadoPaginadoBase
    {
        int PaginaActual { get; set; }
        int ContadorPagina { get; set; }
        int TamanoPagina { get; set; }
        int ContadorFilas { get; set; }

        int PrimeraFilaEnPagina { get; set; }

        int UltimaFilaEnPagina { get; set; }
    }
}
