using MapModels.ViewModels.PostVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MapModels.ViewModels
{
    public class UserVM
    {
        [Display(Name = "First name" )]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Introduction")]
        public string Intro { get; set; }
        [Display(Name = "User profile")]
        public string Profile { get; set; }

        [Display(Name = "Avatar")]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public byte[]? Avatar { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public ICollection<PostDetailVM> Posts { get; set; }
        //public ICollection<Invoice> Invoices { get; set; }
        //public ICollection<Cart> Carts { get; set; }
        public ICollection<PostCommentVM> Comments { get; set; }
        public ICollection<VoteVM> Votes { get; set; }
    }
}
