using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderCore.Models
{
    public abstract partial class Node : ObservableRecipient
    {
        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public Guid id;

        public virtual void accept(IVisit visitor)
        {
            visitor.Visit(this);
        }

        public Node()
        {
            id = Guid.NewGuid();
        }
    }
}
