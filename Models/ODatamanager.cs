namespace schoolApp.Models
{
    public class ODatamanager
    {
        public Student student { get; set; }=new Student();
        public List<SchoolLevel> schoolLevels { get; set; }= new List<SchoolLevel>();
        public FeesJoinStudent FeesJoinStudent { get; set; } = new FeesJoinStudent();


    }
}
