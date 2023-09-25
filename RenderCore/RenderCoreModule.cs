using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using RenderCore.Views;

namespace RenderCore;

public class RenderCoreModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        var regionManager = containerProvider.Resolve<IRegionManager>();
        regionManager.RegisterViewWithRegion<RenderControl>("RenderControlRegion");
    }
}