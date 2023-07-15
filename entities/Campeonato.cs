using TXTSoccer.helpers;


namespace TXTSoccer.entities
{
    /// <summary>
    /// Uma classe para controlar os eventos e características de um campeonato de futebol.
    /// </summary>
    internal class Campeonato
    {
        public string Name { get; set; }
        public double Prize { get; set; }
        public int Rodadas { get; set; }
        public int RodadaAtual { get; set; } = 1;
        public List<Jogo> JogosRodada { get; set; } = new();
        public List<TimeParticipante> Times { get; set; } = new();

        public Campeonato(string name, double prize, List<Time> times, int rodadas)
        {
            Name = name;
            Prize = prize;
            Rodadas = rodadas;

            times.ForEach(t => this.Times.Add(new TimeParticipante(t)));
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
                    JogosRodada.Add(new Jogo(new DateOnly(2023, 12, 3), t.Time, Times[Times.IndexOf(t) - 1].Time));
            });
        }

        /// <summary>
        /// Inicia dos <see cref="Jogo">jogos</see> da rodada atual.<br/>
        /// </summary>
        public void IniciarRodada()
        {
            JogosRodada.ForEach(j =>
            {
                j.IniciarJogo();
                ContabilizarResuiltadoJogo(j);

                HelperPrinter.WaitKey("COMEÇAR O PRÓXIMO JOGO");
            });

            HelperPrinter.ImprimirTabelaTimesCampeonato(this);
            RodadaAtual++;
        }

        /// <summary>
        /// Contabiliza as pontuações pós-jogo.<br/>
        /// </summary>
        /// <param name="r"><see cref="ResultadoJogo"/> a ser contabilizado</param>
        private void ContabilizarResuiltadoJogo(Jogo j)
        {
            TimeParticipante? mandante = Times.Find(t => t.Time == j.Mandante);
            TimeParticipante? visitante = Times.Find(t => t.Time == j.Visitante);

            if (mandante is null || visitante is null) return;

            if (j.PlacarMandante > j.PlacarVisitante)
            {
                mandante.vitorias++;
                visitante.derrotas++;
            } else if (j.PlacarMandante < j.PlacarVisitante)
            {
                mandante.derrotas++;
                visitante.vitorias++;
            } else
            {
                mandante.empates++;
                visitante.empates++;
            }
        }
    }

    /// <summary>
    /// Controla a participação de um time em um campeonato
    /// </summary>
    class TimeParticipante
    {
        public Time Time { get; set; }
        public int vitorias = 0, derrotas = 0, empates = 0;

        public TimeParticipante(Time time)
        {
            Time = time;
        }

        public int GetPontuacao() 
        {
            int pontos = (vitorias * 2) + empates;
            return pontos;
        }
    }
}
