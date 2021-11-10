using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoardF;

namespace WindowGameF
{
    public partial class FormGameF : Form
    {
        Game game;        
        const int size = 4;
        public FormGameF()
        {
            InitializeComponent();
            game = new Game(size);
            HideButtons();
        }
        /// <summary>
        /// Нажатие на кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt00_Click(object sender, EventArgs e)
        {
            if (game.IsSolved())
                return;
            Button button = (Button)sender;
            int x = int.Parse(button.Name.Substring(2, 1));
            int y = int.Parse(button.Name.Substring(3, 1));
            game.PressAt(x, y);
            ShowButtons();
            if (game.IsSolved())
            {
                lbMoves.Text = "Game finished in " + game.moves + " moves";
            }
        }

        /// <summary>
        /// Кнопка старта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btStart_Click(object sender, EventArgs e)
        {
            game.Start(1000 + DateTime.Now.DayOfYear);
            ShowButtons();
        }

        /// <summary>
        /// Спрятать кнопки
        /// </summary>
        void HideButtons()
        {
            for(int x = 0; x< size; x++)
            {
                for (int y = 0;  y< size; y++)
                {
                    ShowDigitAt(0, x, y);
                }
            }
            lbMoves.Text = "Welcome to Game F";
        }

        /// <summary>
        /// Показать кнопки
        /// </summary>
        void ShowButtons()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    ShowDigitAt(game.GetDigitAt(x,y), x, y);
                }
            }
            lbMoves.Text ="Moves: " + game.moves;
        }

        void ShowDigitAt(int digit, int x, int y)
        {
            Button button = (Button)Controls["bt" + x + y];
            button.Text = DecToHex(digit);
            button.Visible = digit > 0;
        }

        /// <summary>
        /// Даем название кнопкам
        /// </summary>
        /// <param name="digit"></param>
        /// <returns></returns>
        string DecToHex(int digit)
        {
            if (digit == 0)
                return "";
            if (digit < 10)
                return digit.ToString();
            return ((char)('A' + digit - 10)).ToString();
        }

    }
}
