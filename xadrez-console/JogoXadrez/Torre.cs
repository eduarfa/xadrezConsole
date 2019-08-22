using Tabuleiro;

namespace JogoXadrez
{
    class Torre : Peca
    {
        public Torre(Tabula tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tabu.peca(pos);
            return p == null || p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabu.linhas, tabu.colunas];

            Posicao pos = new Posicao(0, 0);

            pos.definirPosicao(posicao.linha - 1, posicao.coluna); // acima da torre
            while (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tabu.peca(pos) != null && tabu.peca(pos).cor != this.cor)
                {
                    break;
                }
                pos.linha--;
            }

            pos.definirPosicao(posicao.linha + 1, posicao.coluna); // abaixo da torre
            while (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tabu.peca(pos) != null && tabu.peca(pos).cor != this.cor)
                {
                    break;
                }
                pos.linha++;
            }

            pos.definirPosicao(posicao.linha, posicao.coluna + 1); // direita da torre
            while (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tabu.peca(pos) != null && tabu.peca(pos).cor != this.cor)
                {
                    break;
                }
                pos.coluna++;
            }

            pos.definirPosicao(posicao.linha, posicao.coluna - 1); // esquerda da torre
            while (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tabu.peca(pos) != null && tabu.peca(pos).cor != this.cor)
                {
                    break;
                }
                pos.coluna--;
            }

            return mat;
        }
    }
}