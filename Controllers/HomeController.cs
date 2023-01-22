using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shortenyour.link.Data;
using shortenyour.link.Models;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace shortenyour.link.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LinkContext _linkContext;

        public HomeController(ILogger<HomeController> logger, LinkContext linkContext)
        {
            _logger = logger;
            _linkContext = linkContext;
        }


        public string GetUserId()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
        public string GetUserMail()
        {
            var userMail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            return userMail;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] Link link, [FromForm] string originalUrl)
        {

            string LinkUrl = "";
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError(string.Empty, "You must be logged in to shorten a link");
                return RedirectToAction("Index", "home");
            }
            else
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var s_link = await _linkContext.Links
                    .Where(l => l.originalUrl == originalUrl)
                    .FirstOrDefaultAsync();
                    if (s_link != null)
                    {

                        TempData["Message"] = "This link has already been shortened !";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        using (SHA256 sha256 = SHA256.Create())
                        {
                            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(originalUrl));
                            LinkUrl = BitConverter.ToString(hash).Replace("-", "").Substring(0, 8);
                            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                            var userEmail = User.FindFirst(ClaimTypes.Email).Value;
                            link.OwnerId = (userId);
                            link.OwnerMail = userEmail;
                            link.LinkUrl = LinkUrl;
                            _linkContext.Links.Add(link);
                            await _linkContext.SaveChangesAsync();
                            _linkContext.SaveChanges();
                            ViewData["ShortedLink"] = link.LinkUrl;
                            TempData["Message"] = "Link has been shortened successfully.";
                        }
                    }

                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Index(Link link)
        {
            return View();
        }
        public IActionResult Redirect(string shortUrl)
        {
            string? originalUrl = _linkContext.Links
                         .Where(l => l.LinkUrl == shortUrl)
                         .Select(l => l.originalUrl)
                         .FirstOrDefault();
            TempData["originalUrl"] = originalUrl;
            if (originalUrl == null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                var link = _linkContext.Links.FirstOrDefault(l => l.LinkUrl == shortUrl);

                if (link != null)
                {
                    link.Click_Count++;
                    if (link.Click_Count % 20 == 0)
                    {
                        link.LinkBalance += (decimal)0.10;
                    }
                    _linkContext.SaveChanges();
                }
                return RedirectToAction("RedirectPage");
            }
        }

        public IActionResult RedirectPage(string shortUrl)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}