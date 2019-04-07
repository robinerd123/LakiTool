using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.OBJs.Special
{
    class SpecialConsts
    {
        // Special Preset types
        public const byte SPTYPE_NO_YROT_OR_PARAMS = 0; // object is 8-bytes long, no y-rotation or any behavior params
        public const byte SPTYPE_YROT_NO_PARAMS = 1; // object is 10-bytes long, has y-rotation but no params
        public const byte SPTYPE_PARAMS_AND_YROT = 2; // object is 12-bytes long, has y-rotation and params
        public const byte SPTYPE_UNKNOWN = 3;// object is 14-bytes long, has 3 extra bytes that get converted to floats.
        public const byte SPTYPE_DEF_PARAM_AND_YROT = 4; // object is 10-bytes long, has y-rotation and uses the default param

        public static LakiTool.OBJs.Special.Utils.GSOUtil[] SpecialConstItems =
        {
            new Utils.GSOUtil("SPTYPE_NO_YROT_OR_PARAMS", SPTYPE_NO_YROT_OR_PARAMS),
            new Utils.GSOUtil("SPTYPE_YROT_NO_PARAMS", SPTYPE_YROT_NO_PARAMS),
            new Utils.GSOUtil("SPTYPE_PARAMS_AND_YROT", SPTYPE_PARAMS_AND_YROT),
            new Utils.GSOUtil("SPTYPE_UNKNOWN", SPTYPE_UNKNOWN),
            new Utils.GSOUtil("SPTYPE_DEF_PARAM_AND_YROT", SPTYPE_DEF_PARAM_AND_YROT),
        };

        public const string specialItFile = "include/special_presets.h";
    }
}
