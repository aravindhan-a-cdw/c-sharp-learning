/*

Print the following Pattern

Input: 5

Output:
    *
   ***
  *****
 *******
*********

*/


using System;

class Pattern
{
    static void Main(string[] args)
    {
        Console.WriteLine("Priting Pattern for input: " + args[0]);
        int inputNumber = Convert.ToInt32(args[0]);

        for (int line = 0; line < inputNumber; line++)
        {
            for (int space = inputNumber - line - 1; space > 0; space--)
                Console.Write(" ");

            for (int star = 2 * line + 1; star > 0; star--)
                Console.Write("*");

            Console.WriteLine("");
        }
    }
}