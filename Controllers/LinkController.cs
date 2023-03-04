using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shortenyour.link.Data;

namespace shortenyour.link.Controllers
{
    public class LinkController : Controller
    {
        private readonly LinkContext _linkContext;
        private readonly ILogger<LinkController> _logger;
        public LinkController(ILogger<LinkController> logger, LinkContext linkContext)
        {
            _logger = logger;
            _linkContext = linkContext;
        }
        public async Task<IActionResult> LinkInfo(int id)
        {
            var link = _linkContext.Links.FirstOrDefault(l => l.Id == id);
            return View(link);
        }
        public async Task<IActionResult> DeleteLink(int id)
        {
            var link = _linkContext.Links.FirstOrDefault(l => l.Id == id);
            _linkContext.Remove(link);
            _linkContext.SaveChangesAsync();
            return RedirectToPage("LinkManagement");
        }
        public IActionResult MoneyRequest(int id)
        {
            TempData["LinkId"] = id;
            return View();
        }
    }
}