
using System;

class ConsoleDisplay
{

    public static void WriteLine(string line)
    {
        for (int index = 0; index < line.Length; ++index)
        {
            Console.Write(line[index]);
            System.Threading.Thread.Sleep(10);
        }
        Console.WriteLine();
    }

    public static void WriteColorLine(string line, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(line);
        Console.ResetColor();
    }

    public static void WriteColor(string line, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(line);
        Console.ResetColor();
    }
}