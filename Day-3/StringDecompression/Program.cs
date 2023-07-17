
using System;
using System.Text;
class StringDecompression
{
    static string getRepeatedString(string source, int count)
    {
        // Console.WriteLine($"Source: {source}  Count: {count}");
        StringBuilder sb = new StringBuilder(count * source.Length);
        for (int i = 0; i < count; i++)
        {
            sb.Append(source);
        }

        return sb.ToString();
    }

    static void Main(string[] args)
    {
        string compressedString = args[0];
        StringBuilder decompressedString = new StringBuilder();

        int index = 0;
        StringBuilder repeatingString = new StringBuilder("");
        int repeatitionCount = 0;

        while (index < compressedString.Length)
        {
            char currentChar = compressedString[index];
            // Console.WriteLine($"CurrentChar: {currentChar}");
            if (Char.IsNumber(currentChar))
            {
                repeatitionCount = repeatitionCount * 10 + Convert.ToInt32(currentChar.ToString());
                // Console.WriteLine($"Repitition Count: {repeatitionCount}");
                index++;
                if (index == compressedString.Length)
                {
                    decompressedString.Append(getRepeatedString(repeatingString.ToString(), repeatitionCount));
                    continue;
                }
                char upcomingChar = compressedString[index];
                if (Char.IsLetter(upcomingChar))
                {
                    decompressedString.Append(getRepeatedString(repeatingString.ToString(), repeatitionCount));
                    repeatitionCount = 0;
                    repeatingString.Clear();
                }
                continue;
            }
            repeatingString.Append(currentChar);
            index++;
        }
        Console.WriteLine(decompressedString);
    }
}