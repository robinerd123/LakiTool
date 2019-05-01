using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Mdl.OBJ.OBJFile
{
    class o
    {
        public string oname;
        public List<Mdl.OBJ.OBJFile.v> verts = new List<OBJFile.v>();
        public List<Mdl.OBJ.OBJFile.vt> coords = new List<OBJFile.vt>();
        public List<Mdl.OBJ.OBJFile.vn> normals = new List<OBJFile.vn>();
        public List<Mdl.OBJ.OBJFile.f> faces = new List<OBJFile.f>();

        private void optimizeObject()
        {

        }

        public List<string> val()
        {
            optimizeObject();

            List<string> oLines = new List<string>();
            oLines.Add("o " + oname);

            oLines.Add("#Verts");
            foreach (OBJFile.v vert in verts)
            {
                oLines.Add(vert.val());
            }
            oLines.Add("#Tex coords");
            foreach (OBJFile.vt coord in coords)
            {
                oLines.Add(coord.val());
            }

            oLines.Add("#Normals");
            foreach (OBJFile.vn normal in normals)
            {
                oLines.Add(normal.val());
            }

            oLines.Add("#Faces");
            foreach (OBJFile.f face in faces)
            {
                oLines.Add(face.val());
            }

            return oLines;
        }
    }
}