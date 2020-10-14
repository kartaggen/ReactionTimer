using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ReactionTimerWebsite.Data;
using ReactionTimerWebsite.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ReactionTimerWebsite.Pages
{
	[ValidateAntiForgeryToken]
	public class LoginModel : PageModel
	{
		private readonly ReactionTimerContext context;
		public LoginModel(ReactionTimerContext context)
		{
			this.context = context;
		}

		[BindProperty]
		[Required]
		[MaxLength(20, ErrorMessage = "Wrong username!")]
		[Display(Name = "Username")]
		public string Username { get; set; }

		[BindProperty]
		[Required]
		[MaxLength(20, ErrorMessage = "Wrong password!")]
		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				User curUser = IsValidUser();
				if (curUser == null)
					return Page();
				
				var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
				identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Username));
				identity.AddClaim(new Claim(ClaimTypes.Name, Username));
				identity.AddClaim(new Claim(ClaimTypes.Sid, curUser.ID + ""));

				var principal = new ClaimsPrincipal(identity);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = false });
				return RedirectToPage("Index");
			}
			else
			{
				ModelState.AddModelError("", "Username or password is blank!");
				return Page();
			}
		}

		public User IsValidUser()
		{
			User user = context.User.Where(query => query.username.Equals(Username)).SingleOrDefault();
			if (user == null)
			{
				ModelState.AddModelError("", "Username does not exist!");
				return null;
			}
			if (BCrypt.Net.BCrypt.Verify(Password, user.password))
				return user;
			else
			{
				ModelState.AddModelError("", "Password is invalid!");
				return null;
			}
		}
		public async Task<IActionResult> LogoutAsync()
		{
			await HttpContext.SignOutAsync();
			return RedirectToPage("Login");
		}
	}
}