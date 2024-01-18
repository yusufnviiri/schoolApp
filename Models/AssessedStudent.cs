using System.ComponentModel.DataAnnotations.Schema;

namespace schoolApp.Models
{
    public class AssessedStudent
    {

        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
public int AssessedStudentId { get; set; }
        public int StudentId { get; set; }

        public SchoolLevel SchoolLevel { get; set; }
        public Semester Semester { get; set; }

    }
}
