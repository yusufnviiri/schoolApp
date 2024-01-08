using System.ComponentModel.DataAnnotations.Schema;

namespace schoolApp.Models
{
    public class LevelChange
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LevelChangeId { get; set; }
        public string LevelName { get; set;}
        public string Semester { get; set;} 
        public int StudentId { get; set;}
        public decimal schoolFess   { get; set;}
    }
}
