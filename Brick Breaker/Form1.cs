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
        PictureBox[] blocksArray;

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
            placeBlocks();
        }

        private void setUpGame()
        {
            gameIsOver = false;
            score = 0;
            ballx = 5;
            bally = 7;
            playerSpeed = 18;
            txtScore.Text = "Score: " + score;

            ball.Left = 430;
            ball.Top = 403;
            ball.BackColor = Color.Green; ball.ForeColor = Color.Blue;

            player.Left = 391;
            player.Top = 429;

            gameTimer.Start();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(random.Next(256),random.Next(256), random.Next(256));
                }
            }
        }

        private void gameOver(string message)
        {
            gameIsOver = true;
            gameTimer.Stop();

            txtScore.Text = "Score: " + score + " " + message;
        }

        private void placeBlocks()
        {
            blocksArray = new PictureBox[40];

            int a = 0;

            int top = 40;
            int left = 18;

            for (int i = 0; i <  blocksArray.Length; i++)
            {
                blocksArray[i] = new PictureBox();
                blocksArray[i].Height = 20;
                blocksArray[i].Width = 100;
                blocksArray[i].Tag = "blocks";
                blocksArray[i].BackColor = Color.Black;

                if(a == 8)
                {
                    top += 25;
                    left = 18;
                    a = 0;
                }

                if (a < 8)
                {
                    a++;
                    blocksArray[i].Left = left;
                    blocksArray[i].Top = top;
                    this.Controls.Add(blocksArray[i]);
                    left += 105;


                }
            }

            setUpGame();

        }

        private void removeBlocks()
        {
            foreach(PictureBox x in blocksArray)
            {
                this.Controls.Remove(x);
            }
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {

            txtScore.Text = "Score "+ score;

            if(goLeft == true && player.Left > 18)
            {
                player.Left -= playerSpeed;
            }

            if(goRight == true && player.Left < 760)
            {
                player.Left += playerSpeed;
            }

            ball.Left += ballx;
            ball.Top += bally;

            if (ball.Left < 0 || ball.Left > 868)
            {
                ballx = - ballx;
            }
            if(ball.Top < 0)
            {
                bally = - bally;
            }

            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                bally = random.Next(7, 12) * -1;

                if( ballx < 0)
                {
                    ballx = random.Next(5, 12) * -1;
                }
                else
                {
                    ballx = random.Next(5,12);
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;
                        bally = -bally;

                        this.Controls.Remove(x);
                    }
                }
            }

            if (score == 40 )
            {
                gameOver("You Win!! Press Enter To Play Again");
                gameTimer.Stop();
            }

            if(ball.Top > 429)
            {
                gameOver("You Lose!! Press Enter To Play Again");
                ball.BackColor = Color.Red;
                gameTimer.Stop();
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
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (e.KeyCode == Keys.Enter && gameIsOver == true)
            {
                removeBlocks();
                placeBlocks();
            }
        }
    }
}
