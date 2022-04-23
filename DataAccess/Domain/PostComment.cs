using System;
using System.Collections.Generic;

namespace DataAccess.Domain
{
    public class PostComment
    {
        public long PostCommentId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public long PostId { get; set; }
        public Post Post { get; set; }
        //public long? ParentId { get => ParentId ?? 0; set => ParentId = value; }
        public long? ParentId { get; set; }
        public PostComment ParentPostComment { get; set; }
        public string Title { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string Content { get; set; }
        public bool IsHidedByAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<PostComment> ChildPostComments { get; set; }
    }
}
