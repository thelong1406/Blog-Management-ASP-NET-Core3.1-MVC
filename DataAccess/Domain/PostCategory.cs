namespace DataAccess.Domain
{
    public class PostCategory
    {
        public long PostId { get; set; }   
        public Post Post { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
