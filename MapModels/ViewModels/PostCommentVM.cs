using MapModels.ViewModels.PostVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MapModels.ViewModels
{
    public class PostCommentVM
    {
        [Display(Name = "Comment Id")]
        public long? PostCommentId { get; set; }


        [Display(Name = "User commented Id")]
        public string UserId { get; set; }

        [Display(Name = "User commented")]
        public UserVM User { get; set; }

        [Display(Name = "Post Commented Id")]
        public long? PostId { get; set; }

        [Display(Name = "Post Commented")]
        public PostDetailVM Post { get; set; }
        //public long? ParentId { get => ParentId ?? 0; set => ParentId = value; }

        [Display(Name = "Parent Comment Id")]
        public long? ParentId { get; set; }

        [Display(Name = "Parent Comment")]
        public PostCommentVM ParentPostComment { get; set; }

        [Display(Name = "Comment Title")]
        [StringLength(100, ErrorMessage = "Comment title have to has less than 100 charaters!")]
        public string Title { get; set; }
        [Display(Name = "Published Status")]
        public bool Published { get; set; }


        [Display(Name = "Created at")]
        public DateTime CreatedAt { get; set; }


        [Display(Name = "Published at")]
        [DisplayFormat(DataFormatString = "{0:MMMM dd yyyy}")]
        public DateTime? PublishedAt { get; set; }

        [Display(Name = "Comment Content")]
        public string Content { get; set; }

        [Display(Name = "Hide Status")]
        public bool IsHidedByAdmin { get; set; }


        [Display(Name = "Deleted Status")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Reply Comments")]
        public ICollection<PostCommentVM> ChildPostComments { get; set; }
    }
}
