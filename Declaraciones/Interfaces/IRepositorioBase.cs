using System;
using System.Collections.Generic;
//using Microsoft.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Declaraciones.Interfaces
{
    /// <summary>
    /// Interface de Base para ser implementada con los métodos CRUD usados para las clases de entidades
    /// </summary>
    /// <typeparam name="T">Clase Parametro</typeparam>
    public interface IRepositorioBase<T>
    {
        /// <summary>
        /// Método de implementación que obtiene todos los datos de la entidad
        /// </summary>
        /// <returns>Objeto Consulta Linq</returns>
        IQueryable<T> EncontrarTodos();

        /// <summary>
        /// Método de implementación que obtiene los datos de la entidad por medio de una condición
        /// </summary>
        /// <param name="expresion">Expresión de Condición</param>
        /// <returns>Objeto Consulta Linq</returns>
        IQueryable<T> EncontrarPorCondicion(Expression<Func<T, bool>> expresion);

        /// <summary>
        /// Método de implementación que obtiene los datos de la entidad por medio de una condición, con arrastre de clases
        /// </summary>
        /// <param name="expresion">Expresión de Condición</param>
        /// <returns>Objeto Consulta Linq</returns>
        IQueryable<T> EncontrarPorCondicionArrastre(Expression<Func<T, bool>> expresion);

        /// <summary>
        /// Método de implementación que permite insertar un objeto con los datos de la entidad
        /// </summary>
        /// <param name="entidad">Objeto de Entidad</param>
        void Crear(T entidad);

        /// <summary>
        /// Método de implementación que permite actualizar los datos del objeto de entidad
        /// </summary>
        /// <param name="entidad">Objeto de Entidad</param>
        void Actualizar(T entidad);

        /// <summary>
        /// Método de implementación que permite eliminar los datos de un objeto de entidad
        /// </summary>
        /// <param name="entidad">Objeto de Entidad</param>
        void Eliminar(T entidad);

        /// <summary>
        /// Método de implementación que permite obtener los datos utilizando una consulta de tipo nativa
        /// <example>
        /// Resumen: Es importante que la query este para metrizada de la siguiente manera
        /// <code>
        ///     "SELECT * FROM Tabla"
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="consultaNativa">Consulta Nativa de tipo String</param>
        /// <returns>Objeto Consulta Linq</returns>
        IQueryable<T> EncontrarPorConsultaNativa(string consultaNativa);

        /// <summary>
        /// Método de implementación que permite obtener los datos utilizando una consulta de tipo nativa
        /// </summary>
        /// <param name="consultaNativa">Consulta Nativa de tipo String</param>
        /// <param name="parametros">Lista de parametros</param>
        /// <returns>Objeto Consulta Linq</returns>
        IQueryable<T> EncontrarPorConsultaNativa(string consultaNativa, IList<string> parametros);

        /// <summary>
        /// Método de implementación que permite obtener los datos utilizando una consulta de tipo nativa
        /// </summary>
        /// <param name="consultaNativa">Consulta Nativa de tipo String</param>
        /// <param name="parametros">Lista de parametros</param>
        /// <returns>Objeto Consulta Linq</returns>
        //IQueryable<T> EncontrarPorConsultaNativa(string consultaNativa, IList<SqlParameter> parametros);

        /// <summary>
        /// Método de implementación que permite obtener los datos utilizando una consulta de tipo nativa
        /// </summary>
        /// <param name="consultaNativa">Consulta Nativa de tipo String</param>
        /// <param name="parametros">Lista de parametros</param>
        /// <returns>Objeto Consulta Linq</returns>
        IQueryable<T> EncontrarPorConsultaNativa(string consultaNativa, string[] parametros);

        /// <summary>
        /// Método de implementación que permite obtener los datos utilizando una consulta de tipo nativa
        /// </summary>
        /// <param name="consultaNativa">Consulta Nativa de tipo String</param>
        /// <param name="parametros">Lista de parametros</param>
        /// <returns>Objeto Consulta Linq</returns>
        //IQueryable<T> EncontrarPorConsultaNativa(string consultaNativa, SqlParameter[] parametros);

        /// <summary>
        /// Método Asíncrono de implementación que permite guardar los cambios CRUD
        /// </summary>
        Task GuardarAsinc();
    }
}
