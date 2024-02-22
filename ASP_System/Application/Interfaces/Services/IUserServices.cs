using Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserServices
    {
        public Task<IdentityResult> SignUpAsync(UserSignUpDTO model);
        public Task<string> SignInAsync(UserSignInDTO model);
    }
}
