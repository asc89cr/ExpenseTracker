using ExpenseTracker.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginModel loginModel);
        Task<IdentityResult> Register(RegisterModel registerModel);
    }
}
