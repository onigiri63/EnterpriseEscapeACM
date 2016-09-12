using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enterprise_Escape
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rand = new Random();

        int cellCount = 21;
        int cellSize = 40;
        int maxValue = 100;
        Point startPoint = new Point(10, 10);

        //int cellCount = 151;
        //int cellSize = 5;
        //int maxValue = 100;
        //Point startPoint = new Point(105, 45);

        const int intSize = 2147483647;
        List<List<int>> nodes;
        bool firstClick = true;

        private void button1_Click(object sender, EventArgs e)
        {
            if (firstClick)
            {
                firstClick = false;
                for (int i = 0; i < cellCount; i++)
                {
                    DataGridViewTextBoxColumn tempColumn = new DataGridViewTextBoxColumn();
                    tempColumn.Width = cellSize;
                    tempColumn.DefaultCellStyle.Font = new Font("Arial", cellSize / 2, GraphicsUnit.Pixel);
                    this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { tempColumn });
                    tempColumn = new DataGridViewTextBoxColumn();
                    tempColumn.Width = cellSize;
                    tempColumn.DefaultCellStyle.Font = new Font("Arial", cellSize / 3, GraphicsUnit.Pixel);
                    this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { tempColumn });
                }

                for (int i = 0; i < cellCount; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Height = cellSize;

                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Height = cellSize;
                }
            }

            for (int i = 0; i < cellCount; i++)
            {
                for (int j = 0; j < cellCount; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = rand.Next(1, maxValue);
                    if (i == startPoint.X && j == startPoint.Y)
                    {
                        this.dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                        this.dataGridView2.Rows[i].Cells[j].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        this.dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                        this.dataGridView2.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                    dataGridView2.Rows[i].Cells[j].Value = null;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nodes = new List<List<int>>();
            for (int i = 0; i < cellCount; i++)
            {
                nodes.Add(new List<int>());
                for (int j = 0; j < cellCount; j++)
                {
                    nodes[i].Add(Convert.ToInt32(this.dataGridView1.Rows[i].Cells[j].Value));
                }
            }
            D d = new D(nodes);
            this.label1.Text = Convert.ToString(d.shortPath(startPoint));

            double fraction = 255d / (double)d.maxDist;
            List<List<int>> a = d.dist;

            for (int i = 0; i < a.Count; i++)
                for (int j = 0; j < a[0].Count; j++)
                {
                    this.dataGridView2.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView2.Rows[i].Cells[j].Value = a[i][j];
                    this.dataGridView2.Rows[i].Cells[j].Style.BackColor =
                        Color.FromArgb(
                        255,
                        255,
                        255 - (int)(fraction * a[i][j]),
                        255 - (int)(fraction * a[i][j]));
                }

            List<Point> path = d.path;
            for (int i = 0; i < path.Count; i++)
            {
                int x = path[i].X;
                int y = path[i].Y;
                this.dataGridView1.Rows[x].Cells[y].Style.BackColor = Color.Blue;
            }
        }
    }
}
