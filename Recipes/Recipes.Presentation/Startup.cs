using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Recipes.Presentation.Startup))]
namespace Recipes.Presentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
