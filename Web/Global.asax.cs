using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject.Web.Mvc;
using Ninject;
using System.Web.Security;
using INF = Infrastructure;
using System.Data.Common;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute
            //{
            //    ExceptionType = typeof(DbException),
            //    // DbError.cshtml is a view in the Shared folder.
            //    View = "DbError",
            //    Order = 2
            //});
            //filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Auth", action = "Login", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            //A special step only needed when using static repositories
            if (INF.ConfigReader.RepositoryMode.ToLower().Equals("static"))
            {
                Data.Repositories.Static.StaticRepositoryPopulator.Populate();
            }
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            if (INF.ConfigReader.RepositoryMode.ToLower().Equals("static"))
            {
                kernel.Load(new Web.Infrastructure.Ninject.StaticRepositoryModule());
            }
            else if (INF.ConfigReader.RepositoryMode.ToLower().Equals("pg"))
            {
                kernel.Load(new Web.Infrastructure.Ninject.PGRepositoryModule());
            }
            kernel.Load(new Web.Infrastructure.Ninject.GenericModule());
            kernel.Inject(Membership.Provider);
            kernel.Inject(Roles.Provider);
            return kernel;
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Request["RequireUploadifySessionSync"] != null)
                UploadifySessionSync();
        }

        /// <summary>
        /// Uploadify uses a Flash object to upload files. This method retrieves and hydrates Auth and Session objects when the Uploadify Flash is calling.
        /// </summary>
        /// <remarks>
        ///     Props: http://geekswithblogs.net/apopovsky/archive/2009/05/06/working-around-flash-cookie-bug-in-asp.net-mvc.aspx
        ///     More props: http://stackoverflow.com/questions/1729179/uploadify-session-and-authentication-with-asp-net-mvc
        /// </remarks>
        protected void UploadifySessionSync()
        {
            try
            {
                string session_param_name = "SessionId";
                string session_cookie_name = "ASP.NET_SessionId";

                if (HttpContext.Current.Request[session_param_name] != null)
                    UploadifyUpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
            }
            catch { }

            try
            {
                string auth_param_name = "SecurityToken";
                string auth_cookie_name = FormsAuthentication.FormsCookieName;

                if (HttpContext.Current.Request[auth_param_name] != null)
                    UploadifyUpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
            }
            catch { }
        }

        private void UploadifyUpdateCookie(string cookie_name, string cookie_value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
            if (cookie == null)
                cookie = new HttpCookie(cookie_name);
            cookie.Value = cookie_value;
            HttpContext.Current.Request.Cookies.Set(cookie);
        } 

    }
}