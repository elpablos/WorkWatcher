using Lorenzo.WorkWatcher.Common;
using Lorenzo.WorkWatcher.Core.Common;
using Lorenzo.WorkWatcher.Core.Managers;
using Lorenzo.WorkWatcher.Core.Services;
using Lorenzo.WorkWatcher.ViewModels;
using Lorenzo.WorkWatcher.Views;
using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;

namespace Lorenzo.WorkWatcher
{
    /// <summary>
    /// Vychozi nastaveni simple injectoru
    /// </summary>
    public static class SimpleInjectorContainer
    {
        /// <summary>
        /// Tvorba spoju mezi interfaci a instancemi
        /// </summary>
        /// <returns></returns>
        public static Container Build()
        {
            // inicializace kontaineru
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new LifetimeScopeLifestyle();

            // core
            container.RegisterSingleton(container);
            container.RegisterSingleton<ILogger, NLogLogger>();
            container.Register<ICommandManager, CommandManager>(Lifestyle.Scoped);
            container.Register<IWindowManager, WindowManager>(Lifestyle.Scoped);

            container.Register<IRawDataService, RawDataService>(Lifestyle.Scoped);
            container.Register<IDataManager, DataManager>(Lifestyle.Scoped);

            

            container.Register<IChartViewModel, ChartViewModel>(Lifestyle.Scoped);
            container.Register<IChartView, ChartView>(Lifestyle.Scoped);
            container.Register<IShellViewModel, ShellViewModel>(Lifestyle.Scoped);
            container.Register<IShellView, ShellView>(Lifestyle.Scoped);

            // kontrola intergity 
            container.Verify();

            return container;
        }
    }
}
