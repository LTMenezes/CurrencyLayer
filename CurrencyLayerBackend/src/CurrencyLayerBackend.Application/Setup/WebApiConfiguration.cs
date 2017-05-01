using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;

namespace CurrencyLayerBackend.Application.Setup
{
    public class WebApiConfiguration
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var container = DependencyInjector.ApplicationContainer();
            var config = this.GetHttpConfiguration(container);

            SetContainerAsMiddleware(ref appBuilder, container);

            config.EnsureInitialized();
            appBuilder.UseWebApi(config);
        }

        private HttpConfiguration GetHttpConfiguration(Container container)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.EnableCors();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            return config;
        }

        private void SetContainerAsMiddleware(ref IAppBuilder appBuilder, Container container)
        {
            appBuilder.Use(async (context, next) =>
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });
        }
    }
}
