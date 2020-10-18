using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex_Game
{
    public partial class Form1 : Form
    {

        bool jumping = false;
        int jumpSpeed;
        int force = 12;
        int score = 0;
        int obstacleSpeed = 7;
        Random rand = new Random();
        int position;
        bool isGameOver = false;


        public Form1()
        {
            InitializeComponent();
            GameReset();
        }

        private void MainGameTimeEvent(object sender, EventArgs e)
        {
            tRex.Top += jumpSpeed;

            gameScore.Text = "Score : " + score;

            if (jumping == true && force<0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            if (tRex.Top > 382 && jumping == false)
            {
                force = 12;
                tRex.Top = 383;
                jumpSpeed = 0;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 15);
                        score++;
                    }
                    if (tRex.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        tRex.Image = Properties.Resources.dead;
                        gameScore.BackColor = System.Drawing.Color.Red;
                        gameScore.Text += " Press R to restart the game!";
                        isGameOver = true;
                    }
                }
            }

            if (score >= 10 && score < 20)
            {
                obstacleSpeed = 12;
            }
            else if ( score >= 20)
            {
                obstacleSpeed = 16;
                
            }

        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode == Keys.R && isGameOver == true)
            {
                GameReset();
            }
        }

        private void GameReset()
        {
            force = 12;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 7;
            gameScore.Text = "Score: " + score;
            gameScore.BackColor = System.Drawing.Color.White;
            tRex.Image = Properties.Resources.running;
            isGameOver = false;
            tRex.Top = 383;

            foreach (Control x in Controls)
            {

                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    position = this.ClientSize.Width + rand.Next(500, 800) + (x.Width * 10);
                    x.Left = position;
                }
            }

            gameTimer.Start();
        }
    }
}
