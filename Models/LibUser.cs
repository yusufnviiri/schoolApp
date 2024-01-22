namespace schoolApp.Models
{
    public class LibUser
    {
        public int LibUserId { get; set; }
        public int StudentId { get; set; }
        public int LibItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime ReturnDate { get; set; }= DateTime.Today;
    }
}
