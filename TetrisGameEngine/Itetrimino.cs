//Revision history
//Mohammadreza Abolhassani 2034569      2021-12-10      Created the Itetrimino object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisGameEngine
{
    public class Itetrimino : Tetrimino
    {
        public Itetrimino(int piPosX, int piPosY, Color pColor) : base(1, 4, piPosX, piPosY, pColor)
        {
            this.imageGrid = new Color[Width, Height];
            this.imageGrid[0, 0] = TetriminoColor;
            this.imageGrid[0, 1] = TetriminoColor;
            this.imageGrid[0, 2] = TetriminoColor;
            this.imageGrid[0, 3] = TetriminoColor;
        }

        public override Tetrimino Clone()
        {
            return new Itetrimino(this.giPosX, this.giPosY, this.TetriminoColor);
        }
    }
}
