namespace TXTSoccer.entities
{
    internal class Time
    {
        private readonly string nome;
        private List<Jogador> plantel = new();

        public Time(string nome)
        {
            this.nome = nome;
        }

        /// <summary>
        /// Faz a escalação do time. Usa o <see cref="plantel"/> do time devolve uma lista com 11 titulares e 7 reservas.
        /// </summary>
        /// <returns>Lista de <see cref="Jogador"> titulares e reservas.</returns>
        /// <remarks></remarks>
        public List<Jogador> EscalarJogadores()
        {
            Dictionary<string, int> posicao = new()
            {
                {"Atacante", 3 }, {"Meio-campo", 2}, { "Zagueiro", 3}, {"Goleiro", 1}, {"lateral", 2}
            };

            List<Jogador> disponivel = new(Plantel);
            List<Jogador> selecionados = new();

            // Seleciona os titulares de acordo com a quantidade de jogadores em cada posição
            // Compara os jogadores com posições iguais (ordem decrescente).
            posicao.Keys.ToList().ForEach(p =>
            {
                List<Jogador> jogadoresDaPosicao = new(disponivel.FindAll(j => (j.Posicao.Nome == p)));
                jogadoresDaPosicao.Sort((x, y) => y.Qualidade.CompareTo(x.Qualidade));

                for (int i = 0; i <= posicao.GetValueOrDefault(p) && i < jogadoresDaPosicao.Count; i++)
                {
                    selecionados.Add(jogadoresDaPosicao[i]);
                    disponivel.Remove(jogadoresDaPosicao[i]);
                }
            });

            // Caso tenha menos jogadores que o necessário para as posições, os lugares são preenchidos
            // com os melhores jogadores, não importando a posição em que joguem
            disponivel.Sort((x, y) => y.Qualidade.CompareTo(x.Qualidade));

            int jogadoresFaltantes = 11 - selecionados.Count;

            if (jogadoresFaltantes > 0)
            {
                selecionados.AddRange(disponivel.GetRange(0, jogadoresFaltantes - 1));
                disponivel.RemoveRange(0, jogadoresFaltantes - 1);
            }

            // Preencher as vagas dos reservas
            selecionados.AddRange(disponivel.Count >= 7 ? disponivel.GetRange(0, 6) : disponivel);

            return selecionados;
        }

        // Getters e Setters
        public string Nome { get => nome; }
        internal List<Jogador> Plantel { get => plantel; set => plantel = value; }
    }
}
