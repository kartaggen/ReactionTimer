using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReactionTimerWebsite.Models;
using ReactionTimerWebsite.Data;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace ReactionTimerWebsite.Pages
{
	[ValidateAntiForgeryToken]
	public class RegisterModel : PageModel
	{
		private readonly ReactionTimerContext context;

		public RegisterModel(ReactionTimerContext context)
		{
			this.context = context;
		}


		[BindProperty]
		[Required]
		[MaxLength(20, ErrorMessage = "First name must be up to 20 characters!")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[BindProperty]
		[Required]
		[MaxLength(20, ErrorMessage = "Last name must be up to 20 characters!")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[BindProperty]
		[Required]
		[MaxLength(20, ErrorMessage = "Username must be up to 20 characters!")]
		[Display(Name = "Username")]
		public string Username { get; set; }

		[BindProperty]
		[Required]
		[MaxLength(20, ErrorMessage = "Password must be up to 20 characters!")]
		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		//[BindProperty]
		[Required]
		[Display(Name = "Confirm Password")]
		[Compare(nameof(Password), ErrorMessage = "Passwords must match!")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				if (!userExists())
				{
					const int workFactor = 13;
					var hashed = BCrypt.Net.BCrypt.HashPassword(Password, workFactor);

					User regUser = new User();
					regUser.firstName = FirstName;
					regUser.lastName = LastName;
					regUser.username = Username;
					regUser.password = hashed;
					regUser.regDate = System.DateTime.Now;

					context.User.Add(regUser);
					await context.SaveChangesAsync();

					ViewData["Success"] = "Registration successful! Please log in.";
					ViewData["Error"] = "";
					return Page();
				}
				else
				{
					ModelState.AddModelError("", "User already exists!");
					return Page();
				}
			}
			else
			{
				return Page();
			}
		}
		public bool userExists()
		{
			User user = context.User.Where(query => query.username.Equals(Username)).SingleOrDefault();

			return user != null;
		}
		public async Task<IActionResult> LogoutAsync()
		{
			await HttpContext.SignOutAsync();
			return RedirectToPage("Login");
		}
	}
}