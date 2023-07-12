using Image;
using System;

class VideoEditor : Image.Editor
{
    public void anotherAssemblySubclass()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Different assembly and a sub class");
        Console.ResetColor();
        Console.WriteLine("Can Access the following: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(this.publicField);
        Console.WriteLine(this.protectedField);
        Console.WriteLine(this.protectedInternalField);
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Cannot access: Private, Internal, Private Protected\n");
        Console.ResetColor();

    }
}


class OutsideClass
{
    static void Main()
    {
        Calling.Main();
        VideoEditor currentClassInstance = new VideoEditor();
        currentClassInstance.anotherAssemblySubclass();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Different assembly and not a sub class");
        Console.ResetColor();
        Console.WriteLine("Can Access the following: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(currentClassInstance.publicField);
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Cannot access: Private, Internal, Private Protected, Protected, Protected Internal \n");
        Console.ResetColor();

        // Image.ImageEditor instance = new Image.ImageEditor();
        // instance.subclassMethod();
    }
}