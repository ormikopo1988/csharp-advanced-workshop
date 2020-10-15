using System.Collections.Generic;

namespace TransactionBasicDemo
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}