using FlashCards.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Pages
{
    public class EditModel : PageModel
    {
        public Set Set { get; set; }

        private readonly DatabaseContext _context;

        public EditModel(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Set = await _context.Sets.Include(x => x.Cards).FirstOrDefaultAsync(x => x.Id == id);
            if (Set == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostNewcardAsync(int setid)
        {
            Set = await _context.Sets.Include(x => x.Cards).FirstOrDefaultAsync(x => x.Id == setid);
            if (Set == null) return NotFound();
            Set.Cards.Add(new Card { Front = "", Back = "" });
            await _context.SaveChangesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeletecardAsync(int setid, int cardid)
        {
            Set = await _context.Sets.Include(x => x.Cards).FirstOrDefaultAsync(x => x.Id == setid);
            if (Set == null) return NotFound();
            var card = Set.Cards.FirstOrDefault(x => x.Id == cardid);
            if (card == null) return NotFound();
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return Page();
        }
    }
}