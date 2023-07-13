
using System;
using System.Collections.Generic;
class Application
{

    static Bank bank1 = new Bank();

    static void printUserSelectionOptions()
    {
        Console.Clear();
        ConsoleDisplay.WriteColorLine("Select a option to proceed:", ConsoleColor.DarkCyan);
        Console.WriteLine("1. Bank Administration");
        Console.WriteLine("2. Customer");
        Console.WriteLine();
        ConsoleDisplay.WriteColor("Enter your option: ", ConsoleColor.DarkMagenta);
    }

    static void printBankAdminOptions()
    {
        Console.Clear();
        ConsoleDisplay.WriteColorLine("Select a option to proceed:", ConsoleColor.DarkCyan);
        Console.WriteLine("1. View all customers data");
        Console.WriteLine("2. View all Issued Cards");
        Console.WriteLine("3. Add new Customer");
        Console.WriteLine("4. Issue New Credit Card");
        Console.WriteLine("5. View blocked Cards");
        Console.WriteLine("6. Close/Block Credit Card");
        Console.WriteLine("7. Logout");
        Console.WriteLine();
        ConsoleDisplay.WriteColor("Enter your option: ", ConsoleColor.DarkMagenta);
    }
    static void printCustomerOptions()
    {
        Console.Clear();
        ConsoleDisplay.WriteColorLine("Select a option to proceed:", ConsoleColor.DarkCyan);
        Console.WriteLine("1. Apply for new Credit Card");
        Console.WriteLine("2. View Balance");
        Console.WriteLine("3. Close/Block Credit Card");
        Console.WriteLine("4. Logout");
        ConsoleDisplay.WriteColor("Enter your option: ", ConsoleColor.DarkMagenta);
    }

    static void invalidOptionError()
    {

        ConsoleDisplay.WriteColorLine("\nYou have selected an invalid option. Try Again!\n", ConsoleColor.Red);
    }

    static void waitForConfirmation()
    {
        Console.Write("\nPress ENTER to go back");
        Console.ReadKey();
    }

    static void addNewCustomer()
    {
        ConsoleDisplay.WriteColorLine("Help me by answering the following questions!", ConsoleColor.DarkCyan);
        Console.Write("Enter the name of the customer: ");
        string name = Console.ReadLine();
        Console.Write("Enter the customer aadhar number: ");
        uint aadharNumber;
        UInt32.TryParse(Console.ReadLine(), out aadharNumber);
        Customer customer = bank1.addNewCustomer(name, aadharNumber);
        ConsoleDisplay.WriteColorLine($"{customer} has been created successfully!", ConsoleColor.Green);
    }

    static void applyCreditCard()
    {
        Console.Write("Enter you aadhar number: ");
        ulong aadharNumber;
        UInt64.TryParse(Console.ReadLine(), out aadharNumber);
        bool created = bank1.applyCreditCard(aadharNumber);
        if (!created)
        {
            ConsoleDisplay.WriteColorLine("You do not have any account in our bank!", ConsoleColor.Red);
            return;
        }
        ConsoleDisplay.WriteColorLine("You have successfully requested for Credit Card", ConsoleColor.Green);
    }

    static void bankAdminLoop()
    {
        while (true)
        {
            int selectedOption;
            printBankAdminOptions();
            Int32.TryParse(Console.ReadLine(), out selectedOption);
            if (selectedOption < 1 || selectedOption > 7)
            {
                invalidOptionError();
            }

            if (selectedOption == 7) break;

            switch (selectedOption)
            {
                case 1:
                    Console.Clear();
                    bank1.viewAllAccountInfo();
                    waitForConfirmation();
                    break;
                case 2:
                    Console.Clear();
                    bank1.viewAllIssuedCardsInfo();
                    waitForConfirmation();
                    break;
                case 3:
                    Console.Clear();
                    addNewCustomer();
                    waitForConfirmation();
                    break;
                case 4:
                    Console.Clear();
                    bank1.issueCreditCard();
                    waitForConfirmation();
                    Console.Clear();
                    break;
                case 5:
                    Console.Clear();
                    bank1.viewAllBlockedCardsInfo();
                    waitForConfirmation();
                    break;
                case 6:
                    Console.Clear();
                    ulong cardNumber;
                    ConsoleDisplay.WriteColor("Enter the card number to block: ", ConsoleColor.DarkCyan);
                    UInt64.TryParse(Console.ReadLine(), out cardNumber);
                    bank1.blockCard(cardNumber);
                    waitForConfirmation();
                    break;

            }
        }
    }

    static void customerLoop()
    {
        while (true)
        {
            int selectedOption;
            printCustomerOptions();
            Int32.TryParse(Console.ReadLine(), out selectedOption);
            if (selectedOption < 1 || selectedOption > 4)
            {
                invalidOptionError();
            }

            if (selectedOption == 4) break;

            switch (selectedOption)
            {
                case 1:
                    Console.Clear();
                    applyCreditCard();
                    waitForConfirmation();
                    break;
                case 2:
                    while (true)
                    {
                        break;
                    }
                    break;
            }
        }
    }

    static void programLoop()
    {
        while (true)
        {
            int selectedOption;
            printUserSelectionOptions();
            Int32.TryParse(Console.ReadLine(), out selectedOption);
            if (selectedOption > 2 || selectedOption < 1)
            {
                invalidOptionError();
            }

            switch (selectedOption)
            {
                case 1:
                    bankAdminLoop();
                    break;
                case 2:
                    customerLoop();
                    break;
            }

        }
    }

    static void initialize()
    {
        bank1.addNewCustomer("Aravindhan", 123456);
        bank1.addNewCustomer("Sowdesh", 43524545);
    }

    static void Main()
    {
        initialize();
        Console.Clear();
        ConsoleDisplay.WriteLine("Hello, Welcome to the application!");
        programLoop();
        Console.Clear();
        ConsoleDisplay.WriteColorLine("Thanks you for availing our service! Visit us Again!", ConsoleColor.Green);
    }
}