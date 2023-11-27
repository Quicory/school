
using System.Text.Json.Serialization;

namespace School_Data.Models
{
    public class TeacherClassroomDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastname")]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("profession")]
        public string Profession { get; set; }
        [JsonPropertyName("create_at")]
        public DateTime? create_at { get; set; }
        [JsonPropertyName("update_at")]
        public DateTime? update_at { get; set; }
        [JsonPropertyName("classrooms")]
        public string Classrooms { get; set; }        
    }
}
