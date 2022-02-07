namespace FlashCards.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Set> Sets { get; set; } = new List<Set>();
    }
}
