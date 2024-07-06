using tabuleiro;
using xadrez;

namespace ChessProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaXadrez partida = new PartidaXadrez();

                while (!partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirPartida(partida);

                        

                        Console.WriteLine();
                        Console.Write("Posição de Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        Console.WriteLine(origem);
                        partida.validarPosicaoOrigem(origem);

                        bool[,] posicoesPossiveis = partida.Tab.peca(origem).movimentosPossiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.Tab, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Posição de Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDestino(origem, destino);

                        partida.realizaJogada(origem, destino);
                    }
                    catch (TabuleiroException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }


                Tela.imprimirTabuleiro(partida.Tab);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}