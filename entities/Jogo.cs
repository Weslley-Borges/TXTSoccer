namespace TXTSoccer.entities
{

    /// <summary>
    /// Uma classe que realiza os métodos e guarda informações obra partida de uma <see cref="Serie"/>
    /// </summary>
    internal class Jogo
    {
        public TimeParticipante Mandante { get; }
        public TimeParticipante Visitante { get; }
        public int PlacarMandante { get; set; }
        public int PlacarVisitante { get; set; }

        public List<Jogador> EscalacaoMandante { get; }
        public List<Jogador> EscalacaoVisitante { get; }

        public Jogo(TimeParticipante mandante, TimeParticipante visitante)
        {
            Mandante = mandante;
            Visitante = visitante;
            PlacarMandante = 0;
            PlacarVisitante = 0;

            EscalacaoMandante = mandante.Time.EscalarJogadores();
            EscalacaoVisitante = visitante.Time.EscalarJogadores();
        }

        public void IniciarJogo()
        {
            GerarLesoes();
            GerarCartoes();
            GerarResultado();
            PermitirTreinamento();
        }

        private void GerarLesoes()
        {
            List<Jogador> jogadores = new();
            jogadores.AddRange(EscalacaoMandante.FindAll(j => EscalacaoMandante.IndexOf(j) < 11));
            jogadores.AddRange(EscalacaoVisitante.FindAll(j => EscalacaoVisitante.IndexOf(j) < 11));

            Random rnd = new();
            int nLesoes = rnd.Next(2);

            for (int i = 0; i < nLesoes; i++)
                jogadores[rnd.Next(jogadores.Count() - 1)].SofrerLesao();

        }

        private void GerarCartoes()
        {
            List<Jogador> jogadores = new();
            List<Jogador> jogadoresReceberamCartao = new();

            jogadores.AddRange(EscalacaoMandante.FindAll(j => EscalacaoMandante.IndexOf(j) < 11));
            jogadores.AddRange(EscalacaoVisitante.FindAll(j => EscalacaoVisitante.IndexOf(j) < 11));

            Random rnd = new Random();
            for (int i = 0; i < rnd.Next(0, 10); i++)
            {
                Jogador j = jogadores[rnd.Next(jogadores.Count - 1)];
                j.AplicarCartao(1);

                if (jogadoresReceberamCartao.Contains(j))
                    j.Suspenso = true;
                else
                    jogadoresReceberamCartao.Add(j);
            }
        }

        private void PermitirTreinamento()
        {
            Mandante.Time.Plantel.ForEach(j => j.JaTreinou = false);
            Visitante.Time.Plantel.ForEach(j => j.JaTreinou = false);
        }

        /*
         * Usando a probabilidade, são contadas as quantidades de gols de cada time para decidir o vencedor.
        */
        private void GerarResultado()
        {
            List<Jogador> TimeMandanteSemExpulsoes = new(EscalacaoMandante.FindAll(j => !j.Suspenso));
            List<Jogador> TimeVisitanteSemExpulsoes = new(EscalacaoVisitante.FindAll(j => !j.Suspenso));

            int nTimeMandante = SomarQualidades(TimeMandanteSemExpulsoes);
            int nTimeVisitante = SomarQualidades(TimeVisitanteSemExpulsoes);

            int diferencaTimes = Math.Abs(nTimeMandante - nTimeVisitante);
            int somaTimes = nTimeMandante + nTimeVisitante;

            int nEmpate = somaTimes - diferencaTimes;


            Random rnd = new();

            for (int i = 0; i < 5; i++)
            {
                int resultado = rnd.Next(1, nEmpate + nTimeMandante + nTimeVisitante);

                if (resultado <= nTimeMandante)
                    PlacarMandante++;
                else if (resultado >= nTimeMandante + 1 && resultado <= nTimeMandante + nTimeVisitante)
                    PlacarVisitante++;
            }

            int SomarQualidades(List<Jogador> time)
            {
                int somatorioQualidades = 0;
                time.ForEach(j => somatorioQualidades += j.Qualidade);

                return somatorioQualidades;
            }

            ContabilizarJogo();
        }


        private void ContabilizarJogo()
        {
            if (PlacarMandante > PlacarVisitante)
            {
                Mandante.vitorias++;
                Visitante.derrotas++;
            }
            else if (PlacarMandante < PlacarVisitante)
            {
                Mandante.derrotas++;
                Visitante.vitorias++;
            }
            else
            {
                Mandante.empates++;
                Visitante.empates++;
            }

            Mandante.golsFeitos += PlacarMandante;
            Mandante.golsTomados += PlacarVisitante;

            Visitante.golsFeitos += PlacarVisitante;
            Visitante.golsTomados += PlacarMandante;
        }
    }
}
