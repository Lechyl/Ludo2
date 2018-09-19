using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole
{
    class Dice
    {
        private static Random random = new Random();
        public int diceroll(int a, int b)
        {
            int i = random.Next(a, b);
            return i;
        }

    }
}
