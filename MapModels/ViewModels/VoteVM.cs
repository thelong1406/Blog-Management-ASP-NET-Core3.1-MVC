using MapModels.ViewModels.PostVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MapModels.ViewModels
{
    public class VoteVM
    {

        [Display(Name = "User voted Id")]
        public string UserId { get; set; }

        [Display(Name = "Post was voted Id")]
        public long PostId { get; set; }

        [Display(Name = "Rating")]
        [Range(1,5)]
        public byte Rate { get; set; }

        [Display(Name = "User voted")]
        public UserVM User { get; set; }

        [Display(Name = "Post was voted")]
        public PostDetailVM Post { get; set; }
    }
}
