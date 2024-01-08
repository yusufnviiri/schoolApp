namespace schoolApp.Models
{
    public class Student:Person
    {
        public int StudentId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public SchoolLevel SchoolLevel { get; set;}
        public int LevelId { get; set; }
        public decimal SchoolFees { get; set;}
        public string Semester { get; set;}
        public Guardian? Guardian { get; set;}= new Guardian();

    }
}
