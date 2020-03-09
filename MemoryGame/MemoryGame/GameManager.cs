using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public class GameManager
    {
        private static List<string> icons;
        private static int counter = 10;
        private static Random random = new Random();
        private static int iconsFound;      
        
        public static void Start()
        {
            iconsFound = 0;
            icons = new List<string>()
            {
                "!", "~", "a", "b", "c", "h", "w", "#", "!", "~", "a", "b", "c", "h", "w", "#"
            };
        }

        public static void AssignIcons(TableLayoutControlCollection controls)
        {
            foreach (Label label in controls) // This is used since the layout has the controls we need to play with
            {
                int randIndex = random.Next(0, icons.Count);
                label.Text = icons[randIndex];
                icons.RemoveAt(randIndex);
                label.ForeColor = label.BackColor;
            }
        }

        public static void ResetGame(Label MessageText, TableLayoutControlCollection controls)
        {
            counter = 10;
            iconsFound = 0;
            MessageText.Text = "Number of tries: " + counter;
            icons = new List<string>()
            {
                "!", "~", "a", "b", "c", "h", "w", "#", "!", "~", "a", "b", "c", "h", "w", "#"
            };
            AssignIcons(controls);
        }

        public static void CheckForWinner(Label firstLabelClicked, Label secondLabelClicked, int rows, int columns)
        {
            if (secondLabelClicked.Text == firstLabelClicked.Text)
            {
                iconsFound++;
                if (iconsFound == rows * columns / 2)
                {
                    DialogResult res = MessageBox.Show("You won the thing!!!");
                    if (res == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
            }
        }

        public static void UpdateCounter(Label MessageText)
        {
            counter--;
            MessageText.Text = "Number of tries: " + counter;
        }

        public static void StartCounter(Label MessageText)
        {
            MessageText.Text = "Number of tries: " + counter;
        }

        public static void CheckForGameOver(Label MessageText, TableLayoutControlCollection controls)
        {
            if (counter <= 0)
            {
                foreach (Label label in controls) // This is used since the layout has the controls we need to play with
                {
                    if (label.ForeColor == label.BackColor)
                        label.ForeColor = Color.Red;
                }
                DialogResult res = MessageBox.Show("You lost the thing!!!");
                if (res == DialogResult.OK)
                {
                    ResetGame(MessageText, controls);
                }
            }
        }
    }
}
