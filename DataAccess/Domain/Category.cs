using System.Collections.Generic;

namespace DataAccess.Domain
{
    public class Category
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        //public long? ParentId { get => ParentId ?? 0; set => ParentId = value; }
        public Category ParentCategory { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; }

    }
}
