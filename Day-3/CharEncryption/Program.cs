
using System;
using System.Text;
class CharEncryption
{

    static int getFirstDigit(int number)
    {
        while (number / 10 != 0)
        {
            number = number / 10;
        }
        return number;
    }

    static string Encrypt(char letter)
    {
        byte asciiCode = Convert.ToByte(letter);
        byte asciiCodeEnd = (byte)(asciiCode % 10);
        int asciiCodeStart = getFirstDigit(asciiCode);
        char encryptedChar = Convert.ToChar(asciiCode + asciiCodeEnd);
        return encryptedChar.ToString() +
        Convert.ToString(asciiCodeStart) +
        Convert.ToString(asciiCodeEnd) +
        Convert.ToChar(asciiCode - asciiCodeStart).ToString();
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