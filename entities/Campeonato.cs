using System.Collections.Generic;
using System.Linq;
using TXTSoccer.helpers;


namespace TXTSoccer.entities
{
	/// <summary>
	/// Uma classe para controlar os eventos e características de um campeonato de futebol.
	/// </summary>
	internal class Campeonato
	{
		public string Name { get; set; }
		public int Temporadas { get; set; } = 3;
        public int TemporadaAtual { get; set; } = 1;
        public int Rodadas { get; set; }
		public int RodadaAtual { get; set; } = 1;
		public List<Serie> Series { get; set; } = new();

		public Campeonato(string name, int rodadas)
		{
			Name = name;
			Rodadas = rodadas;
		}

		/// <summary>
		/// Determina os jogos da rodada atual de cada <see cref="Serie"/>. <br/>
		/// </summary>
		public void DefinirJogosRodada()
		{
			Series.ForEach(s => s.DefinirJogosRodada());
		}

		/// <summary>
		/// Inicia dos <see cref="Jogo">jogos</see> da rodada atual.<br/>
		/// Imprime os jogos de todas as séries na rodada atual do campeonato.
		/// </summary>
		public void IniciarRodada()
		{
			Series.ForEach(s => s.IniciarRodada());
			List<string> options = new(){ "Ir para a tabela", "Ver jogos" };

			do
			{
				Series.ForEach(s => HelperPrinter.ImprimirJogosRodada(this, s));
				SelectInput.instance = Select.Instance.GetChoice(options, SelectInput.Instance);

			} while (SelectInput.instance.Key != ConsoleKey.Enter);

			if (SelectInput.instance.ChoiceIndex == 1)
				Series.ForEach(s => s.ShowEscalacaoTimesJogo());

			HelperPrinter.ImprimirTabelaTimesCampeonato(this);

			RodadaAtual++;
		}

		public void TerminarTemporada()
		{
			List<List<TimeParticipante>> RebaixadosCampeonato = new();
			List<List<TimeParticipante>> PromovidosCampeonato = new();
			List<Serie> NovasSeries = new();

			/*
			 * Separa os times promovidos de cada série (menos os da série principal)
			 * e os times rebaixados de cada série (menos os da última série)
			 */
		  
			for (int i = 0; i < Series.Count; i++)
			{
                Series[i].Times.Sort((x, y) => y.GetPontuacao().CompareTo(x.GetPontuacao()));
                int nTimes = Series[i].Times.Count - 1;

                if (i != Series.IndexOf(Series.Last()))
				{
					List<TimeParticipante> RebaixadosSerie = new();

					RebaixadosSerie.AddRange(Series[i].Times.GetRange(nTimes - 3, 3));
					RebaixadosCampeonato.Add(RebaixadosSerie);
                    Series[i].Times.RemoveRange(nTimes - 4, 3);

					RebaixadosSerie.ForEach(t =>
					{
						Console.WriteLine($"{t.Time.Nome} foi rebaixado da serie {Series[i].Name}");
					});
				}

				if (i != 0)
				{
					List<TimeParticipante> PromovidosSerie = new();

					PromovidosSerie.AddRange(Series[i].Times.GetRange(0, 3));
					PromovidosCampeonato.Add(PromovidosSerie);
                    Series[i].Times.RemoveRange(0, 3);

                    PromovidosSerie.ForEach(t =>
                    {
                        Console.WriteLine($"{t.Time.Nome} foi promovido da serie {Series[i].Name}");
                    });
                }

			}

			/*
			 * Monta as próximas séries, começando pelos rebaixados, seguidos pelos
			 * times que não mudaram de série e, por último, os promovidos
			 */
			int RebaixadoIDX = 0;
            int PromovidoIDX = 0;

            for (int i = 0; i < Series.Count; i++)
			{
                Serie s = new(Series[i].Name, Series[i].Prize);

                if (i != 0)
				{
					RebaixadosCampeonato[RebaixadoIDX].ForEach(t => s.Times.Add(t));
					RebaixadoIDX++;
				}

				Series[i].Times.ForEach(t => s.Times.Add(t));

                if (i != Series.IndexOf(Series.Last()))
                {
                    PromovidosCampeonato[PromovidoIDX].ForEach(t => s.Times.Add(t));
                    PromovidoIDX++;
                }

				s.ResetarPontuacoes();
				NovasSeries.Add(s);
            }

			Series = NovasSeries;
			Temporadas++;
			RodadaAtual = 1;
        }
	}
}
