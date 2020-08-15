using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskagerPro.Core.DTOs.Account;
using TaskagerPro.Services.Interfaces;

namespace TaskagerPro.Api.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IAccountRepository _accountService;

        public RegisterController(IAccountRepository accountService)
        {
            _accountService = accountService ??
                throw new ArgumentNullException(nameof(accountService));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);
            try
            {
                await _accountService.RegisterAsync(model);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
            return Ok();
        }
    }
}