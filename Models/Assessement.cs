using System.ComponentModel.DataAnnotations.Schema;

namespace schoolApp.Models
{
    public class Assessement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssessementId { get; set; }
        public int AssesedCourseId { get; set; }
        public int Cat1 { get; set; }
        public int Cat2 { get; set; }
        public int MidExam { get; set; }
        public int Final { get; set; }
        public int TotalScore { get; set; }

        public string Grade { get; set; }= string.Empty;

    }
    
}
