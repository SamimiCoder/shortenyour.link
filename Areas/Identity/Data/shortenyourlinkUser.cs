using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace shortenyour.link.Areas.Identity.Data;

// Add profile data for application users by adding properties to the shortenyourlinkUser class
public class shortenyourlinkUser : IdentityUser
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string CardNo { get; set; }
}