using System;
using System.Collections.Generic;

class Application : ICustomerOperations
{
    static List<Bank> banks = new();

    static List<Customer> customers = new();

    static void Initialize()
    {
        banks.Add(new KVBBank());
        banks.Add(new HDFCBank());
        banks.Add(new CDWBank());

        banks[2].addNewCustomer("Aravindhan", 123456);
        banks[2].addNewCustomer("Sowdesh", 09876);
    }

    static int OptionSelection(int max, int min = 1)
    {
        while (true)
        {
            try
            {
                ConsoleDisplay.WriteColor("Enter your option: ", ConsoleColor.DarkMagenta);
                int selectedOption;
                Int32.TryParse(Console.ReadLine(), out selectedOption);
                if (selectedOption < min || selectedOption > max)
                {
                    throw new InvalidDataException();
                }
                return selectedOption;
            }
            catch (Exception)
            {
                ConsoleDisplay.InvalidOptionError();
            }
        }
    }

    static int SelectBank()
    {
        ConsoleDisplay.WriteColorLine("Select a bank: ", ConsoleColor.Cyan);
        int index = 0;
        foreach (Bank bank in banks)
        {
            Console.WriteLine($"{++index}. {bank.name}");
        }
        return OptionSelection(max: banks.Count) - 1;
    }

    static int SelectUserType()
    {
        ConsoleDisplay.WriteColorLine("Select your role: ", ConsoleColor.DarkCyan);
        Console.WriteLine("1. BankAdmin\n2. Customer");
        // int selectedOption;
        // Int32.TryParse(Console.ReadLine(), out selectedOption);
        return OptionSelection(max: 2);
    }

