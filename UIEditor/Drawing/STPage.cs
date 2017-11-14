using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UIEditor.CommandManager;
using UIEditor.Component;
using UIEditor.Entity;

namespace UIEditor.Drawing
{
    public class STPage : STContainer
    {
        private LayerControls mLayerControls;
        private PageNode mPageNode;

        public STPage(PageNode node, float ratio)
        {
            this.mPageNode = node;
            UpdateThisPage(ratio);

            this.BackgroundImageLayout = ImageLayout.Stretch;

            this.mLayerControls = new LayerControls(this.mPageNode, ratio);
            this.Controls.Add(this.mLayerControls);
        }

        private void UpdateThisPage(float ratio)
        {
            int pWidth = this.mPageNode.DrawWidth; //.GetDrawWidth(ratio);
            int pHeight = this.mPageNode.DrawHeight; //.GetDrawHeight(ratio);
            this.Size = new Size(pWidth, pHeight);

            if ((null != this.mPageNode) && (null != this.mPageNode.ImgBackgroundImage))
            {
                this.BackgroundImage = this.mPageNode.ImgBackgroundImage;
            }
            else
            {
                this.BackgroundImage = null;
                this.BackColor = this.mPageNode.BackgroundColor;
            }
        }

        #region 对外公共方法
        public CommandQuene GetCommandQueue()
        {
            return this.mLayerControls.GetCommandQueue();
        }

        public void SelectControl(ViewNode node)
        {
            this.mLayerControls.SetSelectedControl(node);
        }

        public void AddControl(ViewNode node)
        {
            this.mLayerControls.AddControl(node);
        }

        public void RemoveControl(ViewNode node)
        {
            this.mLayerControls.RemoveControl(node);
        }

        public void PagePropertyChanged(float ratio)
        {
            this.mLayerControls.PagePropertyChanged(ratio);
            this.UpdateThisPage(ratio);
        }

        public void ControlPropertyChanged(ViewNode node)
        {
            this.mLayerControls.ControlPropertyChanged(node);
        }

        public void AddNewControl(Type ControlType)
        {
            LayerControls.ToAddControl = ControlType;
        }

        public void Save()
        {
            this.mLayerControls.Saved();
        }

        public void AlignLeft()
        {
            this.mLayerControls.AlignLeft();
        }

        public void AlignRight()
        {
            this.mLayerControls.AlignRight();
        }

        public void AlignTop() 
        {
            this.mLayerControls.AlignTop();
        }

        public void AlignBottom()
        {
            this.mLayerControls.AlignBottom();
        }

        public void AlignHorizontalCenter()
        {
            this.mLayerControls.AlignHorizontalCenter();
        }

        public void AlignVerticalCenter()
        {
            this.mLayerControls.AlignVerticalCenter();
        }

        public void HorizontalEquidistanceAlignment()
        {
            this.mLayerControls.HorizontalEquidistanceAlignment();
        }

        public void VerticalEquidistanceAlignment()
        {
            this.mLayerControls.VerticalEquidistanceAlignment();
        }

        public void WidthAlignment()
        {
            this.mLayerControls.WidthAlignment();
        }

        public void HeightAlignment()
        {
            this.mLayerControls.HeightAlignment();
        }

        public void CenterHorizontal()
        {
            this.mLayerControls.CenterHorizontal();
        }

        public void CenterVertical()
        {
            this.mLayerControls.CenterVertical();
        }

        public void CutControls()
        {
            this.mLayerControls.CutControls();
        }

        public void CopyControls()
        {
            this.mLayerControls.CopyControls();
        }

        public void PasteControls()
        {
            this.mLayerControls.PasteControls();
        }

        public void KeyDowns(KeyEventArgs e)
        {
            this.mLayerControls.LayerControlsKeyDowns(e);
        }

        public void KeyUps(KeyEventArgs e)
        {
            this.mLayerControls.LayerControlsKeyUps(e);
        }

        public event UIEditor.Drawing.LayerControls.ControlSelectedEventDelegate PageBoxControlSelectedEvent
        {
            add
            {
                this.mLayerControls.ControlSelectedEvent += value;
            }
            remove
            {
                this.mLayerControls.ControlSelectedEvent -= value;
            }
        }

        public event UIEditor.Drawing.LayerControls.PageChangedEventDelegate PageBoxPageChangedEvent
        {
            add
            {
                this.mLayerControls.PageChangedEvent += value;
            }
            remove
            {
                this.mLayerControls.PageChangedEvent -= value;
            }
        }

        public event UIEditor.Drawing.LayerControls.SelectedControlsIsBrotherhoodEventDelegate PageBoxSelectedControlsIsBrotherhoodEvent
        {
            add
            {
                this.mLayerControls.SelectedControlsIsBrotherhoodEvent += value;
            }
            remove
            {
                this.mLayerControls.SelectedControlsIsBrotherhoodEvent -= value;
            }
        }

        public event UIEditor.Drawing.LayerControls.SelectedControlsMoveEventDelegate PageBoxSelectedControlsMoveEvent
        {
            add
            {
                this.mLayerControls.SelectedControlsMoveEvent += value;
            }
            remove
            {
                this.mLayerControls.SelectedControlsMoveEvent -= value;
            }
        }
        #endregion
    }
}
