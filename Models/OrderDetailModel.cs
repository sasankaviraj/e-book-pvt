namespace e_book_pvt.Models
{
    public class OrderDetailModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserID { get; set; }
    }
}
