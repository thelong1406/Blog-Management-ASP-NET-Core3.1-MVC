using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MapModels.ViewModels.PostVM
{
    public class ListPostsVM
    {
        [Display(Name = "Post Id")]
        public long PostId { get; set; }

        [Display(Name = "Title")]
        //[StringLength(75, ErrorMessage = "Blog Title must have less than 75 characters!")]
        public string Title { get; set; }


        [Display(Name = "Author Id")]
        public string UserId { get; set; }


        [Display(Name = "Author")]
        public UserVM User { get; set; }


        [Display(Name = "Summary")]
        //[StringLength(255, ErrorMessage = "Summary of blog must have less than 100 characters!")]
        public string Summary { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MMMM dd, yyyy}")]
        public DateTime CreatedAt { get; set; }


        [Display(Name = "Time to read")]
        public double? MinutesSpentForReading { get; set; }


        [Display(Name = "Blog thumbnail")]
        public byte[] Thumbnail { get; set; }


        [Display(Name = "Blog Rating")]
        public ICollection<VoteVM> Votes { get; set; }

        [Display(Name = "Published Status")]
        public bool Published { get; set; }

        [Display(Name = "Deleted Status")]
        public bool IsDeleted { get; set; }
    }
}
