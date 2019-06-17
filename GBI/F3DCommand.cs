using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.GBI
{
    enum GBICommand
    {
        gsSPEndDisplayList,
        gsSPDisplayList,
        gsSP1Triangle,
        gsSP2Triangles,
        gsSPClearGeometryMode,
        gsSPSetGeometryMode,
        gsSPVertex,
        gsSPLight,
        gsDPSetEnvColor,
        gsDPSetTextureImage,
        gsDPLoadTextureBlock,
        gsDPSetTile
    }

    class F3DCommand
    {
        public F3DCommand(GBICommand command)
        {
            Command = command;
        }

        public F3DCommand(GBICommand command, List<F3DParam> commandparams)
        {
            Command = command;
            Params = commandparams;
        }

        public GBICommand Command;
        public List<F3DParam> Params;
    }
}
