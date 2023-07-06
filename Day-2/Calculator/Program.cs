
using System;

public class GenericCalculator
{
    public static T add<T>(T value1, T value2)
    {
        dynamic left = value1;
        dynamic right = value2;
        return left + right;
    }

    public static T subtract<T>(T value1, T value2)
    {
        dynamic left = value1;
        dynamic right = value2;
        return left - right;
    }

    public static T multiply<T>(T value1, T value2)
    {
        dynamic left = value1;
        dynamic right = value2;
        return left * right;
    }

    public static T divide<T>(T value1, T value2)
    {
        dynamic left = value1;
        dynamic right = value2;
        return left / right;
    }
}

class Calculator
{
    static void Main(String[] args)
    {
        double number1 = Convert.ToDouble(args[1]);
        double number2 = Convert.ToDouble(args[2]);

        switch (args[0])
        {
            case "add":
                {
                    double value = GenericCalculator.add(number1, number2);
                    Console.WriteLine("Output is : " + value);
                }
                break;
            case "subtract":
                {
                    double value = GenericCalculator.subtract(number1, number2);
                    Console.WriteLine("Output is : " + value);
                }
                break;
            case "multiply":
                {
                    double value = GenericCalculator.multiply(number1, number2);
                    Console.WriteLine("Output is : " + value);
                }
                break;
            case "divide":
                {
                    double value = GenericCalculator.divide(number1, number2);
                    Console.WriteLine("Output is : " + value);
                }
                break;
        }
    }
}