using Tabuleiro;

namespace JogoXadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez partida;

        public Rei(Tabula tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
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

        private bool testeTorreRoque (Posicao pos)
        {
            Peca p = tabu.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qteMovimentos == 0;  // use o is para checar se uma superclasse (p que é peça) é da subclasse correspondente (torre no caso)
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

            // #jogada especial - ROQUE

            if (qteMovimentos == 0 && !partida.xeque)
            {
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);  // Roque Pequeno
                if (testeTorreRoque(posT1))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tabu.peca(p1) == null && tabu.peca(p2) == null)
                    {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }
                }
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);  // Roque Grande
                if (testeTorreRoque(posT2))
                {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tabu.peca(p1) == null && tabu.peca(p2) == null && tabu.peca(p3) == null)
                    {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
