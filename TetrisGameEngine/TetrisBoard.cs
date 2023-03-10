//Revision history
//Mohammadreza Abolhassani 2034569      2021-12-10      Created the TetrisBoard object.
//Mohammadreza Abolhassani 2034569      2023-03-09      Collision detection updated so that the tetriminos won't stick to the sides of the board anymore
//Mohammadreza Abolhassani 2034569      2023-03-09      Tetrimino Rotation feature added
//Mohammadreza Abolhassani 2034569      2023-03-09      Game over condition added
//Mohammadreza Abolhassani 2034569      2023-03-09      Level system added
//Mohammadreza Abolhassani 2034569      2023-03-09      Feature showing next tetrimino in line added


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
        private Tetrimino nextTetrimino; //the next tetrimono in line
        private Tetrimino currentTetrimino; //the current tetrimono the player has controll over
        private bool gbGameOver; //is the game over or not
        private int giLevel; //starting at level one, int each level that number of rows need to be completed
        private int giRowsCompleted; //the number of rows completed in this level
        private int giTicksPerUpdate; //the number of ticks of the timer between game updates
        const int MAX_TICK_PER_UPDATE = 64; //initial value for ticks per update

        //read only property for background color
        public Color BackgroundColor { get => Color.Gray; }
        public int Width { get => giWidth; private set => giWidth = value; }
        public int Height { get => giHeight; private set => giHeight = value; }
        public int GravitySpeed { get => giGravitySpeed; set => giGravitySpeed = value; }
        public bool GameOver { get => gbGameOver; }

        public int TicksPerUpdate { get => giTicksPerUpdate; }

        //read only outside access to the grid
        public Color[,] TetrisGrid { get => tetrisGrid; }

        public Tetrimino CurrentTetrimino { get => currentTetrimino; }
        public Tetrimino NextTetrimino { get => nextTetrimino; }


        //giving the view read only access to score
        public int Score { get => giScore; }

        public int Level { get => giLevel; }

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
            giLevel = 1; //start at level 1;
            giRowsCompleted = 0; //0 rows completed in this level
            giTicksPerUpdate = MAX_TICK_PER_UPDATE; //set the time between updated to maximum

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

            //create the first tetrimono of random kind and color   
            nextTetrimino = Tetrimino.GenerateRandomTetrimino(Width / 2);

            //generate a new tetrimino
            GenerateTetrimino();
        }

        public void AddScore(int piScore)
        {
            giScore += piScore;
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
            //bring the next tetramino in line to replace the current tetramino
            currentTetrimino = nextTetrimino;
            //create a new tetrimono of random kind and color at top middle of the grid  
            nextTetrimino = Tetrimino.GenerateRandomTetrimino(Width / 2);
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
                if (isLineFull) //if a full row has been found
                {
                    OmmitLine(y); //omit the row
                    AddScore(100); //add 100 scores
                    giRowsCompleted++; //increase the number of rows completed in this level
                    if(giRowsCompleted == giLevel) //if the level goal has been reached
                    {
                        giLevel++; //level up
                        giRowsCompleted = 0; //no rows completed in the new level yet
                        if(giTicksPerUpdate >= 2)
                            giTicksPerUpdate /= 2; //if possible, double the speed of the game 
                    }
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
                AddScore(1);
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
