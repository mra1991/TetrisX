//Revision history
//Mohammadreza Abolhassani 2034569      2021-12-10      Created the Jtetrimino object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TetrisGameEngine
{
    public class Jtetrimino : Tetrimino
    {
        public Jtetrimino(int piPosX, int piPosY, Color pColor) : base(2, 3, piPosX, piPosY, pColor)
        {
            this.imageGrid = new Color[Width, Height];
            this.imageGrid[0, 2] = TetriminoColor;
            this.imageGrid[1, 0] = TetriminoColor;
            this.imageGrid[1, 1] = TetriminoColor;
            this.imageGrid[1, 2] = TetriminoColor;
        }

        public override Tetrimino Clone()
        {
            return new Jtetrimino(this.giPosX, this.giPosY, this.TetriminoColor);
        }
    }
}
