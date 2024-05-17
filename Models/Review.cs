namespace e_book_pvt.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public string Comment { get; set; }
        public string UserID { get; set; }
        public string UserEmail { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
