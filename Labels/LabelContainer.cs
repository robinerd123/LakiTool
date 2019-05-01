using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakiTool.Labels
{
    class LabelContainer
    {
        public List<Label> labels = new List<Label>();

        public Label findLabelFromName(string name)
        {
            foreach(Label label in labels)
            {
                if (label.labelName == name) return label;
            }
            return null;
        }
    }
}
