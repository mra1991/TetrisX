//Revision history
//Mohammadreza Abolhassani 2034569      2021-12-10      Created the Otetrimino object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisGameEngine
{
    public class Otetrimino :Tetrimino
    {
        public Otetrimino(int piPosX, int piPosY, Color pColor) : base(2, 2, piPosX, piPosY, pColor)
        {
            this.imageGrid = new Color[Width, Height];
            this.imageGrid[0, 0] = TetriminoColor;
            this.imageGrid[0, 1] = TetriminoColor;
            this.imageGrid[1, 0] = TetriminoColor;
            this.imageGrid[1, 1] = TetriminoColor;
        }

        public override Tetrimino Clone()
        {
            return new Otetrimino(this.giPosX, this.giPosY, this.TetriminoColor);
        }
    }
}
