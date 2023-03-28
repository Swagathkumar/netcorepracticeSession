using System.ComponentModel.DataAnnotations;

namespace netcorepracticeSession.Models
{
    public class Category                                       //modal  
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name should not be null or empty")]                                              //modal validation
        public string Name { get; set; }
        [Required(ErrorMessage = "Imageurl should not be null or empty")]                                              //modal validation
        public string Imageurl { get; set; }
        public ICollection<Property> Properties { get; set; }                  // to make a relationship between two tables in database of category and properties
    }
}
