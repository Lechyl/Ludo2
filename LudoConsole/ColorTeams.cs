using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LudoConsole
{
    class ColorTeams : Team
    {

       Pieces piece;
       Dice dice = new Dice();
     private int roll;
     private int pieceNr = 0;
     private bool notHome = true;
     private int rollCounter = 0;
     private bool canIMove = true;
     private string teamColor = "";
     private string teamLogo = "";
        public ColorTeams(string teamColor1, int homeGround)
        {
            teamColor = teamColor1;
            teamLogo = teamColor[0].ToString();
            piece = new Pieces(teamColor, homeGround, false);
        }

        public Board[] Team(Board[] boards)
        {
            switch (teamLogo)
            {
                case "R":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "B":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "G":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "Y":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    break;
            }
            canIMove = true;
            rollCounter = 0;
            Console.WriteLine("\n\n{0}'s Turn!", teamColor);

            //check if pieces is dead or alive
            for (int i = 0; i < 4; i++)
            {

                if (piece.pieceCordi[i] > -1)
                {
                    if (!boards[piece.pieceCordi[i]].Brikker.StartsWith(teamLogo))
                    {
                        piece.pieceCordi[i] = -1;
                    }
                }


            }
            piece.piecesOut = false;

            for (int i = 0; i < 4; i++)
            {

                if (piece.pieceCordi[i] != -1)
                {
                    piece.piecesOut = true;
                }
            }
            do
            {
                Thread.Sleep(30);
                roll = dice.diceroll(1, 7);
                Thread.Sleep(30);

                rollCounter++;
                Console.WriteLine("You've rolled a {0}", roll);
                if (piece.piecesOut == false && roll != 6)
                {
                    Console.WriteLine("Press enter to reroll");
                }
                //this readline make manually press enter to reroll, remove for automatic 3 rolls.
                Console.ReadLine();
            } while (piece.piecesOut == false && roll != 6 && rollCounter < 3);


            if (roll == 6)
            {
                boards = Revive(boards);
            }
            if (piece.piecesOut == true && canIMove == true)
            {
                boards = Move(boards);
            }

            return boards;
        }




        public Board[] Revive(Board[] boards)
        {
            Console.WriteLine("pieces is on {0}, {1}, {2}, {3}", piece.pieceCordi[0], piece.pieceCordi[1], piece.pieceCordi[2], piece.pieceCordi[3]);

            Console.WriteLine("Do you want to revive a piece Y/n");
            string svar = Console.ReadLine();
            if (svar.ToLower() == "y" || svar == "")
            {

                for (int i = 0; i < 4; i++)
                {
                    if (piece.pieceCordi[i] == -1)
                    {
                        piece.piecesOut = true;
                        canIMove = false;
                        Console.WriteLine("You've revived a piece");
                        if (boards[piece.home].Brikker.Length > 0)
                        {


                            if (boards[piece.home].Brikker.StartsWith(teamLogo))
                            {
                                piece.pieceCordi[i] = piece.home;
                                boards[piece.pieceCordi[i]].Brikker += teamLogo;
                                break;
                            } 
                            else
                            {
                                piece.pieceCordi[i] = piece.home;
                                boards[piece.pieceCordi[i]].Brikker = teamLogo;
                                break;
                            }
                        }
                        else
                        {
                            piece.pieceCordi[i] = piece.home;
                            boards[piece.pieceCordi[i]].Brikker = teamLogo;
                            break;

                        }

                    }
                }


            }

            return boards;
        }
        public Board[] innerCircle(Board[] boards)
        {
            //54 slår 4.
            bool not = false;
            string input = "";
            foreach (char item in boards[piece.pieceCordi[pieceNr]].Brikker)
            {

                if (item == Convert.ToChar(teamLogo) && not == false)
                {
                    not = true;

                }
                else
                {
                    input += item.ToString();
                }
            }
            boards[piece.pieceCordi[pieceNr]].Brikker += input;
            int nextTile = piece.pieceCordi[pieceNr] + roll;

            if (nextTile > 57)
            {
                nextTile = 57 - (nextTile - 57);

            }
            boards[nextTile].Brikker += teamLogo;
            piece.pieceCordi[pieceNr] = nextTile;

            return boards;
        }
        public void Print()
        {
            string text = "pieces is on {0}, {1}, {2}, {3}";
            string text2 = "Chose which piece to move [1][2][3][4]";
            for (int i = 0; i < 4; i++)
            {
                if (piece.pieceCordi[i] == 57)
                {
                    text2.Replace(i.ToString(), string.Empty);

                    text.Replace((i + 1).ToString(), string.Empty);
                }
            }
            Console.WriteLine(text, piece.pieceCordi[0], piece.pieceCordi[1], piece.pieceCordi[2], piece.pieceCordi[3]);

            Console.WriteLine(text2);

        }
        public Board[] Move(Board[] boards)
        {
            bool innerBoard = false;
            do
            {
                Print();
                try
                {
                    pieceNr = int.Parse(Console.ReadLine()) - 1;

                }
                catch(Exception e)
                {
                    Console.WriteLine("Error Wrong: " + e);
                    
                }
                if (piece.pieceCordi[pieceNr] >= (piece.win - 6) && piece.pieceCordi[pieceNr] <= piece.win || piece.pieceCordi[pieceNr] >= 52 && piece.pieceCordi[pieceNr] <= 57 )
                {
                    if (piece.pieceCordi[pieceNr] + roll >= piece.win)
                    {
                        innerBoard = true;

                    }
                }
                if (!innerBoard)
                {

                    if (piece.pieceCordi[pieceNr] > -1 && piece.pieceCordi[pieceNr] < 52)
                    {
                        int oldCordi = piece.pieceCordi[pieceNr];

                        string removed = boards[oldCordi].Brikker;
                  //      Console.WriteLine(removed + " Before");
                        char[] please = removed.ToCharArray();
                        
                        List<char> sad = please.ToList();
                        sad.Remove(Convert.ToChar(teamLogo));
                        bool remove = false;
                        removed = "";
                        foreach (char c in sad)
                        {
                            if (c == Convert.ToChar(teamLogo) && remove == false)
                            {
                                remove = true;
                            }
                            else
                            {
                                removed += c.ToString();

                            }
                        }
                        boards[oldCordi].Brikker = removed;
                        //Console.WriteLine(boards[oldCordi].Brikker + " after");

                        Console.WriteLine("This piece can be moved");
                        if ((piece.pieceCordi[pieceNr] + roll) < 52)
                        {
                            int nextTile = piece.pieceCordi[pieceNr] + roll;
                            if (boards[nextTile].Brikker.Length <= 1)
                            {
                                if (boards[nextTile].Brikker == teamLogo || boards[nextTile].Brikker == "")
                                {
                                    boards[nextTile].Brikker += teamLogo;
                                }
                                else
                                {
                                    boards[nextTile].Brikker = teamLogo;
                                }

                            }
                            else
                            {
                                string check = boards[nextTile].Brikker;
                                if (check.Substring(0, 1) != teamLogo)
                                {
                                    piece.pieceCordi[pieceNr] = -1;
                                    break;
                                }
                            }
                            piece.pieceCordi[pieceNr] = nextTile;
                        }
                        else
                        {


                            int nextTile = (piece.pieceCordi[pieceNr] + roll) - 52;
                            if (boards[nextTile].Brikker.Length <= 1)
                            {
                                if (boards[nextTile].Brikker == teamLogo || boards[nextTile].Brikker == "")
                                {
                                    boards[nextTile].Brikker += teamLogo;
                                }
                                else
                                {
                                    boards[nextTile].Brikker = teamLogo;
                                }

                            }
                            else
                            {
                                string check = boards[piece.pieceCordi[pieceNr]].Brikker;
                                check = check.Substring(0, 1);
                                if (check != teamLogo)
                                {
                                    piece.pieceCordi[pieceNr] = -1;
                                    break;
                                }
                            }
                            piece.pieceCordi[pieceNr] = nextTile;

                        }

                        //  boards[piece.pieceCordi[pieceNr]].Brikker = teamLogo;

                        //array[,] for Cordinates graphic
                        //array[] for each piece on this tile



                        notHome = false;

                    }
                }
                else
                {
                    boards = innerCircle(boards);
                    notHome = false;
                }

            }
            while (notHome);

            notHome = true;
            Console.WriteLine("I'm moving piece {0} to coordinate {1}", pieceNr + 1, piece.pieceCordi[pieceNr]);

            Console.WriteLine("pieces is on {0}, {1}, {2}, {3}", piece.pieceCordi[0], piece.pieceCordi[1], piece.pieceCordi[2], piece.pieceCordi[3]);
            return boards;
        }
    }
}
