using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole
{
    public class Pieces
    {
        private string farve;
        // public bool active { get; set; }
        public bool piecesOut { get; set; }
        public int win { get; }
        public int home { get; set; }

        public int[] pieceCordi = new int[4] { -1, -1, -1, -1 };

        public Pieces(string Farve, int Home, bool PiecesOut)
        {
            piecesOut = PiecesOut;
            farve = Farve;
            home = Home;
            win = (home + 50) % 52;


        }

        

    }

}
