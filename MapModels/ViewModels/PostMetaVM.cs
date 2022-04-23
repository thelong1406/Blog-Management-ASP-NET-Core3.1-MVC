using MapModels.ViewModels.PostVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MapModels.ViewModels
{
    public class PostMetaVM
    {
        [Display(Name = "Blog Metadata Id")]
        public long? Id { get; set; }


        [Display(Name = "Blog Id")]
        [Required]
        public long PostId { get; set; }


        [Display(Name = "Blog")]
        public PostEditVM Post { get; set; }


        [Display(Name = "Metadata Key")]
        [Required]
        [StringLength(50, ErrorMessage = "Metadata key must has less than 50 characters!")]
        public string Key { get; set; }


        [Display(Name = "Metadata Content")]
        public string Content { get; set; }
    }
}
