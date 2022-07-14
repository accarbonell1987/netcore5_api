using Entidades.Utilidades.Paginado.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Utilidades.Paginado
{
    public abstract class ResultadoPaginadoBase : IResultadoPaginadoBase
    {
        public int PaginaActual { get; set; }
        public int ContadorPagina { get; set; }
        public int TamanoPagina { get; set; }
        public int ContadorFilas { get; set; }

        public int PrimeraFilaEnPagina
        {
            set => PrimeraFilaEnPagina = value;
            get => ((PaginaActual - 1) * TamanoPagina + 1);
        }

        public int UltimaFilaEnPagina
        {
            set => UltimaFilaEnPagina = value;
            get => Math.Min(PaginaActual * TamanoPagina, ContadorFilas);
        }
    }
}
