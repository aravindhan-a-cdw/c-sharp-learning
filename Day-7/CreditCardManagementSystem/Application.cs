
using System;
using System.Collections.Generic;
using System.IO;

class Application : BankAdminOperations, CustomerOperations
{

    static Bank bank1 = new Bank();
    Dictionary<int, int> store;


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
        Console.WriteLine("4. Pay for a purchase");
        Console.WriteLine("5. Deposit Money");
        Console.WriteLine("6. Logout");
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

            if (selectedOption == 7)
            {
                break;
            }

            Console.Clear();
            switch (selectedOption)
            {
                case 1:
                    viewAllCustomerData();
                    break;
                case 2:
                    viewAllIssuedCards();
                    break;
                case 3:
                    addNewCustomer();
                    break;
                case 4:
                    issueNewCreditCard();
                    break;
                case 5:
                    viewBlockedCards();
                    break;
                case 6:
                    adminBlockCreditCard();
                    break;

            }
            waitForConfirmation();
        }
    }
    static void customerLoop()
    {
        while (true)
        {
            int selectedOption;
            printCustomerOptions();
            Int32.TryParse(Console.ReadLine(), out selectedOption);
            if (selectedOption < 1 || selectedOption > 6)
            {
                invalidOptionError();
            }

            if (selectedOption == 6) break;

            Console.Clear();
            switch (selectedOption)
            {
                case 1:
                    applyCreditCard();
                    break;
                case 2:
                    viewBalance();
                    break;
                case 3:
                    customerBlockCreditCard();
                    break;
                case 4:
                    payForPurchase();
                    break;
                case 5:
                    depositMoney();
                    break;
            }
            waitForConfirmation();
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

    public static void viewAllCustomerData()
    {
        bank1.viewAllAccountInfo();
    }

    public static void viewAllIssuedCards()
    {
        bank1.viewAllIssuedCardsInfo();
    }

    public static void addNewCustomer()
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

    public static void issueNewCreditCard()
    {
        bank1.issueCreditCard();
    }

    public static void viewBlockedCards()
    {
        bank1.viewAllBlockedCardsInfo();
    }

    public static void applyCreditCard()
    {
        Console.Write("Enter you aadhar number: ");
        ulong aadharNumber;
        UInt64.TryParse(Console.ReadLine(), out aadharNumber);
        ConsoleDisplay.WriteColorLine("Select a card you wish to apply:", ConsoleColor.DarkCyan);
        Console.WriteLine("1. Silver Card\n2. Gold Card\n3. Platinum Card\nEnter an option: ");
        ushort cardType;
        UInt16.TryParse(Console.ReadLine(), out cardType);
        if (cardType < 1 || cardType > 3)
        {
            invalidOptionError();
            return;
        }
        bool created = bank1.applyCreditCard(aadharNumber, cardType);
        if (!created)
        {
            ConsoleDisplay.WriteColorLine("You cannot request credit card!", ConsoleColor.Red);
            return;
        }
        ConsoleDisplay.WriteColorLine("You have successfully requested for Credit Card", ConsoleColor.Green);
    }

    static List<Card> GetAllActiveCards()
    {
        ConsoleDisplay.WriteColorLine("Enter you aadhar number: ", ConsoleColor.DarkCyan);
        ulong aadharNumber;
        UInt64.TryParse(Console.ReadLine(), out aadharNumber);
        Account userAccount = bank1.GetAccount(aadharNumber);
        if (userAccount == null)
        {
            return null;
        }
        List<Card> activeCards = userAccount.cards.FindAll(card => card.status == CardStatus.ACTIVE);
        return activeCards;
    }

    public static void viewBalance()
    {
        List<Card> activeCards = GetAllActiveCards();
        if (activeCards == null || activeCards.Count == 0)
        {
            ConsoleDisplay.WriteColorLine("You donot have any card linked to your account!", ConsoleColor.Red);
            return;
        }
        ConsoleDisplay.WriteColorLine("Select a card to view balance: ", ConsoleColor.Cyan);
        Card card;
        for (int index = 0; index < activeCards.Count; ++index)
        {
            card = activeCards[index];
            Console.WriteLine($"{index + 1}. {card.cardNumber}");
        }

        int selectedIndex;
        Int32.TryParse(Console.ReadLine(), out selectedIndex);

        if (selectedIndex < 1 || selectedIndex > activeCards.Count)
        {
            invalidOptionError();
            return;
        }
        card = activeCards[selectedIndex - 1];
        ConsoleDisplay.WriteColorLine("Enter you card pin: ", ConsoleColor.Cyan);
        ushort cardPin;

        UInt16.TryParse(Console.ReadLine(), out cardPin);

        if (card.PinNumber != cardPin)
        {
            ConsoleDisplay.WriteColorLine("You have entered a wrong pin!", ConsoleColor.Red);
            return;
        }
        Console.WriteLine($"You have {card.spendingLimit - card.amountSpent}");
    }

    public static void adminBlockCreditCard()
    {
        ulong cardNumber;
        ConsoleDisplay.WriteColor("Enter the card number to block: ", ConsoleColor.DarkCyan);
        UInt64.TryParse(Console.ReadLine(), out cardNumber);
        if (bank1.blockCard(cardNumber))
        {
            ConsoleDisplay.WriteColorLine($"You have successfully blocked the card with card number: {cardNumber}", ConsoleColor.Green);
        }
    }

    public static bool verifyCardPin(Card card)
    {
        ConsoleDisplay.WriteColorLine("Enter you card pin: ", ConsoleColor.Cyan);
        ushort cardPin;

        UInt16.TryParse(Console.ReadLine(), out cardPin);

        if (card.PinNumber != cardPin)
        {
            ConsoleDisplay.WriteColorLine("You have entered a wrong pin!", ConsoleColor.Red);
            return false;
        }
        return true;
    }

    public static void customerBlockCreditCard()
    {
        List<Card> activeCards = GetAllActiveCards();
        if (activeCards == null || activeCards.Count == 0)
        {
            ConsoleDisplay.WriteColorLine("You donot have any card linked to your account!", ConsoleColor.Red);
            return;
        }
        ConsoleDisplay.WriteColorLine("Select a card to block: ", ConsoleColor.Cyan);
        Card card;
        for (int index = 0; index < activeCards.Count; ++index)
        {
            card = activeCards[index];
            Console.WriteLine($"{index + 1}. {card.cardNumber}");
        }

        int selectedIndex;
        Int32.TryParse(Console.ReadLine(), out selectedIndex);

        if (selectedIndex < 1 || selectedIndex > activeCards.Count)
        {
            invalidOptionError();
            return;
        }
        card = activeCards[selectedIndex - 1];

        if (verifyCardPin(card))
        {
            card.status = CardStatus.BLOCKED;
            Console.WriteLine($"You have blocked your card with card number {card.cardNumber}");
        }
    }

    public static void payForPurchase()
    {
        List<Card> activeCards = GetAllActiveCards();
        if (activeCards == null || activeCards.Count == 0)
        {
            ConsoleDisplay.WriteColorLine("You donot have any card linked to your account!", ConsoleColor.Red);
            return;
        }
        ConsoleDisplay.WriteColorLine("Select a card to complete purchase: ", ConsoleColor.Cyan);
        Card card;
        for (int index = 0; index < activeCards.Count; ++index)
        {
            card = activeCards[index];
            Console.WriteLine($"{index + 1}. {card.cardNumber}");
        }

        int selectedIndex;
        Int32.TryParse(Console.ReadLine(), out selectedIndex);

        if (selectedIndex < 1 || selectedIndex > activeCards.Count)
        {
            invalidOptionError();
            return;
        }
        card = activeCards[selectedIndex - 1];

        if (verifyCardPin(card))
        {
            Console.Write("Enter purchase item name: ");
            string itemName = Console.ReadLine();
            Console.Write("Enter amount to pay:");
            uint payAmount;
            UInt32.TryParse(Console.ReadLine(), out payAmount);

            if (card.getBalance() < payAmount)
            {
                ConsoleDisplay.WriteColorLine("You donot have balance to spend for this purchase!", ConsoleColor.Red);
                return;
            }
            ConsoleDisplay.WriteColor($"Are you sure to spend {payAmount} on this purchase? (Y/N) ", ConsoleColor.Cyan);
            if (Console.ReadLine() == "N") return;
            try
            {
                card.debit(itemName, payAmount);
                ConsoleDisplay.WriteColorLine("The amount has been debited from your account successfully!", ConsoleColor.Green);
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp);
                ConsoleDisplay.WriteColorLine("You donot have sufficient balance in your account!", ConsoleColor.Red);
            }
        }
    }

    public static void depositMoney()
    {
        List<Card> activeCards = GetAllActiveCards();
        if (activeCards == null || activeCards.Count == 0)
        {
            ConsoleDisplay.WriteColorLine("You donot have any card linked to your account!", ConsoleColor.Red);
            return;
        }
        ConsoleDisplay.WriteColorLine("Select a card to complete purchase: ", ConsoleColor.Cyan);
        Card card;
        for (int index = 0; index < activeCards.Count; ++index)
        {
            card = activeCards[index];
            Console.WriteLine($"{index + 1}. {card.cardNumber}");
        }

        int selectedIndex;
        Int32.TryParse(Console.ReadLine(), out selectedIndex);

        if (selectedIndex < 1 || selectedIndex > activeCards.Count)
        {
            invalidOptionError();
            return;
        }
        card = activeCards[selectedIndex - 1];

        if (verifyCardPin(card))
        {
            Console.WriteLine("Enter amount to deposit:");
            uint payAmount;
            UInt32.TryParse(Console.ReadLine(), out payAmount);

            card.amountSpent -= payAmount;
            ConsoleDisplay.WriteColorLine("The amount has been credited to your account successfully!", ConsoleColor.Green);
        }
    }
}