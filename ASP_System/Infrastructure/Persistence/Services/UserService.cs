using API.Helper;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Firebase.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services
{
    public class UserService : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService() { }
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration,
            RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
            this._roleManager = roleManager;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();

            // Optionally, you can also sign out from external authentication providers if used
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        public Task<UserDTO> GetUserByIDlAsync(string id)
        {
            var user = _userManager.FindByIdAsync(id);

            UserDTO userDTO = new UserDTO
            {
                FirstName = user.Result.FirstName,
                LastName = user.Result.LastName,
                Birthday = user.Result.Birthday,
            };
            return Task.FromResult(userDTO);
        }

        public async Task<ApplicationUser> SignInAsync(UserSignInDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                return await _userManager.FindByEmailAsync(model.Email);
               
            }
            return null;
        }


        //public async Task<SignInResult> SignInAsync(UserSignInDTO model)
        //{
        //    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        //    return result;
        //}

        public async Task<IdentityResult> SignUpAsync(UserSignUpDTO model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if(result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(AppRole.Customer))
                {
                    await _roleManager.CreateAsync(new IdentityRole(AppRole.Customer));
                }
                await _userManager.AddToRoleAsync(user, AppRole.Customer);
            }
            return result;
        }

    }
}
