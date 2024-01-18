using System.ComponentModel.DataAnnotations.Schema;

namespace schoolApp.Models
{
    public class Semester
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
    }
}
