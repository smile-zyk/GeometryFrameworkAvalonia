using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderCore.Models
{
    public interface IVisit
    {
        public void Visit(Node node) {}
        public void Visit(Collection node) {}
        public void Visit(MeshNode node) {}
        public void Visit(CurveNode node) {}
        public void Visit(Scene node) {}
    }
}