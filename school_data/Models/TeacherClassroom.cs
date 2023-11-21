
using Microsoft.EntityFrameworkCore;

namespace School_Data.Models
{
    [Index(nameof(ClassroomId), IsUnique = true, Name = "Unique_ClassroomId")]
    public class TeacherClassroom
    {        
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }        
    }
}
