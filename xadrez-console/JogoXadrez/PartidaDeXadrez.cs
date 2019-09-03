using System;
using System.Collections.Generic;
using Tabuleiro;


namespace JogoXadrez
{
    class PartidaDeXadrez
    {
        public Tabula tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vulneralEnPassant { get; private set; }

        public PartidaDeXadrez() // construindo a partida de xadrez (construtor)
        {
            tab = new Tabula(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            vulneralEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino) // método com a lógica de movimento das peças 
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // #jogadaespecial Roque
            if (p is Rei && destino.coluna == origem.coluna + 2)  // roque pequeno
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(origemTorre);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoTorre);
            }
            if (p is Rei && destino.coluna == origem.coluna - 2)  // roque grande
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);  
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(origemTorre);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoTorre);
            }

            // #jogadaespecial En Passant
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) // desfaz o movimento feito para em caso de xeque voltar a jogada e não permitir que o jogador se coloque em xeque
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
            if (p is Rei && destino.coluna == origem.coluna + 2)  // roque pequeno
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(destinoTorre);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemTorre);
            }
            if (p is Rei && destino.coluna == origem.coluna - 2)  // roque grande
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(destinoTorre);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemTorre);
            }
            // #jogadaespecial En passant
            if (p is Peao)
            {
                if(origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                    tab.colocarPeca(peao, posP);
                }
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino) // método que executa a jogada de movimento de peça
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmCheque(jogadorAtual)) // testando se a jogada feita não colocou o rei do jogador em xeque, o que inviabiliza a jogada
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque");
            }
            Peca p = tab.peca(destino);
            // #jogadaespecial Promocao do Peao
            if (p is Peao)
            {
                if ((p.cor == Cor.Branca && destino.linha==0) || (p.cor == Cor.Preta && destino.linha == 7))
                {
                    p = tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca Promovida = new Rainha(tab, p.cor);
                    tab.colocarPeca(Promovida, destino);
                    pecas.Add(Promovida);
                }
            }

            if (estaEmCheque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequeMate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }
            
            // #jogadaespecial En Passant
            if (p is Peao && (destino.linha == origem.linha + 2 || destino.linha == origem.linha - 2))
            {
                vulneralEnPassant = p;
            }
            else
            {
                vulneralEnPassant = null;
            }
            
        }

        public void validarPosicaoDeOrigem(Posicao pos) // verifica se a posicao de origem contém uma peça que pode ser movimentada
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça para mover na posição indicada");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Essa peça não possui movimentos possíveis");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) // verifica se a posicao de destino num movimento é uma posicao valida
        {
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino Inválida");
            }
        }

        public HashSet<Peca> pecasCapturadas (Cor cor) // retorna as peças caturadas de determinada cor
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca item in capturadas)
            {
                if (item.cor == cor)
                {
                    aux.Add(item);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca item in pecas)
            {
                if (item.cor == cor)
                {
                    aux.Add(item);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor)); // retorna as pecas em jogos, excluindo as que foram capturadas (que estao no conjunto de capturadas)
            return aux;
        }

        private void mudaJogador() // muda o jogador da vez de jogar
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
            
        }

        private Cor adversaria (Cor cor) // retorna a cor adversária
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor) // retorna a peça rei de determinada cor (assim como sua posicao)
        {
            foreach (Peca item in pecasEmJogo(cor))
            {
                if (item is Rei)  // se o item da superclasse Peca é da subclasse filha Rei
                {
                    return item;
                }
            }
            return null;
        }

        public bool estaEmCheque(Cor cor) // método que testa se um rei está em cheque
        {
            Peca R = rei(cor);
            foreach (Peca item in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = item.movimentosPossiveis(); // construida uma matriz de movimentos possiveis de todas as peças adversarias do rei ainda em jogo
                if (mat[R.posicao.linha, R.posicao.coluna])  // se alguma das posicoes da matriz de movimentos possiveis for a posicao do rei, significa que ele está em cheque
                {
                    return true;
                }
            }
            return false;
        }


        public bool testeXequeMate(Cor cor)
        {
            if (!estaEmCheque(cor))
            {
                return false;
            }
            foreach (Peca item in pecasEmJogo(cor))
            {
                bool[,] mat = item.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = item.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmCheque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }


        public void colocarNovaPeca(char coluna, int linha, Peca peca) // método apra colocar peças no tabuleiro e adicionar elas ao conjunto de peças
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }


        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rainha(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rainha(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
        }
    }


}
