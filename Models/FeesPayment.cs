using System.ComponentModel.DataAnnotations.Schema;

namespace schoolApp.Models
{
    public class FeesPayment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeespaymentId { get; set; }
        public DateTime DateOfPayment { get; set; }= DateTime.Now;
        public  Semester Semester { get; set; }
        public SchoolLevel StudentLevel { get; set; }
        public int  StudentId { get; set; }
        public decimal Amount { get;set; }
        public decimal Balance { get; set; }
        public decimal PrevBalance { get; set; }
        public string Reason { get; set; }="School Fees Payment";
        public string RecievedBy { get; set; } = "Burser";
    }
}
