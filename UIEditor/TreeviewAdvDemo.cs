using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aga.Controls.Tree;

namespace UIEditor
{
    public partial class TreeviewAdvDemo : Form
    {
        private readonly TreeModel _model;
        private TreeViewAdv treeViewAdv1 = new TreeViewAdv();

        public TreeviewAdvDemo()
        {
            InitializeComponent();


            var nodeTextBox1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            var nodeStateIcon1 = new Aga.Controls.Tree.NodeControls.NodeStateIcon();

            treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            treeViewAdv1.DefaultToolTipProvider = null;
            treeViewAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
            treeViewAdv1.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            treeViewAdv1.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
            treeViewAdv1.Location = new System.Drawing.Point(3, 33);
            treeViewAdv1.Model = null;
            treeViewAdv1.Name = "treeViewAdv1";
            treeViewAdv1.NodeControls.Add(nodeStateIcon1);
            treeViewAdv1.NodeControls.Add(nodeTextBox1);
            treeViewAdv1.NodeFilter = null;
            treeViewAdv1.SelectedNode = null;
            treeViewAdv1.TabIndex = 0;
            treeViewAdv1.Text = "treeViewAdv1";

            _model = new TreeModel();
            InsertNode();

            this.Controls.Add(treeViewAdv1);
        }

        private void InsertNode()
        {
            this._model.Nodes.Add(new Node("Root0"));
            this._model.Nodes.Add(new Node("Root1"));
            this._model.Nodes[1].Nodes.Add(new Node("Child0"));
            this._model.Nodes[1].Nodes.Add(new Node("Child1"));
            this._model.Nodes[1].Nodes.Add(new Node("Child2"));
            this._model.Nodes[1].Nodes.Add(new Node("Child3"));
            this._model.Nodes[1].Nodes.Add(new Node("Child4"));
            this._model.Nodes[1].Nodes.Add(new Node("Child5"));
            this._model.Nodes.Add(new Node("Root2"));
            this._model.Nodes.Add(new Node("Root3"));
            this._model.Nodes.Add(new Node("Root4"));
            this._model.Nodes.Add(new Node("Root5"));
            this._model.Nodes[5].Nodes.Add(new Node("Child0"));
            this._model.Nodes[5].Nodes.Add(new Node("Child1"));
            this._model.Nodes[5].Nodes.Add(new Node("Child2"));
            this._model.Nodes.Add(new Node("Root6"));
            this._model.Nodes.Add(new Node("Root7"));
            this._model.Nodes.Add(new Node("Root8"));
            this._model.Nodes.Add(new Node("Root9"));
            this._model.Nodes.Add(new Node("Root10"));
            this._model.Nodes.Add(new Node("Root11"));
            this._model.Nodes.Add(new Node("Root12"));
            this._model.Nodes.Add(new Node("Root13"));
            this._model.Nodes.Add(new Node("Root14"));

            this.treeViewAdv1.Model = this._model;
            this.treeViewAdv1.NodeFilter = filter;

            //this.model.Nodes[1].Nodes[1].IsHidden = true;
            //this.model.Nodes[1].Nodes[2].IsHidden = true;
            //this.model.Nodes[1].Nodes[3].IsHidden = true;
            //this.model.Nodes[5].IsHidden = true;
            //this.model.Nodes[6].IsHidden = true;
        }

        private bool filter(object obj)
        {
            TreeNodeAdv viewNode = obj as TreeNodeAdv;
            Node n = viewNode != null ? viewNode.Tag as Node : obj as Node;
            return n == null || n.Text.ToUpper().Contains("") || n.Nodes.Any(filter);
        }

    }
}
