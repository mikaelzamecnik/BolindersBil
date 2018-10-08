using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.DataAccess
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private const string _admin = "admin";
        private const string _password = "admin";

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentitySeeder(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CreateAdminAccountIfEmpty()
        {
            if (!_context.Users.Any(u => u.UserName == _admin))
            {
                var user = new IdentityUser
                {
                    UserName = _admin,
                    Email = "admin@bb.se",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, _password);
                var test = result.Succeeded;
            }

            // TODO: Seed admin role and add _admin to this role

            return true;
        }

    }
}
