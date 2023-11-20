namespace School_API.Services
{
    public interface IConvertService
    {
        List<T> StringToJson<T>(string json) where T : class;
    }
}
