using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shortenyour.link.Areas.Identity.Data;
using shortenyour.link.Data;
using shortenyour.link.Models;

namespace shortenyour.link.Controllers
{
    public class NotificationController : Controller
    {
        private readonly LinkContext _linkContext;

        private readonly NotificationContext _notificationContext;
        private readonly UserManager<shortenyourlinkUser> _userManager;
        public NotificationController(LinkContext linkContext, NotificationContext notificationContext, UserManager<shortenyourlinkUser> userManager)
        {
            _linkContext = linkContext;
            _notificationContext = notificationContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> AddWithdrawalNotification(int LinkId, string notificationText)
        {
            var link = _linkContext.Links.FirstOrDefault(l => l.Id == LinkId);

            var notification = new Notification
            {
                NotificationCategory = "Money Request",
                NotificationText = notificationText
            };
            _notificationContext.Notifications.Add(notification);
            _notificationContext.SaveChanges();
            TempData["MoneyRequestResult"] = "YOUR PROCESS İS SUCCESS We will send you your money after reviewing your request. ";
            return RedirectToAction("Index", "home");
        }
    }
}