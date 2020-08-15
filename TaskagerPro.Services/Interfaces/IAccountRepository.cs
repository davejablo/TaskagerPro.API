using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskagerPro.Core.DTOs.Account;
using TaskagerPro.Core.Models;

namespace TaskagerPro.Services.Interfaces
{
    public interface IAccountRepository
    {
        Task RegisterAsync(RegisterDTO model);
        Task<TokenModel> LoginAsync(LoginDTO model);
        Task<AccountDTO> GetAccountByUsernameAsync(string username);
        Task<AccountDTO> GetAccountByIdAsync(Guid userId);
        Task UpdateAccountAsync(string username, UpdateAccountDTO model);
    }
}