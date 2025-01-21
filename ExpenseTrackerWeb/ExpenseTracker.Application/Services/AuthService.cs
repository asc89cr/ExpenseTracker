using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            try
            {
                //Find User by email
                var user = await _userRepository.GetUserByEmailAsync(loginModel.Email);
                if (user == null) { throw new Exception("User not found"); }

                //check password and return jwt token
                return await _userRepository.CheckPasswordAsync(user, loginModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IdentityResult> Register(RegisterModel registerModel)
        {
            try
            {
                var registerResult = await _userRepository.RegisterUserAsync(registerModel);

                ArgumentNullException.ThrowIfNull(registerResult);

                if (registerResult == null) 
                    throw new Exception("It was not possible not register the user");
                else return registerResult;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
