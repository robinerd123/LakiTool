using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace LakiTool
{
    class MISCUtils
    {
        public static int ParseInt(string c)
        {
            if (c.Length == 1 || c[1] != 'x')
            {
                return int.Parse(c);
            }
            else
            {
                return int.Parse(c.Substring(2), System.Globalization.NumberStyles.HexNumber);
            }
        }

        static public string[] ParseAsmbd(string line)
        {
            return System.Text.RegularExpressions.Regex.Replace(line.Replace(",", ""), @"\s+", " ").Split(' ');
        }
    }
}
