/*

Find the final product of the given number till it reaches single digit
Input: 347
Output: 6

Explanation:
347 -> 3*4*7 = 84 -> 8*4 -> 32 -> 3*2 -> 6

Note: Use Recursion to solve this problem

*/

using System;

class SmallestProductOfDigits
{

    static int minifiedProduct(int number)
    {
        if (number > 9)
        {
            int result = 1;
            while (number != 0)
            {
                result = result * (number % 10);
                number = number / 10;
            }
            if (result > 9) return minifiedProduct(result);  // Recursive Case
            return result;
        }
        return number;  // Base Case
    }

    static void Main(string[] args)
    {
        int inputNumber = Convert.ToInt32(args[0]);
        Console.WriteLine(minifiedProduct(inputNumber));
    }
}