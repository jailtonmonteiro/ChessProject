using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor {  get; protected set; }
        public int qtdMovimentos { get;  protected set; }
        public Tabuleiro tab {  get; protected set; }

        public Peca(Cor cor, Tabuleiro tab)
        {
            this.posicao = null;
            this.cor = cor;
            this.tab = tab;
            this.qtdMovimentos = 0;
        }

        public void incrementarMovimentos()
        {
            qtdMovimentos++;
        }

        public abstract bool[,] movimentoPossiveis(){

        }
    }
}
