namespace schoolApp.Models
{
    public class LevelData
    {
        public int LevelDataId { get; set; }
        public Student Student { get; set; }= new Student();
    }
}
