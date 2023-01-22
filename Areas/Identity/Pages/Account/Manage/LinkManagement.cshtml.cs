using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using shortenyour.link.Areas.Identity.Data;
using shortenyour.link.Data;

namespace shortenyour.link.Areas.Identity.Pages.Account.Manage
{
    public class LinkManagementModel : PageModel
    {
        private readonly UserManager<shortenyourlinkUser> _userManager;
        private readonly SignInManager<shortenyourlinkUser> _signInManager;
        private readonly LinkContext _linkContext;
        public LinkManagementModel(
            UserManager<shortenyourlinkUser> userManager,
            SignInManager<shortenyourlinkUser> signInManager,
            LinkContext linkContext
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _linkContext = linkContext;
        }
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Kullanýcýnýn oluþturduðu linkleri getirin
            var links = _linkContext.Links.Where(l => l.OwnerId == user.Id).ToList();
            ViewData["links"] = links;
            return Page();
        }
    }
}
