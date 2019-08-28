using Tabuleiro;

namespace JogoXadrez
{
    class Rainha : Peca
    {
        public Rainha(Tabula tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "D";  // D de Dama
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

            pos.definirPosicao(posicao.linha - 1, posicao.coluna + 1); // diagonal direita acima
            while (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tabu.peca(pos) != null && tabu.peca(pos).cor != this.cor)
                {
                    break;
                }
                pos.linha--;
                pos.coluna++;
            }

            pos.definirPosicao(posicao.linha - 1, posicao.coluna - 1); // diagonal esquerda acima
            while (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tabu.peca(pos) != null && tabu.peca(pos).cor != this.cor)
                {
                    break;
                }
                pos.linha--;
                pos.coluna--;
            }

            pos.definirPosicao(posicao.linha + 1, posicao.coluna + 1); // diagonal direita abaixo
            while (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tabu.peca(pos) != null && tabu.peca(pos).cor != this.cor)
                {
                    break;
                }
                pos.coluna++;
                pos.linha++;
            }

            pos.definirPosicao(posicao.linha + 1, posicao.coluna - 1); // diagonal esquerda abaixo
            while (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tabu.peca(pos) != null && tabu.peca(pos).cor != this.cor)
                {
                    break;
                }
                pos.coluna--;
                pos.linha++;
            }

            return mat;
        }
    }
}