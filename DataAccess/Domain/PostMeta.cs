namespace DataAccess.Domain
{
    public class PostMeta
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public Post Post { get; set; }
        public string Key { get; set; }
        public string Content { get; set; }

    }
}
