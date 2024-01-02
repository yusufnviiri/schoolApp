namespace schoolApp.Models
{
    public class StudentCharge
    {
   public int StudentChargeId { get; set; }
        public Student student { get; set; }
        public DateTime PayDate { get; set; }= DateTime.Now;
        public bool Ream { get; set; }
        public bool DevFee{ get; set; }
        public decimal SchoolFees { get; set; }




    }
}
