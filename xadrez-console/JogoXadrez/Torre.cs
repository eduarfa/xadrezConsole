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
    }
}