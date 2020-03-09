using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        private Label firstLabelClicked, secondLabelClicked;
        
        public Form1()
        {
            InitializeComponent();
            GameManager.Start();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            GameManager.AssignIcons(this.tableLayoutPanel1.Controls);
            GameManager.StartCounter(this.MessageText);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (firstLabelClicked == null || secondLabelClicked == null)
                return;

            if (firstLabelClicked.Text != secondLabelClicked.Text)
            {
                firstLabelClicked.ForeColor = firstLabelClicked.BackColor;
                secondLabelClicked.ForeColor = secondLabelClicked.BackColor;
                GameManager.UpdateCounter(this.MessageText);
            }
            firstLabelClicked = null;
            secondLabelClicked = null;
            timer1.Stop();
            GameManager.CheckForGameOver(this.MessageText, this.tableLayoutPanel1.Controls);
        }

        private void OnLabelClick(object sender, MouseEventArgs e)
        {
            if (firstLabelClicked != null && secondLabelClicked != null)
                return;

            Label label = sender as Label;
            if (label.ForeColor != Color.Black)
            {
                label.ForeColor = Color.Black;

                if (firstLabelClicked == null)
                {
                    firstLabelClicked = label;
                }
                else
                {
                    secondLabelClicked = label;
                    GameManager.CheckForWinner(firstLabelClicked, 
                        secondLabelClicked, 
                        this.tableLayoutPanel1.RowCount, 
                        this.tableLayoutPanel1.ColumnCount);
                    timer1.Start();
                }
            }
        }
    }
}
