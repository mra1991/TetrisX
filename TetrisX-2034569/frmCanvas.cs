﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TetrisGameEngine; 

namespace TetrisX_2034569
{
    public partial class frmCanvas : Form
    {
        const int PIXEL_SIZE = 20; //how many real pixels on the screen is a square on our grid
        const int LEFT_OFFSET_PIXELS = 200; //how many pixels from the left of the form is the grid drawn
        const int TOP_OFFSET_PIXELS = 15; //how many pixels from the top of the form is the grid drawn
        TetrisBoard tetrisGame = new TetrisBoard(20, 35, 1);
        const int UPDATE_EVERY_NTH_TICK = 15; //how many ticks of timer should there be between updates
        int tickCounter;

        public frmCanvas()
        {
            InitializeComponent();
        }

        private void frmCanvas_Load(object sender, EventArgs e)
        {
            tickCounter = 0;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        private void PaintBoard(Graphics g)
        {
            //going through the grid pixel by pixel
            for (int x = 0; x < tetrisGame.Width; x++)
            {
                for (int y = 0; y < tetrisGame.Height; y++)
                {
                    //define a solid brush with the color set to the color of that pixel in the grid  
                    SolidBrush sb = new SolidBrush(tetrisGame.TetrisGrid[x, y]);
                    //paint a filled square for the pixel. leave 2 real screen pixels between adjacent squares.
                    g.FillRectangle(sb, new Rectangle(LEFT_OFFSET_PIXELS + x * PIXEL_SIZE + 1, TOP_OFFSET_PIXELS + y * PIXEL_SIZE + 1, PIXEL_SIZE - 2, PIXEL_SIZE - 2));
                }
            }

            //display the score the player
            //lblScore.Text = "SCORE: " + tetrisGame.Score.ToString("D4");
        }

        private void PaintTetrimino(Graphics g)
        {
            //going through the image pixel by pixel
            for (int x = 0; x < tetrisGame.CurrentTetrimino.Width; x++)
            {
                for (int y = 0; y < tetrisGame.CurrentTetrimino.Height; y++)
                {
                    if (tetrisGame.CurrentTetrimino.ImageGrid[x, y].IsNamedColor) //if the current pixel in the image is set to a color (is not transparent)
                    {
                        //define a solid brush with the color set to the color of that pixel in the image  
                        SolidBrush sb = new SolidBrush(tetrisGame.CurrentTetrimino.ImageGrid[x, y]);
                        //paint a filled square for the pixel. leave 2 real screen pixels between adjacent squares.
                        g.FillRectangle(sb, new Rectangle(LEFT_OFFSET_PIXELS + (tetrisGame.CurrentTetrimino.PosX + x) * PIXEL_SIZE + 1, TOP_OFFSET_PIXELS + (tetrisGame.CurrentTetrimino.PosY + y) * PIXEL_SIZE + 1, PIXEL_SIZE - 2, PIXEL_SIZE - 2));
                    }
                }
            }
        }

        private void frmCanvas_Paint(object sender, PaintEventArgs e)
        {
            PaintBoard(e.Graphics);
            PaintTetrimino(e.Graphics);
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            tickCounter++;
            if ((tickCounter % UPDATE_EVERY_NTH_TICK) == (UPDATE_EVERY_NTH_TICK - 1)) //every nth tick of the timer
            {
                tetrisGame.Updade(); //apply gravity to the tetrimino
                this.Refresh(); //repaint screen by calling frmCanvas_Paint()
            }
        }

        private void frmCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    tetrisGame.MoveTetrimino(-1, 0); //move tetrimino left
                    this.Refresh(); //repaint screen by calling frmCanvas_Paint()
                    tickCounter = 0; //wait for n timer ticks to update
                    break;
                case Keys.D:
                    tetrisGame.MoveTetrimino(1, 0); //move tetrimino right
                    this.Refresh(); //repaint screen by calling frmCanvas_Paint()
                    tickCounter = 0; //wait for n frames to update
                    break;
                case Keys.S:
                    tetrisGame.MoveTetrimino(0, 1); //move tetrimino down
                    this.Refresh(); //repaint screen by calling frmCanvas_Paint()
                    tickCounter = 0; //wait for n timer ticks to update
                    break;
                default:
                    break;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //activate the timer to start the game
            tmrUpdate.Enabled = true;
        }
    }
}