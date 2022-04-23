using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MapModels.ViewModels.PostVM
{
    public class PostVM : ListPostsVM
    {
        [Display(Name = "Parent Id")]
        public long? ParentId { get; set; }


        [Display(Name = "Parent Post")]
        public PostVM ParentPost { get; set; }


        [Display(Name = "Meta Title")]
        [StringLength(100, ErrorMessage = "Meta Title of blog must have less than 100 characters!")]
        public string MetaTitle { get; set; }

        [Required(ErrorMessage = "Summary field is needed to be filled!")]
        [Display(Name = "Slug")]
        [StringLength(100, ErrorMessage = "Slug of blog must have less than 100 characters!")]
        public string Slug { get; set; }


        [Display(Name = "Updated At")]
        [DisplayFormat(DataFormatString = "{0:MMMM dd yyyy}")]
        public DateTime? UpdatedAt { get; set; }


        [Display(Name = "Published At")]
        [DisplayFormat(DataFormatString = "{0:MMMM dd yyyy}")]
        public DateTime? PublishedAt { get; set; }


        [Display(Name = "Blog Content")]
        public string Content { get; set; }


        [Display(Name = "Video / Blog")]
        public bool IsVideoPost { get; set; }

        public bool ValidDatePublishedAndModify()
        {
            if (this.UpdatedAt <= this.CreatedAt && this.UpdatedAt != null)
                return false;
            if (this.PublishedAt <= this.CreatedAt && this.PublishedAt != null)
                return false;
            return true;
        }
    }
}
