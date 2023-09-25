using g3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderCore.Models
{
    public class CurveNode : Node
    {
        public override void accept(IVisit visitor)
        {
            visitor.Visit(this);
        }
    }
}
