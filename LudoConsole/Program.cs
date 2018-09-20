using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole
{
    class Program
    {
        //mafpgpiødød
        public static Dice dice = new Dice();
        public static Board[] boards = new Board[58];
        // public static Board[] innerBoards = new Board[6];
        public static ColorTeams redTeam = new ColorTeams("Red", 0);
        public static ColorTeams blueTeam = new ColorTeams("Blue", 13);
        public static ColorTeams greenTeam = new ColorTeams("Green", 26);
        public static ColorTeams yellowTeam = new ColorTeams("Yellow", 39);
        public static AITeam redAITeam = new AITeam("Red AI", 0);
        public static AITeam blueAITeam = new AITeam("Blue AI", 13);
        public static AITeam greenAITeam = new AITeam("Green AI", 26);
        public static AITeam yellowAITeam = new AITeam("Yellow AI", 39);
        
        public static Menu menu = new Menu();
        public static bool winning = false;
        // enum TeamColor {Red,Blue,Green,Yellow }


        static void Main(string[] args)
        {
            
            GamePlay();
        }


        public static void GamePlay()
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

        public static void Winning()
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
    class Menu
    {
        string[] Chose = new string[4] { "Red", "Blue", "Green", "Yellow" };
        public List<string> players = new List<string>();
       // public List<string> AI = new List<string>();

        // public string[] Players = new string[] {"","","","" };
        public void menu()
        {

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Player {0}, Chose a color by number [1-4], done when you're ready", i + 1);
                for (int e = 0; e < Chose.Length; e++)
                {
                    Console.WriteLine(Chose[e]);
                }
                string svar = Console.ReadLine();

                if (svar.ToLower() == "done")
                {
                    for (int e  = 0; e < 4; e++)
                    {
                        if(Chose[e].Length > 2)
                        {
                            players.Add(Chose[e] +"AI");
                        }
                    }
                    break;
                }
                else
                {
                    int svarint = int.Parse(svar) - 1;

                    Console.WriteLine(svarint);
                    players.Add(Chose[svarint]);

                    Chose[svarint] = "";

                }
            }

        }

    }
}
