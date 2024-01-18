namespace schoolApp.Models
{
    public class ODatamanager
    {
        public Student student { get; set; }=new Student();
        public List<SchoolLevel> schoolLevels { get; set; }= new List<SchoolLevel>();
        public List<Semester> Semesters { get; set; } = new List<Semester>();
        public int SemesterId { get; set; }
        public ICollection<Course> Courses { get; set; }
        public int SchoolLevelId { get; set; }
        public int studentId { get;set; }
        public Course Course { get; set; }
        public FeesJoinStudent FeesJoinStudent { get; set; } = new FeesJoinStudent();
        public ReportCard ReportCard { get; set; }=new ReportCard();
        public Assessement Assessement { get; set; } =new Assessement();
        public StudentAssessemet StudentAssessemet { get; set; }
        public List<FeesPayment> FeesPayments { get; set; } = new List<FeesPayment>();
        public IEnumerable<Student> students { get; set; }
        public IEnumerable<FeesJoinStudent> FeesJoinStudents { get; set; }
    }
}
