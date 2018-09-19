using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LudoConsole
{
    class AITeam
    {
        //This is an AI
            Pieces piece;
            Dice dice = new Dice();
            int roll;
            int pieceNr = 0;
            bool notHome = true;
            int rollCounter = 0;
            bool canIMove = true;
            //int[] piece.pieceCordi = new int[4] { 0, 0, 0, 0 };
            string teamColor = "";
            public string teamLogo = "";
            public AITeam(string teamColor1, int homeGround)
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
            pieceNr = -1;
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
                do
                {
                    roll = dice.diceroll(1, 7);
                    Thread.Sleep(30);

                    rollCounter++;
                    Console.WriteLine("You've rolled a {0}", roll);
                    if (piece.piecesOut == false && roll != 6)
                    {
                        Console.WriteLine("Press enter to reroll");
                    }
                    //this readline make manually press enter to reroll, remove for automatic 3 rolls.
                   // Console.ReadLine();
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

                Console.WriteLine("Do you want to revive a piece y/n");


                    for (int i = 0; i < 4; i++)
                    {
                        if (piece.pieceCordi[i] == -1)
                        {
                        Console.WriteLine("Yes, I want to revive a piece");
                         canIMove = false;

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
                else { Console.WriteLine("No, All my pieces are out"); }

                
                    }

                    piece.piecesOut = true;
                

                return boards;
            }
            public Board[] innerCircle(Board[] boards)
            {
                //54 slår 4.
                int i = 0;
                foreach (char item in boards[piece.pieceCordi[pieceNr]].Brikker)
                {

                    if (item == Convert.ToChar(teamLogo))
                    {
                        boards[piece.pieceCordi[pieceNr]].Brikker.Remove(i, 1);
                        break;
                    }
                    i++;
                }
                i = 0;
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
                        text2.Replace(i.ToString(), "");

                        text.Replace((i + 1).ToString(), "");
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
                //pieceNr = int.Parse(Console.ReadLine()) - 1;
                for (int i = 0; i < 4; i++)
                {
                    if(boards[piece.pieceCordi[i] + roll].Brikker.Length == 1)
                    {
                        pieceNr = i;
                        break;
                    }

                }
                if(pieceNr == -1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if(piece.pieceCordi[i] + roll == 57)
                        {
                            pieceNr = i;
                            innerBoard = true;
                        }
                    }
                }
                if(pieceNr == -1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (piece.pieceCordi[i] >= (piece.win - 6) && piece.pieceCordi[i] <= piece.win)
                        {
                            if (piece.pieceCordi[i] + roll >= piece.win)
                            {
                                pieceNr = i;
                                innerBoard = true;

                            }
                        }
                    }
                }
                if(pieceNr != null)
                {
                    int count = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if(piece.pieceCordi[i] > -1)
                        {
                            count++;
                        }
                    }
                    pieceNr = dice.diceroll(0, count);
                }

                    if (!innerBoard)
                    {

                        if (piece.pieceCordi[pieceNr] > -1 && piece.pieceCordi[pieceNr] < 52)
                        {
                            int oldCordi = piece.pieceCordi[pieceNr];

                            //delete old cordi
                            //         int deleteChar = boards[oldCordi].Brikker.IndexOf(teamLogo);
                            boards[piece.pieceCordi[pieceNr]].Brikker.Remove(0, 1);

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

                                piece.pieceCordi[pieceNr] = (piece.pieceCordi[pieceNr] + roll) - 52;

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
                                    if (check.Substring(0, 1) != teamLogo)
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

                Console.WriteLine("pieces is on {0}, {1}, {2}, {3}", piece.pieceCordi[0], piece.pieceCordi[1], piece.pieceCordi[2], piece.pieceCordi[3]);
                return boards;
            }
        }
    }


