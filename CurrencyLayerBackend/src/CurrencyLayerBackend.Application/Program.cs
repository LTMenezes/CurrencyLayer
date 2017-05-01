using CurrencyLayerBackend.Application.Setup;
using CurrencyLayerBackend.Commons.Settings;
using Microsoft.Owin.Hosting;
using System;

namespace CurrencyLayerBackend.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = DependencyInjector.ApplicationContainer().GetInstance<IApplicationConfig>().BaseAddress;
            WebApp.Start<WebApiConfiguration>(url: baseAddress);
            Console.ReadLine();
        }
    }
}
