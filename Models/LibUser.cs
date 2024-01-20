namespace schoolApp.Models
{
    public class LibUser
    {
        public int LibUserId { get; set; }
        public Student Student { get; set; }
        public LibItem LibItem { get; set; }
        public int Quantity { get; set; }
        public DateTime ReturnDate { get; set; }= DateTime.Today;
    }
}
