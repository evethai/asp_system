﻿using API.Helper;
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
            RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
            this._roleManager = roleManager;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = _userManager.Users;
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (var user in users)
            {
                UserDTO userDTO = new UserDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Birthday = user.Birthday,
                };
                userDTOs.Add(userDTO);
            }
            return Task.FromResult(userDTOs.AsEnumerable());
        }


        //public async Task LogOutAsync()
        //{
        //    await _signInManager.SignOutAsync();

        //    // Optionally, you can also sign out from external authentication providers if used
        //    //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //}

        public async Task<ProfileUserDTO> GetUserByIDlAsync(string userId)
        {
            var user = _userManager.FindByIdAsync(userId);
            var artworkList =  _unitOfWork.Repository<Artwork>().GetQueryable().Where(a => a.User.Id == user.Result.Id).ToList();
            var ArtworkDTOList = _mapper.Map<List<ArtworkDTO>>(artworkList);

            List<Artwork_Profile> artwork_Profiles = new List<Artwork_Profile>();
            foreach (var artwork in ArtworkDTOList)
            {
                var likeNumber = _unitOfWork.Repository<Like>().GetQueryable().Where(p => p.Artwork.ArtworkId == artwork.ArtworkId).Count();
                var commentNumber = _unitOfWork.Repository<Comment>().GetQueryable().Where(p => p.Artwork.ArtworkId == artwork.ArtworkId).Count();
                var artwork_Profile = new Artwork_Profile
                {
                    ArtworkId = artwork.ArtworkId,
                    Title = artwork.Title,
                    Description = artwork.Description,
                    Price = artwork.Price,
                    LikeNumber = likeNumber,
                    CommentNumber = commentNumber
                };
                artwork_Profiles.Add(artwork_Profile);
            }

            ProfileUserDTO dto = new ProfileUserDTO
            {
                FirstName = user.Result.FirstName,
                LastName = user.Result.LastName,
                Birthday = user.Result.Birthday,
                Email = user.Result.Email,
                Artworks = artwork_Profiles
            };
            return (dto);
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

                if(! await _roleManager.RoleExistsAsync(AppRole.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(AppRole.Admin));
                }
                if(model.IsAdmin)
                {
                    await _userManager.AddToRoleAsync(user, AppRole.Admin);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, AppRole.Customer);
                }

                //await _userManager.AddToRoleAsync(user, AppRole.Customer);
            }
            return result;
        }

    }
}
