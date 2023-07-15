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

        private static Dictionary<ConsoleKey, int> GetChoice(List<string> choices, int choice)
        {
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
                    break;
            }

            Console.Clear();
            return new Dictionary<ConsoleKey, int> { { ch, choice} };
        }

        public static int Show(Campeonato c, List<string> choices)
        {
            int choice = 0;

            while (true)
            {
                HelperPrinter.ImprimirJogosRodada(c);

                Dictionary<ConsoleKey, int> input = GetChoice(choices, choice);
                choice = input.Values.ToList()[0];

                if (input.Keys.ToList()[0] == ConsoleKey.Enter)
                    return choice;
            }
        }

        public static int Show(string question, List<string> choices)
        {
            int choice = 0;

            while (true)
            {
                Console.WriteLine(question);

                Dictionary<ConsoleKey, int> input = GetChoice(choices, choice);
                choice = input.Values.ToList()[0];

                if (input.Keys.ToList()[0] == ConsoleKey.Enter)
                    return choice;
            }
        }


        public static int Show(Time t, List<string> choices)
        {
            int choice = 0;

            while (true)
            {
                HelperPrinter.ImprimirJogadores(t);

                Dictionary<ConsoleKey, int> input = GetChoice(choices, choice);
                choice = input.Values.ToList()[0];

                if (input.Keys.ToList()[0] == ConsoleKey.Enter)
                    return choice;
            }
        }

        public static int Show(Jogo j, List<string> choices)
        {
            int choice = 0;

            while (true)
            {
                HelperPrinter.ImprimirJogadores(j.Mandante.Nome, j.EscalacaoMandante);
                HelperPrinter.ImprimirJogadores(j.Visitante.Nome, j.EscalacaoVisitante);

                Dictionary<ConsoleKey, int> input = GetChoice(choices, choice);
                choice = input.Values.ToList()[0];

                if (input.Keys.ToList()[0] == ConsoleKey.Enter)
                    return choice;
            }
        }
    }
}
