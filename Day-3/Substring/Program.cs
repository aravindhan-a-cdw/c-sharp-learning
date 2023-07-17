using System;
using System.Text;

class Substring
{
    static void Main(string[] args)
    {
        string inputString = args[0];
        int trimLength = Convert.ToInt32(args[1]);

        if (trimLength * 2 > inputString.Length)
        {
            Console.WriteLine("The string length must be greater than the number length!");
            Environment.Exit(1);
        }

        StringBuilder output = new StringBuilder();
        int index = 0;
        while (index < inputString.Length)
        {
            if (index == trimLength)
            {
                index = inputString.Length - trimLength;
            }
            output.Append(inputString[index]);
            ++index;
        }

        Console.WriteLine($"Output: {output}");
    }
}