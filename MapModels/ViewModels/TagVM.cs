using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MapModels.ViewModels
{
    public class TagVM
    {
        [Display(Name = "Tag Id")]
        public long Id { get; set; }
        
        
        [Display(Name = "Tag Title")]
        [StringLength(75, ErrorMessage = "Tag title's characters length cannot over 75 characters!")]
        [Required(ErrorMessage = "Tag title is required!")]
        public string Title { get; set; }
        
        
        [Display(Name = "Meta Title")]
        [StringLength(100, ErrorMessage = "Meta title's characters length cannot over 100 characters!")]
        public string MetaTitle { get; set; }
        
        
        [Display(Name = "Slug")]
        [StringLength(100, ErrorMessage = "Tag slug's characters length cannot over 100 characters!")]
        [Required(ErrorMessage = "Slug is required!")]
        public string Slug { get; set; }
        
        
        [Display(Name = "Content")]
        public string Content { get; set; }
        
        
        [Display(Name = "Published status")]
        public bool IsDeleted { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
