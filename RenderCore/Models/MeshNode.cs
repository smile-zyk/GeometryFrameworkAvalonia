using g3;
using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderCore.Models
{
    public class MeshNode : Node
    {
        public DMesh3 Mesh { get; private set; }
        public vtkActor Actor { get; private set; }
        public string FilePath { get; private set; }

        // vtk 临时数据
        private vtkPolyData VtkPolyData;
        private vtkPolyDataMapper VtkMapper;
        
        // vtk 顶点数据
        private vtkPoints VtkPoints;
        // vtk 拓扑结构
        private vtkCellArray VtkCellArray;

        public override void accept(IVisit visitor)
        {
            visitor.Visit(this);
        }

        public MeshNode(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException();
            FilePath = path;
            string extension = Path.GetExtension(path);
            vtkAbstractPolyDataReader fileReader;
            switch (extension)
            {
                case ".obj":
                    fileReader = new vtkOBJReader();
                    break;
                case ".stl":
                    fileReader = new vtkSTLReader();
                    break;
                case ".ply":
                    fileReader = new vtkPLYReader();
                    break;
                default:
                    throw new FormatException();
            }
            fileReader.SetFileName(path);
            // 这个一定要加!
            fileReader.Update();
            Name = Path.GetFileName(path);
            VtkMapper = vtkPolyDataMapper.New();
            Actor = vtkActor.New();

            if (extension != ".stl")
            {
                vtkTriangleFilter filter = new vtkTriangleFilter();
                filter.SetInputConnection(fileReader.GetOutputPort());
                filter.Update();
                VtkMapper.SetInputConnection(filter.GetOutputPort());
            }
            else
                VtkMapper.SetInputConnection(fileReader.GetOutputPort());
            
            Actor.SetMapper(VtkMapper);
            VtkPolyData = vtkPolyData.SafeDownCast(VtkMapper.GetInput());
            VtkPoints = VtkPolyData.GetPoints();
            VtkCellArray = VtkPolyData.GetPolys();

            Mesh = new DMesh3();

            int pointNumber = (int)VtkPoints.GetNumberOfPoints();
            for (int i = 0; i < pointNumber; i++)
            {
                Mesh.AppendVertex(new Vector3d(VtkPoints.GetPoint(i)));
            }

            int cellNumber = (int)VtkCellArray.GetNumberOfCells();
            
            for (int i = 0; i < cellNumber; i++)
            {
                vtkCell cell = VtkPolyData.GetCell(i);
                vtkIdList idList = cell.GetPointIds();
                if (idList.GetNumberOfIds() != 3)
                    continue;
                Mesh.AppendTriangle((int)idList.GetId(0), (int)idList.GetId(1), (int)idList.GetId(2));
            }
        }

        public MeshNode(DMesh3 mesh)
        {
            Mesh = mesh;
        }

        public MeshNode(vtkActor actor)
        {
            Actor = actor;
        }

        public void MeshModified()
        {
        }

        public void ActorModified()
        {
            
        }
    }
}
