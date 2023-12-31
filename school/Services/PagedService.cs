﻿using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using School_Data.DTOs;
using School_Data.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Drawing.Printing;
using System.Data;

namespace School_API.Services
{
    public class PagedService : IPagedService
    {        
        private readonly string _connectionString;

        public PagedService(IConfiguration config)
        {     
            _connectionString = config.GetConnectionString("SchoolConnetion");
        }
        /// <summary>
        /// Sirve para retornar datos de la clase, con paginación y total de registros.
        /// </summary>
        /// <typeparam name="T">Clase de datos a generar</typeparam>
        /// <param name="query">Sentencia o instrucciones para generar los datos.</param>
        /// <param name="paging">Datos o propiedades para realizar la consulta</param>
        /// <param name="obj">clase para retornar y buscar datos</param>
        /// <returns>Datos devueltos de la consulta y el total</returns>
        public async Task<PagedResults> Sentence<T>(string query, PagingDTO paging, T obj) where T : class 
        {
            
            string orderby = " order by 1";
            if (!paging.FieldOrder.IsNullOrEmpty())
            {
                // Verify if in class
                if(HasProperty(paging.FieldOrder, obj))
                {
                    orderby = " order by " + paging.FieldOrder + (paging.IsAsc ? " asc" : " desc");
                }
            }

            var parameters = new DynamicParameters();
            string where = "";
            if (!paging.Filter.IsNullOrEmpty())
            {
                string[] field = paging.FilterFieldName.Split(",");
                
                foreach (var item in field)
                {                    
                    if (HasProperty(item, obj))
                    {
                        parameters.Add("@" + item, "%" + paging.Filter + "%");
                        if(where.IsNullOrEmpty())
                            where = " where " + item + " Like @" + item;
                        else
                            where += " or " + item + " Like @" + item;

                    }
                }
            }

            string sent = String.Format(query, where, orderby);

            var results = new PagedResults();

            parameters.Add("@Offset", (paging.Page - 1) * paging.PageSize);
            parameters.Add("@PageSize", paging.PageSize);             

            using (var conn = new SqlConnection(_connectionString))
            {
                var multi = await conn.QueryMultipleAsync(sent, parameters);

                results.Items = await multi.ReadAsync<T>();
                results.TotalCount = multi.ReadFirst<int>();
              
            }

            return results;
        }
        /// <summary>
        /// Sirve para retornar datos único de la consulta a realizar.
        /// </summary>
        /// <typeparam name="T">Clase de datos a generar</typeparam>
        /// <param name="query">>Sentencia o instrucciones para generar los datos.</param>
        /// <param name="parameters">Parametros de la consulta a realizar</param>
        /// <returns>Datos devueltos de la consulta</returns>
        public async Task<T> SentenceUnique<T>(string query, DynamicParameters parameters) where T : class
        { 
            using (var conn = new SqlConnection(_connectionString))
            {                
                var result = await conn.QueryFirstOrDefaultAsync<T>(query, parameters);

                return result;
            };

        }
        /// <summary>
        /// Utilizado para buscar si hay una propiedad dentro de la clase
        /// </summary>
        /// <typeparam name="T">Clase de datos a buscar</typeparam>
        /// <param name="FieldName">Nombre del campo a buscar</param>
        /// <param name="obj">Misma clase donde esta la T generico</param>
        /// <returns>Devuelve verdadero o falso, si lo encuentra o no, la propiedad</returns>
        private bool HasProperty<T>(string FieldName, T obj) where T : class 
        {
            foreach (var item in obj.GetType().GetProperties())
            {
                if (item.Name.ToLower() == FieldName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
