using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brick_Breaker
{
    public partial class BrickBreaker : Form
    {


        bool goLeft;
        bool goRight;
        bool gameIsOver;

        int score;
        int ballx;
        int bally;
        int playerSpeed;

        Random random = new Random();


        public BrickBreaker()
        {
            InitializeComponent();
            setUpGame();
        }

        private void setUpGame()
        {
            score = 0;
            ballx = 5;
            bally = 5;
            playerSpeed = 12;
            txtScore.Text = "Score: " + score;

            gameTimer.Start();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(random.Next(256),random.Next(256), random.Next(256));
                }
            }
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            if(goLeft == true && player.Left > 18)
            {
                player.Left -= playerSpeed;
            }

            if(goRight == true && player.Left < 760)
            {
                player.Left += playerSpeed;
            }
        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            goLeft = false;
            goRight = false;
        }
    }
}
