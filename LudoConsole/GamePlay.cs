using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole
{
    class GamePlay
    {
        Board[] boards = new Board[58];
        Menu menu = new Menu();
        ColorTeams redTeam = new ColorTeams("Red", 0);
        ColorTeams blueTeam = new ColorTeams("Blue", 13);
        ColorTeams greenTeam = new ColorTeams("Green", 26);
        ColorTeams yellowTeam = new ColorTeams("Yellow", 47);
        AITeam redAITeam = new AITeam("Red AI", 0);
        AITeam blueAITeam = new AITeam("Blue AI", 13);
        AITeam greenAITeam = new AITeam("Green AI", 26);
        AITeam yellowAITeam = new AITeam("Yellow AI", 39);

        bool winning = false;
        public void Play()
        {

            int counter = 1;


            menu.menu();
            for (int i = 0; i < 58; i++)
            {
                boards[i] = new Board()
                {
                    Brikker = ""
                };
            }
            while (!winning)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("\nRound {0} ", counter);
                Console.ResetColor();

                for (int i = 0; i < menu.players.Count; i++)
                {
                    switch (menu.players[i])
                    {
                        //Player Teams
                        case "Red":
                            boards = redTeam.Team(boards);

                            break;
                        case "Blue":
                            boards = blueTeam.Team(boards);

                            break;
                        case "Green":
                            boards = greenTeam.Team(boards);

                            break;
                        case "Yellow":
                            boards = yellowTeam.Team(boards);

                            break;
                        // AI Teams
                        case "RedAI":
                            boards = redAITeam.Team(boards);

                            break;
                        case "BlueAI":
                            boards = blueAITeam.Team(boards);

                            break;
                        case "GreenAI":
                            boards = greenAITeam.Team(boards);

                            break;
                        case "YellowAI":
                            boards = yellowAITeam.Team(boards);

                            break;
                        default:
                            break;
                    }
                    Winning();
                }


                counter++;
            }
            Console.WriteLine("We has a Winner!");
            Console.ReadLine();
        }

        public void Winning()
        {
            int red = 0;
            int blue = 0;
            int green = 0;
            int yellow = 0;
            foreach (char item in boards[57].Brikker)
            {
                switch (item)
                {
                    case 'R':
                        red++;
                        break;
                    case 'B':
                        blue++;
                        break;
                    case 'G':
                        green++;
                        break;
                    case 'Y':
                        yellow++;
                        break;
                    default:
                        break;
                }
            }
            if (red >= 4 || blue >= 4 || green >= 4 || yellow >= 4)
            {
                winning = true;
            }
        }
    }
}
