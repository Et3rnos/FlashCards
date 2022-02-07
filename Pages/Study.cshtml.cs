using FlashCards.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Pages
{
    public class StudyModel : PageModel
    {
        public Set Set { get; set; }
        public Card Card { get; set; }

        private readonly DatabaseContext _context;

        public StudyModel(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Set = await _context.Sets.Include(x => x.Cards).FirstOrDefaultAsync(x => x.Id == id);
            if (Set == null) return NotFound();
            
            if (Set.Cards.Any())
            {
                Card = Set.Cards[new Random().Next(0, Set.Cards.Count)];
            }
            return Page();
        }
    }
}