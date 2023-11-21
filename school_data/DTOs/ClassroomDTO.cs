using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace School_Data.DTOs
{
    public class ClassroomDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }        
        public DateTime? create_at { get; set; }
        public DateTime? update_at { get; set; }
    }
}
