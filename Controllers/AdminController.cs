using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shortenyour.link.Areas.Identity.Data;
using shortenyour.link.Data;
using shortenyour.link.Models;
using System;

namespace shortenyour.link.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminContext _adminContext;
        private readonly MemberContext _user;
        private readonly UserManager<shortenyourlinkUser> _userManager;
        private readonly LinkContext _linkContext;
        public AdminController(AdminContext adminContext, MemberContext user, UserManager<shortenyourlinkUser> userManager, LinkContext linkContext)
        {
            _adminContext = adminContext;
            _user = user;
            _userManager = userManager;
            _linkContext = linkContext;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] Admin admin)
        {
            var adminModel = await _adminContext.Admins
            .SingleOrDefaultAsync(a => a.AdminMail == admin.AdminMail && a.AdminPassword == admin.AdminPassword && a.AdminSecretKey == admin.AdminSecretKey);

            if (adminModel == null)
            {
                TempData["LoginResult"] = "BAŞARISIZ" + admin.AdminMail + " - " + admin.AdminPassword + " - " + admin.AdminSecretKey;

                return RedirectToAction("Login");


            }

            HttpContext.Session.SetInt32("AdminId", adminModel.Id);
            TempData["LoginResult"] = "BAŞARILI";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync();
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public async Task<IActionResult> MemberList()
        {
            var Users = await _user.Users.ToListAsync();
            return View(Users);
        }
        [HttpGet("/admin/MemberDetails/{id}")]
        public async Task<IActionResult> MemberDetails(string id)
        {
            var member = await _user.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (member == null)
            {
                return NotFound("THERE İS NO USER");
            }
            else
            {
                return View(member);
            }
        }
        [HttpGet("/admin/banhim/{id}")]
        public async Task<IActionResult> BanHim(string id)
        {
            var user = await _userManager.FindByIdAsync(id);



            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    Console.WriteLine("BANNED", user);
                    TempData["Result"] = "SUCCESFUL";
                    return View(user);
                }
                else
                {
                    Console.WriteLine("Error Occured: ", result.Errors);
                    TempData["Result"] = "FAİL";
                    return View();
                }
            }
            TempData["Result"] = "USER WAS NULL";
            return View();

        }
        [HttpGet("/admin/hislinks/{id}")]
        public async Task<IActionResult> hislinks(string id)
        {
            var links = _linkContext.Links.FirstOrDefaultAsync(p => p.OwnerId == id);
            return View(links);
        }
        public IActionResult Index()
        {
            return View();
        }
    }

}