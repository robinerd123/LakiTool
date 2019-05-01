using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.OBJs.Util
{
    class OBJMdlUtil
    {
        static List<CubeDataCont> cubes;

        public static CubeData getCubeDataFromDL(string DLname)
        {
            CubeDataCont data = findDataContFromName(DLname);
            if (data.cubeDataName == null)
            {
                CubeData cd = new CubeData();
                cd.x1 = -32768;
                cd.x2 = 32767;
                cd.y1 = -32768;
                cd.y2 = 32767;
                cd.z1 = -32768;
                cd.z2 = 32767;
            }
            return data.data;
        }

        public static CubeDataCont findDataContFromName(string name)
        {
            foreach(CubeDataCont cube in cubes)
            {
                if (cube.cubeDataName == name)
                {
                    return cube;
                }
            }
            return new CubeDataCont();
        }
    }
}
