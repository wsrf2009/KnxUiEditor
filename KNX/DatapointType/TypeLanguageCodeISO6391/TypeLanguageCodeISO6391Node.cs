using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KNX.DatapointType.TypeLanguageCodeISO6391.LanguageCodeAlpha2ASCII;

namespace KNX.DatapointType.TypeLanguageCodeISO6391
{
    class TypeLanguageCodeISO6391Node:DatapointType
    {
        public TypeLanguageCodeISO6391Node()
        {
            this.KNXMainNumber = DPT_234;
            this.DPTName = "language code ISO 639-1";
            this.Type = KNXDataType.Bit16;
        }

        public static TreeNode GetAllTypeNode()
        {
            TypeLanguageCodeISO6391Node nodeType = new TypeLanguageCodeISO6391Node();
            nodeType.Text = nodeType.KNXMainNumber + "." + nodeType.KNXSubNumber + " " + nodeType.DPTName;

            nodeType.Nodes.Add(LanguageCodeAlpha2ASCIINode.GetTypeNode());

            return nodeType;
        }
    }
}
