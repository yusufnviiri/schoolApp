namespace schoolApp.Models
{
    public class FeesJoinStudent
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Sex { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public decimal PrevBalance { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string RecievedBy { get; set; } = "Burser";
        public SchoolLevel SchoolLevel { get; set; }
        public int LevelId { get; set; }
        public decimal SchoolFees { get; set; }
        public string? Residance { get; set; }   
        public Semester Semester { get; set; }
        public int SemesterId { get; set;}
        public Course Course { get; set; }
        public int DayR { get; set; }
        public int MonthR { get; set; }
        public int YearR { get; set; }
        public int DayB { get; set; }
        public string? District { get; set; }
        public string ContactS { get; set; }
        public string ContactG { get; set; }
        public Assessement Assessement { get; set; }
        public int MonthB { get; set; }
        public int YearB { get; set; }
        public Guardian? Guardian { get; set; }
    }
}
