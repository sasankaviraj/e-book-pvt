namespace e_book_pvt.Models
{
    public class Item
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string UserID { get; set; }
    }
}
