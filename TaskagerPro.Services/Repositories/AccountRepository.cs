using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using TaskagerPro.Core.DTOs.Account;
using TaskagerPro.Core.Identities;
using TaskagerPro.Core.Models;
using TaskagerPro.DAL;
using TaskagerPro.Services.Interfaces;

namespace TaskagerPro.Services.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TaskagerProContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(TaskagerProContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task RegisterAsync(RegisterDTO model)
        {
            var result = await _userManager.CreateAsync(new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                AccountTypeId = model.AccountTypeId
            }, model.Password);

            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(";", result.Errors.Select(x => x.Description)));
        }

        public async Task<TokenModel> LoginAsync(LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (!result.Succeeded)
                throw new InvalidOperationException("User with given username and/or password doesn't exists");

            var user = await _userManager.FindByNameAsync(model.Username);

            return new TokenModel
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AccountType = (await _dbContext.AccountTypes.SingleAsync(at => at.Id == user.AccountTypeId)).Name,
                AccountTypeId = user.AccountTypeId
            };
        }

        public async Task<AccountDTO> GetAccountByUsernameAsync(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }
            var user = await _dbContext.Users.SingleAsync(x => x.NormalizedUserName == username.ToUpper());

            return new AccountDTO
            {
                Id = user.Id.ToString(),
                Username = user.UserName,
                Email = user.Email,
                AccountTypeId = user.AccountTypeId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                HourWage = user.HourWage,
                Sex = user.Sex
            };
        }

        public async Task<AccountDTO> GetAccountByIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return new AccountDTO
            {
                Id = user.Id.ToString(),
                Username = user.UserName,
                Email = user.Email,
                AccountTypeId = user.AccountTypeId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                HourWage = user.HourWage,
                Sex = user.Sex
            };
        }

        public async Task UpdateAccountAsync(string username, UpdateAccountDTO model)
        {
            var user = await _dbContext.Users.SingleAsync(x => x.NormalizedUserName == username.ToUpper());

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Sex = string.IsNullOrWhiteSpace(model.Sex) ? null : model.Sex.First().ToString().ToUpper();

            await _dbContext.SaveChangesAsync();
        }
    }
}