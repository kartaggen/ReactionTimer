using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using ReactionTimerWebsite.Models;
using ReactionTimerWebsite.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ReactionTimerWebsite.Pages
{
	[ValidateAntiForgeryToken]
	public class IndexModel : PageModel
	{
		private readonly ReactionTimerContext context;
		private readonly ILogger<IndexModel> logger;

		public IndexModel(ILogger<IndexModel> logger, ReactionTimerContext context)
		{
			this.logger = logger;
			this.context = context;
		}

		public void OnGet()
		{
			if (!User.Identity.IsAuthenticated)
			{
				RedirectToAction("Login");
			}
		}

		public void OnPost([FromBody]double score)
		{
			int userId = 0;
			var identity = (ClaimsIdentity)User.Identity;
			IEnumerable<Claim> claims = identity.Claims;
			foreach (Claim claim in claims)
			{
				if (claim.Type == ClaimTypes.Sid)
				{
					if (int.TryParse(claim.Value, out userId))
						Console.WriteLine("String parsed successfully.");
					else
						Console.WriteLine("String could not be parsed.");
				}
			}

			User user = context.User.Where(query => query.ID.Equals(userId)).SingleOrDefault();
			HighScore highScore = context.HighScore.Where(query => query.user.ID.Equals(userId)).SingleOrDefault();

			GameScore game = new GameScore();
			game.user = user;
			game.gameScore = score;
			game.gameDate = DateTime.Now;
			context.GameScore.Add(game);
			context.SaveChanges();

			if (highScore == null)
			{
				HighScore newHighScore = new HighScore();
				newHighScore.user = user;
				newHighScore.highScore = score;
				newHighScore.scoreDate = DateTime.Now;
				context.HighScore.Add(newHighScore);
				context.SaveChanges();
			}
			else if (highScore.highScore > score)
			{
				highScore.highScore = score;
				highScore.scoreDate = DateTime.Now;
				context.Attach(highScore).State = EntityState.Modified;
				try
				{
					context.SaveChanges();
				}
				catch (DbUpdateConcurrencyException e)
				{
					throw e;
				}
			}
		}
	}
}
