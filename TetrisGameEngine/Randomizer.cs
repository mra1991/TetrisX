//Revision history
//Mohammadreza Abolhassani 2034569      2021-12-10      Created the Randomizer object.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace TetrisGameEngine
{
    public class Randomizer : Random
    {
        //singleton instance
        private static Randomizer instance = null;

        private Randomizer() : base()
        {

        }


        //implements singleton design and provides public access to the singleton instance
        public static Randomizer Instance()
        {
            try
            {
                if (instance == null)
                {
                    instance = new Randomizer();
                }
                return instance;
            }
            catch (Exception e)
            {
                throw new Exception("<ERROR> ", e);
            }
        }

        //the following method returns a random integer between 1 and six
        public static int RollDice()
        {
            return Instance().Next(6) + 1;
        }

        //rolls a dice and returns a rendom color

        public static Color GenerateRandomColor()
        {
            Color randomColor;
            switch (Randomizer.RollDice())
            {
                case 1:
                    randomColor = Color.Green;
                    break;
                case 2:
                    randomColor = Color.Blue;
                    break;
                case 3:
                    randomColor = Color.Yellow;
                    break;
                case 4:
                    randomColor = Color.Red;
                    break;
                case 5:
                    randomColor = Color.Orange;
                    break;
                case 6:
                    randomColor = Color.Purple;
                    break;
                default:
                    randomColor = Color.Black;
                    break;
            }
            return randomColor;
        }
    }
}
