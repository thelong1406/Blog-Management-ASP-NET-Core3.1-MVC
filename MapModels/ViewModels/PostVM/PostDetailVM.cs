using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MapModels.ViewModels.PostVM
{
    public class PostDetailVM : PostEditVM
    {
        [Display(Name = "Child Blog")]
        public ICollection<ListPostsVM> ChildPosts { get; set; }


        [Display(Name = "Blog Comments")]
        public ICollection<PostCommentVM> PostComments { get; set; }
    }
}
