
using System;
using System.Reflection;
using Image;

namespace Image
{
    class Editor
    {
        public string publicField = "Public";

        private string privateField = "Private";

        internal string internalField = "Internal";

        protected string protectedField = "Protected";

        private protected string privateProtectedField = "Private Protected";

        protected internal string protectedInternalField = "Protected Internal";
    }

    class ImageEditor : Editor
    {
        public void subclassMethod()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Inside same assembly and a sub class");
            Console.ResetColor();
            Console.WriteLine("Can Access the following: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(this.publicField);
            Console.WriteLine(this.internalField);
            Console.WriteLine(this.protectedField);
            Console.WriteLine(this.protectedInternalField);
            Console.WriteLine(this.privateProtectedField);
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Cannot access: Private\n");
            Console.ResetColor();
        }
    }

    class UserInterface
    {

    }
}

public class Calling
{

    public static void listProperties(object obj)
    {
        PropertyInfo[] props = obj.GetType().GetProperties();
        Console.WriteLine(props.Length);
        foreach (var prop in props)
        {
            Console.WriteLine(prop.Name + ": " + prop.GetValue(obj, null));
        }
    }

    public static void Main()
    {
        ImageEditor instance = new ImageEditor();

        instance.subclassMethod();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Inside same assembly but not a sub class");
        Console.ResetColor();
        Console.WriteLine("Can Access the following: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(instance.publicField);
        Console.WriteLine(instance.internalField);
        Console.WriteLine(instance.protectedInternalField);
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Cannot access: Private, Protected, PrivateProtected\n");
        Console.ResetColor();
    }
}