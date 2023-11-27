using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [Produces("application/json")]
    
    public class ClassroomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserController> _logger;
        protected APIResponse _resp;
        private readonly IPagedService _paged;
        private readonly IMapper _mapper;
        public ClassroomController(ILogger<UserController> logger, IPagedService paged, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _resp = new();
            _paged = paged;
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna los datos de las aulas en paginación.
        /// </summary>
        /// <param name="paging">Datos o propiedades para realizar la consulta</param>
        /// <returns>Retorna los datos de las aulas, si es exitoso o no.</returns>
        [HttpGet]
        public async Task<APIResponse> Get([FromQuery] PagingDTO paging)
        {
            _logger.LogInformation("Ejecutando paginación aulas");

            // Search field
            paging.FilterFieldName = "Name";
            var query = @"
                    SELECT * FROM Classrooms
                    {0}
                    {1}
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM Classrooms
                    {0};
                    ";

            var obj = new ClassroomDTO();
            var result = await _paged.Sentence<ClassroomDTO>(query, paging, obj);

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
        /// Retorna los datos del aula único
        /// </summary>
        /// <param name="Id">Identificación del aula</param>
        /// <returns>Retorna los datos del aula, si es exitoso o no.</returns>
        [HttpGet("{id:int}")]
        public async Task<APIResponse> Get(int Id)
        {
            _logger.LogInformation("Ejecutando aula por ID.");

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
            var query = @"SELECT * FROM Classrooms where Id = @Id";

            parameters.Add("@Id", Id);
            var result = await _paged.SentenceUnique<ClassroomDTO>(query, parameters);

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
        /// Crear una aula
        /// </summary>
        /// <param name="model">Datos del aula</param>
        /// <returns>Retorno los datos del aula</returns>
        [HttpPost]
        public async Task<APIResponse> Create([FromBody] ClassroomCreateDTO model)
        {
            _logger.LogInformation("Creando aula.");

            if (!ModelState.IsValid || model == null)
            {
                _resp.IsValid = false;
                _resp.Result = ModelState;
                _resp.Message = "Hubo un error o no hay datos en el resultado";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            try
            {
                var obj = new Classroom();
                obj = _mapper.Map<Classroom>(model);

                _context.Classrooms.Add(obj);

                await _context.SaveChangesAsync();

                _resp.Message = "Aula creada.";
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
        /// Actualizar el aula.
        /// </summary>
        /// <param name="Id">Identificación</param>
        /// <param name="model">Datos a modificar</param>
        /// <returns>Retorno los datos modificados, si son correctos.</returns>
        [HttpPut("{id:int}")]
        public async Task<APIResponse> Update(int Id, [FromBody] ClassroomUpdateDTO model)
        {
            _logger.LogInformation("Actualizando aula.");

            if (!ModelState.IsValid || model == null)
            {
                _resp.IsValid = false;
                _resp.Result = ModelState;
                _resp.Message = "Hubo un error o no hay datos en el resultado";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);

                return _resp;
            }
            
            var obj_search = await _context.Classrooms.FirstOrDefaultAsync(x => x.Id == Id);
            if (obj_search == null)
            {
                _resp.IsValid = false;
                _resp.Message = "No se ha encontrado el aula.";
                _resp.StatusCode = HttpStatusCode.NotFound;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            try
            {
                obj_search = _mapper.Map(model, obj_search);
                obj_search.update_at = DateTime.Now;

                _context.Classrooms.Update(obj_search);

                await _context.SaveChangesAsync();

                _resp.Message = "Aula actualizado.";
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
        /// Eliminación del aula.
        /// </summary>
        /// <param name="Id">Identificación</param>
        /// <returns>Retorno los datos eliminados, si son correctos.</returns>
        [HttpDelete("{id:int}")]
        //[Authorize(Roles = "Admin")]
        public async Task<APIResponse> Delete(int Id)
        {
            _logger.LogInformation("Eliminando aula.");

            if (Id <= 0)
            {
                _resp.IsValid = false;
                _resp.Message = "La identificación tiene que ser mayor a cero.";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            var subject = await _context.Classrooms.FirstOrDefaultAsync(x => x.Id == Id);
            if (subject == null)
            {
                _resp.IsValid = false;
                _resp.Message = "No se ha encontrado el aula.";
                _resp.StatusCode = HttpStatusCode.NotFound;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            try
            {
                _context.Classrooms.Remove(subject);

                await _context.SaveChangesAsync();

                _resp.Message = "Aula eliminado.";
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
