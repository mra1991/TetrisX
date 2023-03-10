//Revision history
//Mohammadreza Abolhassani 2034569      2021-12-10      Created the frmCanvas object.
//Mohammadreza Abolhassani 2034569      2023-03-09      Game pause/play feature added (uses space key)
//Mohammadreza Abolhassani 2034569      2023-03-09      Game controls changed to arrow keys
//Mohammadreza Abolhassani 2034569      2023-03-09      Keyboard hints added to the form
//Mohammadreza Abolhassani 2034569      2023-03-09      Game over condition and restart button added
//Mohammadreza Abolhassani 2034569      2023-03-09      Level system added
//Mohammadreza Abolhassani 2034569      2023-03-09      Feature showing next tetrimino in line added


using System;
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
        const int PIXEL_SIZE = 20; //how many real pixels on the screen is a square on our grids
        const int LEFT_OFFSET_PIXELS = 250; //how many pixels from the left of the form is the main grid drawn
        const int TOP_OFFSET_PIXELS = 15; //how many pixels from the top of the form is the main grid drawn
        const int NEXT_LEFT_OFFSET = 50; //how many pixels from the left of the form is the next tetrimino grid drawn
        const int NEXT_TOP_OFFSET = 200; //how many pixels from the top of the form is the next tetrimino grid drawn
        TetrisBoard tetrisGame = new TetrisBoard(25, 39, 1);
        int tickCounter; //how many times has the timer ticked since last reset
        public bool isPaused { get => !tmrUpdate.Enabled; }

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

            //draw a 3 by 4 grid for next tetrimino in line
            SolidBrush sbBackground = new SolidBrush(tetrisGame.BackgroundColor);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 4; j++)
                    //paint a filled square for the pixel. leave 2 real screen pixels between adjacent squares.
                    g.FillRectangle(sbBackground, new Rectangle(NEXT_LEFT_OFFSET + i * PIXEL_SIZE + 1, NEXT_TOP_OFFSET + j * PIXEL_SIZE + 1, PIXEL_SIZE - 2, PIXEL_SIZE - 2));


            //display the score to the player
            lblScore.Text = "SCORE: " + tetrisGame.Score.ToString("D4");

            //display the level to the player
            lblLevel.Text = "LEVEL: " + tetrisGame.Level.ToString("D2");
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

        private void PaintNextTetrimino(Graphics g)
        {
            //going through the image pixel by pixel
            for (int x = 0; x < tetrisGame.NextTetrimino.Width; x++)
            {
                for (int y = 0; y < tetrisGame.NextTetrimino.Height; y++)
                {
                    if (tetrisGame.NextTetrimino.ImageGrid[x, y].IsNamedColor) //if the current pixel in the image is set to a color (is not transparent)
                    {
                        //define a solid brush with the color set to the color of that pixel in the image  
                        SolidBrush sb = new SolidBrush(tetrisGame.NextTetrimino.ImageGrid[x, y]);
                        //paint a filled square for the pixel. leave 2 real screen pixels between adjacent squares.
                        g.FillRectangle(sb, new Rectangle(NEXT_LEFT_OFFSET + x * PIXEL_SIZE + 1, NEXT_TOP_OFFSET + y * PIXEL_SIZE + 1, PIXEL_SIZE - 2, PIXEL_SIZE - 2));
                    }
                }
            }
        }

        private void frmCanvas_Paint(object sender, PaintEventArgs e)
        {
            PaintBoard(e.Graphics);
            PaintTetrimino(e.Graphics);
            PaintNextTetrimino(e.Graphics);
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (tetrisGame.GameOver)
            {
                tmrUpdate.Enabled = false;
                btnRestart.Visible = true;
                btnRestart.Enabled = true;
                lblGameOver.Visible = true;
            }
            else
            {
                tickCounter++;
                tickCounter %= tetrisGame.TicksPerUpdate;
                if (tickCounter == (tetrisGame.TicksPerUpdate - 1)) //every nth tick of the timer
                {
                    tetrisGame.Updade(); //apply gravity to the tetrimino
                    this.Refresh(); //repaint screen by calling frmCanvas_Paint()
                }
            }
        }

        private void frmCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Left:
                    if (!isPaused)
                    {
                        tetrisGame.MoveTetrimino(-1, 0); //move tetrimino left
                        this.Refresh(); //repaint screen by calling frmCanvas_Paint()
                        //tickCounter = 0; //wait for n timer ticks to update
                    }
                    break;
                case Keys.Right:
                    if (!isPaused)
                    {
                        tetrisGame.MoveTetrimino(1, 0); //move tetrimino right
                        this.Refresh(); //repaint screen by calling frmCanvas_Paint()
                        //tickCounter = 0; //wait for n frames to update
                    }
                    break;
                case Keys.Down:
                    if (!isPaused)
                    {
                        tetrisGame.MoveTetrimino(0, 1); //move tetrimino down
                        this.Refresh(); //repaint screen by calling frmCanvas_Paint()
                        //tickCounter = 0; //wait for n timer ticks to update
                    }
                    break;
                case Keys.Enter:
                    if (!isPaused)
                    {
                        tetrisGame.RotateTetrimino(); //rotate tetrimino 90 degrees clockwise if possible
                        this.Refresh(); //repaint screen by calling frmCanvas_Paint()
                        //tickCounter = 0; //wait for n timer ticks to update
                    }
                    break;
                case Keys.Space:
                    if (!tetrisGame.GameOver)
                    {
                        // activate/deactivate the timer to play/pause the game
                        tmrUpdate.Enabled = !tmrUpdate.Enabled;
                        lblPause.Visible = !lblPause.Visible;
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            tetrisGame.InitializeBoard();
            this.Refresh();
            btnRestart.Visible = false;
            btnRestart.Enabled = false;
            lblGameOver.Visible = false;
            lblPause.Visible = false;
            tmrUpdate.Enabled = true;
        }
    }
}
