using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MapModels.ViewModels.PostVM
{
    public class PostEditVM : PostVM
    {
        [Display(Name = "Blog Metadata")]
        public ICollection<PostMetaVM> PostMetas { get; set; }


        [Display(Name = "Tags of Blog")]
        public ICollection<PostTagVM> PostTags { get; set; }


        [Display(Name = "Categories of Blog")]
        public ICollection<PostCategoryVM> PostCategories { get; set; }
    }
}
