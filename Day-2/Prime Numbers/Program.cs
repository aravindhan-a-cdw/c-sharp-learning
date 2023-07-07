
/*

2. Write a program to print all the Prime Numbers within a given Range 

Input: 1 10
Output: 2 3 5 7 

*/

using System;
class PrimeNumber
{

    static int getFactors(int number)
    {
        int factors = 1;
        if (number == 1) return 1;
        for (int index = 1; index <= Math.Sqrt(number); index++)
        {
            if (number % index == 0)
            {
                factors++;
            }
        }
        // Console.WriteLine("Number of factors for " + number + " are " + factors);
        return factors;
    }

    static bool isPrime(int number) => getFactors(number) == 2;
    // {
    //     return getFactors(number) == 2;
    // }

    static void Main(string[] args)
    {
        int start = Convert.ToInt32(args[0]);
        int end = Convert.ToInt32(args[1]);
        Console.WriteLine("Printing Prime Numbers in Range " + start + " to " + end);
        if (end < start)
        {
            Console.WriteLine("Start must be greater than end");
            Environment.Exit(1);
        }
        for (int number = start; number <= end; number++)
        {
            // isPrime(number) ? Console.Write(number + " ") : Console.Write("");
            if (isPrime(number))
            {
                Console.Write(number + " ");
            }
        }
        Console.WriteLine("");
    }
}