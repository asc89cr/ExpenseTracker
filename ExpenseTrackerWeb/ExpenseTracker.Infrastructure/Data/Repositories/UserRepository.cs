using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJWTTokenService _jwtTokenService;

        public UserRepository(UserManager<IdentityUser> userManager, IJWTTokenService jWTTokenService) 
        {
            _userManager = userManager;
            _jwtTokenService = jWTTokenService;
        }

        public async Task<string> CheckPasswordAsync(IdentityUser user, LoginModel loginModel)
        {
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
                return _jwtTokenService.GenerateToken(user);

            else return default;
        }

        public async Task<IdentityUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterModel register)
        {
            var user = new IdentityUser { UserName = register.Email, Email = register.Email };
            return await _userManager.CreateAsync(user, register.Password);
        }
    }
}
