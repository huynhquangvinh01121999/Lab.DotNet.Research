using Hangfire.Annotations;
using Hangfire.Dashboard;
using System.Linq;

namespace Client.Middlewares
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // kiem tra user da signin chua?
            var isAuthenticated = httpContext.User.Identity?.IsAuthenticated ?? false;

            // lay ra danh sach claims
            var claims = httpContext.User.Claims.ToList();

            //var role = claims?.FirstOrDefault(x => x.Type.Equals("roles", StringComparison.OrdinalIgnoreCase))?.Value; // get the first role
            var roles = httpContext.User.Claims.Where(c => c.Type == "roles").ToList(); // get list roles

            var isRoleAdmin = roles.Where(x => x.Value.Equals("Admin")).FirstOrDefault(); // neu co role "Admin" moi duoc phep truy cap Dashboard Hangfire

            return isRoleAdmin != null ? true : false;
        }

        //public bool Authorize([NotNull] DashboardContext context)
        //{
        //    //return context.GetHttpContext().User.Identity?.IsAuthenticated ?? false;
        //    var httpContext = context.GetHttpContext();
        //    var authService = httpContext.RequestServices.GetRequiredService<IUserService>();
        //    var user = authService.GetUser();
        //    if (user != null)
        //        return true;
        //    return false;
        //}

        //public bool Authorize([NotNull] DashboardContext context)
        //{
        //    var httpContext = context.GetHttpContext();
        //    return httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole("Admin");
        //}
    }
}
