using System;

class AdditionMatix
{

    static int[] getAdditionCombo(int number)
    {
        int[] output = new int[3];
        Random rand = new Random();
        output[0] = rand.Next(1, number - 2);
        output[1] = rand.Next(1, number - output[0]);
        output[2] = number - output[0] - output[1];

        // Console.WriteLine($"{output[0]} + {output[1]} + {output[2]} = {number}");
        return output;
    }
    static void Main()
    {
        Console.Write("Enter a number: ");
        int input = Convert.ToInt32(Console.ReadLine());
        int[] combo = getAdditionCombo(input);
        Console.WriteLine($"{combo[0]} {combo[1]} {combo[2]}");
        Console.WriteLine($"{combo[1]} {combo[2]} {combo[0]}");
        Console.WriteLine($"{combo[2]} {combo[0]} {combo[1]}");
    }
}