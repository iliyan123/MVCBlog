using Microsoft.Owin;
using MVCBlog.Migrations;
using MVCBlog.Models;
using Owin;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(MVCBlog.Startup))]
namespace MVCBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<BlogDbContext, Configuration>());

            ConfigureAuth(app);
        }
    }
}
