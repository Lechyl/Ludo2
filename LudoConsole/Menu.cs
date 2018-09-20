using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole
{
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
                    for (int e = 0; e < 4; e++)
                    {
                        if (Chose[e].Length > 2)
                        {
                            players.Add(Chose[e] + "AI");
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
