
//Pacman Game 
//NItya Patel

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman
{
    public partial class Pacman : Form
    {
        //Player object represting Pacman
        Player pc = new Player();

        //Only one ghost in this game: Blinky 
        Ghost blinky = new Ghost();
        MODE mode = new MODE();

        int score = 0;
        Random rand = new Random();
        const int NUM_ROWS = 31;
        const int NUM_COLS = 28;
        const int CELL_SIZE = 25;

        DIR dir = new DIR();

        SoundPlayer lose = new SoundPlayer(Properties.Resources.Bum_bum_bum_bummmm_sound_effect);
        
        bool up, right, left, down;

        PictureBox[,] pbArr = new PictureBox[NUM_ROWS, NUM_COLS]; //2D array of pictureboxes for the grid 
        
        PictureBox pb ;

        //Map array represting the level
        int[,] mapArr = new int[NUM_ROWS, NUM_COLS]   {
               {8, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 9, 8, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 9},
 {10, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 10, 10, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 10},
 {10, 2, 8, 5, 5, 9, 2, 8, 5, 5, 5, 9, 2, 10, 10, 2, 8, 5, 5, 5, 9, 2, 8, 5, 5, 9, 2, 10},
 {10, 4, 10, 3, 3, 10, 2, 10, 3, 3, 3, 10, 2, 10, 10, 2, 10, 3, 3, 3, 10, 2, 10, 3, 3, 10, 4, 10},
 {10, 2, 0, 5, 5, 1, 2, 0, 5, 5, 5, 1, 2, 0, 1, 2, 0, 5, 5, 5, 1, 2, 0, 5, 5, 1, 2, 10},
 {10, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 10},
 {10, 2, 8, 5, 5, 9, 2, 8, 9, 2, 8, 5, 5, 5, 5, 5, 5, 9, 2, 8, 9, 2, 8, 5, 5, 9, 2, 10},
 {10, 2, 0, 5, 5, 1, 2, 10, 10, 2, 0, 5, 5, 9, 8, 5, 5, 1, 2, 10, 10, 2, 0, 5, 5, 1, 2, 10},
 {10, 2, 2, 2, 2, 2, 2, 10, 10, 2, 2, 2, 2, 10, 10, 2, 2, 2, 2, 10, 10, 2, 2, 2, 2, 2, 2, 10},
 {0, 5, 5, 5, 5, 9, 2, 10, 0, 5, 5, 9, 2, 10, 10, 2, 8, 5, 5, 1, 10, 2, 8, 5, 5, 5, 5, 1},
 {3, 3, 3, 3, 3, 10, 2, 10, 8, 5, 5, 1, 2, 0, 1, 2, 0, 5, 5, 9, 10, 2, 10, 3, 3, 3, 3, 3},
 {3, 3, 3, 3, 3, 10, 2, 10, 10, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 10, 10, 2, 10, 3, 3, 3, 3, 3},
 {3, 3, 3, 3, 3, 10, 2, 10, 10, 2, 8, 5, 5, 7, 7, 5, 5, 9, 2, 10, 10, 2, 10, 3, 3, 3, 3, 3},
 {5, 5, 5, 5, 5, 1, 2, 0, 1, 2, 10, 3, 3, 3, 3, 3, 3, 10, 2, 0, 1, 2, 0, 5, 5, 5, 5, 5},
 {2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 10, 3, 3, 3, 3, 3, 3, 10, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
 {5, 5, 5, 5, 5, 9, 2, 8, 9, 2, 10, 3, 3, 3, 3, 3, 3, 10, 2, 8, 9, 2, 8, 5, 5, 5, 5, 5},
 {3, 3, 3, 3, 3, 10, 2, 10, 10, 2, 0, 5, 5, 5, 5, 5, 5, 1, 2, 10, 10, 2, 10, 3, 3, 3, 3, 3},
 {3, 3, 3, 3, 3, 10, 2, 10, 10, 2, 2, 2, 2,2, 2, 2, 2, 2, 2, 10, 10, 2, 10, 3, 3, 3, 3, 3},
 {3, 3, 3, 3, 3, 10, 2, 10, 10, 2, 8, 5, 5, 5, 5, 5, 5, 9, 2, 10, 10, 2, 10, 3, 3, 3, 3, 3},
 {8, 5, 5, 5, 5, 1, 2, 0, 1, 2, 0, 5, 5, 9, 8, 5, 5, 1, 2, 0, 1, 2, 0, 5, 5, 5, 5, 9},
 {10, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 10, 10, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 10},
 {10, 2, 8, 5, 5, 9, 2, 8, 5, 5, 5, 9, 2, 10, 10, 2, 8, 5, 5, 5, 9, 2, 8, 5, 5, 9, 2, 10},
 {10, 2, 0, 5, 9, 10, 2, 0, 5, 5, 5, 1, 2, 0, 1, 2, 0, 5, 5, 5, 1, 2, 10, 8, 5, 1, 2, 10},
 {10, 4, 2, 2, 10, 10, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 10, 10, 2, 2, 4, 10},
 {0, 5, 9, 2, 10, 10, 2, 8, 9, 2, 8, 5, 5, 5, 5, 5, 5, 9, 2, 8, 9, 2, 10, 10, 2, 8, 5, 1},
 {8, 5, 1, 2, 0, 1, 2, 10, 10, 2, 0, 5, 5, 9, 8, 5, 5, 1, 2, 10, 10, 2, 0, 1, 2, 0, 5, 9},
 {10, 2, 2, 2, 2, 2, 2, 10, 10, 2, 2, 2, 2, 10, 10, 2, 2, 2, 2, 10, 10, 2, 2, 2, 2, 2, 2, 10},
 {10, 2, 8, 5, 5, 5, 5, 1, 0, 5, 5, 9, 2, 10, 10, 2, 8, 5, 5, 1, 0, 5, 5, 5, 5, 9, 2, 10},
 {10, 2, 0, 5, 5, 5, 5, 5, 5, 5, 5, 1, 2, 0, 1, 2, 0, 5, 5, 5, 5, 5, 5, 5, 5, 1, 2, 10},
 {10, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 10},
 {0, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1}
        };

        public Pacman()
        {
            InitializeComponent();

        }
        public enum DIR
        {
            // Enum for directions
            UP,
            DOWN,
            LEFT,
            RIGHT
        }
        public enum MODE
        {
            // Enum for ghost mode- blinky just chase the pacman 
            CHASE
        }
        public struct Player
        {

            // Player struct
            public int pic;
            public DIR dir;
            public int row;
            public int col;
        }
        public struct Ghost
        {

            // Ghost struct
            public int pic;
            public DIR dir;
            public int row;
            public int col;
            public MODE mode;
            
        }
        private void init()
        {
            //Initialize game
            this.Height = NUM_ROWS * CELL_SIZE;
            this.Width = NUM_COLS * CELL_SIZE;

            tmrUpGhost.Start();

            pacmanArrays();
            pacmanMap();
            pacmanLocation();
 
           
        }
        private void Pacman_Load(object sender, EventArgs e)
        {
            init();
           
        }

        private void pacmanArrays()
        {
            // Initialize PictureBox array
            for (int r = 0; r < NUM_ROWS; r++)
            {
                for (int c = 0; c < NUM_COLS; c++)
                {
                    pb = new PictureBox();
                    pb.Width = CELL_SIZE;
                    pb.Height = CELL_SIZE;
                    pb.BackColor = Color.Black;
                    tlpMap.Controls.Add(pb);
                    pb.Visible = true;
                    pb.Margin = new Padding(0);
                    pbArr[r, c] = pb;
                   




                }
            }
        }

        private void pacmanMap()
        {
            // Draw the map tiles based on mapArr values
            for (int r = 0; r < NUM_ROWS; r++)
            {
                for (int c = 0; c < NUM_COLS; c++)
                { 
                    switch (mapArr[r, c])
                    {
                        case 0:
                            pbArr[r, c].Image = iglImage.Images[0];
                            break;
                        case 1:
                            pbArr[r, c].Image = iglImage.Images[1];
                            break;
                        case 2:
                            pbArr[r, c].Image = iglImage.Images[2];
                            break;
                        case 3:
                            pbArr[r, c].Image = iglImage.Images[3];
                            break;
                        case 4:
                            pbArr[r, c].Image = iglImage.Images[4];
                            break;
                        case 5:
                            pbArr[r, c].Image = iglImage.Images[5];
                            break;
                        case 6:
                            pbArr[r, c].Image = iglImage.Images[6];
                            break;
                        case 7:
                            pbArr[r, c].Image = iglImage.Images[7];
                            break;
                        case 8:
                            pbArr[r, c].Image = iglImage.Images[8];
                            break;
                        case 9:
                            pbArr[r, c].Image = iglImage.Images[9];
                            break;
                        case 10:
                            pbArr[r, c].Image = iglImage.Images[10];
                            break;
                        case 11:
                            pbArr[r, c].Image = iglImage.Images[11];
                            break;
                        case 12:
                            pbArr[r, c].Image = iglImage.Images[12];
                            break;
                        case 13:
                            pbArr[r, c].Image = iglImage.Images[13];
                            break;
                        case 14:
                            pbArr[r, c].Image = iglImage.Images[14];
                            break;
                        case 15:
                            pbArr[r, c].Image = iglImage.Images[15];
                            break;

                    }
                   
                      
                

                }
            }
        }

        private void tmrUp_Tick(object sender, EventArgs e)
        {
            // Move up if possible
            if (up)
            {
                if (mapArr[pc.row - 1, pc.col] == 2 || mapArr[pc.row - 1, pc.col] == 4 || mapArr[pc.row - 1, pc.col] == 3)
                {
                    pc.row--;
                    pbArr[pc.row, pc.col].Image = iglImage.Images[15];
                    mapArr[pc.row + 1, pc.col] = 3;
                    pbArr[pc.row + 1, pc.col].Image = iglImage.Images[3];
                    
                }
                else if (mapArr[pc.row + 1, pc.col] != 2 || mapArr[pc.row + 1, pc.col] != 4 || mapArr[pc.row + 1, pc.col] != 3)
                {
                    tmrUp.Stop();
                }
                
            }
            
        }

        private void tmrDown_Tick(object sender, EventArgs e)
        {
            // Move down if possible
            if (down)
            {
                if (mapArr[pc.row + 1, pc.col] == 2 || mapArr[pc.row + 1, pc.col] == 4 || mapArr[pc.row + 1, pc.col] == 3)
                {
                    pc.row++;
                    pbArr[pc.row, pc.col].Image = iglImage.Images[13];
                    mapArr[pc.row - 1, pc.col] = 3;
                    pbArr[pc.row - 1, pc.col].Image = iglImage.Images[3];
                   
                }
                else if (mapArr[pc.row - 1, pc.col] != 2 || mapArr[pc.row - 1, pc.col] != 4 || mapArr[pc.row - 1, pc.col] != 3)
                {
                    tmrDown.Stop();
                }
            }
           
        }

        private void tmrLeft_Tick(object sender, EventArgs e)
        {
            // Move left if possible
            if (left)
            {
                if (pc.row == 14 && pc.col == 0)
                {
                    pbArr[pc.row, pc.col].Image = iglImage.Images[3];
                    pc.row = 14;
                    pc.col = 27;
                }
                if (mapArr[pc.row, pc.col - 1] == 2 || mapArr[pc.row, pc.col - 1] == 4 || mapArr[pc.row, pc.col - 1] == 3)
                {

                    pc.col--;
                    pbArr[pc.row, pc.col].Image = iglImage.Images[14];
                    mapArr[pc.row , pc.col+1] = 3;
                    pbArr[pc.row, pc.col + 1].Image = iglImage.Images[3];

                }
                else if (mapArr[pc.row, pc.col + 1] != 2 || mapArr[pc.row, pc.col + 1] != 4 || mapArr[pc.row, pc.col + 1] != 3)
                {
                    tmrLeft.Stop();
                }
            }
           

        }

        private void tmrRight_Tick(object sender, EventArgs e)
        {
            // Move right if possible
            if (right)
            {
                if(pc.row==14 && pc.col==27)
                {
                    pbArr[pc.row, pc.col].Image = iglImage.Images[3];
                    pc.row = 14;
                    pc.col = 0;
                }
                if (mapArr[pc.row, pc.col + 1] == 2 || mapArr[pc.row, pc.col + 1] == 4 || mapArr[pc.row, pc.col + 1] == 3)
                {

                    pc.col++;
                    pbArr[pc.row, pc.col].Image = iglImage.Images[11];
                    mapArr[pc.row, pc.col-1] = 3;
                    pbArr[pc.row, pc.col - 1].Image = iglImage.Images[3];

                }
                else if (mapArr[pc.row, pc.col + 1] != 2 || mapArr[pc.row, pc.col + 1] != 4 || mapArr[pc.row, pc.col + 1] != 3)
                {
                    tmrRight.Stop();
                }
            }
           
        }

        private void tmrGhost_Tick(object sender, EventArgs e)
        {
            //Timer for ghost 
            if (pbArr[blinky.row, blinky.col].Bounds.IntersectsWith(pbArr[pc.row,pc.col].Bounds) )
            {
               
                lose.Play();
                gameOver();
            }
 

            if (mapArr[blinky.row - 1, blinky.col] == 2 || mapArr[blinky.row - 1, blinky.col] == 4 || mapArr[blinky.row - 1, blinky.col] == 3 || mapArr[blinky.row - 1, blinky.col] == 7)
            {
                if (pc.row<blinky.row)
                {
                    blinky.dir = DIR.UP;
                    int upBlinky = mapArr[blinky.row, blinky.col];
                    pbArr[blinky.row, blinky.col].Image.Tag = upBlinky;
                    pbArr[blinky.row, blinky.col].Image = iglImage.Images[upBlinky];
                    blinky.row--;
                    pbArr[blinky.row, blinky.col].Image = iglImage.Images[12];
                    
                }
               

            }
            if (mapArr[blinky.row + 1, blinky.col] == 2 || mapArr[blinky.row + 1, blinky.col] == 4 || mapArr[blinky.row - 1, blinky.col] == 3 || mapArr[blinky.row - 1, blinky.col] == 7)
            {
                if (pc.row>blinky.row)
                {
                    blinky.dir = DIR.DOWN;
                    int downBlinky = mapArr[blinky.row, blinky.col];
                    pbArr[blinky.row, blinky.col].Image.Tag = downBlinky;
                    pbArr[blinky.row, blinky.col].Image = iglImage.Images[downBlinky];
                    blinky.row++;
                    pbArr[blinky.row, blinky.col].Image = iglImage.Images[12];
                    
                }
                   

            }
            if (mapArr[blinky.row, blinky.col - 1] == 2 || mapArr[blinky.row, blinky.col - 1] == 4 || mapArr[blinky.row, blinky.col - 1] == 3 || mapArr[blinky.row - 1, blinky.col] == 7)
            {
                if(pc.col<blinky.col)
                {
                    blinky.dir = DIR.RIGHT;
                    int rightBlinky = mapArr[blinky.row, blinky.col];
                    pbArr[blinky.row, blinky.col].Image.Tag = rightBlinky;
                    pbArr[blinky.row, blinky.col].Image = iglImage.Images[rightBlinky];
                    blinky.col--;
                    pbArr[blinky.row, blinky.col].Image = iglImage.Images[12];
                }
               
                

            }
            if (mapArr[blinky.row, blinky.col + 1] == 2 || mapArr[blinky.row, blinky.col + 1] == 4 || mapArr[blinky.row, blinky.col + 1] == 3 || mapArr[blinky.row - 1, blinky.col] == 7)
            {
                if(pc.col>blinky.col)
                {
                    blinky.dir = DIR.LEFT;
                    int leftBlinky = mapArr[blinky.row, blinky.col];
                    pbArr[blinky.row, blinky.col].Image.Tag = leftBlinky;
                    pbArr[blinky.row, blinky.col].Image = iglImage.Images[leftBlinky];
                    blinky.col++;
                    pbArr[blinky.row, blinky.col].Image = iglImage.Images[12];
                }
               
                

            }
            
        }
        
        private void Pacman_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle keyboard up, down, right, left input
            switch (e.KeyCode)
            {
                case (Keys.Right):
                    pc.dir = DIR.RIGHT;

                    if (mapArr[pc.row, pc.col + 1] == 2 || mapArr[pc.row, pc.col + 1] == 4 || mapArr[pc.row, pc.col + 1] == 3)
                    {
                        right = true;
                        
                        tmrRight.Start();
                    }
                    break;

                case (Keys.Left):
                    pc.dir = DIR.LEFT;

                    if (mapArr[pc.row, pc.col - 1] == 2 || mapArr[pc.row, pc.col - 1] == 4 || mapArr[pc.row, pc.col - 1] == 3)
                    {
                        left = true;
                        tmrLeft.Start();
                    }
                    break;

                case (Keys.Down):
                    pc.dir = DIR.DOWN;

                    if (mapArr[pc.row + 1, pc.col] == 2 ||  mapArr[pc.row + 1, pc.col] == 4 || mapArr[pc.row + 1, pc.col] == 3)
                    {
                        down = true;
                        tmrDown.Start();
                      
                    }
                    break;

                case (Keys.Up):
                    pc.dir = DIR.UP;

                    if (mapArr[pc.row - 1, pc.col] == 2 ||  mapArr[pc.row - 1, pc.col] == 4 || mapArr[pc.row - 1, pc.col] == 3)
                    {
                        up = true;
                        tmrUp.Start();
                    }
                    break;
            }
        }

        public void pacmanLocation()
        {
            // Place Pacman and Blinky at starting positions
            pc.col = 1;
            pc.row = 1;

            blinky.col = 12;
            blinky.row = 11;

            blinky.mode = MODE.CHASE;

            pbArr[pc.row, pc.col].Image = iglImage.Images[11];
            pbArr[blinky.row,blinky.col].Image = iglImage.Images[12];


        }

        public void gameOver()
        {
            // Game over 
            tmrUpGhost.Stop();
            tmrRight.Stop();
            tmrLeft.Stop();
            tmrDown.Stop();
            tmrUp.Stop();

            MessageBox.Show("Game Over!!");
            lose.Stop();

            Application.Exit();
        }
      


    }
}
