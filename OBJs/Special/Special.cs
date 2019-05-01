using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.OBJs.Special
{
    class Special
    {        
        public byte preset_id;
        public byte type;      // Determines whether object is 8, 10, 12 or 14 bytes long.
        public byte defParam;  // Default parameter, only used when type is SPTYPE_DEF_PARAM_AND_YROT
        public string modelName;
        public string behaviorName;

        public string GeoName()
        {
            //System.Windows.Forms.MessageBox.Show(modelName);
            return OBJs.Util.ObjUtil.getGeoNameFromModelName(modelName);
        }
    }
}
