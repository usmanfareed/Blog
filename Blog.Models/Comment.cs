namespace Blog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        // User id of comment 
        public int PostId { get; set; }

        public int ReplyToId { get; set; }

    }
}