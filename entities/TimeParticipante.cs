namespace TXTSoccer.entities
{
    /// <summary>
    /// Controla a participação de um time em um campeonato
    /// </summary>
    internal class TimeParticipante
    {
        public Time Time { get; set; }
        public int vitorias = 0, derrotas = 0, empates = 0;
        public int golsFeitos = 0, golsTomados = 0;

        public TimeParticipante(Time time)
        {
            Time = time;
        }

        /// <summary>
        /// Recebe a pontuação do time.
        /// </summary>
        /// <returns>A pontuacao do <see cref="Time">time</see> no <see cref="Campeonato"/></returns>
        public int GetPontuacao()
        {
            int pontos = (vitorias * 2) + empates + (golsFeitos - golsTomados);
            return pontos;
        }

        public void ResetarPontuacao()
        {
            vitorias = 0;
            derrotas = 0;
            empates = 0;
            golsFeitos = 0;
            golsTomados = 0;
        }
    }
}
