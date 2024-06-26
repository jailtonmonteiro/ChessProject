using tabuleiro;
using xadrez;
using System;

namespace ChessProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
                tab.colocarPeca(new Torre(Cor.Preta, tab), new Posicao(1, 3));
                tab.colocarPeca(new Rei(Cor.Preta, tab), new Posicao(0, 6));
                tab.colocarPeca(new Rainha(Cor.Branca, tab), new Posicao(4, 7));
                tab.colocarPeca(new Peao(Cor.Branca, tab), new Posicao(5, 5));

                Tela.imprimirTabuleiro(tab);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}