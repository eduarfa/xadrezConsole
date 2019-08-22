using Tabuleiro;

namespace JogoXadrez
{
    class Rei : Peca
    {
        public Rei(Tabula tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "R";
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

            pos.definirPosicao(posicao.linha - 1, posicao.coluna); // checando posicao norte do rei
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha - 1, posicao.coluna + 1); // checando posicao nordeste
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha, posicao.coluna + 1); // checando posicao direita do rei
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha + 1, posicao.coluna + 1); // checando posicao sudeste do rei
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha + 1, posicao.coluna); // checando posicao sul do rei
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha + 1, posicao.coluna - 1); // checando posicao sudoeste do rei
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha, posicao.coluna - 1); // checando posicao oeste do rei
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.definirPosicao(posicao.linha - 1, posicao.coluna - 1); // checando posicao noroeste do rei
            if (tabu.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            return mat;
        }
    }
}
