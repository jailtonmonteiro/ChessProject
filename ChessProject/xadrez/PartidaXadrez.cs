using System.Runtime.ExceptionServices;
using tabuleiro;

namespace xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque {  get; private set; }

        public PartidaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retiraPeca(origem);
            p.incrementarMovimentos();
            Peca pecaCapturada = Tab.retiraPeca(destino);
            Tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (validarXeque(JogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Voce não pode se colocar em xeque!");
            }

            if (validarXeque(adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (validarXequeMate(adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                mudaJogador();
            }
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.retiraPeca(destino);
            p.decrementarMovimentos();
            if(pecaCapturada != null)
            {
                Tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            Tab.colocarPeca(p, origem);
        }

        public void validarPosicaoOrigem(Posicao pos)
        {
            if (Tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem não é da sua!");
            }
            if (!Tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça escolhida.");
            }
        }

        public void validarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posicao de destino inválida!");
            }
        }

        private void mudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca>pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private Cor adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca p in pecasEmJogo(cor))
            {
                if(p is Rei)
                {
                    return p;
                }
            }
            return null;
        }

        public bool validarXeque(Cor cor)
        {
            Peca R = rei(cor);
            if(R == null)
            {
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");
            }

            foreach (Peca p in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = p.movimentosPossiveis();
                if (mat[R.posicao.Linha, R.posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool validarXequeMate(Cor cor)
        {
            if (!validarXeque(cor))
            {
                return false;
            }
            foreach (Peca p in pecasEmJogo(cor))
            {
                bool[,] mat = p.movimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = p.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = validarXeque(cor);
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

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tab));
            colocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tab));
            colocarNovaPeca('d', 1, new Rei(Cor.Branca, Tab));
            colocarNovaPeca('e', 1, new Rainha(Cor.Branca, Tab));
            colocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tab));
            colocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tab));
            colocarNovaPeca('h', 1, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('a', 2, new Peao(Cor.Branca, Tab));
            colocarNovaPeca('b', 2, new Peao(Cor.Branca, Tab));
            colocarNovaPeca('c', 2, new Peao(Cor.Branca, Tab));
            colocarNovaPeca('d', 2, new Peao(Cor.Branca, Tab));
            colocarNovaPeca('e', 2, new Peao(Cor.Branca, Tab));
            colocarNovaPeca('f', 2, new Peao(Cor.Branca, Tab));
            colocarNovaPeca('g', 2, new Peao(Cor.Branca, Tab));
            colocarNovaPeca('h', 2, new Peao(Cor.Branca, Tab));

            colocarNovaPeca('a', 8, new Torre(Cor.Preta, Tab));
            colocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tab));
            colocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tab));
            colocarNovaPeca('d', 8, new Rainha(Cor.Preta, Tab));
            colocarNovaPeca('e', 8, new Rei(Cor.Preta, Tab));
            colocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tab));
            colocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tab));
            colocarNovaPeca('h', 8, new Torre(Cor.Preta, Tab));
            colocarNovaPeca('a', 7, new Peao(Cor.Preta, Tab));
            colocarNovaPeca('b', 7, new Peao(Cor.Preta, Tab));
            colocarNovaPeca('c', 7, new Peao(Cor.Preta, Tab));
            colocarNovaPeca('d', 7, new Peao(Cor.Preta, Tab));
            colocarNovaPeca('e', 7, new Peao(Cor.Preta, Tab));
            colocarNovaPeca('f', 7, new Peao(Cor.Preta, Tab));
            colocarNovaPeca('g', 7, new Peao(Cor.Preta, Tab));
            colocarNovaPeca('h', 7, new Peao(Cor.Preta, Tab));
        }
    }
}

