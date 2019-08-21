using System;
using Tabuleiro;
using JogoXadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Tabula tab = new Tabula(8, 8);

                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 9));
                tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 2));

                Tela.imprimirTabula(tab);
            }
            catch (TabuleiroException e )
            {

                Console.WriteLine(e.Message);
            }
            

            Console.ReadLine();





        }

     
    }
}
