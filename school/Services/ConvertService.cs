using School_API.Controllers;

namespace School_API.Services
{
    public class ConvertService : IConvertService
    {
        private readonly ILogger<UserController> _logger;

        public ConvertService(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public List<T> StringToJson<T>(string json) where T : class
        {
            try
            {
                //return Utf8Json.JsonSerializer.Deserialize<T>(json);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>("[]");
            }
        }
    }
}
