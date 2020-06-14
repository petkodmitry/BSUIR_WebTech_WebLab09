using Microsoft.AspNetCore.Builder;
using WebLab.Middleware;

namespace WebLab.Extensions
{
	public static class AppExtensions {
		public static IApplicationBuilder UseLogging(this IApplicationBuilder app) {
			return app.UseMiddleware<LogMiddleware>();
		}
	}
}
