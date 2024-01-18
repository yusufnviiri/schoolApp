namespace schoolApp.Models
{
    public class Student:Person
    {
        public int StudentId { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Today;
        public SchoolLevel SchoolLevel { get; set;}
        public decimal SchoolFees { get; set;}
        public Semester Semester { get; set;}
        public Guardian? Guardian { get; set;}= new Guardian();

    }
}
