using CurrencyLayerBackend.Commons.Settings;
using CurrencyLayerBackend.Core.Controllers;
using CurrencyLayerBackend.Core.Processors;
using CurrencyLayerBackend.Infrastructure.DataService;
using Nerdle.AutoConfig;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace CurrencyLayerBackend.Application.Setup
{
    public static class DependencyInjector
    {
        public static Container ApplicationContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();        

            container.RegisterSingleton<IApplicationConfig>(() =>
            {
                return AutoConfig.Map<IApplicationConfig>("applicationConfig");
            });

            container.RegisterSingleton<ICurrencyLayerEndpoints>(() =>
            {
                return AutoConfig.Map<ICurrencyLayerEndpoints>("currencyLayerEndpoints");
            });

            container.RegisterSingleton<ICurrencyLayerConfig>(() =>
            {
                return AutoConfig.Map<ICurrencyLayerConfig>("currencyLayerConfig");
            });

            //Data Service
            container.Register<ICurrencyLayerApiProvider, CurrencyLayerApiProvider>(Lifestyle.Singleton);

            //Processors
            container.Register<IHistoricalRateProcessor, HistoricalRateProcessor>(Lifestyle.Scoped);

            //Controllers
            container.Register<CurrencyRateController>(Lifestyle.Scoped);

            container.Verify();
            return container;
        }
    }
}
