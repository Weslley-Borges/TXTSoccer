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
					JogosRodada.Add(new Jogo(t, Times[Times.IndexOf(t) - 1]));
			});
		}

		/// <summary>
		/// Inicia os<see cref="Jogo">jogos</see> da rodada atual.<br/>
		/// </summary>
		public void IniciarRodada()
		{
			JogosRodada.ForEach(j => j.IniciarJogo());

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

		public void ResetarPontuacoes() 
		{
			Times.ForEach(t => t.ResetarPontuacao());
		}

        /// <summary>
        /// Imprime a tabela de jogadores de um time
        /// </summary>
        /// <param name="t">O <see cref="Time"/></param>
        /// <param name="options">Lista de opções disponíveis</param>
        /// <returns>Índice da opção escolhida</returns>
        public void ShowJogadoresTime()
		{
			int timeIdx = 0;
			List<string> options = new() { "Proximo time", "Time anterior", "Proxima serie" };


            while (true)
			{
                Time t = Times[timeIdx].Time;

                do
                {
                    HelperPrinter.ImprimirJogadores(t);
					SelectInput.instance = Select.Instance.GetChoice(options, SelectInput.Instance);

                } while (SelectInput.Instance.Key != ConsoleKey.Enter);

                switch (SelectInput.Instance.ChoiceIndex)
				{
					case 0:
						if (timeIdx < Times.Count - 1) timeIdx++;
						break;
					case 1:
						if (timeIdx != 0) timeIdx--;
						break;
					case 2:
						return;
				}
			}
		}

        /// <summary>
        /// Mostra a escalação do time mandante e do time visitante e um menu de seleção.
        /// </summary>
        /// <param name="s">A <see cref="Serie">série</see> a qual o <see cref="Jogo"/> pertence.</param>
        /// <param name="j">O <see cref="Jogo"/> atual</param>
        /// <param name="choices">Lista de opções disponíveis.</param>
        /// <returns>Índice da opção escolhida</returns>
        public void ShowEscalacaoTimesJogo()
		{
			int jogoIdx = 0;
			List<string> options = new() { "Proximo jogo", "Jogo anterior", "Continuar" };

            while (true)
			{
                Jogo j = JogosRodada[jogoIdx];

                do
				{
					Console.WriteLine($"--------------------- {Name}");
					HelperPrinter.ImprimirJogadores(j.Mandante.Time.Nome, j.EscalacaoMandante);
					HelperPrinter.ImprimirJogadores(j.Visitante.Time.Nome, j.EscalacaoVisitante);

                    SelectInput.instance = Select.Instance.GetChoice(options, SelectInput.Instance);

				} while (SelectInput.Instance.Key != ConsoleKey.Enter) ;

                switch (SelectInput.Instance.ChoiceIndex)
				{
					case 0:
						if (jogoIdx < JogosRodada.Count - 1)
							jogoIdx++;
						break;
					case 1:
						if (jogoIdx != 0) 
							jogoIdx--;
						break;
					case 2:
						return;
				}
			}
		}
	}
}
