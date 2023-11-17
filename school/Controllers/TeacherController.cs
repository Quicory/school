using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    //[Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserController> _logger;
        protected APIResponse _resp;
        private readonly IPagedService _paged;
        private readonly IMapper _mapper;
        public TeacherController(ILogger<UserController> logger, IPagedService paged, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _resp = new();
            _paged = paged;
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna los datos de los maestros en paginación.
        /// </summary>
        /// <param name="paging">Datos o propiedades para realizar la consulta</param>
        /// <returns>Retorna los datos de los maestros, si es exitoso o no.</returns>
        [HttpGet]
        public async Task<APIResponse> Get([FromQuery] PagingDTO paging)
        {
            // Search field
            paging.FilterFieldName = "Name";
            var query = @"SELECT *, Subjects = (SELECT S.* FROM Subjects S inner join SubjectTeacher ST 
						        on S.Id = ST.SubjectsId where ST.TeachersId = T.Id FOR JSON AUTO)
                            FROM Teachers T
                    {0}
                    {1}
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM Subjects
                    {0};
                    ";

            var obj = new TeacherDTO();
            var result = await _paged.Sentence<TeacherDTO>(query, paging, obj);

            if (result == null)
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

        /// <summary>
        /// Retorna los datos de la asignatura único
        /// </summary>
        /// <param name="Id">Identificación de la asignatura</param>
        /// <returns>Retorna los datos de la asignatura, si es exitoso o no.</returns>
        [HttpGet("id:int")]
        public async Task<APIResponse> Get(int Id)
        {
            if (Id <= 0)
            {
                _logger.LogError("El parametro no puede estar vacio.");

                _resp.IsValid = false;
                _resp.Message = "El parametro no puede estar vacio.";
                _resp.StatusCode = HttpStatusCode.BadRequest;
                return _resp;
            }

            var parameters = new DynamicParameters();
            var query = @"SELECT *, Subjects = (SELECT S.* FROM Subjects S inner join SubjectTeacher ST 
						        on S.Id = ST.SubjectsId where ST.TeachersId = T.Id FOR JSON AUTO)
                            FROM Teachers T where Id = @Id";

            parameters.Add("@Id", Id);
            var result = await _paged.SentenceUnique<TeacherDTO>(query, parameters);

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
                _resp.StatusCode = HttpStatusCode.OK;
            }

            return _resp;
        }
        /// <summary>
        /// Crear un maestro
        /// </summary>
        /// <param name="model">Datos del maestro</param>
        /// <returns>Retorno los datos del maestro</returns>
        [HttpPost]
        public async Task<APIResponse> Create([FromBody] TeacherCreateDTO model )
        {
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
                var obj = new Teacher();
                obj = _mapper.Map<Teacher>(model);
                
                //_context.Teachers.Add(obj);
                //foreach (var item in model.detail)
                //{
                //    _context
                //}


                await _context.SaveChangesAsync();

                _resp.Message = "Maestro creado.";
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
        /// Actualizar la asignatura.
        /// </summary>
        /// <param name="Id">Identificación</param>
        /// <param name="model">Datos a modificar</param>
        /// <returns>Retorno los datos modificados, si son correctos.</returns>
        [HttpPut("id")]
        public async Task<APIResponse> Update(int Id, [FromBody] SubjectCreateDTO model)
        {
            if (!ModelState.IsValid || model == null)
            {
                _resp.IsValid = false;
                _resp.Result = ModelState;
                _resp.Message = "Hubo un error o no hay datos en el resultado";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == Id);
            if (subject == null)
            {
                _resp.IsValid = false;
                _resp.Message = "No se ha encontrado la asignatura.";
                _resp.StatusCode = HttpStatusCode.NotFound;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            try
            {
                subject.Name = model.Name;
                subject.update_at = DateTime.Now;

                _context.Subjects.Update(subject);

                await _context.SaveChangesAsync();

                _resp.Message = "Asignatura actualizada.";
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
        /// <summary>
        /// Eliminación la asignatura.
        /// </summary>
        /// <param name="Id">Identificación</param>
        /// <returns>Retorno los datos eliminados, si son correctos.</returns>
        [HttpDelete("id")]
        public async Task<APIResponse> Delete(int Id)
        {
            if (Id <= 0)
            {
                _resp.IsValid = false;
                _resp.Message = "La identificación tiene que ser mayor a cero.";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == Id);
            if (subject == null)
            {
                _resp.IsValid = false;
                _resp.Message = "No se ha encontrado la asignatura.";
                _resp.StatusCode = HttpStatusCode.NotFound;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            try
            {
                _context.Subjects.Remove(subject);

                await _context.SaveChangesAsync();

                _resp.Message = "Asignatura eliminada.";
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
