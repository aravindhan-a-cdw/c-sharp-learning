using System;
using System.Text;

class Substring
{
    static void Main(string[] args)
    {
        string inputString = args[0];
        int trimLength = Convert.ToInt32(args[1]);

        if (trimLength > inputString.Length)
        {
            Console.WriteLine("The string length must be greater than the number length!");
            Environment.Exit(1);
        }

        StringBuilder output = new StringBuilder();

        output.Append(inputString.Substring(0, trimLength));
        output.Append(inputString.Substring(inputString.Length - trimLength));

        Console.WriteLine($"Output: {output}");
    }
}