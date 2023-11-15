using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using School_API.Services;
using School_Data.DTOs;
using School_Data.Helpers;
using School_Data.Models;
using System.Data;
using System.Net;
using System.Net.NetworkInformation;

namespace School_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        protected APIResponse _resp;
        private readonly IPagedService _paged;
                
        public UserController(ILogger<UserController> logger, IPagedService paged)
        {
            _logger = logger;
            _resp = new();
            _paged = paged;
        }

        [HttpGet]
        public async Task<APIResponse> Get([FromQuery] PagingDTO paging)
        {
            // Search field
            paging.FilterFieldName = "CompleteName";
            var query = @"
                    SELECT * FROM AspNetUsers
                    {0}
                    {1}
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM AspNetUsers
                    {0};
                    ";

            var obj = new ApplicationUser();
            var result = await _paged.Sentence<ApplicationUser>(query, paging, obj);

            if(result == null)
            {
                _resp.IsValid = false;
                _resp.Message = "Hubo un error o no hay datos en el resultado.";
                _resp.StatusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                _resp.Result = result;
                _resp.Message = "Consulta realizada exitosamente.";
                _resp.StatusCode = HttpStatusCode.OK;
            }

            return _resp;
        }

        [HttpGet("id")]
        public async Task<APIResponse> Get(string Id)
        {
            if (Id.IsNullOrEmpty())
            {
                _logger.LogError("El parametro no puede estar vacio.");

                _resp.IsValid = false;
                _resp.Message = "El parametro no puede estar vacio.";
                _resp.StatusCode = HttpStatusCode.BadRequest;
                return _resp;
            }

            var parameters = new DynamicParameters();
            var query = @"SELECT * FROM AspNetUsers where Id = @Id";

            parameters.Add("@Id", Id);            
            var result = await _paged.SentenceUnique<ApplicationUser>(query, parameters);

            if (result == null)
            {
                _resp.IsValid = false;
                _resp.Message = "Hubo un error o no hay datos en el resultado";
                _resp.StatusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                _resp.Result = result;
                _resp.Message = "Consulta realizada exitosamente.";
                _resp.StatusCode =HttpStatusCode.OK;
            }

            return _resp;
        }
    }
}
