
using System;
using System.Text;
class CharEncryption
{

    static int getFirstDigit(int number)
    {
        while (number / 10 != 0)
        {
            number = (number / 10);
        }
        return number;
    }

    static string Encrypt(char letter)
    {
        int ascii = letter;
        Console.WriteLine(ascii);
        int asciiLetter = letter;

        int asciiCodeEnd = asciiLetter % 10;
        int asciiCodeStart = getFirstDigit(asciiLetter);
        char encryptedChar = (char)(asciiLetter + asciiCodeEnd);
        char differenceLetter = (char)(asciiLetter - asciiCodeStart);
        return $"{encryptedChar}{asciiCodeStart}{asciiCodeEnd}{differenceLetter}";
    }

    static void Main()
    {
        StringBuilder encryptedString = new StringBuilder();

        Console.WriteLine("Enter number of characters to Encrypt: ");
        int count = Convert.ToInt32(Console.ReadLine());
        while (count-- != 0)
        {
            char inputChar = Convert.ToChar(Console.ReadLine());
            encryptedString.Append(Encrypt(inputChar));
        }
        Console.WriteLine(encryptedString);
    }
}