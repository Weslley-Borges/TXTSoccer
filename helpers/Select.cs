using System.ComponentModel;
using TXTSoccer.entities;

namespace TXTSoccer.helpers
{
    internal class Select
    {
        public static Select? instance = null;
        public static Select Instance
        {
            get
            {
                instance ??= new Select();
                return instance;
            }
        }

        private int GetChoice(List<string> choices)
        {
            return 0;
        }

        public static int Show(string question, List<string> choices)
        {
            int choice = 0;

            while (true)
            {
                Console.WriteLine(question);

                choices.ForEach(c =>
                {
                    string selector = choices.IndexOf(c) == choice ? "->" : "-";
                    Console.WriteLine($"{selector} {c}");
                });

                Console.WriteLine("\n[Setas cima e baixo] - Mudar opcao\t[ENTER] - Selecionar opcao");

                var ch = Console.ReadKey(false).Key;

                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        if (choice > 0)
                            choice -= 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (choice < choices.Count - 1)
                            choice += 1;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return choice;
                }

                Console.Clear();
            }
        }


        public int Show(Time t, List<string> choices)
        {
            int choice = 0;

            while (true)
            {
                HelperPrinter.ImprimirJogadores(t);

                choices.ForEach(c =>
                {
                    string selector = choices.IndexOf(c) == choice ? "->" : "-";
                    Console.WriteLine($"{selector} {c}");
                });

                Console.WriteLine("[Setas cima e baixo] - Mudar opcao\t[ENTER] - Selecionar opcao");

                var ch = Console.ReadKey(false).Key;

                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        if (choice > 0)
                            choice -= 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (choice < choices.Count - 1)
                            choice += 1;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return choice;
                }

                Console.Clear();
            }
        }
    }
}
