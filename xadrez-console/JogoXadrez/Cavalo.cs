using Tabuleiro;

namespace JogoXadrez
{
    class Cavalo : Peca
    {

        public Cavalo(Tabula tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "C";
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

            pos.definirPosicao(posicao.linha - 2, posicao.coluna + 1); // 
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha - 2, posicao.coluna - 1); // 
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha + 2, posicao.coluna + 1); // 
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha + 2, posicao.coluna - 1); // 
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha + 1, posicao.coluna + 2); // 
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha + 1, posicao.coluna - 2); // 
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha - 1, posicao.coluna - 2); // 
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha - 1, posicao.coluna + 2); // 
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            return mat;
        }
    }
}