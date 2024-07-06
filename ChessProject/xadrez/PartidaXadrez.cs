﻿using tabuleiro;

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

        public PartidaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retiraPeca(origem);
            p.incrementarMovimentos();
            Peca pecaCapturada = Tab.retiraPeca(destino);
            Tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            Turno++;
            mudaJogador();
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
            if (!Tab.peca(origem).podeMoverPara(destino))
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


            colocarNovaPeca('a', 8, new Torre(Cor.Preta, Tab));
            colocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tab));
            colocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tab));
            colocarNovaPeca('d', 8, new Rainha(Cor.Preta, Tab));
            colocarNovaPeca('e', 8, new Rei(Cor.Preta, Tab));
            colocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tab));
            colocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tab));
            colocarNovaPeca('h', 8, new Torre(Cor.Preta, Tab));
        }
    }
}

