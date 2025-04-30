namespace NewsSite.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Authors { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsApproved { get; set; }

        public int NewsId { get; set; }
        public NewsItem News { get; set; }
    }
}
