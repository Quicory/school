using Dapper;
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
                if (HasProperty(paging.FilterFieldName, obj))
                {
                    parameters.Add("@" + paging.FilterFieldName, "%" + paging.Filter + "%" );
                    where = " where " + paging.FilterFieldName + " Like @" + paging.FilterFieldName;
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

        public async Task<T> SentenceUnique<T>(string query, DynamicParameters parameters) where T : class
        { 
            using (var conn = new SqlConnection(_connectionString))
            {                
                var result = await conn.QueryFirstOrDefaultAsync<T>(query, parameters);

                return result;
            };

        }

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
