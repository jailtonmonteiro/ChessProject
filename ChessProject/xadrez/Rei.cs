using System;
using tabuleiro;

namespace xadrez
{
    internal class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro tab) : base(cor, tab)
        {

        }
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }
        public override bool[,] movimentoPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //Norte
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Nordeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if(tab.posicaoValida(pos) || podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Leste
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) || podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Sudeste
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) || podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Sul
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) || podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Sudoeste
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) || podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Oeste
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(pos) || podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //Noroeste
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) || podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
