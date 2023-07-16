using TXTSoccer.helpers;

namespace TXTSoccer.entities
{
    internal class Jogo
    {
        public Time Mandante { get; }
        public Time Visitante { get; }
        public int PlacarMandante { get; set; }
        public int PlacarVisitante { get; set; }

        public List<Jogador> EscalacaoMandante { get; }
        public List<Jogador> EscalacaoVisitante { get; }

        public Jogo(Time mandante, Time visitante)
        {
            Mandante = mandante;
            Visitante = visitante;
            PlacarMandante = 0;
            PlacarVisitante = 0;

            EscalacaoMandante = mandante.EscalarJogadores();
            EscalacaoVisitante = visitante.EscalarJogadores();
        }

        public void IniciarJogo()
        {
            GerarLesoes();
            GerarCartoes();
            GerarResultado();
            permitirTreinamento();
        }

        private void GerarLesoes()
        {
            List<Jogador> jogadores = new();
            EscalacaoMandante.ForEach(j => jogadores.Add(j));
            EscalacaoVisitante.ForEach(j => jogadores.Add(j));

            Random rnd = new();
            int nLesoes = rnd.Next(2);

            for (int i = 0; i < nLesoes; i++)
                jogadores[rnd.Next(jogadores.Count() - 1)].SofrerLesao();

        }

        private void GerarCartoes()
        {
            List<Jogador> jogadores = new();
            List<Jogador> jogadoresReceberamCartao = new();

            EscalacaoMandante.ForEach(j => jogadores.Add(j));
            EscalacaoVisitante.ForEach(j => jogadores.Add(j));
            
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

        private void permitirTreinamento()
        {
            Mandante.Plantel.ForEach(j => j.JaTreinou = false);
            Visitante.Plantel.ForEach(j => j.JaTreinou = false);
        }

        /*
         * Usando a probabilidade, são contadas as quantidades de gols de cada time para decidir o vencedor.
        */
        private void GerarResultado()
        {
            int nTimeMandante = SomarQualidades(EscalacaoMandante);
            int nTimeVisitante = SomarQualidades(EscalacaoVisitante);

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
        }
    }
}
