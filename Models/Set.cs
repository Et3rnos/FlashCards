namespace FlashCards.Models
{
    public class Set
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Card> Cards { get; set;} = new List<Card>();
        public Category Category { get; set; }
    }
}
