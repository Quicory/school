using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Data.DTOs;
using System.Data;

namespace School_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        protected APIResponse _resp;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
            _resp = new();
        }

        [HttpGet]
        public async Task<APIResponse> Get()
        {
            var uerlist = await Task.FromResult(new string[] { "Virat", "Messi", "Ozil", "Lara", "MS Dhoni" });
            _resp.Result= uerlist;
            _resp.StatusCode = System.Net.HttpStatusCode.OK;
            return _resp;
        }
    }
}
