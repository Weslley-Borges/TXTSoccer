using TXTSoccer.helpers;

namespace TXTSoccer.entities
{
    internal class Jogador
    {
        public int Id { get; }
        public string Nome { get; }
        public string Apelido { get; }
        public int Numero { get; set; }
        public int Cartoes { get; set; }
        public bool Suspenso { get; set; } = false;
        public int Qualidade { get; set; }
        public bool JaTreinou { get; set; }
        public int JogosSuspenso { get; set; } = 0;
        public Posicao Posicao { get; }
        public DateOnly DataNascimento { get; }


        public Jogador(
            int id, string nome,
            string apelido, Posicao posicao, int numero,
            int qualidade, DateOnly dataNascimento
            )
        {
            Id = id;
            Nome = nome;
            Numero = numero;
            Apelido = apelido;
            Posicao = posicao;
            Qualidade = qualidade;
            DataNascimento = dataNascimento;
        }

        public bool VerificarCondicaoDeJogo()
        {
            Suspenso = Cartoes >= 3;
            return Suspenso;
        }

        public void AplicarCartao(int quantidade)
        {
            Cartoes += quantidade;

            if (VerificarCondicaoDeJogo())
                JogosSuspenso = 2;
        }

        public void CumprirSuspensao()
        {
            if (!Suspenso)
                return;

            if (JogosSuspenso == 0)
            {
                Cartoes = 0;
                Suspenso = false;
            }
            else JogosSuspenso --;
        }

        public void SofrerLesao()
        {
            Random rnd = new Random();

            int gravidaDaLesao = rnd.Next(1, 100);

            if (gravidaDaLesao <= 5) Qualidade -= (Qualidade * 15) / 100;
            else if (gravidaDaLesao > 5 && gravidaDaLesao <= 15) Qualidade -= (Qualidade * 10) / 100;
            else if (gravidaDaLesao > 15 && gravidaDaLesao <= 30) Qualidade -= (Qualidade * 5) / 100;
            else if (gravidaDaLesao > 30 && gravidaDaLesao <= 60) Qualidade -= 2;
            else Qualidade -= 1;

            if (Qualidade < 0) Qualidade = 0;
        }

        public void ExecutarTreino()
        {
            if (JaTreinou)
                return;

            Random rnd = new();

            int eficacioDoTreino = rnd.Next(1, 100);

            if (eficacioDoTreino <= 5) Qualidade += 5;
            else if (eficacioDoTreino > 5 && eficacioDoTreino <= 15) Qualidade += 4;
            else if (eficacioDoTreino > 15 && eficacioDoTreino <= 30) Qualidade += 3;
            else if (eficacioDoTreino > 30 && eficacioDoTreino <= 60) Qualidade += 2;
            else if (eficacioDoTreino > 60 && eficacioDoTreino <= 100) Qualidade += 1;

            if (Qualidade > 100) Qualidade = 100;
            JaTreinou = true;
        }
    }
}
