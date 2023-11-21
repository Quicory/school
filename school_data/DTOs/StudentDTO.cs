
using System.Text.Json.Serialization;

namespace School_Data.Models
{
    public class StudentDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastname")]
        public string LastName { get; set; }
        [JsonPropertyName("birthdate")]
        public DateOnly? Birthdate { get; set; }
        [JsonPropertyName("idnumber")]
        public string IDNumber { get; set; }
        [JsonPropertyName("fathername")]
        public string FatherName { get; set; }
        [JsonPropertyName("mothername")]
        public string MotherName { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("create_at")]
        public DateTime? create_at { get; set; }
        [JsonPropertyName("update_at")]
        public DateTime? update_at { get; set; }
    }
}
