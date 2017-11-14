using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.Entity;
using System.Reflection;
using UIEditor.CommandManager;
using UIEditor.CommandManager.CommandNodeProperty;
using UIEditor.Properties;
using UIEditor.Component;

namespace UIEditor.UserUIControl
{
    public partial class UCProperty : UserControl
    {
        #region 变量定义
        private ViewNode CurNode { get; set; }
        private CommandQuene cqp { get; set; }
        #endregion

        #region 通知定义
        public delegate void NodePropertyChangeEvent(object sender, UCPropertyChangedEventArgs e);
        public event NodePropertyChangeEvent NodePropertyChange;
        #endregion

        #region 构造函数
        public UCProperty()
        {
            InitializeComponent();
        }
        #endregion

        #region 公共方法
        public void DisplayNode(ViewNode node)
        {
            try
            {
                if (null != node)
                {
                    this.CurNode = node;
                    this.propertyGrid.SelectedObject = node;
                    //node.Tag = this.propertyGrid;
                    SetPropertyGridTitle(node);
                }
                else
                {
                    this.CurNode = null;
                    this.propertyGrid.SelectedObject = null;
                    SetPropertyGridTitle(null);
                    SetCommandQueue(null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SetCommandQueue(CommandQuene cq)
        {
            this.cqp = cq;
        }

        public void RefreshNodeProperty()
        {
            if (null != this.CurNode)
            {
                this.propertyGrid.Refresh();
                SetPropertyGridTitle(this.CurNode);
            }
        }
        #endregion

        #region 私有方法
        #region 事件通知
        private void NodePropertyChangeNotify(object sender, UCPropertyChangedEventArgs e)
        {
            if (null != this.NodePropertyChange)
            {
                NodePropertyChange(sender, e);
            }
        }
        #endregion

        #region 属性网格事件
        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            try
            {
                //var item = propertyGrid.SelectedObject;
                //var item = propertyGrid.SelectedGridItem.Parent.Value;
                var gi = propertyGrid.SelectedGridItem.Parent;
                while (null == gi.Value)
                {
                    gi = gi.Parent;
                }

                var item = gi.Value;

                var propertyName = e.ChangedItem.PropertyDescriptor.Name;
                PropertyInfo pi = item.GetType().GetProperty(propertyName);
                var oldValue = e.OldValue;
                var newValue = e.ChangedItem.Value;

                if ("Title" == pi.Name)
                {
                    this.CurNode.SetText(null);

                    PageNode pageNode = this.CurNode as PageNode;
                    if (null != pageNode)
                    {
                        pageNode.SetNewTitle(newValue as string);
                    }
                }

                if (null != this.cqp)
                {
                    this.cqp.AddCommand(new NodePropertyInfo(item, pi, oldValue, newValue));
                }

                SetPropertyGridTitle(this.CurNode);

                UCPropertyChangedEventArgs args = new UCPropertyChangedEventArgs();
                args.pi = pi;
                NodePropertyChangeNotify(this.CurNode, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 属性标题
        private void SetPropertyGridTitle(ViewNode node)
        {
            if (null != node)
            {
                this.lblTitle.Text = Resources.ResourceManager.GetObject("Property") + " - "/* + node.Name + " " */+ node.Text;
                //this.lblTitle.Text = Resources.ResourceManager.GetObject("Property") + " - " + node.Name + "-" + node.Title;
            }
            else
            {
                this.lblTitle.Text = "";
            }
        }
        #endregion
        #endregion
    }

    public class UCPropertyChangedEventArgs : EventArgs
    {
        public PropertyInfo pi;
    }
}
