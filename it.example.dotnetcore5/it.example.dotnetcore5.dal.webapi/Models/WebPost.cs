namespace it.example.dotnetcore5.dal.webapi.Models
{
    public class WebPost
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
