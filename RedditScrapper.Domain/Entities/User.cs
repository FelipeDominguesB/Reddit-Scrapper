using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditScrapper.Domain.Entities
{
    public class User : BaseEntity
    {

        public User() { }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsAdmin { get; set; }   
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
