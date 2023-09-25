using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RenderCore.Models
{
    public partial class Collection : Node
    {
        public override void accept(IVisit visitor)
        {
            visitor.Visit(this);
            foreach (var child in Children)
                child.accept(visitor);
        }

        [ObservableProperty]
        private BindingList<Node> _children;

        public virtual void AddChild(Node node)
        {
            Children.Add(node);
        }

        public Collection() 
        {
            Children =  new BindingList<Node>(); 
        }
    }
}