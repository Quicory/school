using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_API.Services;
using School_Data.Helpers;
using School_Data.Models;
using System.Data;
using System.Net;

namespace School_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
                
        protected APIResponse _resp;
        public AuthController(IAuthService authService, ILogger<AuthController> logger, IMapper mapper)
        {
            _authService = authService;
            _logger = logger;
            _mapper = mapper;
            _resp = new();
        }

        /// <summary>
        /// Sirve para acceder al API.
        /// </summary>
        /// <param name="model">Datos para acceder</param>
        /// <returns>Retorno de datos sobre acceso al API.</returns>
        [HttpPost]
        [Route("login")]
        public async Task<APIResponse> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Error en algunos campos.");

                    _resp.IsValid = false;
                    _resp.Message = "Error en algunos campos.";
                    _resp.StatusCode = HttpStatusCode.BadRequest;
                    return _resp;
                }

                _logger.LogInformation("Enviando respuesta correcta desde login.");
                return await _authService.Login(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _resp.IsValid = false;
                _resp.Message = "Error interno sistema.";
                _resp.StatusCode = (HttpStatusCode)StatusCodes.Status500InternalServerError;
                _resp.ErrorMessages = new List<string> { ex.Message };
                return _resp;
            }
        }

        /// <summary>
        /// Silve para registrar el usuario.
        /// </summary>
        /// <param name="model">Datos para registrarse</param>
        /// <returns>Respuesta sobre datos que son aceptados o rechazados</returns>
        [HttpPost]
        [Route("registeration")]
        [Authorize(Roles = "Admin")]
        public async Task<APIResponse> Register(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Error en algunos campos.");

                    _resp.IsValid = false;
                    _resp.Message = "Error en algunos campos.";
                    _resp.StatusCode = HttpStatusCode.BadRequest;
                    return _resp;
                }

                 _resp = await _authService.Registeration(model, UserRoles.Admin);
                //if (status == 0)
                //{
                //    return BadRequest(message);
                //}
                //return CreatedAtAction(nameof(Register), model);
                _logger.LogInformation("Enviando respuesta desde Register.");
                return _resp;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _resp.IsValid = false;
                _resp.Message = "Error interno sistema.";
                _resp.StatusCode = (HttpStatusCode)StatusCodes.Status500InternalServerError;
                _resp.ErrorMessages = new List<string> { ex.Message };
                return _resp;
            }
        }
    }
}
