using System;
using System.Collections.Generic;

namespace DataAccess.Domain
{
    public class Post
    {
        public long PostId { get; set; }
        public long? ParentId { get; set; }
        //public long? ParentId { get => ParentId ?? 0; set => ParentId = value; }
        public Post ParentPost { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Content { get; set; }
        public bool IsVideoPost { get; set; }
        public double? MinutesSpentForReading { get; set; }
        public byte[] Thumbnail { get; set; }
        public ICollection<Post> ChildPosts { get; set; }
        public ICollection<PostMeta> PostMetas { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
