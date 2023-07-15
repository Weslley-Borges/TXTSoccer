using TXTSoccer.entities;
using TXTSoccer.helpers;

namespace TXTSoccer
{
    class Program
    {
        static readonly HelperDataHolder data = new();
        static readonly int qtdCadastrados = 25;
        static readonly Random rnd = new();
        static List<Time> times = new();

        public static void Main(string[] args)
        {
            Random rnd = new();

            int r = Select.Show(
                "---------------- TXTSoccer  v1.0 ----------------\n", 
                new List<string>{"Iniciar com times pre-definidos", "Iniciar com times criados."});

            if (r == 0)
            {
                times.Add(new("Real Marola"));
                times.Add(new("Los Cucos"));
                times.Add(new("Xilindro"));
                times.Add(new("Zaparov"));
                times.Add(new("Atletico MG"));
                times.Add(new("Gremio"));
                times.Add(new("Bahia"));
                times.Add(new("Vitoria"));
                times.Add(new("Panthers"));
                times.Add(new("Supercampeoes"));
                times.Add(new("Los Revisionistas"));
                times.Add(new("Tropa do pau medio"));
                times.Add(new("Os Perna-Bamba"));
                times.Add(new("Cabeca pesada"));
            } else
            {
                times = GerarTimes();
            }

            CadastraJogadores();

            r = 0;
            int timeIdx = 0;
            while (r != 2)
            {
                r = new Select().Show(times[timeIdx],
                    new List<string> { "Proximo time", "Time anterior", "Começar o campeonato"});

                switch (r)
                {
                    case 0:
                        if (timeIdx < times.Count - 1) timeIdx++;
                        break;
                    case 1:
                        if (timeIdx != 0) timeIdx--;
                        break;
                    case 2:
                        break;
                }
            }

            Campeonato c = new("Campeonato dos Pe-de-moleque", 100000, times, 10);

            while (c.RodadaAtual <= c.Rodadas)
            {
                c.DefinirJogosRodada();
                c.IniciarRodada();

                c.Times.ForEach(t => t.Time.Plantel.ForEach(j =>
                {
                    j.ExecutarTreino();
                    j.CumprirSuspensao();
                }));
            }
        }

        static void CadastraJogadores()
        {
            int id = 1;

            times.ForEach(t =>
            {
                for (int j = 0; j < qtdCadastrados; j++)
                {

                    int cartoes = rnd.Next(4);
                    int numero = rnd.Next(100);
                    int qualidade = rnd.Next(100);
                    string nome = data.nomesJogadores[rnd.Next(0, data.nomesJogadores.Count)];
                    string apelido = data.apelidosJogadores[rnd.Next(0, data.apelidosJogadores.Count)];
                    Posicao posicao = data.formacao.Keys.ToList()[rnd.Next(0, data.formacao.Keys.ToList().Count)];
                    DateOnly dataNascimento = new DateOnly(rnd.Next(1980, 2003), rnd.Next(1, 12), rnd.Next(1, 28));

                    t.Plantel.Add(new Jogador(id, nome, apelido, posicao, numero, qualidade, cartoes, dataNascimento));

                    id++;
                }
            });
        }

        private static List<Time> GerarTimes()
        {
            int nTimes = 1;
            List<Time> times = new();

            while (nTimes % 2 != 0)
            {
                Console.Write("[?] Digite a quantidade de times (deve ser um numero par): ");
                string? qtdTimes = Console.ReadLine();

                if (qtdTimes == null || int.Parse(qtdTimes) % 2 != 0)
                {
                    PrintException("Input invalido!");
                    continue;
                }

                nTimes = int.Parse(qtdTimes);
            };

            for (int i=0; i < nTimes; i++)
            {
                Console.Write($"[?] Digite o nome do {i+1}o time: ");
                string? nomeTime = Console.ReadLine();

                if (nomeTime == null)
                {
                    PrintException("O nome do time nao pode ser nulo");
                    continue;
                }

                times.Add(new Time(nomeTime));
            }

            return times;

            void PrintException(string message)
            {
                Console.WriteLine($"[!] {message}");
                HelperPrinter.WaitKey("TENTAR NOVAMENTE.");
            }
        }
    }
}