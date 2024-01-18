using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace schoolApp.Models
{
    public class StudentAssessemet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int StudentAssessementId { get; set; }
        public int AssessedStudentId { get; set; }
        public int AssessedLevelId { get; set; }
        public int AssessedSemesterId { get; set; }
        public int CourseId { get; set;}
        public Assessement Assessement { get; set; } = new Assessement();

    }
}
