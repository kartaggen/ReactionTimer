using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReactionTimerWebsite.Data;
using ReactionTimerWebsite.Models;

namespace ReactionTimerWebsite.Pages
{
    public class HighScoresModel : PageModel
    {
        private readonly ReactionTimerContext context;
        public HighScoresModel(ReactionTimerContext context)
        {
            this.context = context;
        }
        public List<HighScore> HighScores { get; set; }
        public List<rowHS> rows { get; set; }
        public async Task OnGetAsync()
        {
            int userId = 0;
            string username = "";
            bool hasCurUser = false;
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
                else if (claim.Type == ClaimTypes.Name)
                {
                    username = claim.Value;
                }
            }

            rows = new List<rowHS>();
            rows.Clear();

            HighScores = await context.HighScore
                .OrderBy(i => i.highScore)
                .Take(10)
                .Include(i => i.user)
                .AsNoTracking()
                .ToListAsync();
                        
            for (int i = 1;i <= HighScores.Count; i++)
            {
                rowHS row = new rowHS();
                row.place = i;
                row.username = HighScores[i - 1].user.username;
                row.timePerTarget = HighScores[i - 1].highScore;
                if (HighScores[i - 1].user.ID == userId)
                {
                    row.isCurUser = true;
                    hasCurUser = true;
                }
                else
                    row.isCurUser = false;

                rows.Add(row);
            }

            if (!hasCurUser && userId != 0)
            {
                HighScore highScore = context.HighScore.Where(query => query.user.ID.Equals(userId))
                .Include(i => i.user).SingleOrDefault();

                if (highScore != null)
                {
                    int betterPlayers = context.HighScore.Where(query => query.highScore.CompareTo(highScore.highScore) < 0).Count();

                    rowHS rowUser = new rowHS();
                    rowUser.place = betterPlayers + 1;
                    rowUser.username = highScore.user.username;
                    rowUser.timePerTarget = highScore.highScore;
                    rowUser.isCurUser = true;

                    rows.Add(rowUser);
                }
            }
        }

        public class rowHS
        {
            [DisplayName("#")]
            public int place { get; set; }
            [DisplayName("Player")]
            public string username { get; set; }
            [DisplayName("Time per target")]
            public double timePerTarget { get; set; }
            [DisplayName("Time per target")]
            public bool isCurUser { get; set; }
        }
    }
}