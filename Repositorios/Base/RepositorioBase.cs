using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Declaraciones.Interfaces;
using Entidades;

namespace Repositorio.Base
{
    /// <summary>
    /// Clase de Repositorio Base, para implemetación con Entity Framework Core
    /// </summary>
    /// <typeparam name="T">Clase de Entidad del Modelo</typeparam>
    public abstract class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        protected ContextoBD ContextoBD { get; set; }

        public RepositorioBase(ContextoBD contextoBD)
        {
            ContextoBD = contextoBD;
        }

        protected RepositorioBase() { }

        /// <summary>
        /// Método que obtiene todos los datos de la entidad
        /// </summary>
        /// <returns>Objeto Consulta Linq</returns>
        public IQueryable<T> EncontrarTodos()
        {
            return this.ContextoBD.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Método que obtiene los datos de la entidad por medio de una condición
        /// </summary>
        /// <param name="expresion">Expresión de Condición</param>
        /// <returns>Objeto Consulta Linq</returns>
        public IQueryable<T> EncontrarPorCondicion(Expression<Func<T, bool>> expresion)
        {
            return this.ContextoBD.Set<T>().Where(expresion).AsNoTracking();
        }

        /// <summary>
        /// Método que obtiene los datos de la entidad por medio de una condición, con arrastre de clases
        /// </summary>
        /// <param name="expresion">Expresión de Condición</param>
        /// <returns>Objeto Consulta Linq</returns>
        public IQueryable<T> EncontrarPorCondicionArrastre(Expression<Func<T, bool>> expresion)
        {
            return this.ContextoBD.Set<T>().Where(expresion);
        }

        /// <summary>
        /// Método que permite insertar un objeto con los datos de la entidad
        /// </summary>
        /// <param name="entidad">Objeto de Entidad</param>
        public void Crear(T entidad)
        {
            this.ContextoBD.Set<T>().Add(entidad);
        }

        /// <summary>
        /// Método que permite actualizar los datos del objeto de entidad
        /// </summary>
        /// <param name="entidad">Objeto de Entidad</param>
        public void Actualizar(T entidad)
        {
            this.ContextoBD.Set<T>().Update(entidad);
        }

        /// <summary>
        /// Método que permite eliminar los datos de un objeto de entidad
        /// </summary>
        /// <param name="entidad">Objeto de Entidad</param>
        public void Eliminar(T entidad)
        {
            this.ContextoBD.Set<T>().Remove(entidad);
        }

        /// <summary>
        /// Método que permite obtener los datos utilizando una consulta de tipo nativa
        /// <example>
        /// Resumen: Es importante que la query este para metrizada de la siguiente manera
        /// <code>
        ///     "SELECT * FROM Tabla"
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="consultaNativa">Consulta Nativa de tipo String</param>
        /// <returns>Objeto Consulta Linq</returns>
        public IQueryable<T> EncontrarPorConsultaNativa(string consultaNativa)
        {
            var consultaParametros = this.ContextoBD.Set<T>().FromSqlRaw(consultaNativa);

            return consultaParametros;
        }

        /// <summary>
        /// Método que permite obtener los datos utilizando una consulta de tipo nativa
        /// <example>
        /// Resumen: Es importante que la query este para metrizada de la siguiente manera
        /// <code>
        ///     "SELECT * FROM Tabla  WHERE ParamTabla = {0} / ParamaTabla2 = {1}"
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="consultaNativa">Consulta Nativa de tipo String</param>
        /// <param name="parametros">Lista de parametros</param>
        /// <returns>Objeto Consulta Linq</returns>
        public IQueryable<T> EncontrarPorConsultaNativa(string consultaNativa, IList<string> parametros)
        {
            string[] cadenaParametros = parametros.Cast<string>().ToArray<string>();
            var consultaParametros = this.ContextoBD.Set<T>().FromSqlRaw(consultaNativa, cadenaParametros);

            return consultaParametros;
        }

        /// <summary>
        /// Método que permite obtener los datos utilizando una consulta de tipo nativa
        /// <example>
        /// Resumen: Es importante que la query este para metrizada de la siguiente manera:
        /// <code>
        /// "SELECT * FROM Tabla  WHERE ParamTabla = @paramTabla1 / ParamaTabla2 = @paramTabla2"
        /// </code>
        ///         El ingreso de cada elemento debe estar creado de la siguiente manera:
        ///     <code>
        ///         new SqlParameter("@paramTabla1", paramaTabla1) 
        ///     </code>
        /// </example>
        /// </summary>
        /// <param name="consultaNativa">Consulta Nativa de tipo String</param>
        /// <param name="parametros">Lista de parametros</param>
        /// <returns>Objeto Consulta Linq</returns>
        public IQueryable<T> EncontrarPorConsultaNativa(string consultaNativa, IList<SqlParameter> parametros)
        {
            SqlParameter[] cadenaParametros = parametros.Cast<SqlParameter>().ToArray();
            var consultaParametros = this.ContextoBD.Set<T>().FromSqlRaw(consultaNativa, cadenaParametros).AsNoTracking();

            return consultaParametros;
        }

        /// <summary>
        /// Método que permite obtener los datos utilizando una consulta de tipo nativa
        /// <example>
        /// Resumen: Es importante que la query este para metrizada de la siguiente manera:
        /// <code>
        ///     "SELECT * FROM Tabla  WHERE ParamTabla = {0} / ParamaTabla2 = {1}"
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="consultaNativa">Consulta Nativa de tipo String</param>
        /// <param name="parametros">Lista de parametros</param>
        /// <returns>Objeto Consulta Linq</returns>
        public IQueryable<T> EncontrarPorConsultaNativa(string consultaNativa, string[] parametros)
        {
            var consultaParametro = this.ContextoBD.Set<T>().FromSqlRaw(consultaNativa, parametros);

            return consultaParametro;
        }

        /// <summary>
        /// Método que permite obtener los datos utilizando una consulta de tipo nativa
        /// Resumen: Es importante que la query este para metrizada de la siguiente manera:
        /// <code>
        /// "SELECT * FROM Tabla  WHERE ParamTabla = @paramTabla1 / ParamaTabla2 = @paramTabla2"
        /// </code>
        ///         El ingreso de cada elemento debe estar creado de la siguiente manera:
        ///     <code>
        ///         new SqlParameter("@paramTabla1", paramaTabla1) 
        ///     </code>
        /// </example>
        /// </summary>
        /// <param name="consultaNativa">Consulta Nativa de tipo String</param>
        /// <param name="parametros">Lista de parametros</param>
        /// <returns>Objeto Consulta Linq</returns>
        public IQueryable<T> EncontrarPorConsultaNativa(string consultaNativa, SqlParameter[] parametros)
        {
            var consultaParametro = this.ContextoBD.Set<T>().FromSqlRaw(consultaNativa, parametros);

            return consultaParametro;
        }

        /// <summary>
        /// Método que permite grabar datos utilizando una consulta de tipo nativa
        /// <example>
        /// Resumen: Es importante que la query este para metrizada de la siguiente manera
        /// <code>
        ///     "SELECT * FROM Tabla"
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="consultaNativa">Consulta Nativa de tipo String</param>
        /// <param name="parametros">Lista de parametros</param>
        /// <returns>Objeto Consulta Linq</returns>
        public int GrabarPorConsultaNativa(string consultaNativa, SqlParameter[] parametros)
        {
            var consultaParametros = this.ContextoBD.Database.ExecuteSqlRaw(consultaNativa, parametros);

            return consultaParametros;
        }

        /// <summary>
        /// Método Asíncrono que permite guardar los cambios CRUD
        /// </summary>
        public async Task GuardarAsinc()
        {
            await this.ContextoBD.SaveChangesAsync();
        }
    }
}
