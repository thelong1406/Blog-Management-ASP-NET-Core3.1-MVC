using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DataAccess.Domain
{
    public class User : IdentityUser
    {
        //public long Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        //public string Mobile { get; set; }
        //public string Email { get; set; }
        //public string PasswordHash { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Intro { get; set; }
        public string Profile { get; set; }
        public byte[] Avatar { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Cart> Carts  { get; set; }
        public ICollection<PostComment> Comments { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
