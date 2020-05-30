namespace WebApplication2.Models
{
    public class favStatus
    {
        public int Id { get; set; }
        public int CodeId { get; set; }
        public string UserId { get; set; }
        public Code code { get; set; }
    }
}