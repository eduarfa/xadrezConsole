using Tabuleiro;

namespace JogoXadrez
{
    class Peao : Peca
    {
        private PartidaDeXadrez partida;

        public Peao(Tabula tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = tabu.peca(pos);
            return p != null && p.cor != this.cor;
        }

        private bool posicaoLivre(Posicao pos)
        {
            return tabu.peca(pos) == null;
            
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tabu.linhas, tabu.colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branca)
            {
                pos.definirPosicao(posicao.linha - 1, posicao.coluna); // 
                if (tabu.posicaoValida(pos) && posicaoLivre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirPosicao(posicao.linha - 2, posicao.coluna); // 
                if (tabu.posicaoValida(pos) && posicaoLivre(pos) && qteMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirPosicao(posicao.linha - 1, posicao.coluna - 1); // 
                if (tabu.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirPosicao(posicao.linha - 1, posicao.coluna + 1); // 
                if (tabu.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                // #jogadaespecial En Passant 
                if (posicao.linha == 3)
                {
                    Posicao esq = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tabu.posicaoValida(esq) && existeInimigo(esq) && tabu.peca(esq) == partida.vulneralEnPassant)
                    {
                        mat[esq.linha - 1, esq.coluna] = true;
                    }
                    Posicao dir = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tabu.posicaoValida(dir) && existeInimigo(dir) && tabu.peca(dir) == partida.vulneralEnPassant)
                    {
                        mat[dir.linha - 1, dir.coluna] = true;
                    }
                }
            }
            else
            {
                pos.definirPosicao(posicao.linha + 1, posicao.coluna); // 
                if (tabu.posicaoValida(pos) && posicaoLivre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirPosicao(posicao.linha + 2, posicao.coluna); // 
                if (tabu.posicaoValida(pos) && posicaoLivre(pos) && qteMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirPosicao(posicao.linha + 1, posicao.coluna - 1); // 
                if (tabu.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirPosicao(posicao.linha + 1, posicao.coluna + 1); // 
                if (tabu.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                // #jogadaespecial En Passant
                if (posicao.linha == 4)
                {
                    Posicao esq = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tabu.posicaoValida(esq) && existeInimigo(esq) && tabu.peca(esq) == partida.vulneralEnPassant)
                    {
                        mat[esq.linha + 1, esq.coluna] = true;
                    }
                    Posicao dir = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tabu.posicaoValida(dir) && existeInimigo(dir) && tabu.peca(dir) == partida.vulneralEnPassant)
                    {
                        mat[dir.linha + 1, dir.coluna] = true;
                    }
                }
            }
            return mat;
        }
    }
}