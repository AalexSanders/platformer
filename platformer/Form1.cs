using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace platformer
{
    public partial class Form1 : Form
    {
        bool goleft = false;
        bool goright = false;
        bool jumping = false;
        bool hasKey = false; // будет тру, если игрок ключ соберет

        int jumpSpeed = 7; // скорость прыжка
        int force = 5; // высота прыжка
        int score = 0;

        int playSpeed = 18; //скорость движ
        int backLeft = 8; //скорость окр среды

        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimer(object sender, EventArgs e)
        {
            player.Top += jumpSpeed;

            player.Refresh();

            if (jumping && force < 0)
            {
                jumping = false;
            }

            if (jumping)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }
 
            if (goleft && player.Left > 100)
            {
                player.Left -= playSpeed;
            }

            if (goright && player.Left + (player.Width + 100) < this.ClientSize.Width)
            {
                player.Left += playSpeed;

            }

            if (goright && background.Left > -1353)
            {
                background.Left -= backLeft;


                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "platform"
                        || x is PictureBox && x.Tag == "coin"
                        || x is PictureBox && x.Tag == "door"
                        || x is PictureBox && x.Tag == "key")
                    {
                        x.Left -= backLeft;
                    }
                }

            }

            if (goleft && background.Left < 2)
            {
                background.Left += backLeft;

                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "platform" 
                        || x is PictureBox && x.Tag == "coin" 
                        || x is PictureBox && x.Tag == "door" 
                        || x is PictureBox && x.Tag == "key")
                    {
                        x.Left += backLeft;
                    }
                }
            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && !jumping)
                    {
                        force = 8; 
                        player.Top = x.Top - player.Height; 
                        jumpSpeed = 0; 
                    }
                }

                if (x is PictureBox && x.Tag == "coin")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x); 
                        score++; 
                    }
                }
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && hasKey)
            {
                //door.Image = Properties.Resources.dooropen;

                gameTimer.Stop();
                MessageBox.Show("Ура победа!");
            }


            if (player.Bounds.IntersectsWith(key.Bounds))
            {
                this.Controls.Remove(key);
                hasKey = true;
            }


            if (player.Top + player.Height > this.ClientSize.Height + 60)
            {
                gameTimer.Stop(); 
                MessageBox.Show("О нет! Ты проиграл :("); 
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }          

            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }

            if (e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }

            if (jumping)
            {
                jumping = false;
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void player_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {

        }
    }
}
