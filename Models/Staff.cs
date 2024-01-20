namespace schoolApp.Models
{
    public class Staff:Person
    {
        public int StaffId{ get; set; }
        public string Category { get; set;}
        public decimal Salary { get; set;}
        public DateTime EmployementDate { get; set; } = DateTime.Today;
        public int CoursesId { get; set; }
        public int LeaveDays { get; set; }
        public string JobTitle { get; set; }
        public string MaritalStatus { get; set; }
        public string Qualification { get; set; }

    }
}
