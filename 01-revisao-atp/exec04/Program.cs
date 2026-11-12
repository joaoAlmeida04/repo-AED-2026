using System;

namespace exec04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[4,4];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.WriteLine("Digite o elemento da linha " +i+ " e coluna "+j+":");
                    matrix [i,j] = int.Parse(Console.ReadLine());
                }
            }

            int linhasNulas = 0;
            int colunasNulas = 0;
            int contador =  0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                contador = 0;

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j] == 0)
                    {
                        contador++;
                    }
                }

                if(contador == matrix.GetLength(1))
                {
                    linhasNulas++;
                }
            }

            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                contador = 0;

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i,j] == 0)
                    {
                        contador++;
                    }
                }

                if(contador == matrix.GetLength(0))
                {
                    colunasNulas++;
                }
            }

            Console.WriteLine("Linhas nulas: "+linhasNulas);
            Console.WriteLine("Colunas nulas: "+ colunasNulas);
        }
    }
}