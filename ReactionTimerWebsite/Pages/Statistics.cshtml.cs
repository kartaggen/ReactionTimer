using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReactionTimerWebsite.Data;
using ReactionTimerWebsite.Models;

namespace ReactionTimerWebsite.Pages
{
    public class StatisticsModel : PageModel
    {
        private readonly ReactionTimerContext context;
        public StatisticsModel(ReactionTimerContext context)
        {
            this.context = context;
        }
        public string[] delimiters { get; set; }
        public int[] times { get; set; }
        public int totalGames { get; set; }

        public void OnGet()
		{
            delimiters = new string[40];
            for (int i = 0; i < 39; i++)
                delimiters[i] = (25*(i+1)) + "ms";
            delimiters[39] = ">1000ms";

            times = new int[40];

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

            List<GameScore> games = context.GameScore.Where(query => query.user.ID.Equals(userId)).ToList();
            totalGames = games.Count;
            foreach (GameScore game in games)
            {
                if (game.gameScore < 1000)
                    times[(int)(game.gameScore / 25)]++;
                else
                    times[39]++;
            }
        }
	}
}