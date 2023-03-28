using System.Text.Json.Serialization;

namespace netcorepracticeSession.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Imageurl { get; set; }

        public double price { get; set; }
        public bool istrending { get; set; }

        public int categoryid { get; set; }
        //[JsonIgnore]
        public Category category { get; set; }
        public int userid { get; set; }
       // [JsonIgnore]
        public User user { get; set; }
    }
}
