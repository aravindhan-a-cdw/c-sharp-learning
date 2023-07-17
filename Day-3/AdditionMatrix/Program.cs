using System;

class AdditionMatix
{

    static void getAdditionCombo(int number, out int number1, out int number2, ref int number3)
    {
        int[] output = new int[3];
        Random rand = new Random();
        number1 = rand.Next(1, number - 2);
        number2 = rand.Next(1, number - number1);
        number3 = number - number1 - number2;
    }
    static void Main()
    {
        Console.Write("Enter a number: ");
        int input = Convert.ToInt32(Console.ReadLine());
        int number1, number2, number3 = 0;
        getAdditionCombo(input, out number1, out number2, ref number3);
        int[][] matrix = new int[3][];
        matrix[0] = new int[] { number1, number2, number3 };
        matrix[1] = new int[] { number2, number3, number1 };
        matrix[2] = new int[] { number3, number1, number2 };

        for (int row = 0; row < matrix.Length; ++row)
        {
            for (int column = 0; column < matrix[row].Length; ++column)
            {
                Console.Write(matrix[row][column] + " ");
            }
            Console.WriteLine();
        }
    }
}