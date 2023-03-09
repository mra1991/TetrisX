//Revision history
//Mohammadreza Abolhassani 2034569      2021-12-10      Created the Tetrimino object.
//Mohammadreza Abolhassani 2034569      2023-03-09      Tetrimino Rotation feature added


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisGameEngine
{
    public class Tetrimino
    {
        protected int giWidth, giHeight; //global integers for dimentions of the tetrimino
        protected int giPosX, giPosY; //global integers for position of the tetrimino
        protected Color[,] imageGrid; //an array of colors with the above dimentions to represent the graphics of the tetrimino
        protected Color tetriminoColor; //default color of the tetrimino
        protected int numOfRotations; //how many times the tetrimino has been rotated clockwise? can retain a value of 0, 1, 2 or 3

        public Tetrimino(int piWidth, int piHeight, int piPosX, int piPosY, Color pColor)
        {
            this.giWidth = piWidth;
            this.giHeight = piHeight;
            this.giPosX = piPosX;
            this.giPosY = piPosY;
            this.tetriminoColor = pColor;
        }

        public virtual Tetrimino Clone()
        {
            return new Tetrimino(this.giWidth, this.giHeight, this.giPosX, this.giPosY, this.tetriminoColor);
        }

        //Rotates the imageGrid matrix 90 degrees clockwise
        public void RotateClockwise()
        {
            if(this.imageGrid != null)
            {
                //make a new matrix with reverse dimetnions and fill it 
                Color[,] newGrid = new Color[Height, Width];
                for(int i = 0; i < Width; i++)
                    for (int j = 0; j < Height; j++)
                        newGrid[Height - j - 1, i] = imageGrid[i, j];

                //swap height and width
                int tmpHeight = giHeight;
                giHeight = giWidth;
                giWidth = tmpHeight;
                //replace old matrix by new matrix
                imageGrid = newGrid;

                numOfRotations++; //increase the number of rotations 
                numOfRotations %= 4; //make sure number of rotations remains under 4
            }
        }
        public int Width { get => giWidth; protected set => giWidth = value; }
        public int Height { get => giHeight; protected set => giHeight = value; }
        public int PosX { get => giPosX; set => giPosX = value; }
        public int PosY { get => giPosY; set => giPosY = value; }
        public Color TetriminoColor { get => tetriminoColor; set => tetriminoColor = value; }

        //read only property for image array
        public Color[,] ImageGrid { get => imageGrid; }

        public static Tetrimino GenerateRandomTetrimino(int piPosX)
        {
            //choose a random color for the new tetrimino
            Color randomColor = Randomizer.GenerateRandomColor();

            //create a new tetrimono of random kind top at the given x position 
            Tetrimino newTetrimino;
            switch (Randomizer.Instance().Next(7) + 1)
            {
                case 1:
                    newTetrimino = new Itetrimino(piPosX, 0, randomColor);
                    break;
                case 2:
                    newTetrimino = new Jtetrimino(piPosX, 0, randomColor);
                    break;
                case 3:
                    newTetrimino = new Ltetrimino(piPosX, 0, randomColor);
                    break;
                case 4:
                    newTetrimino = new Otetrimino(piPosX, 0, randomColor);
                    break;
                case 5:
                    newTetrimino = new Stetrimino(piPosX, 0, randomColor);
                    break;
                case 6:
                    newTetrimino = new Ttetrimino(piPosX, 0, randomColor);
                    break;
                case 7:
                    newTetrimino = new Ztetrimino(piPosX, 0, randomColor);
                    break;
                default:
                    newTetrimino = new Tetrimino(0, 0, 0, 0, randomColor);
                    break;
            }
            return newTetrimino;
        }
    }
}
