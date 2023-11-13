﻿using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        public async Task<APIResponse> Get()
        {
            var uerlist = await Task.FromResult(new string[] { "Virat", "Messi", "Ozil", "Lara", "MS Dhoni" });
            return Ok(uerlist);
        }
    }
}
