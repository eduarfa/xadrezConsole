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
    }
}
