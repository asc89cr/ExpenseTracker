using ExpenseTracker.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUserAsync(RegisterModel register);
        Task<IdentityUser?> GetUserByEmailAsync(string email);
        Task<string> CheckPasswordAsync(IdentityUser user, LoginModel loginModel);
    }
}
