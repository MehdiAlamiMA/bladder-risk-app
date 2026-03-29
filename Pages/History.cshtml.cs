using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BladderRiskApp.Data;
using BladderRiskApp.Models;
using System.Security.Claims;

namespace BladderRiskApp.Pages
{
    public class HistoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        // Inject database context
        public HistoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // List that will contain user's risk checks
        public List<RiskCheck> RiskChecks { get; set; } = new();

        public void OnGet()
        {
            // Get the unique user ID from the logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Load all checks for this user, newest first
            RiskChecks = _context.RiskChecks
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }
    }
}