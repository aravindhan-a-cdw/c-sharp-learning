
/*

1. Write a C# program to find if a given number is Armstrong number or Not 
Input : 153
Output : Armstrong

Input: 57
Output: Not an Armstrong Number

Note: A number is considered as an Armstrong number if the sum of its own digits raised to the power number of digits gives the number itself. 
For example: 0, 1, 153, 370, 371, 407, 1634, 8208, 9474 are Armstrong numbers

*/

using System;

class ArmstrongNumber
{
    static void Main(string[] args)
    {
        Console.WriteLine("Armstrong Number");
        Console.Write("Enter a number: ");
        int number = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("You have entered number: " + number);

        double sumOfPoweredDigits = 0;
        int numOfDigits = 0;
        int numberCopy = number;

        while (numberCopy != 0)
        {
            numOfDigits++;
            numberCopy = numberCopy / 10;
        }
        numberCopy = number;
        while (numberCopy != 0)
        {
            int digit = numberCopy % 10;
            sumOfPoweredDigits += Math.Pow(digit, numOfDigits);
            numberCopy = numberCopy / 10;
        }

        Console.WriteLine("The sum is " + sumOfPoweredDigits + " and the number of digits is " + numOfDigits);

        bool isArmstrongNumber = sumOfPoweredDigits == number;
        switch (isArmstrongNumber)
        {
            case true:
                Console.WriteLine("It is an Armstrong number");
                break;
            case false:
                Console.WriteLine("It is not an Armstrong number");
                break;
        }
    }
}