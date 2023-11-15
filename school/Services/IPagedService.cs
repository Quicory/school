using School_Data.DTOs;
using School_Data.Helpers;
using School_Data.Models;

namespace School_API.Services
{
    public interface IPagedService
    {
        Task<PagedResults> Sentence<T>(string query, PagingDTO paging, T obj) where T : class;
    }
}
