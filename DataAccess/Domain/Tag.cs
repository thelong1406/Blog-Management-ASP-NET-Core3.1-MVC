using System.Collections.Generic;

namespace DataAccess.Domain
{
    public class Tag
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
