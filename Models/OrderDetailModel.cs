using System.Text.Json;

namespace e_book_pvt.Models
{
    public class OrderDetailModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public string ItemsJson { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserID { get; set; }
        public bool? IsDelivered { get; set; }

        public void DeserializeItems()
        {
            if (!string.IsNullOrEmpty(ItemsJson))
            {
                Items = JsonSerializer.Deserialize<List<Item>>(ItemsJson);
            }
        }
    }
}
