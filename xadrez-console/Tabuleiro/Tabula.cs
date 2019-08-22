

namespace Tabuleiro
{
    class Tabula
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas;

        public Tabula(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }

        public Peca peca (int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna];
        }

        public bool existePeca (Posicao pos)
        {
            validerPosicao(pos);
            return peca(pos) != null;  // se peca(pos) for diferente de null (se existe uma peca na posicao pos), retorna true
        }

        public void colocarPeca (Peca p, Posicao pos)
        {
            if (existePeca(pos))
            {
                throw new TabuleiroException("Ja existe uma peça nessa posição!");
            }
            pecas[pos.linha, pos.coluna] = p;
            p.posicao = pos;
        }

        public Peca retirarPeca(Posicao pos)
        {
            if (peca(pos)==null)
            {
                return null;
            }
            Peca aux = peca(pos);
            aux.posicao = null;
            pecas[pos.linha, pos.coluna] = null;
            return aux;
        }

        public bool posicaoValida (Posicao pos)
        {
            if(pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas)
            {
                return false;
            }
            return true;
        }

        public void validerPosicao (Posicao pos)
        {
            if (posicaoValida(pos) == false)
            {
                throw new TabuleiroException("Posição Inválida!");
            }
        }
    }
}
