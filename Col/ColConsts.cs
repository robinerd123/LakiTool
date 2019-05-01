using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Col
{
    class ColConsts
    {
        const short colInit = 0x0040;
        const short colTriStop = 0x0041;
        const short colEnd = 0x0042;
        const short colSpecialInit = 0x0043;
        const short colWaterBoxInit = 0x0044;

        //Collision Surface Types (shamelessly copied from Hack64 wiki)
        const short SURF_ENV_DEFAULT = 0x00;  //Environment default
        const short SURF_BURNING = 0x01;  //Burn / Frostbite
        const short SURF_04 = 0x04;  //Unused
        const short SURF_CEILING = 0x05;  //Ceiling that Mario can climb on
        const short SURF_SLOW = 0x09;  //Slow down Mario
        const short SURF_DEATH_FLOOR = 0x0A;  //Death floor
        const short SURF_CLOSE_CAM = 0x0B;  //Close camera
        const short SURF_WATER = 0x0D;  //Water
        const short SURF_WATER_FLOW = 0x0E;  //Water (flowing)
        const short SURF_INTANGIBLE = 0x12;  //Intangible (Seperates BBH mansion from merry-go-round = for room usage)
        const short SURF_SLIPPERY = 0x13;  //Slippery
        const short SURF_SLIGHT_SLIP = 0x14;  //Slippery (slightly)
        const short SURF_NO_SLIP = 0x15;  //Anti-slippery
        const short SURF_VARIED_NOISE = 0x1A;  //Varied noise depending on terrain (mostly unused)
        const short SURF_INST_WARP_1B = 0x1B;  //Instant warp to another area
        const short SURF_INST_WARP_1C = 0x1C;  //Instant warp to another area
        const short SURF_INST_WARP_1D = 0x1D;  //Instant warp to another area
        const short SURF_INST_WARP_1E = 0x1E;  //Instant warp to another area
        const short SURF_SAND_21 = 0x21;  //Sand (depth of 10 units)
        const short SURF_QUICKSAND_22 = 0x22;  //Quicksand (lethal = slow = depth of 160 units)
        const short SURF_QUICKSAND_23 = 0x23;  //Quicksand (lethal = instant)
        const short SURF_QUICKSAND_24 = 0x24;  //Moving quicksand (flowing = depth of 160 units)
        const short SURF_QUICKSAND_25 = 0x25;  //Moving quicksand (flowing = depth of 25 units)
        const short SURF_QUICKSAND_26 = 0x26;  //Moving quicksand (60 units)
        const short SURF_QUICKSAND_27 = 0x27;  //Moving quicksand (flowing = depth of 60 units)
        const short SURF_WALL_ETC = 0x28;  //Wall / Fence / Cannon
        const short SURF_NOISE_DEF = 0x29;  //Default floor with noise
        const short SURF_NOISE_SLIP = 0x2A;  //Slippery floor with noise
        const short SURF_WIND = 0x2C;  //Wind
        const short SURF_QUICKSAND_2D = 0x2D;  //Quicksand (lethal = flowing)
        const short SURF_SLIP_ICE = 0x2E;  //Slippery Ice (CCM slide)
        const short SURF_LOOK_WARP = 0x2F;  //Look up and warp (Wing cap entrance)
        const short SURF_HARD_FLOOR = 0x30;  //Hard floor (Always has fall damage)
        const short SURF_WARP = 0x32;  //Surface warp
        const short SURF_TIMER_ON = 0x33;  //Timer start (Peach's secret slide)
        const short SURF_TIMER_OFF = 0x34;  //Timer stop (Peach's secret slide)
        const short SURF_HARD_SLIP = 0x35;  //Hard and slippery (Always has fall damage)
        const short SURF_HARD = 0x36;  //Hard (Slide for CCM = Always has fall damage)
        const short SURF_NO_SLIP_ICE = 0x37;  //Non-slippery areas in ice levels
        const short SURF_DEATH_WIND = 0x38;  //Death at bottom with wind
        const short SURF_WIDE_CAM = 0x65;  //Wide camera (BOB)
        const short SURF_WALLS_66 = 0x66;  //Walls in THI area 3
        const short SURF_PYR_TOP_BOOM = 0x6E;  //Step on 4 to make Pyramid top explode
        const short SURF_CAM_6F = 0x6F;  //Camera-related (Bowser 1)
        const short SURF_CAM_70 = 0x70;  //Camera-related (BOB)
        const short SURF_NO_ACCESS = 0x72;  //Inaccessible Area = only used to restrict camera movement
        const short SURF_NOISE_SLD_73 = 0x73;  //Slide with noise (unused)
        const short SURF_NOISE_SLD_74 = 0x74;  //Slide with noise (unused)
        const short SURF_CAM_75 = 0x75;  //Camera-related (CCM)
        const short SURF_FLAG_SURF_76 = 0x76;  //Surface with flags
        const short SURF_FLAG_SURF_77 = 0x77;  //Surface with flags (unused)
        const short SURF_UNK_NOISE = 0x78;  //Possibly for camera behavior = has noise
        const short SURF_SLIPPERY_79 = 0x79;  //Slippery (for camera behavior)
        const short SURF_ACTIVATE = 0x7A;  //Activate switches or Dorrie
        const short SURF_VAN_CAP_WALL = 0x7B;  //Vanish cap walls
        const short SURF_PAINTING_A6 = 0xA6;  //Painting wobble (BoB 1)
        const short SURF_PAINTING_A7 = 0xA7;  //Painting wobble (BoB 2)
        const short SURF_PAINTING_A8 = 0xA8;  //Painting wobble (BoB 3)
        const short SURF_PAINTING_A9 = 0xA9;  //Painting wobble (CCM 1)	
        const short SURF_PAINTING_AA = 0xAA;  //Painting wobble (CCM 2)
        const short SURF_PAINTING_AB = 0xAB;  //Painting wobble (CCM 3)
        const short SURF_PAINTING_AC = 0xAC;  //Painting wobble (WF 1)
        const short SURF_PAINTING_AD = 0xAD;  //Painting wobble (WF 2)
        const short SURF_PAINTING_AE = 0xAE;  //Painting wobble (WF 3)
        const short SURF_PAINTING_AF = 0xAF;  //Painting wobble (JRB 1)
        const short SURF_PAINTING_B0 = 0xB0;  //Painting wobble (JRB 2)
        const short SURF_PAINTING_B1 = 0xB1;  //Painting wobble (JRB 3)
        const short SURF_PAINTING_B2 = 0xB2;  //Painting wobble (LLL 1)
        const short SURF_PAINTING_B3 = 0xB3;  //Painting wobble (LLL 2)
        const short SURF_PAINTING_B4 = 0xB4;  //Painting wobble (LLL 3)
        const short SURF_PAINTING_B5 = 0xB5;  //Painting wobble (SSL 1)
        const short SURF_PAINTING_B6 = 0xB6;  //Painting wobble (SSL 2)
        const short SURF_PAINTING_B7 = 0xB7;  //Painting wobble (SSL 3)
        const short SURF_PAINTING_B8 = 0xB8;  //Painting wobble (?)
        const short SURF_PAINTING_B9 = 0xB9;  //Painting wobble (?)
        const short SURF_PAINTING_BA = 0xBA;  //Painting wobble (?)
        const short SURF_PAINTING_BB = 0xBB;  //Painting wobble (BFS?)
        const short SURF_PAINTING_BC = 0xBC;  //Painting wobble (BFS?)
        const short SURF_PAINTING_BD = 0xBD;  //Painting wobble (BFS?)
        const short SURF_PAINTING_BE = 0xBE;  //Painting wobble (WDW 1)
        const short SURF_PAINTING_BF = 0xBF;  //Painting wobble (WDW 2)
        const short SURF_PAINTING_C0 = 0xC0;  //Painting wobble (WDW 3)
        const short SURF_PAINTING_C1 = 0xC1;  //Painting wobble (THI t 1)
        const short SURF_PAINTING_C2 = 0xC2;  //Painting wobble (THI t 2)
        const short SURF_PAINTING_C3 = 0xC3;  //Painting wobble (THI t 3)
        const short SURF_PAINTING_C4 = 0xC4;  //Painting wobble (TTM 1)
        const short SURF_PAINTING_C5 = 0xC5;  //Painting wobble (TTM 2)
        const short SURF_PAINTING_C6 = 0xC6;  //Painting wobble (TTM 3)
        const short SURF_PAINTING_C7 = 0xC7;  //Painting wobble (?)
        const short SURF_PAINTING_C8 = 0xC8;  //Painting wobble (?)
        const short SURF_PAINTING_C9 = 0xC9;  //Painting wobble (?)
        const short SURF_PAINTING_CA = 0xCA;  //Painting wobble (SML 1 = unused)
        const short SURF_PAINTING_CB = 0xCB;  //Painting wobble (SML 2 = unused)
        const short SURF_PAINTING_CC = 0xCC;  //Painting wobble (SML 3 = unused)
        const short SURF_PAINTING_CD = 0xCD;  //Painting wobble (THI h 1)
        const short SURF_PAINTING_CE = 0xCE;  //Painting wobble (THI h 2)
        const short SURF_PAINTING_CF = 0xCF;  //Painting wobble (THI h 3)
        const short SURF_PAINTING_D0 = 0xD0;  //Painting wobble (Metal cap?)
        const short SURF_PAINTING_D1 = 0xD1;  //Painting wobble (Metal cap?)
        const short SURF_PAINTING_D2 = 0xD2;  //Painting wobble (Metal cap?)
        const short SURF_H_LVL_EN_D3 = 0xD3;  //Horizontal level entrance
        const short SURF_H_LVL_EN_D4 = 0xD4;  //Horizontal level entrance
        const short SURF_H_LVL_EN_D5 = 0xD5;  //Horizontal level entrance
        const short SURF_H_LVL_EN_D6 = 0xD6;  //Horizontal level entrance
        const short SURF_H_LVL_EN_D7 = 0xD7;  //Horizontal level entrance
        const short SURF_H_LVL_EN_D8 = 0xD8;  //Horizontal level entrance
        const short SURF_H_LVL_EN_D9 = 0xD9;  //Horizontal level entrance
        const short SURF_H_LVL_EN_DA = 0xDA;  //Horizontal level entrance
        const short SURF_H_LVL_EN_DB = 0xDB;  //Horizontal level entrance
        const short SURF_H_LVL_EN_DC = 0xDC;  //Horizontal level entrance
        const short SURF_H_LVL_EN_DD = 0xDD;  //Horizontal level entrance
        const short SURF_H_LVL_EN_DE = 0xDE;  //Horizontal level entrance
        const short SURF_H_LVL_EN_DF = 0xDF;  //Horizontal level entrance
        const short SURF_H_LVL_EN_F0 = 0xF0;  //Horizontal level entrance
        const short SURF_H_LVL_EN_F1 = 0xF1;  //Horizontal level entrance
        const short SURF_H_LVL_EN_F2 = 0xF2;  //Horizontal level entrance
        const short SURF_H_LVL_EN_F3 = 0xF3;  //Horizontal level entrance
        const short SURF_H_LVL_EN_F4 = 0xF4;  //Horizontal level entrance
        const short SURF_H_LVL_EN_F5 = 0xF5;  //Horizontal level entrance
        const short SURF_H_LVL_EN_F6 = 0xF6;  //Horizontal level entrance
        const short SURF_H_LVL_EN_F7 = 0xF7;  //Horizontal level entrance
        const short SURF_H_LVL_EN_F8 = 0xF8;  //Horizontal level entrance
        const short SURF_H_LVL_EN_F9 = 0xF9;  //Horizontal level entrance
        const short SURF_H_LVL_EN_FA = 0xFA;  //Horizontal level entrance
        const short SURF_H_LVL_EN_FB = 0xFB;  //Horizontal level entrance
        const short SURF_H_LVL_EN_FC = 0xFC;  //Horizontal level entrance
        const short SURF_POOL_WARP = 0xFD;  //Pool warp (HMC)
        const short SURF_TRAPDOOR = 0xFF;  //Bowser 1 trapdoor

        public static LakiTool.Col.Util.GSUtil[] ColConstVals = new LakiTool.Col.Util.GSUtil[] {
            new LakiTool.Col.Util.GSUtil("colInit", colInit),
            new LakiTool.Col.Util.GSUtil("colTriStop", colTriStop),
            new LakiTool.Col.Util.GSUtil("colEnd", colEnd),
            new LakiTool.Col.Util.GSUtil("colSpecialInit", colSpecialInit),
            new LakiTool.Col.Util.GSUtil("colWaterBoxInit", colWaterBoxInit),
            new LakiTool.Col.Util.GSUtil("SURF_ENV_DEFAULT", SURF_ENV_DEFAULT),
            new LakiTool.Col.Util.GSUtil("SURF_BURNING", SURF_BURNING),
            new LakiTool.Col.Util.GSUtil("SURF_04", SURF_04),
            new LakiTool.Col.Util.GSUtil("SURF_CEILING", SURF_CEILING),
            new LakiTool.Col.Util.GSUtil("SURF_SLOW", SURF_SLOW),
            new LakiTool.Col.Util.GSUtil("SURF_DEATH_FLOOR", SURF_DEATH_FLOOR),
            new LakiTool.Col.Util.GSUtil("SURF_CLOSE_CAM", SURF_CLOSE_CAM),
            new LakiTool.Col.Util.GSUtil("SURF_WATER", SURF_WATER),
            new LakiTool.Col.Util.GSUtil("SURF_WATER_FLOW", SURF_WATER_FLOW),
            new LakiTool.Col.Util.GSUtil("SURF_INTANGIBLE", SURF_INTANGIBLE),
            new LakiTool.Col.Util.GSUtil("SURF_SLIPPERY", SURF_SLIPPERY),
            new LakiTool.Col.Util.GSUtil("SURF_SLIGHT_SLIP", SURF_SLIGHT_SLIP),
            new LakiTool.Col.Util.GSUtil("SURF_NO_SLIP", SURF_NO_SLIP),
            new LakiTool.Col.Util.GSUtil("SURF_VARIED_NOISE", SURF_VARIED_NOISE),
            new LakiTool.Col.Util.GSUtil("SURF_INST_WARP_1B", SURF_INST_WARP_1B),
            new LakiTool.Col.Util.GSUtil("SURF_INST_WARP_1C", SURF_INST_WARP_1C),
            new LakiTool.Col.Util.GSUtil("SURF_INST_WARP_1D", SURF_INST_WARP_1D),
            new LakiTool.Col.Util.GSUtil("SURF_INST_WARP_1E", SURF_INST_WARP_1E),
            new LakiTool.Col.Util.GSUtil("SURF_SAND_21", SURF_SAND_21),
            new LakiTool.Col.Util.GSUtil("SURF_QUICKSAND_22", SURF_QUICKSAND_22),
            new LakiTool.Col.Util.GSUtil("SURF_QUICKSAND_23", SURF_QUICKSAND_23),
            new LakiTool.Col.Util.GSUtil("SURF_QUICKSAND_24", SURF_QUICKSAND_24),
            new LakiTool.Col.Util.GSUtil("SURF_QUICKSAND_25", SURF_QUICKSAND_25),
            new LakiTool.Col.Util.GSUtil("SURF_QUICKSAND_26", SURF_QUICKSAND_26),
            new LakiTool.Col.Util.GSUtil("SURF_QUICKSAND_27", SURF_QUICKSAND_27),
            new LakiTool.Col.Util.GSUtil("SURF_WALL_ETC", SURF_WALL_ETC),
            new LakiTool.Col.Util.GSUtil("SURF_NOISE_DEF", SURF_NOISE_DEF),
            new LakiTool.Col.Util.GSUtil("SURF_NOISE_SLIP", SURF_NOISE_SLIP),
            new LakiTool.Col.Util.GSUtil("SURF_WIND", SURF_WIND),
            new LakiTool.Col.Util.GSUtil("SURF_QUICKSAND_2D", SURF_QUICKSAND_2D),
            new LakiTool.Col.Util.GSUtil("SURF_SLIP_ICE", SURF_SLIP_ICE),
            new LakiTool.Col.Util.GSUtil("SURF_LOOK_WARP", SURF_LOOK_WARP),
            new LakiTool.Col.Util.GSUtil("SURF_HARD_FLOOR", SURF_HARD_FLOOR),
            new LakiTool.Col.Util.GSUtil("SURF_WARP", SURF_WARP),
            new LakiTool.Col.Util.GSUtil("SURF_TIMER_ON", SURF_TIMER_ON),
            new LakiTool.Col.Util.GSUtil("SURF_TIMER_OFF", SURF_TIMER_OFF),
            new LakiTool.Col.Util.GSUtil("SURF_HARD_SLIP", SURF_HARD_SLIP),
            new LakiTool.Col.Util.GSUtil("SURF_HARD", SURF_HARD),
            new LakiTool.Col.Util.GSUtil("SURF_NO_SLIP_ICE", SURF_NO_SLIP_ICE),
            new LakiTool.Col.Util.GSUtil("SURF_DEATH_WIND", SURF_DEATH_WIND),
            new LakiTool.Col.Util.GSUtil("SURF_WIDE_CAM", SURF_WIDE_CAM),
            new LakiTool.Col.Util.GSUtil("SURF_WALLS_66", SURF_WALLS_66),
            new LakiTool.Col.Util.GSUtil("SURF_PYR_TOP_BOOM", SURF_PYR_TOP_BOOM),
            new LakiTool.Col.Util.GSUtil("SURF_CAM_6F", SURF_CAM_6F),
            new LakiTool.Col.Util.GSUtil("SURF_CAM_70", SURF_CAM_70),
            new LakiTool.Col.Util.GSUtil("SURF_NO_ACCESS", SURF_NO_ACCESS),
            new LakiTool.Col.Util.GSUtil("SURF_NOISE_SLD_73", SURF_NOISE_SLD_73),
            new LakiTool.Col.Util.GSUtil("SURF_NOISE_SLD_74", SURF_NOISE_SLD_74),
            new LakiTool.Col.Util.GSUtil("SURF_CAM_75", SURF_CAM_75),
            new LakiTool.Col.Util.GSUtil("SURF_FLAG_SURF_76", SURF_FLAG_SURF_76),
            new LakiTool.Col.Util.GSUtil("SURF_FLAG_SURF_77", SURF_FLAG_SURF_77),
            new LakiTool.Col.Util.GSUtil("SURF_UNK_NOISE", SURF_UNK_NOISE),
            new LakiTool.Col.Util.GSUtil("SURF_SLIPPERY_79", SURF_SLIPPERY_79),
            new LakiTool.Col.Util.GSUtil("SURF_ACTIVATE", SURF_ACTIVATE),
            new LakiTool.Col.Util.GSUtil("SURF_VAN_CAP_WALL", SURF_VAN_CAP_WALL),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_A6", SURF_PAINTING_A6),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_A7", SURF_PAINTING_A7),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_A8", SURF_PAINTING_A8),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_A9", SURF_PAINTING_A9),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_AA", SURF_PAINTING_AA),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_AB", SURF_PAINTING_AB),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_AC", SURF_PAINTING_AC),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_AD", SURF_PAINTING_AD),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_AE", SURF_PAINTING_AE),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_AF", SURF_PAINTING_AF),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_B0", SURF_PAINTING_B0),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_B1", SURF_PAINTING_B1),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_B2", SURF_PAINTING_B2),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_B3", SURF_PAINTING_B3),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_B4", SURF_PAINTING_B4),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_B5", SURF_PAINTING_B5),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_B6", SURF_PAINTING_B6),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_B7", SURF_PAINTING_B7),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_B8", SURF_PAINTING_B8),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_B9", SURF_PAINTING_B9),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_BA", SURF_PAINTING_BA),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_BB", SURF_PAINTING_BB),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_BC", SURF_PAINTING_BC),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_BD", SURF_PAINTING_BD),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_BE", SURF_PAINTING_BE),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_BF", SURF_PAINTING_BF),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_C0", SURF_PAINTING_C0),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_C1", SURF_PAINTING_C1),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_C2", SURF_PAINTING_C2),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_C3", SURF_PAINTING_C3),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_C4", SURF_PAINTING_C4),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_C5", SURF_PAINTING_C5),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_C6", SURF_PAINTING_C6),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_C7", SURF_PAINTING_C7),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_C8", SURF_PAINTING_C8),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_C9", SURF_PAINTING_C9),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_CA", SURF_PAINTING_CA),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_CB", SURF_PAINTING_CB),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_CC", SURF_PAINTING_CC),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_CD", SURF_PAINTING_CD),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_CE", SURF_PAINTING_CE),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_CF", SURF_PAINTING_CF),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_D0", SURF_PAINTING_D0),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_D1", SURF_PAINTING_D1),
            new LakiTool.Col.Util.GSUtil("SURF_PAINTING_D2", SURF_PAINTING_D2),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_D3", SURF_H_LVL_EN_D3),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_D4", SURF_H_LVL_EN_D4),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_D5", SURF_H_LVL_EN_D5),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_D6", SURF_H_LVL_EN_D6),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_D7", SURF_H_LVL_EN_D7),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_D8", SURF_H_LVL_EN_D8),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_D9", SURF_H_LVL_EN_D9),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_DA", SURF_H_LVL_EN_DA),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_DB", SURF_H_LVL_EN_DB),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_DC", SURF_H_LVL_EN_DC),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_DD", SURF_H_LVL_EN_DD),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_DE", SURF_H_LVL_EN_DE),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_DF", SURF_H_LVL_EN_DF),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_F0", SURF_H_LVL_EN_F0),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_F1", SURF_H_LVL_EN_F1),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_F2", SURF_H_LVL_EN_F2),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_F3", SURF_H_LVL_EN_F3),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_F4", SURF_H_LVL_EN_F4),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_F5", SURF_H_LVL_EN_F5),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_F6", SURF_H_LVL_EN_F6),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_F7", SURF_H_LVL_EN_F7),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_F8", SURF_H_LVL_EN_F8),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_F9", SURF_H_LVL_EN_F9),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_FA", SURF_H_LVL_EN_FA),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_FB", SURF_H_LVL_EN_FB),
            new LakiTool.Col.Util.GSUtil("SURF_H_LVL_EN_FC", SURF_H_LVL_EN_FC),
            new LakiTool.Col.Util.GSUtil("SURF_POOL_WARP", SURF_POOL_WARP),
            new LakiTool.Col.Util.GSUtil("SURF_TRAPDOOR", SURF_TRAPDOOR)
        };
    }
}
