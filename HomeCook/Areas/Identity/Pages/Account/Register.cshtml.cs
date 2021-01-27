using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HC.Model;
using HC.Ultility;
using HomeCook.Areas.Extension;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace HomeCook.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
     //   private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
       //     IEmailSender emailSender, 
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment hostEnviroment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
       //     _emailSender = emailSender;
            _roleManager = roleManager;
            _hostEnvironment = hostEnviroment; 
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [MaxLength(50)]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Display(Name = "Address 1")]
            [MaxLength(50)]
            public string Address1 { get; set; }
            [Display(Name = "Address 2")]
            [MaxLength(50)]
            public string Address2 { get; set; }

            [Display(Name = "City")]
            [MaxLength(50)]
            public string City { get; set; }

            [Display(Name = "State")]
            [MaxLength(2)]
            public string State { get; set; }

            [Display(Name = "Zip Code")]
            [MaxLength(5)]
            public string PostCode { get; set; }

            [Required]
            [Display(Name = "Avatar")]
            [MaxLength(100)]
            public string AvartarUrl { get; set; }

      
            [Display(Name = "Country")]
            [MaxLength(50)]
            public string Country { get; set; }


            [Required]
            [Display(Name = "Phone Number")]
            [MaxLength(50)]
            public string PhoneNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.Name,
                    Country = Input.Country,
                    State = Input.State,
                    Address1 = Input.Address1,
                    Address2 = Input.Address2,
                    City = Input.City,
                    PostCode = Input.PostCode,
                    PhoneNumber = Input.PhoneNumber,
                    AvartarUrl = "Avatar.jpg",
                    
                };

               
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    //Store the avatar
                    if (Input.AvartarUrl != null && Input.AvartarUrl.Length > 0)
                    {

                        string contextName = "UPLOAD_PICS"; 
                        string folderName = HttpContext.Session.GetObject<string>(contextName);
                        string savedAvatarFileName = ImageManagment.SaveAvatarPicToServer(_hostEnvironment, folderName, Input.AvartarUrl);
                        if (savedAvatarFileName.Length > 0)
                        {
                            user.AvartarUrl = savedAvatarFileName; 
                            await _userManager.UpdateAsync(user);
                        }
                        
                    }
                    

                   
                    _logger.LogInformation("User created a new account with password.");

                    //Update the role 
                    string role = Request.Form["rdUserRole"].ToString();
                    if (role == UserType.AdminRole || role == UserType.SupplierRole
                        || role == UserType.CustomerRole)
                    {
                        await _userManager.AddToRoleAsync(user, role); 
                    }

                    // Process the avartar if have 
                    

/*                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
*/
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                      //  await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

       

    }
}
