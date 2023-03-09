//Revision history
//Mohammadreza Abolhassani 2034569      2021-12-10      Created the TetrisBoard object.
//Mohammadreza Abolhassani 2034569      2023-03-09      Collision detection updated so that the tetriminos won't stick to the sides of the board anymore
//Mohammadreza Abolhassani 2034569      2023-03-09      Tetrimino Rotation feature added
//Mohammadreza Abolhassani 2034569      2023-03-09      Game over condition added

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisGameEngine
{
    public class TetrisBoard
    {
        private int giWidth, giHeight; //global integers for dimentions of the grid
        private Color[,] tetrisGrid; //an array of colors with the above dimentions wich will be initialized with background color
        private int giGravitySpeed; //how fast the tetrimino will move vertically if left alone
        private int giScore; //global integer for score 
        private Tetrimino currentTetrimino; //the current tetrimono the player has controll over
        private bool gbGameOver; //is the game over or not


        //read only property for background color
        public Color BackgroundColor { get => Color.Gray; }
        public int Width { get => giWidth; private set => giWidth = value; }
        public int Height { get => giHeight; private set => giHeight = value; }
        public int GravitySpeed { get => giGravitySpeed; set => giGravitySpeed = value; }
        public bool GameOver { get => gbGameOver; }

        //read only outside access to the grid
        public Color[,] TetrisGrid { get => tetrisGrid; }

        public Tetrimino CurrentTetrimino { get => currentTetrimino; }

        //giving the view read only access to score
        public int Score { get => giScore; }

        //the dimentions of the grid are required to create the object. wind speed optional
        public TetrisBoard(int piWidth, int piHeight, int piGravSpeed = 0)
        {
            this.giWidth = piWidth;
            this.giHeight = piHeight;
            this.giGravitySpeed = piGravSpeed;
            InitializeBoard();
        }
        public void InitializeBoard()
        {
            giScore = 0; //reset score
            gbGameOver = false; //make sure gemaOver is not true

            //create the array of color representing the grid
            tetrisGrid = new Color[giWidth, giHeight];

            //set every cell in the grid to the default(background) color 
            for (int x = 0; x < giWidth; x++)
            {
                for (int y = 0; y < giHeight; y++)
                {
                    tetrisGrid[x, y] = BackgroundColor;
                }
            }

            //generate a new tetrimino
            GenerateTetrimino();
        }

        public void AddScore()
        {
            giScore++;
        }

        private bool DoesCollide(Tetrimino poTetrimino)
        {
            //check for collision with the board's top and bottom boundaries
            if (poTetrimino.PosY < 0 || poTetrimino.PosY > (Height - poTetrimino.Height))
            {
                return true;
            }
            //go through the balloom image pixel by pixel
            for (int x = 0; x < poTetrimino.Width; x++)
            {
                for (int y = 0; y < poTetrimino.Height; y++)
                {
                    if (poTetrimino.ImageGrid[x, y].IsNamedColor) //if that pixel has a color (is not transparent)
                    {
                        if (tetrisGrid[poTetrimino.PosX + x, poTetrimino.PosY + y] != BackgroundColor) //if in that position in the world(board) there is something
                        {
                            return true; //collision has been detected
                        }
                    }
                }
            }
            return false;
        }

        private void GenerateTetrimino()
        {
            //create a new tetrimono of random kind and color at top middle of the grid  
            currentTetrimino = Tetrimino.GenerateRandomTetrimino(Width / 2);
            if (DoesCollide(currentTetrimino))
                gbGameOver = true;
        }

        private void MergeTetrimino()
        {
            //go through the tetrimino image pixel by pixel
            for (int x = 0; x < currentTetrimino.Width; x++)
            {
                for (int y = 0; y < currentTetrimino.Height; y++)
                {
                    if (currentTetrimino.ImageGrid[x, y].IsNamedColor) //if that pixel has a color (is not transparent)
                    {
                        tetrisGrid[currentTetrimino.PosX + x, currentTetrimino.PosY + y] = currentTetrimino.ImageGrid[x, y]; //imprint the color of that pixel on the board
                    }
                }
            }
            CheckFullLines();
        }

        //checks all lines and if find any full, removes it
        private void CheckFullLines()
        {
            bool isLineFull;
            for(int y = Height - 1; y >= 0; y--)  //go through the lines from bottom up
            {
                isLineFull = true;
                for(int x = 0; x < Width; x++) //go through that line pixel by pixel
                {
                    if (tetrisGrid[x, y] == BackgroundColor) //empty cell found, so line is not full
                    {
                        isLineFull = false;
                    }
                }
                if (isLineFull)
                {
                    OmmitLine(y);
                    AddScore();
                }
            }
        }


        //takes a line index and removes it
        public void OmmitLine(int piY)
        {
            for(int y = piY; y > 0; y--) //start from that line and go up, till you reach the second line
            {
                for(int x = 0; x < Width; x++)
                {
                    tetrisGrid[x, y] = tetrisGrid[x, y - 1]; //change the color of each pixel to the color of the one on top of it
                }
            }

            //make sure the first line is empty
            for (int x = 0; x < Width; x++)
            {
                tetrisGrid[x, 0] = BackgroundColor;
            }
        }

        public void MoveTetrimino(int piDeltaX, int piDeltaY)
        {
            //create a temporary tetrimino copy
            Tetrimino tmpTetrimino = currentTetrimino.Clone();

            //move the fake tetrimino
            tmpTetrimino.PosX += piDeltaX;
            tmpTetrimino.PosY += piDeltaY;

            //check for collision with the board's left and right boundaries
            if (tmpTetrimino.PosX < 0 || tmpTetrimino.PosX > (Width - tmpTetrimino.Width))
            {
                //the tetrimono can't go there, but it is not necessarily a collision
                tmpTetrimino.PosX -= piDeltaX; //change back to original x position
            }

            //see if the fake tetrimino collides with the environment (previous objects left in the board or the edges of the board itself)
            if (DoesCollide(tmpTetrimino))
            {
                //merge the tetrimino with the environment
                MergeTetrimino();
                //generate a new tetrimino as current tetrimino
                GenerateTetrimino();
                //add score
                AddScore();
            }
            else
            {
                //move the actual tetrimino
                currentTetrimino.PosX = tmpTetrimino.PosX;
                currentTetrimino.PosY = tmpTetrimino.PosY;
            }
        }

        public void RotateTetrimino()
        {
            //create a temporary tetrimino copy
            Tetrimino tmpTetrimino = currentTetrimino.Clone();

            //move the fake tetrimino
            tmpTetrimino.RotateClockwise();

            //see if the fake tetrimino collides with the environment (previous objects left in the board or the edges of the board itself)
            if (tmpTetrimino.PosX > (Width - tmpTetrimino.Width) || DoesCollide(tmpTetrimino))
            {
                return; //the tetrimono can't rotate
            }
            else
            {
                //move the actual tetrimino
                currentTetrimino.RotateClockwise();
            }
        }

        public void Updade()
        {
            //the gravity affects the tetrimino every frame
            MoveTetrimino(0, GravitySpeed);
        }
    }
}
