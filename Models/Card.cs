namespace FlashCards.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public Set Set { get; set; }
    }
}
