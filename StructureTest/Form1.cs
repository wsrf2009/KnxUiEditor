using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StructureTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var tlpLayout = CreateTableLayoutPanel();

            var gridPanel = CreateTableLayoutPanel();
            gridPanel.BackColor = Color.Gold;

            tlpLayout.Controls.Add(gridPanel, 0, 1);
            tlpLayout.SetRowSpan(gridPanel, 5);


            Button control = new Button();
            control.Size = new Size(80, 25);
            control.Text = "test";
            control.BackColor = Color.LightGreen;
            control.Dock = DockStyle.Fill;

            tlpLayout.Controls.Add(control, 1, 2);
            tlpLayout.SetRowSpan(control, 1);
            tlpLayout.SetColumnSpan(control, 1);

            this.Controls.Add(tlpLayout);
        }

        private static TableLayoutPanel CreateTableLayoutPanel()
        {
            int row = 10;
            int col = 3;

            TableLayoutPanel tlpLayout = new TableLayoutPanel();
            tlpLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tlpLayout.Dock = DockStyle.Fill;
            tlpLayout.RowCount = row;
            tlpLayout.ColumnCount = col;

            for (int i = 0; i < row; i++)
            {
                tlpLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            }

            for (int i = 0; i < col; i++)
            {
                tlpLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            }
            //设置tablelayoutpanel控件的DoubleBuffered 属性为true，这样可以减少或消除由于不断重绘所显示图面的某些部分而导致的闪烁
            tlpLayout.GetType()
                .GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(tlpLayout, true, null);
            return tlpLayout;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
