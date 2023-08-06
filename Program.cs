using TXTSoccer.entities;
using TXTSoccer.helpers;

namespace TXTSoccer
{
	class Program
	{
		static readonly HelperDataHolder data = new();
		static readonly Random rnd = new();

		public static void Main(string[] args)
		{
			Console.WriteLine("---------------- TXTSoccer  v1.1.0 ----------------\n");
			HelperPrinter.WaitKey("COMEÇAR!");


			Campeonato c = new("Campeonato dos Pe-de-moleque", 10);
			c.Series.Add(new("Serie A", 1000000));
			c.Series.Add(new("Serie B", 500000));
			c.Series.Add(new("Serie C", 250000));

			c.Series.ForEach(s =>
			{
				for (int i=0; i < 10; i++)
				{
					string nome1 = HelperDataHolder.timesNomes1[rnd.Next(0, HelperDataHolder.timesNomes1.Count - 1)];
					string nome2 = HelperDataHolder.timesNomes2[rnd.Next(0, HelperDataHolder.timesNomes2.Count - 1)];

                    Time t = new($"{nome1} {nome2}") { Plantel = CadastraJogadores() };

                    s.Times.Add(new TimeParticipante(t));
                }
				
			});

			c.Series.ForEach(s => s.ShowJogadoresTime());
			
			while (c.TemporadaAtual <= c.Temporadas)
			{
                while (c.RodadaAtual <= c.Rodadas)
                    c.IniciarRodada();
				c.TerminarTemporada();
            }
		}

		/// <summary>
		/// Registro dos jogadores em um time
		/// </summary>
		/// <returns>O plantel com todos os jogadores daquele time</returns>
		public static List<Jogador> CadastraJogadores()
		{
			int id = 1;
			List<Jogador> plantel = new();

			for (int j = 0; j < 20; j++)
			{
				int numero = rnd.Next(100);
				int qualidade = rnd.Next(100);
				string nome = data.nomesJogadores[rnd.Next(0, data.nomesJogadores.Count)];
				string apelido = data.apelidosJogadores[rnd.Next(0, data.apelidosJogadores.Count)];
				Posicao posicao = data.formacao.Keys.ToList()[rnd.Next(0, data.formacao.Keys.ToList().Count)];
				DateOnly dataNascimento = new DateOnly(rnd.Next(1980, 2003), rnd.Next(1, 12), rnd.Next(1, 28));

				plantel.Add(new Jogador(id, nome, apelido, posicao, numero, qualidade, dataNascimento));

				id++;
			}

			return plantel;
		}
	}
}