
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
            if (currentChar > 47 && currentChar < 58)
            {
                repeatitionCount = repeatitionCount * 10 + Convert.ToInt32(currentChar.ToString());
                index++;
                if (index == compressedString.Length)
                {
                    decompressedString.Append(getRepeatedString(repeatingString.ToString(), repeatitionCount));
                }
                else
                {
                    char upcomingChar = compressedString[index];
                    if (Char.IsLetter(upcomingChar))
                    {
                        decompressedString.Append(getRepeatedString(repeatingString.ToString(), repeatitionCount));
                        repeatitionCount = 0;
                        repeatingString.Clear();
                    }

                }
            }
            else
            {
                repeatingString.Append(currentChar);
                index++;
            }
        }
        Console.WriteLine(decompressedString);
    }
}