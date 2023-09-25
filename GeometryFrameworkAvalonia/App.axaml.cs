using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using GeometryFrameworkAvalonia.ViewModels;
using GeometryFrameworkAvalonia.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;

namespace GeometryFrameworkAvalonia
{
    public partial class App : PrismApplication
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            
            // Initializes Prism.Avalonia
            base.Initialize();
        }
        
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<RenderCore.RenderCoreModule>();
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override IAvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}