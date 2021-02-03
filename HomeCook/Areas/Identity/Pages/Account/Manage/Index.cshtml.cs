using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HC.Model;
using HC.Ultility;
using HomeCook.Areas.Extension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeCook.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
             IWebHostEnvironment hostEnviroment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostEnvironment = hostEnviroment; 
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }


        public string AvatarPath { get; set; }
        public class InputModel
        {
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

         
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

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Email = user.Email,
                Name = user.Name,
                Country = user.Country,
                State = user.State,
                Address1 = user.Address1,
                Address2 = user.Address2,
                City = user.City,
                PostCode = user.PostCode,
                PhoneNumber = user.PhoneNumber,
                AvartarUrl = user.AvartarUrl,
            };
            AvatarPath = PathConfiguration.GetAvatarStoreFolder(); 
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync((ApplicationUser)user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var updateduser = (ApplicationUser)user;
            updateduser.Name = Input.Name;
            updateduser.Country = Input.Country;
            updateduser.State = Input.State;
            updateduser.Address1 = Input.Address1;
            updateduser.Address2 = Input.Address2;
            updateduser.City = Input.City;
            updateduser.PostCode = Input.PostCode;
            updateduser.PhoneNumber = Input.PhoneNumber;

            // Process the avatar
            if (Input.AvartarUrl != null && Input.AvartarUrl.Length > 0 &&  updateduser.AvartarUrl != Input.AvartarUrl)
            {
                //Avatar is changed
            
                string folderName = HttpContext.Session.GetObject<string>(SessionType.UploadImage);
                string savedAvatarFileName = ImageManagment.SaveAvatarPicToServer(_hostEnvironment, folderName, Input.AvartarUrl);
                if (savedAvatarFileName.Length > 0)
                {
                    //Delete the old avatar on server 
                    if (updateduser.AvartarUrl != null && updateduser.AvartarUrl.Length > 0) {
                        ImageManagment.DeleteOldAvatar(_hostEnvironment, updateduser.AvartarUrl);
                    }
                    updateduser.AvartarUrl = savedAvatarFileName;
                   
                }
            

            }

          
            if (!ModelState.IsValid)
            {
                                
                await LoadAsync(updateduser);
                return Page();
            }

            var result = await _userManager.UpdateAsync(updateduser);
            if (result.Succeeded == false)
            {
                throw new InvalidOperationException($"Unexpected error occurred on updatting profile of  '{updateduser.Id}'.");
            }
            
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
