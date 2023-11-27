using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using School_API.Services;
using School_Data.DTOs;
using School_Data.Helpers;
using School_Data.Models;
using System.Net;

namespace School_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserController> _logger;
        protected APIResponse _resp;
        private readonly IPagedService _paged;
        private readonly IMapper _mapper;
        public StudentController(ILogger<UserController> logger, IPagedService paged, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _resp = new();
            _paged = paged;
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna los datos de los estadiantes en paginación.
        /// </summary>
        /// <param name="paging">Datos o propiedades para realizar la consulta</param>
        /// <returns>Retorna los datos de los estadiantes, si es exitoso o no.</returns>
        [HttpGet]
        public async Task<APIResponse> Get([FromQuery] PagingDTO paging)
        {
            _logger.LogInformation("Ejecutando paginación estudiantes");

            // Search field
            if (!paging.Filter.IsNullOrEmpty())
            {
                if (paging.FilterFieldName.IsNullOrEmpty())
                {
                    paging.FilterFieldName = "FirstName,LastName";
                }
                else if (!(paging.FilterFieldName.ToLower() == "firstname" || paging.FilterFieldName.ToLower() == "lastname"))
                {
                    _resp.IsValid = false;
                    _resp.Message = "No puede usar campo diferentes a Nombre o Apellido.";
                    _resp.StatusCode = HttpStatusCode.BadRequest;

                    _logger.LogError(_resp.Message);

                    return _resp;
                }
            }

            var query = @"
                    SELECT * FROM Students
                    {0}
                    {1}
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM Students
                    {0};
                    ";

            var obj = new StudentDTO();
            var result = await _paged.Sentence<StudentDTO>(query, paging, obj);

            if (result == null)
            {
                _resp.IsValid = false;
                _resp.Message = "Hubo un error o no hay datos en el resultado.";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);

            }
            else
            {
                _resp.Result = result;
                _resp.Message = "Consulta realizada exitosamente.";
                _resp.StatusCode = HttpStatusCode.OK;

                _logger.LogInformation(_resp.Message);
            }

            return _resp;
        }

        /// <summary>
        /// Retorna los datos del estudiante único
        /// </summary>
        /// <param name="Id">Identificación del estudiante</param>
        /// <returns>Retorna los datos del estudiante, si es exitoso o no.</returns>
        [HttpGet("{id:int}")]
        public async Task<APIResponse> Get(int Id)
        {
            _logger.LogInformation("Ejecutando estudiante por ID.");

            if (Id <= 0)
            {
                _logger.LogError("El parametro no puede estar vacio.");

                _resp.IsValid = false;
                _resp.Message = "El parametro no puede estar vacio.";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);
                return _resp;
            }

            var parameters = new DynamicParameters();
            var query = @"SELECT * FROM Students where Id = @Id";

            parameters.Add("@Id", Id);
            var result = await _paged.SentenceUnique<StudentDTO>(query, parameters);

            if (result == null)
            {
                _resp.IsValid = false;
                _resp.Message = "Hubo un error o no hay datos en el resultado";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);
            }
            else
            {
                _resp.Result = result;
                _resp.Message = "Consulta realizada exitosamente.";
                _resp.StatusCode = HttpStatusCode.OK;

                _logger.LogInformation(_resp.Message);
            }

            return _resp;
        }
        /// <summary>
        /// Crear un estudiante
        /// </summary>
        /// <param name="model">Datos del estudiante</param>
        /// <returns>Retorno los datos del estudiante</returns>
        [HttpPost]
        public async Task<APIResponse> Create([FromBody] StudentCreateDTO model)
        {
            _logger.LogInformation("Creando estudiante.");

            if (!ModelState.IsValid || model == null)
            {
                _resp.IsValid = false;
                _resp.Result = ModelState;
                _resp.Message = "Hubo un error o no hay datos en el resultado";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            var obj_search = await _context.Students.FirstOrDefaultAsync(x => x.IDNumber == model.IDNumber);
            if (obj_search != null)
            {
                _resp.IsValid = false;
                _resp.Message = "La Matrícula del estudiante ya existe.";
                _resp.StatusCode = HttpStatusCode.Conflict;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            try
            {
                var obj = new Student();
                obj = _mapper.Map<Student>(model);

                _context.Students.Add(obj);

                await _context.SaveChangesAsync();

                _resp.Message = "Estudiante creada.";
                _resp.Result = obj;
                _resp.StatusCode = HttpStatusCode.Created;

                _logger.LogInformation(_resp.Message);
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
        /// <summary>
        /// Actualizar el estudiante.
        /// </summary>
        /// <param name="Id">Identificación</param>
        /// <param name="model">Datos a modificar</param>
        /// <returns>Retorno los datos modificados, si son correctos.</returns>
        [HttpPut("{id:int}")]
        public async Task<APIResponse> Update(int Id, [FromBody] StudentCreateDTO model)
        {
            _logger.LogInformation("Actualizando estudiante.");

            if (!ModelState.IsValid || model == null)
            {
                _resp.IsValid = false;
                _resp.Result = ModelState;
                _resp.Message = "Hubo un error o no hay datos en el resultado";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            var obj_search = await _context.Students.FirstOrDefaultAsync(x => x.IDNumber == model.IDNumber && x.Id != Id);
            if (obj_search != null)
            {
                _resp.IsValid = false;
                _resp.Message = "La Matrícula del estudiante ya existe.";
                _resp.StatusCode = HttpStatusCode.Conflict;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            obj_search = await _context.Students.FirstOrDefaultAsync(x => x.Id == Id);
            if (obj_search == null)
            {
                _resp.IsValid = false;
                _resp.Message = "No se ha encontrado el estudiante.";
                _resp.StatusCode = HttpStatusCode.NotFound;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            try
            {
                obj_search = _mapper.Map(model, obj_search);
                obj_search.update_at = DateTime.Now;

                _context.Students.Update(obj_search);

                await _context.SaveChangesAsync();

                _resp.Message = "Estudiante actualizado.";
                _resp.Result = obj_search;
                _resp.StatusCode = HttpStatusCode.OK;

                _logger.LogInformation(_resp.Message);
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
        /// <summary>
        /// Eliminación del estudiante.
        /// </summary>
        /// <param name="Id">Identificación</param>
        /// <returns>Retorno los datos eliminados, si son correctos.</returns>
        [HttpDelete("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public async Task<APIResponse> Delete(int Id)
        {
            _logger.LogInformation("Eliminando estudiante.");

            if (Id <= 0)
            {
                _resp.IsValid = false;
                _resp.Message = "La identificación tiene que ser mayor a cero.";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            var subject = await _context.Students.FirstOrDefaultAsync(x => x.Id == Id);
            if (subject == null)
            {
                _resp.IsValid = false;
                _resp.Message = "No se ha encontrado el estudiante.";
                _resp.StatusCode = HttpStatusCode.NotFound;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            try
            {
                _context.Students.Remove(subject);

                await _context.SaveChangesAsync();

                _resp.Message = "Estudiante eliminado.";
                _resp.Result = subject;
                _resp.StatusCode = HttpStatusCode.OK;

                _logger.LogInformation(_resp.Message);
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
