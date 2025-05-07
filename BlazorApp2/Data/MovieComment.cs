namespace BlazorApp2.Data
{
    public class MovieComment
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string? UserId { get; set; }
        public string? Comment { get; set; }
        public float? Rate { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
