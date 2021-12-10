//Revision history
//Mohammadreza Abolhassani 2034569      2021-12-10      Created the Ttetrimino object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisGameEngine
{
    public class Ttetrimino : Tetrimino
    {
        public Ttetrimino(int piPosX, int piPosY, Color pColor) : base(3, 2, piPosX, piPosY, pColor)
        {
            this.imageGrid = new Color[Width, Height];
            this.imageGrid[0, 0] = TetriminoColor;
            this.imageGrid[1, 0] = TetriminoColor;
            this.imageGrid[1, 1] = TetriminoColor;
            this.imageGrid[2, 0] = TetriminoColor;
        }

        public override Tetrimino Clone()
        {
            return new Ttetrimino(this.giPosX, this.giPosY, this.TetriminoColor);
        }
    }
}
