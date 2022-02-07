using FlashCards.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseContext _context;

        public IndexModel(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAddCategory(string name)
        {
            if (string.IsNullOrEmpty(name)) return Page();
            await _context.Categories.AddAsync(new Category { Name = name });
            await _context.SaveChangesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAddSet(string name, int categoryid)
        {
            if (string.IsNullOrEmpty(name)) return Page();

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryid);
            if (category == null) return Page();

            category.Sets.Add(new Set { Name = name });
            await _context.SaveChangesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteSet(int setid)
        {
            var set = await _context.Sets.FirstOrDefaultAsync(x => x.Id == setid);
            if (set == null) return Page();

            _context.Sets.Remove(set);
            await _context.SaveChangesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteCategory(int categoryid)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryid);
            if (category == null) return Page();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Page();
        }
    }
}