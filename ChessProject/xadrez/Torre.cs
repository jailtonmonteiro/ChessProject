using System;
using tabuleiro;

namespace xadrez
{
    internal class Torre : Peca
    {
        public Torre(Cor cor, Tabuleiro tab) : base(cor, tab)
        {
        }

        public override string ToString()
        {
            return "T";
        }
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.Linhas, tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            //Norte
            pos.definirValores(posicao.Linha - 1, posicao.Coluna);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(pos.Linha -1, pos.Coluna);
            }
            //Leste
            pos.definirValores(posicao.Linha, posicao.Coluna + 1);
            while(tab.peca(pos) != null && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(pos.Linha, pos.Coluna + 1);
            }
            //Sul
            pos.definirValores(posicao.Linha + 1, posicao.Coluna);
            while(tab.peca(pos) != null && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if(tab.peca(pos) != null && podeMover(pos))
                {
                    break;
                }
                pos.definirValores(pos.Linha + 1, pos.Coluna);
            }
            //Oeste
            pos.definirValores(posicao.Linha, posicao.Coluna - 1);
            while (tab.peca(pos) != null && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                    break;
                }
                pos.definirValores(pos.Linha, pos.Coluna - 1);
            }
            return mat;
        }
    }
}
