using KNX;
using KNX.DatapointAction;
using KNX.DatapointType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Component
{
    public class DPTHelper
    {
        public static string GetDPTName(string mNum, string sNum)
        {
            string text = "";

            foreach (var node in MyCache.NodeTypes)
            {
                text = GetDPTName(node, mNum, sNum);
                if (text.Length > 0)
                {
                    break;
                }
            }

            return text;
        }

        private static string GetDPTName(TreeNode node, string m, string s)
        {
            string text = "";
            DatapointType dpt = node as DatapointType;
            if ((m == dpt.KNXMainNumber) && (s == dpt.KNXSubNumber))
            {
                text = dpt.Text;
            }
            else if (dpt.Nodes.Count > 0)
            {
                foreach (TreeNode n in dpt.Nodes)
                {
                    text = GetDPTName(n, m, s);
                    if (text.Length > 0)
                    {
                        break;
                    }
                }
            }

            return text;
        }

        public static string GetDPTMainName(KNXDataType type)
        {
            string text = "";
            if (KNXDataType.None != type)
            {
                foreach (DatapointType node in MyCache.NodeTypes)
                {
                    if (type == node.Type)
                    {
                        text = node.Text;
                        break;
                    }
                }
            }

            return text;
        }

        public static DatapointType GetTypeNode(string text)
        {
            TreeNode tn = null;

            foreach (DatapointType node in MyCache.NodeTypes)
            {
                tn = GetNode(node, text);
                if (null != tn)
                {
                    break;
                }
            }

            return tn as DatapointType;
        }

        private static TreeNode GetNode(TreeNode node, string text)
        {
            TreeNode tn = null;

            if (text == node.Text)
            {
                tn = node;
            }
            else if (node.Nodes.Count > 0)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    tn = GetNode(n, text);
                    if (null != tn)
                    {
                        break;
                    }
                }
            }

            return tn;
        }

        public static List<DatapointActionNode> GetActionNodes(string text)
        {
            List<DatapointActionNode> listActionNodes = new List<DatapointActionNode>();
            TreeNode typeNode = null;
            foreach (TreeNode node in MyCache.NodeActions)
            {
                typeNode = GetNode(node, text);
                if (null != typeNode)
                {
                    break;
                }
            }

            if (null != typeNode)
            {
                foreach (TreeNode node in typeNode.Nodes)
                {
                    DatapointActionNode dptActNode = node as DatapointActionNode;
                    if (null != dptActNode)
                    {
                        listActionNodes.Add(dptActNode);
                    }
                    else
                    {
                        foreach (TreeNode it in node.Nodes)
                        {
                            DatapointActionNode actNode = it as DatapointActionNode;
                            if (null != actNode)
                            {
                                listActionNodes.Add(actNode);
                            }
                        }
                    }
                }
            }

            return listActionNodes;
        }
    }
}
