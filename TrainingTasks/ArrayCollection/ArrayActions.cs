using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayCollection
{
    public class ArrayActions
    {
        public void PrintmultiDimentionArray()
        {
            Console.WriteLine("Print an Array\n");
            int[,] input= new int[2, 3] { { 1, 2, 3 }, { 4,5,6}};
            int i, j;
            for(i = 0; i < 2; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    Console.WriteLine("a[{0},{1}] = {2}", i, j, input[i, j]);
                }
            }
        }

        public void InputPrintmultiDimentionArray()
        {
            int[,] inputArray= new int[2, 3];
            int row, col;
            Console.WriteLine("Enter the element of Array");
            for (row = 0; row < 2; row++)
            {
                for (col = 0; col < 3; col++)
                {
                    Console.Write($"Element [{row},{col}]:\t");
                    inputArray[row,col]=Convert.ToInt32( Console.ReadLine() );
                }
            }
            Console.WriteLine("The Entered Array looks as follow");
            for (row = 0; row < 2; row++)
            {
                for(col=0; col < 3; col++)
                {
                    Console.Write(inputArray[row,col]+"\t");
                }
                Console.WriteLine();
            }

            int rowLength=inputArray.GetLength(0);
            int colLength=inputArray.GetLength(1);
            int totalLength=inputArray.Length;
            Console.WriteLine($"The Row Length=\t{rowLength}\nThe Column Length=\t{colLength}\nTotal Lenth:\t{totalLength}");
        }
        public void ArithmaticActionOnArrays()
        {
            int[,] inputArray1 = new int[3, 3];
            int[,] inputArray2 = new int[3, 3];
            int[,] solutionArray = new int[3, 3];
            int row, col;

            Console.WriteLine("************ Enter First Array ***************");

            for(row=0; row < 3; row++)
            {
                for( col=0; col < 3; col++)
                {
                    Console.Write($"Array1 [{row},{col}]:\t");
                    inputArray1[row, col] = Convert.ToInt32(Console.ReadLine());
                }
            }
            Console.WriteLine("The First Array looks as follow");
            for (row = 0; row < 3; row++)
            {
                for (col = 0; col < 3; col++)
                {
                    Console.Write(inputArray1[row, col] + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n************ Enter Second Array ***************");

            for (row = 0; row < 3; row++)
            {
                for (col = 0; col < 3; col++)
                {
                    Console.Write($"Array2 [{row},{col}]:\t");
                    inputArray2[row, col] = Convert.ToInt32(Console.ReadLine());
                }
            }
            Console.WriteLine("The Second Array looks as follow");
            for (row = 0; row < 3; row++)
            {
                for (col = 0; col < 3; col++)
                {
                    Console.Write(inputArray2[row, col] + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n************ Addition ***************");
            for(row=0; row < 3; row++)
            {
                for(col=0; col < 3; col++)
                {
                    solutionArray[row,col]= inputArray1[row, col] + inputArray2[row, col];
                    Console.Write(solutionArray[row,col] + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n************ Substraction ***************");
            for (row = 0; row < 3; row++)
            {
                for (col = 0; col < 3; col++)
                {
                    solutionArray[row, col] = inputArray1[row, col] - inputArray2[row, col];
                    Console.Write(solutionArray[row, col] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n************ Multiplication ***************");

            for(row= 0; row < 3; row++)
            {
                for(col= 0; col < 3; col++)
                {
                    solutionArray[row, col] = 0;
                    for(int k=0; k< 3; k++)
                    {
                        solutionArray[row, col] += inputArray1[row, k] * inputArray2[k, col];
                    }
                }
            }

            for (row = 0; row < 3; row++)
            {
                for (col = 0; col < 3; col++)
                {
                    Console.Write(solutionArray[row, col] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nPress Any Key...");
            Console.ReadKey();
        }
    }
}
