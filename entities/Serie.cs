using TXTSoccer.helpers;

namespace TXTSoccer.entities
{
	internal class Serie
	{
		public string Name { get; set; }
		public double Prize { get; set; }
		public List<Jogo> JogosRodada { get; set; } = new();
		public List<TimeParticipante> Times { get; set; } = new();

		public Serie(string name, double prize)
		{
			Name = name;
			Prize = prize;
		}

		/// <summary>
		/// Determina os jogos da rodada atual. <br/>
		/// Os times jogam contra aqueles com 1 colocação de diferença, decidida pelo seu index.
		/// </summary>
		public void DefinirJogosRodada()
		{
			JogosRodada = new();

			Times.Sort((x, y) => y.GetPontuacao().CompareTo(x.GetPontuacao()));

			Times.ForEach(t =>
			{
				if (Times.IndexOf(t) != 0 && Times.IndexOf(t) % 2 != 0)
					JogosRodada.Add(new Jogo(t.Time, Times[Times.IndexOf(t) - 1].Time));
			});
		}

		/// <summary>
		/// Inicia os<see cref="Jogo">jogos</see> da rodada atual.<br/>
		/// </summary>
		public void IniciarRodada()
		{
			JogosRodada.ForEach(j =>
			{
				j.IniciarJogo();
				ContabilizarResuiltadoJogo(j);
			});

			// Pos-rodada
			Times.ForEach(t =>
			{
				t.Time.Plantel.ForEach(j =>
				{
					j.ExecutarTreino();
					j.CumprirSuspensao();
				});
			});
		}

		/// <summary>
		/// Contabiliza as pontuações pós-jogo.<br/>
		/// </summary>
		/// <param name="j">Jogo da rodada atual a ser contabilizado</param>
		private void ContabilizarResuiltadoJogo(Jogo j)
		{
			TimeParticipante? mandante = Times.Find(t => t.Time == j.Mandante);
			TimeParticipante? visitante = Times.Find(t => t.Time == j.Visitante);

			if (mandante is null || visitante is null) return;

			if (j.PlacarMandante > j.PlacarVisitante)
			{
				mandante.vitorias++;
				visitante.derrotas++;
			}
			else if (j.PlacarMandante < j.PlacarVisitante)
			{
				mandante.derrotas++;
				visitante.vitorias++;
			}
			else
			{
				mandante.empates++;
				visitante.empates++;
			}

			mandante.golsFeitos += j.PlacarMandante;
			mandante.golsTomados += j.PlacarVisitante;

			visitante.golsFeitos += j.PlacarVisitante;
			visitante.golsTomados += j.PlacarMandante;
		}

		public void ShowRegistroTimes()
		{
			int timeIdx = 0;

			while (true)
			{
				int input = Select.Show(Times[timeIdx].Time,
					new List<string> { "Proximo time", "Time anterior", "Proxima serie" });

				switch (input)
				{
					case 0:
						if (timeIdx < Times.Count - 1) timeIdx++;
						break;
					case 1:
						if (timeIdx != 0) timeIdx--;
						break;
					case 2:
						break;
				}

				if (input == 2)
					break;
			}
		}

		public void ShowJogosRodada()
		{
			int jogoIdx = 0;
			int input = 0;

			while (input != 2)
			{
				input = Select.Show(
					this,JogosRodada[jogoIdx],
					new List<string> { "Proximo jogo", "Jogo anterior", "Ir para a tabela" 
					});

				switch (input)
				{
					case 0:
						if (jogoIdx < JogosRodada.Count - 1) jogoIdx++;
						break;
					case 1:
						if (jogoIdx != 0) jogoIdx--;
						break;
					case 2:
						break;
				}
			}
		}
	}
}
