using CommunityToolkit.Mvvm.ComponentModel;
using g3;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderCore.Models
{
    public partial class Scene : Collection
    {
        public static Scene DefaultScene()
        {
            Scene scene = new Scene();
            Collection collection = new Collection();
            collection.Name = "Collection";
            scene.AddChild(collection);
            return scene;
        }

        public override void accept(IVisit visitor)
        {
            visitor.Visit(this);
            foreach (var child in Children)
                child.accept(visitor);
        }

        public Scene() { }
        
        public Scene(string path) { }
    }
}
