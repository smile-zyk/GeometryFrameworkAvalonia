using CommunityToolkit.Mvvm.ComponentModel;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderCore.Models
{
    public class Viewer : ObservableRecipient, IVisit
    {
        private vtkRenderWindow _renderWindow;
        
        // 场景渲染器
        private vtkRenderer _sceneRenderer;

        // 交互渲染器
        private vtkRenderer _interactionRenderer;

        public Viewer(vtkRenderWindow renderWindow)
        {
            _renderWindow = renderWindow;
            _sceneRenderer = vtkRenderer.New();
            _interactionRenderer = vtkRenderer.New();
            _renderWindow.SetNumberOfLayers(2);
            _sceneRenderer.SetLayer(0);
            _interactionRenderer.SetLayer(1);
        }

        public void Init(vtkRenderWindow window)
        {
            _renderWindow = window;
            _renderWindow.SetNumberOfLayers(2);
            _sceneRenderer = _renderWindow.GetRenderers().GetFirstRenderer();
            _sceneRenderer.SetLayer(0);
            _sceneRenderer.SetBackground(0.75, 0.75, 0.75);
            _interactionRenderer = vtkRenderer.New();
            _interactionRenderer.SetLayer(1);
            _interactionRenderer.SetActiveCamera(_sceneRenderer.GetActiveCamera());
        }

        public void Visit(Scene scene)
        {
        }

        public void Visit(MeshNode node)
        {
            if(_sceneRenderer.HasViewProp(node.Actor) == 0)
                _sceneRenderer.AddActor(node.Actor);
        }
    }
}
