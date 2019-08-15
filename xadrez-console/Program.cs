using System;
using Tabuleiro;
using JogoXadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabula tab = new Tabula(8, 8);

            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
            tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));

            Tela.imprimirTabula(tab);

            Console.ReadLine();





        }

     
    }
}
