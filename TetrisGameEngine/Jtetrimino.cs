//Revision history
//Mohammadreza Abolhassani 2034569      2021-12-10      Created the Jtetrimino object.
//Mohammadreza Abolhassani 2034569      2023-03-09      Tetrimino Rotation feature added

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
            //create a new tetrimino of the same type;
            Jtetrimino tmpTetrimino = new Jtetrimino(this.giPosX, this.giPosY, this.TetriminoColor);
            //match the state of rotation of the new tetrimino to this one
            for (int i = 0; i < numOfRotations; i++)
                tmpTetrimino.RotateClockwise();
            return tmpTetrimino; // return the new tetrimino
        }
    }
}
