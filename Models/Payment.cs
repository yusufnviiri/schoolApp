namespace schoolApp.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public Staff Staff { get; set; }
        public int StaffId { get;set; }
        public string Reason { get; set; } = "salary";
        public decimal Amount { get; set; }
        public decimal Paye { get; set; }
        public decimal Compulserysavings { get; set; }
        public decimal Balance { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
        public decimal BalanceCalculator(Staff staff, decimal amount,decimal balance)
        {
            if (Reason.ToLower() == "salary")
            {
                Compulserysavings =(decimal) Math.Floor(0.2 *(double) amount);
                Paye = (decimal)Math.Floor(0.15 * (double)amount);


                Balance = (staff.Salary - amount)+balance;
            }
            else { Balance = Balance; }

            return Balance;
        }
    }
}
