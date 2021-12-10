//Revision history
//Mohammadreza Abolhassani 2034569      2021-12-10      Created the Ltetrimino object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisGameEngine
{
    public class Ltetrimino : Tetrimino
    {
        public Ltetrimino(int piPosX, int piPosY, Color pColor) : base(2, 3, piPosX, piPosY, pColor)
        {
            this.imageGrid = new Color[Width, Height];
            this.imageGrid[1, 2] = TetriminoColor;
            this.imageGrid[0, 0] = TetriminoColor;
            this.imageGrid[0, 1] = TetriminoColor;
            this.imageGrid[0, 2] = TetriminoColor;
        }

        public override Tetrimino Clone()
        {
            return new Ltetrimino(this.giPosX, this.giPosY, this.TetriminoColor);
        }
    }
}
