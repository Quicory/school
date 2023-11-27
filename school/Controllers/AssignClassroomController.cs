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
    [Produces("application/json")]    
    [Authorize]
    public class AssignClassroomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserController> _logger;
        protected APIResponse _resp;
        private readonly IPagedService _paged;
        private readonly IMapper _mapper;
        private readonly IConvertService _convert;

        public AssignClassroomController(ILogger<UserController> logger, IPagedService paged, ApplicationDbContext context, IMapper mapper, IConvertService convert)
        {
            _logger = logger;
            _resp = new();
            _paged = paged;
            _context = context;
            _mapper = mapper;
            _convert = convert;
        }

        /// <summary>
        /// Retorna los datos de los maestros con sus asignaciones en paginación.
        /// </summary>
        /// <param name="paging">Datos o propiedades para realizar la consulta</param>
        /// <returns>Retorna los datos de los maestros, si es exitoso o no.</returns>
        [HttpGet]
        public async Task<APIResponse> GetAll([FromQuery] PagingDTO paging)
        {
            _logger.LogInformation("Ejecutando paginación asignaciones de aulas a maestros.");

            // Search field
            if (!(paging.Filter == null))
            {
                if (paging.FilterFieldName == null)
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

            var query = @"SELECT *, Subjects = (SELECT S.* FROM Subjects S inner join TeachersSubjects ST 
						                            on S.Id = ST.SubjectId where ST.TeacherId = T.Id FOR JSON AUTO)
                            FROM Teachers T
                    {0}
                    {1}
                    OFFSET @Offset ROWS
                    FETCH NEXT @PageSize ROWS ONLY;

                    SELECT COUNT(*)
                    FROM Teachers
                    {0};
                    ";

            var obj = new TeacherDTO();
            var result = await _paged.Sentence<TeacherDTO>(query, paging, obj);

            if (result == null)
            {
                _resp.IsValid = false;
                _resp.Message = "Hubo un error o no hay datos en el resultado.";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);
            }
            else
            {
                result.Items = (from l in (IEnumerable<TeacherDTO>)result.Items
                                select new
                                {
                                    id = l.Id,
                                    firstname = l.FirstName,
                                    lastname = l.LastName,
                                    email = l.Email,
                                    profession = l.Profession,
                                    create_at = l.create_at,
                                    update_at = l.update_at,
                                    subjects = _convert.StringToJson<SubjectDTO>(l.Subjects)
                                }).ToList();

                _resp.Result = result;
                _resp.Message = "Consulta realizada exitosamente.";
                _resp.StatusCode = HttpStatusCode.OK;
                _logger.LogError(_resp.Message);
            }

            _logger.LogInformation(_resp.Message);
            return _resp;
        }

        /// <summary>
        /// Retorna los datos del maestro con sus aulas asignadas.
        /// </summary>
        /// <param name="Id">Identificación del maestro</param>
        /// <returns>Retorna los datos del maestro con sus asignaciones, si es exitoso o no.</returns>
        [HttpGet("{id:int}")]
        public async Task<APIResponse> Get(int Id)
        {
            _logger.LogInformation("Ejecutando maestros con aulas por ID.");

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
            var query = @"SELECT *, Classrooms = (SELECT C.* FROM Classrooms C inner join TeachersClassrooms TC
						                            on C.Id = TC.ClassroomId where TC.TeacherId = T.Id FOR JSON AUTO)
                            FROM Teachers T where Id = @Id";

            parameters.Add("@Id", Id);
            var result = await _paged.SentenceUnique<TeacherClassroomDTO>(query, parameters);

            if (result == null)
            {
                _resp.IsValid = false;
                _resp.Message = "Hubo un error o no hay datos en el resultado";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);
            }
            else
            {
                _resp.Result = new
                {
                    id = result.Id,
                    firstname = result.FirstName,
                    lastname = result.LastName,
                    email = result.Email,
                    profession = result.Profession,
                    create_at = result.create_at,
                    update_at = result.update_at,
                    classrooms = _convert.StringToJson<ClassroomDTO>(result.Classrooms)
                };
                _resp.Message = "Consulta realizada exitosamente.";
                _resp.StatusCode = HttpStatusCode.OK;

                _logger.LogInformation(_resp.Message);
            }

            return _resp;
        }

        /// <summary>
        /// Actualizar asignación de maestro.
        /// </summary>
        /// <param name="Id">Identificación</param>
        /// <param name="model">Datos a modificar</param>
        /// <returns>Retorno los datos modificados, si son correctos.</returns>
        [HttpPost("{id:int}")]
        public async Task<APIResponse> Update(int Id, [FromBody] TeacherClassroomCreateDTO model)
        {
            _logger.LogInformation("Asignación de Maestros a Aulas.");

            if (!ModelState.IsValid || model == null)
            {
                _resp.IsValid = false;
                _resp.Result = ModelState;
                _resp.Message = "Hubo un error o no hay datos en el resultado";
                _resp.StatusCode = HttpStatusCode.BadRequest;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            if (model.ClassroomId.Count == 0)
            {
                _resp.IsValid = false;
                _resp.Message = "No hay aulas para asignar.";
                _resp.StatusCode = HttpStatusCode.NotFound;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            var search_dupli = await _context.TeachersClassrooms.Where(x => x.TeacherId != Id && model.ClassroomId.Contains(x.ClassroomId)).ToListAsync();
            if (search_dupli.Count > 0)
            {
                _resp.IsValid = false;
                _resp.Message = "No puede asignar el aula a mas de un maestro.";
                _resp.StatusCode = HttpStatusCode.Conflict;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            var obj_search = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == Id);
            if (obj_search == null)
            {
                _resp.IsValid = false;
                _resp.Message = "No se ha encontrado el maestro.";
                _resp.StatusCode = HttpStatusCode.NotFound;

                _logger.LogError(_resp.Message);

                return _resp;
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.TeachersClassrooms
                            .Where(x => x.TeacherId == Id)
                            .ExecuteDeleteAsync();

                    await _context.SaveChangesAsync();

                    foreach (var item in model.ClassroomId)
                    {
                        var d = new TeacherClassroom()
                        {
                            TeacherId = Id,
                            ClassroomId = item,
                        };
                        _context.TeachersClassrooms.Add(d);
                    }

                    await _context.SaveChangesAsync();

                    await _context.Database.CommitTransactionAsync();

                    _resp.Message = "Aulas asignadas al Maestro.";
                    _resp.StatusCode = HttpStatusCode.Created;
                    _resp.Result = new
                    {
                        id = obj_search.Id,
                        firstname = obj_search.FirstName,
                        lastname = obj_search.LastName,
                        email = obj_search.Email,
                        profession = obj_search.Profession,
                        create_at = obj_search.create_at,
                        update_at = obj_search.update_at,
                        classroom = (from c in _context.Classrooms
                                     join tc in _context.TeachersClassrooms
                                          on c.Id equals tc.ClassroomId
                                     where tc.TeacherId == obj_search.Id
                                     select c).ToList()
                    };

                    _logger.LogInformation(_resp.Message);
                    return _resp;
                }
                catch (Exception ex)
                {
                    await _context.Database.RollbackTransactionAsync();

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
}
