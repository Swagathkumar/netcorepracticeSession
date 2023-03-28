﻿namespace netcorepracticeSession.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public ICollection<Property> Properties { get; set; }               // to make a relationship between two tables in database of user and properties
    }
}