    static void PrintBankAdminOptions()
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
        // ConsoleDisplay.WriteColor("Enter your option: ", ConsoleColor.DarkMagenta);
    }

    static void PrintCustomerOptions()
    {
        Console.Clear();
        ConsoleDisplay.WriteColorLine("Select a option to proceed:", ConsoleColor.DarkCyan);
        Console.WriteLine("1. Apply for new Credit Card");
        Console.WriteLine("2. View Balance");
        Console.WriteLine("3. Close/Block Credit Card");
        Console.WriteLine("4. Pay for a purchase");
        Console.WriteLine("5. Deposit Money");
        Console.WriteLine("6. Logout");
        // ConsoleDisplay.WriteColor("Enter your option: ", ConsoleColor.DarkMagenta);
    }

    static void BankAdminLoop()
    {
        int bankSelection = SelectBank();
        Bank bank = banks[bankSelection];

        while (true)
        {
            Console.Clear();
            PrintBankAdminOptions();
            int selectedOption = OptionSelection(max: 7);

            if (selectedOption == 7)
            {
                break;
            }

            Console.Clear();
            switch (selectedOption)
            {
                case 1:
                    bank.viewAllAccountInfo();
                    break;
                case 2:
                    bank.viewAllIssuedCardsInfo();
                    break;
                case 3:
                    addNewCustomer(bank);
                    break;
                case 4:
                    bank.issueCreditCard();
                    break;
                case 5:
                    bank.viewAllBlockedCardsInfo();
                    break;
                case 6:
                    bank.blockCard();
                    break;

            }
            ConsoleDisplay.WaitForConfirmation();
        }
    }

    private static void addNewCustomer(Bank bank)
    {
        ConsoleDisplay.WriteColor("Enter the customer name: ", ConsoleColor.Cyan);
        string? name = Console.ReadLine();
        if (name == null)
        {
            ConsoleDisplay.InvalidOptionError();
            return;
        }
        ConsoleDisplay.WriteColor("Enter the customer aadhar number: ", ConsoleColor.Cyan);
        uint aadharNumber;
        UInt32.TryParse(Console.ReadLine(), out aadharNumber);
        bank.addNewCustomer(name, aadharNumber);
        ConsoleDisplay.WriteColorLine("New account has been created successfully!", ConsoleColor.Green);
    }

    static void CustomerLoop()
    {
        Customer? customer = GetCustomer();
        if (customer == null)
        {
            ConsoleDisplay.WriteColorLine("You donot have account in any bank!", ConsoleColor.Red);
            ConsoleDisplay.WaitForConfirmation();
            return;
        }
        ConsoleDisplay.WriteColorLine("Select a bank to proceed: ", ConsoleColor.Cyan);
        int bankIndex = 0;
        List<Account> accounts = customer.getAccounts();
        foreach (Account account in accounts)
        {
            Console.WriteLine($"{++bankIndex}. {account.bank}");
        }
        bankIndex = OptionSelection(max: accounts.Count) - 1;

        Bank? selectedBank = banks.Find(bank => bank.name == accounts[bankIndex].bank);
        if (selectedBank == null)
        {
            ConsoleDisplay.WriteColorLine("The bank is currently under maintanance!", ConsoleColor.Red);
            return;
        }

        while (true)
        {
            Console.Clear();
            PrintCustomerOptions();
            int selectedOption = OptionSelection(max: 6);
            // Int32.TryParse(Console.ReadLine(), out selectedOption);
            // if (selectedOption < 1 || selectedOption > 6)
            // {
            //     ConsoleDisplay.InvalidOptionError();
            // }

            if (selectedOption == 6) break;

            Console.Clear();
            switch (selectedOption)
            {
                case 1:
                    ApplyCreditCard(customer, selectedBank);
                    break;
                case 2:
                    ViewBalance(customer, selectedBank);
                    break;
                case 3:
                    CustomerBlockCreditCard(customer, selectedBank);
                    break;
                case 4:
                    PayForPurchase(customer, selectedBank);
                    break;
                case 5:
                    DepositMoney(customer, selectedBank);
                    break;
            }
            ConsoleDisplay.WaitForConfirmation();
        }
    }

    static Customer? GetCustomer()
    {
        uint aadharNumber;
        ConsoleDisplay.WriteColor("Enter your aadhar number: ", ConsoleColor.DarkMagenta);
        UInt32.TryParse(Console.ReadLine(), out aadharNumber);
        Customer? customer = Customer.getCustomer(aadharNumber);
        if (customer == null)
        {
            return null;
        }
        return customer;
    }

    static void ProgramLoop()
    {
        while (true)
        {
            int userOption = SelectUserType();
            switch (userOption)
            {
                case 1:
                    {
                        BankAdminLoop();
                        break;
                    }
                case 2:
                    {
                        CustomerLoop();
                        break;
                    }
            }
            Console.Clear();
        }
    }

    static void Main()
    {
        Console.Clear();
        Initialize();
        ConsoleDisplay.WriteLine("Hello, Welcome to the application!");
        ProgramLoop();
        Console.Clear();
        ConsoleDisplay.WriteColorLine("Thanks you for availing our service! Visit us Again!", ConsoleColor.Green);
    }

    public static void ApplyCreditCard(Customer customer, Bank bank)
    {
        ConsoleDisplay.WriteColorLine("Select a card you wish to apply:", ConsoleColor.DarkCyan);
        Console.WriteLine("1. Silver Card\n2. Gold Card\n3. Platinum Card\nEnter an option: ");
        ushort cardType;
        UInt16.TryParse(Console.ReadLine(), out cardType);
        if (cardType < 1 || cardType > 3)
        {
            ConsoleDisplay.InvalidOptionError();
            return;
        }
        bool created = bank.applyCreditCard(customer.getAadharNumber(), cardType);
        if (!created)
        {
            ConsoleDisplay.WriteColorLine("You cannot request credit card!", ConsoleColor.Red);
            return;
        }
        ConsoleDisplay.WriteColorLine("You have successfully requested for Credit Card", ConsoleColor.Green);
    }

    public static Card? SelectActiveCard(Account account)
    {
        List<Card> activeCards = account.cards.FindAll(card => card.status == CardStatus.ACTIVE);

        if (activeCards.Count == 0)
        {
            ConsoleDisplay.WriteColorLine("You donot have any card linked to your account!", ConsoleColor.Red);
            return null;
        }
        ConsoleDisplay.WriteColorLine("Select a card to view balance: ", ConsoleColor.Cyan);
        Card card;
        for (int index = 0; index < activeCards.Count; ++index)
        {
            card = activeCards[index];
            Console.WriteLine($"{index + 1}. {card.cardNumber}");
        }

        int selectedIndex = OptionSelection(max: activeCards.Count) - 1;
        card = activeCards[selectedIndex];
        return card;
    }

    public static bool VerifiedCard(Card card)
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

    public static void ViewBalance(Customer customer, Bank bank)
    {
        Account? userAccount = bank.GetAccount(customer.getAadharNumber());
        if (userAccount == null)
        {
            ConsoleDisplay.WriteColorLine("You donot have any account!", ConsoleColor.Red);
            return;
        }

        Card? selectedCard = SelectActiveCard(userAccount);
        if (selectedCard == null) return;

        if (VerifiedCard(selectedCard))
        {
            Console.WriteLine($"You have {selectedCard.spendingLimit - selectedCard.amountSpent}");
        }

    }

    public static void CustomerBlockCreditCard(Customer customer, Bank bank)
    {
        Account? userAccount = bank.GetAccount(customer.getAadharNumber());

        if (userAccount == null)
        {
            ConsoleDisplay.WriteColorLine("You donot have any account!", ConsoleColor.Red);
            return;
        }

        Card? selectedCard = SelectActiveCard(userAccount);
        if (selectedCard == null) return;
        if (VerifiedCard(selectedCard))
        {
            selectedCard.status = CardStatus.BLOCKED;
            Console.WriteLine($"You have blocked your card with card number {selectedCard.cardNumber}");
        }
    }

    public static void PayForPurchase(Customer customer, Bank bank)
    {
        Account? userAccount = bank.GetAccount(customer.getAadharNumber());
        if (userAccount == null)
        {
            ConsoleDisplay.WriteColorLine("You donot have any account!", ConsoleColor.Red);
            return;
        }

        Card? selectedCard = SelectActiveCard(userAccount);
        if (selectedCard == null) return;

        if (VerifiedCard(selectedCard))
        {
            Console.Write("Enter purchase item name: ");
            string? itemName = Console.ReadLine();
            if (itemName == null)
            {
                ConsoleDisplay.WriteColorLine("You havn't entered anything!", ConsoleColor.Red);
                return;
            }
            Console.Write("Enter amount to pay: ");
            uint payAmount;
            UInt32.TryParse(Console.ReadLine(), out payAmount);

            if (selectedCard.getBalance() < payAmount)
            {
                ConsoleDisplay.WriteColorLine("You donot have balance to spend for this purchase!", ConsoleColor.Red);
                return;
            }
            ConsoleDisplay.WriteColor($"Are you sure to spend {payAmount} on this purchase? (Y/N) ", ConsoleColor.Cyan);
            if (Console.ReadLine() == "N") return;
            try
            {
                selectedCard.debit(itemName, payAmount);
                ConsoleDisplay.WriteColorLine("The amount has been debited from your account successfully!", ConsoleColor.Green);
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp);
                ConsoleDisplay.WriteColorLine("You donot have sufficient balance in your account!", ConsoleColor.Red);
            }
        }
    }

    public static void DepositMoney(Customer customer, Bank bank)
    {

        Account? userAccount = bank.GetAccount(customer.getAadharNumber());
        if (userAccount == null)
        {
            ConsoleDisplay.WriteColorLine("You donot have any account!", ConsoleColor.Red);
            return;
        }

        Card? selectedCard = SelectActiveCard(userAccount);
        if (selectedCard == null) return;

        if (VerifiedCard(selectedCard))
        {
            Console.WriteLine("Enter amount to deposit:");
            uint payAmount;
            UInt32.TryParse(Console.ReadLine(), out payAmount);

            selectedCard.amountSpent -= payAmount;
            ConsoleDisplay.WriteColorLine("The amount has been credited to your account successfully!", ConsoleColor.Green);
        }
    }
}