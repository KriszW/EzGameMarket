﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using IdentityService.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using IdentityService.API.Models.IdentityViewModels;
using IdentityService.API.Services;
using IdentityService.API.Models.IdentityModels;
using Microsoft.AspNetCore.Http;

namespace IdentityService.API.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IRegisterService _registerService;
        private readonly IIdentityService _identityService;

        public RegisterModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IRegisterService registerService,
            IIdentityService identityService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _registerService = registerService;
            _identityService = identityService;
        }

        [BindProperty]
        public RegisterServiceModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<bool> ValidateUser()
        {
            var userWithUserName = await _userManager.FindByNameAsync(Input.UserName);

            return userWithUserName != default;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var regResult = await _registerService.RegisterAsync(Input);

                await _registerService.SendVerification(new RegisterVerificationModel() { Model = Input, Request = Request, Url = Url, User = regResult.NewUser });

                if (regResult.Result.Succeeded)
                {
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(regResult.NewUser, isPersistent: false);

                        var token = await _identityService.CreateToken(regResult.NewUser);

                        HttpContext.Response.Cookies.Append("access_token", token, new CookieOptions() { HttpOnly = true, Secure = true });

                        return LocalRedirect(returnUrl);
                    }
                }
                else
                {
                    foreach (var error in regResult.Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
