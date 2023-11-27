using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using School_Data.DTOs;
using School_Data.Helpers;
using School_Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace School_API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        protected APIResponse _resp;
        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager
                    , IConfiguration configuration, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _resp = new();
        }
        /// <summary>
        /// Silve para registrar el usuario con su rol.
        /// </summary>
        /// <param name="model">Datos del usuario a registrar</param>
        /// <param name="role">Rol del usuario</param>
        /// <returns>Respuesta sobre datos que son aceptados o rechazados</returns>
        public async Task<APIResponse> Registeration(RegistrationModel model, string role)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                _resp.IsValid = false;
                _resp.Message = "Usuario ya existe";
                _resp.StatusCode = HttpStatusCode.BadRequest;
                return _resp;
            }
                

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                CompleteName = model.CompleteName
            };
            var createUserResult = await userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
            {
                _resp.IsValid = false;
                _resp.Message = "¡Error al crear usuario! Por favor verifique los detalles de la contraseña (Compleja) e inténtelo nuevamente.";
                _resp.StatusCode = HttpStatusCode.BadRequest;
                return _resp;
            }
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            if (await roleManager.RoleExistsAsync(role))
                await userManager.AddToRoleAsync(user, role);
                        
            _resp.Message = "¡Usuario creado exitosamente!";
            _resp.StatusCode = HttpStatusCode.Created;            
            return _resp;
        }
        /// <summary>
        /// Es utilizado para acceder a la API.
        /// </summary>
        /// <param name="model">Datos del usuario a acceder</param>
        /// <returns>Respuesta sobre datos que son aceptados o rechazados</returns>
        public async Task<APIResponse> Login(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                _resp.IsValid = false;
                _resp.Message = "Nombre de usuario no válido";
                _resp.StatusCode = HttpStatusCode.NotFound;
                return _resp;
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                _resp.IsValid = false;
                _resp.Message = "Contraseña invalida";
                _resp.StatusCode = HttpStatusCode.NonAuthoritativeInformation;
                return _resp;                
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            // string token = GenerateToken(authClaims);

            Token token = _mapper.Map<Token>(user);            
            token.token = GenerateToken(authClaims);
            token.Roles = (List<string>)userRoles;

            _resp.Message = "Bienvenido: " + token.CompleteName;
            _resp.StatusCode = HttpStatusCode.OK;
            _resp.Result = token;
            return _resp;
        }

        public async Task<APIResponse> ChangePassword(UserChangePasswordDTO model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                _resp.IsValid = false;
                _resp.Message = "Nombre de usuario no válido";
                _resp.StatusCode = HttpStatusCode.NotFound;
                return _resp;
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password))
            {
                _resp.IsValid = false;
                _resp.Message = "Contraseña invalida";
                _resp.StatusCode = HttpStatusCode.NonAuthoritativeInformation;
                return _resp;
            }

            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.PasswordNew);
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                _resp.IsValid = false;
                _resp.Message = "Error creando la nueva contraseña.";
                _resp.StatusCode = HttpStatusCode.Conflict;
                return _resp;
            }

            _resp.Message = "Contraseña cambiada.";

            return _resp;
        }

        /// <summary>
        /// Sirve para generar el token de acceso.
        /// </summary>
        /// <param name="claims">Listas de claim para generar el token</param>
        /// <returns>Retorno del token generado</returns>
        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                //Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
