namespace schoolApp.Models
{
    public class GeneralLedger
    {
        public int GeneralLedgerId { get;set; }
        public decimal TotalIncome { get;set;}
        public decimal TotalExpenses { get;set; }
        public decimal Amount { get;set; }
        public string Reason { get;set; }
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
