﻿using System;
using Tabuleiro;
using JogoXadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void imprimirTabula(Tabula tab) // statica faz com os métodos possam ser chamados a partir da classe, sem necessitar de instanciação de objeto
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 -i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.peca(i,j)==null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirPeca(tab.peca(i, j));
                        Console.Write(" ");
                    }
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca.cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor; // pega a cor cinza padrao de impressao do console e muda para amarelo, para imprimir peças pretas e depois volta para a cor cinza padrão
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
