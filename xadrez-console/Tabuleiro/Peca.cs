

namespace Tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabula tabu { get; protected set; }

        public Peca(Tabula tabu, Cor cor)
        {
            this.posicao = null;
            this.tabu = tabu;
            this.cor = cor;
            this.qteMovimentos = 0;
        }

        public void incrementarQteMovimentos()
        {
            qteMovimentos++;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for (int i = 0; i < tabu.linhas; i++)
            {
                for (int j = 0; j < tabu.colunas; j++)
                {
                    if (mat[i,j])  // isso é igual a mat[i,j] == true
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool podeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }

        public abstract bool[,] movimentosPossiveis();
   
    }
}
