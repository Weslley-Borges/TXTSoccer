using TXTSoccer.helpers;


namespace TXTSoccer.entities
{
	/// <summary>
	/// Uma classe para controlar os eventos e características de um campeonato de futebol.
	/// </summary>
	internal class Campeonato
	{
		public string Name { get; set; }
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
		/// </summary>
		public void IniciarRodada()
		{
			Series.ForEach(s => s.IniciarRodada());

			int input = Select.ShowJogosSeries(this, new List<string> { "Ir para a tabela", "Ver jogos" });

			if (input == 1)
				Series.ForEach(s => s.ShowJogosRodada());

            HelperPrinter.ImprimirTabelaTimesCampeonato(this);

            RodadaAtual++;
		}
	}
}
