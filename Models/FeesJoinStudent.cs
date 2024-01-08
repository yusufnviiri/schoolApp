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
        public string SchoolLevel { get; set; }
        public decimal SchoolFees { get; set; }
        public string Semester { get; set; }
    }
}
