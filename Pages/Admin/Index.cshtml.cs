using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BladderRiskApp.Pages.Admin;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    public void OnGet() { }
}
