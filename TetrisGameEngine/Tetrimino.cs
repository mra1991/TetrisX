//Revision history
//Mohammadreza Abolhassani 2034569      2021-12-10      Created the Tetrimino object.

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

        public int Width { get => giWidth; protected set => giWidth = value; }
        public int Height { get => giHeight; protected set => giHeight = value; }
        public int PosX { get => giPosX; set => giPosX = value; }
        public int PosY { get => giPosY; set => giPosY = value; }
        public Color TetriminoColor { get => tetriminoColor; set => tetriminoColor = value; }

        //read only property for image array
        public Color[,] ImageGrid { get => imageGrid; }
    }
}
