namespace e_book_pvt.Models
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Items { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserID { get; set; }
        public bool? IsDelivered { get; set; }
    }
}
