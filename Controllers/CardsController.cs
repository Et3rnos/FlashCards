using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FlashCards.Controllers
{
    [Route("/api/[controller]")]
    public class CardsController : Controller
    {
        private readonly DatabaseContext _context;

        public CardsController(DatabaseContext context)
        {
            _context = context;
        }

        public class UpdateCardItem
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public bool IsFront { get; set; }
            [Required]
            public string Text { get; set; }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateCardItem[] updateCardItems)
        {
            var cards = await _context.Cards.Where(x => updateCardItems.Select(x => x.Id).Contains(x.Id)).ToListAsync();
            foreach (var cardItem in updateCardItems)
            {
                var card = cards.FirstOrDefault(x => x.Id == cardItem.Id);
                if (card == null) return Json(new { success = false });

                if (cardItem.IsFront) card.Front = cardItem.Text;
                else card.Back = cardItem.Text;
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}
