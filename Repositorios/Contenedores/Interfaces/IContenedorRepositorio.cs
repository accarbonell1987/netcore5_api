using Declaraciones.Repositorios;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Repositorio.Contenedores.Interfaces
{
    public interface IContenedorRepositorio
    {
        // Agregar Mas Repositorios de Entidades
        IUsuarioRepositorio Usuario { get; }
        IBugRepositorio Bug { get; }
        IProyectoRepositorio Proyecto { get; }

        void Guardar();

        Task GuardarAsinc();

        IDbContextTransaction IniciarTransaccion();
        Task<IDbContextTransaction> IniciarTransaccionAsinc();
    }
}
