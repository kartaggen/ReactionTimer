using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactionTimerWebsite.Data;

namespace ReactionTimerWebsite
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();

			services.AddDbContext<ReactionTimerContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("ReactionTimerContext")));

			services.AddAuthentication(options =>
			{
				options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			}).AddCookie(options => { options.LoginPath = "/Login"; });

			services.AddMvc().AddRazorPagesOptions(options =>
			{
				options.Conventions.AuthorizeFolder("/");
				options.Conventions.AllowAnonymousToPage("/Login");
				options.Conventions.AllowAnonymousToPage("/Register");
				options.Conventions.AllowAnonymousToPage("/HighScores");
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
