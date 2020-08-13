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
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var username = User.Claims.Where(c => c.Type == "username").First().Value;
            var result = await _accountService.GetAccountByUsernameAsync(username);

            return Ok(result);
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
