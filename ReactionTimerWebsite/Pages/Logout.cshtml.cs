using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ReactionTimerWebsite.Pages
{
	public class LogoutModel : PageModel
	{
		public IActionResult OnGet()
		{
			HttpContext.SignOutAsync();
			return RedirectToPage("Login");
		}
	}
}