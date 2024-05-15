namespace e_book_pvt.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public string Name { get; set; }
        public Double Price { get; set; }
        public string ImageUrl { get; set; }
        public string UserID { get; set; }
    }
}
