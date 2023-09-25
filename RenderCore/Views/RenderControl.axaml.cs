using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Kitware.VTK;
using Prism.Ioc;
using RenderCore.Models;

namespace RenderCore.Views;

public partial class RenderControl : UserControl
{
    // Can't Use IContainerExtension to int
    // TODO: Use ViewModel Binding RenderControl
    public RenderControl()
    {
        InitializeComponent();
        
        // Init And Register Property

        //_container = container;
        _scene = Scene.DefaultScene();
        _timer = new DispatcherTimer();
        _viewer = new Viewer(VtkRenderControl.RenderWindow);
        
        // Init Viewer
        VtkRenderControl.AttachedToVisualTree += Init;
    }

    private IContainerExtension _container;
    
    private DispatcherTimer _timer;

    private Scene _scene;

    private Viewer _viewer;
    
    private vtkInteractorStyleTrackballCamera? _style = null;

    private void Init(object? _, VisualTreeAttachmentEventArgs __)
    {
        var renderWindow = VtkRenderControl.RenderWindow;
        var interactor = renderWindow?.GetInteractor();
        var renderer = vtkRenderer.New();
        renderer.SetBackgroundAlpha(1.0);
        renderer.SetGradientBackground(true);
        renderer.SetBackground(0.6, 0.6, 0.6);
        renderer.SetBackground2(0.8, 0.8, 0.8);
        renderWindow?.AddRenderer(renderer);
        interactor?.SetRenderWindow(renderWindow);
        _style = new vtkInteractorStyleTrackballCamera();
        _style.RightButtonPressEvt += (_, _) =>
        {
            VtkRenderControl.ContextFlyout?.ShowAt(VtkRenderControl, true);
        };
        interactor?.SetInteractorStyle(_style);
        vtkCubeSource cubeSource = vtkCubeSource.New();
        vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
        mapper.SetInputConnection(cubeSource.GetOutputPort());
        vtkActor actor = vtkActor.New();
        actor.SetMapper(mapper);
        renderer.AddActor(actor);
        renderWindow.Render();
    }

    private void VtkRenderControl_OnInitialized(object? sender, EventArgs e)
    {
    }
}