namespace schoolApp.Models
{
    public class Student:Person
    {
        public int StudentId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string SchoolLevel { get; set;}
        public decimal SchoolFees { get; set;}
        public string Semester { get; set;}
        public Guardian Guardian { get; set;}= new Guardian();

    }
}
