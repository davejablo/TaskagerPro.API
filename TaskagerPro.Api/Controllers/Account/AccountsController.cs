using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskagerPro.Core.DTOs.Account;
using TaskagerPro.Services.Interfaces;

namespace TaskagerPro.Api.Controllers.Account
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountService;

        public AccountsController(IAccountRepository accountService)
        {
            _accountService = accountService ??
                throw new ArgumentNullException(nameof(accountService));
        }

        [HttpGet]
        public async Task<IActionResult> GetByUsernameAsync()
        {
            var username = User.Claims.Where(c => c.Type == "username").First().Value;
            var result = await _accountService.GetAccountByUsernameAsync(username);

            return Ok(result);
        }

        [HttpGet("{userId}", Name = "GetUserById")]
        public async Task<IActionResult> GetByIdAsync(Guid userId)
        {
            var userFromRepo = await _accountService.GetAccountByIdAsync(userId);
            if (userFromRepo == null)
            {
                return NotFound();
            }
            return Ok(userFromRepo);
        }

        [HttpPatch]
        public async Task<IActionResult> PatchAsync([FromBody] UpdateAccountDTO model)
        {
            var username = User.Claims.Where(c => c.Type == "username").First().Value;
            await _accountService.UpdateAccountAsync(username, model);
            var result = await _accountService.GetAccountByUsernameAsync(username);

            return Ok(result);
        }
    }
}
