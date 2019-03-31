using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool
{
    class GBIConsts
    {
        public const uint G_SHADE = 0x4;
        public const uint G_LIGHTING = 0x20000;
        public static GMUtil[] G_MODES = {
                                            new GMUtil("G_SHADE", G_SHADE),
                                            new GMUtil("G_LIGHTING", G_LIGHTING)
                                         };

        public const uint G_TX_WRAP = 0;
        public const uint G_TX_NOMIRROR = 0;
        public const uint G_TX_MIRROR = 1;
        public const uint G_TX_CLAMP = 2;
        public static GMUtil[] G_TX_MODES = {
                                            new GMUtil("G_TX_WRAP", G_TX_WRAP),
                                            new GMUtil("G_TX_NOMIRROR", G_TX_NOMIRROR),
                                            new GMUtil("G_TX_MIRROR", G_TX_MIRROR),
                                            new GMUtil("G_TX_CLAMP", G_TX_CLAMP)
                                         };
    }
}
