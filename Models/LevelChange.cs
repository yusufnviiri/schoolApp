using System.ComponentModel.DataAnnotations.Schema;

namespace schoolApp.Models
{
    public class LevelChange
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LevelChangeId { get; set; }
        public SchoolLevel SchoolLevel { get; set;}
        public Semester Semester { get; set;} 
        public int StudentId { get; set;}
        public decimal schoolFess   { get; set;}
    }
}
