using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MapModels.ViewModels
{
    public class CategoryVM
    {
        [Display(Name = "Category Id")]
        public long Id { get; set; }
        
        
        [Display(Name = "Parent Id")]
        public long? ParentId { get; set; }
        //public long? ParentId { get => ParentId ?? 0; set => ParentId = value; }
        [Display(Name = "Parent")]
        public Category ParentCategory { get; set; }
        
        
        [Display(Name = "Category Titlte")]
        [Required]
        [StringLength(75)]
        public string Title { get; set; }


        [Display(Name = "Meta Title")]
        [StringLength(100)]
        public string MetaTitle { get; set; }


        [Display(Name = "Slug")]
        [StringLength(100)]
        public string Slug { get; set; }


        [Display(Name = "Content")]
        public string Content { get; set; }


        [Display(Name = "Published Status")]
        public bool IsDeleted { get; set; }
        public ICollection<CategoryVM> ChildCategories { get; set; }
        //public ICollection<PostCategory> PostCategories { get; set; }
        public CategoryVM()
        {
            this.ChildCategories = new List<CategoryVM>();
        }
    }
}
