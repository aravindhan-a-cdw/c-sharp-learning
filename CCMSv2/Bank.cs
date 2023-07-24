
using System;
using System.Collections.Generic;

enum Info
{
    cards,
    customers,
    blocked_cards

}

abstract class Bank
{

    public abstract string name { get; }

    List<Account> accounts = new List<Account>();
    List<Card> cards = new List<Card>();
    // ArrayList customers = new ArrayList();

    public List<Account> getAccounts()
    {
        return accounts;
    }

    public Account? GetAccount(ulong aadharNumber)
    {
        int index = getAccountIndex(aadharNumber);
        if (index != -1)
        {
            return accounts[index];
        }
        return null;
    }

    public Customer? addNewCustomer(string name, uint aadharNumber)
    {
        Customer? customer = Customer.getCustomer(aadharNumber); ;
        if (customer == null)
        {
            customer = new(name, aadharNumber);
        }
        Account newAccount = new Account(customer, this.name);
        customer.addAccount(newAccount);
        accounts.Add(newAccount);
        return customer;
    }

    public int getAccountIndex(ulong aadharNumber)
    {
        int account = accounts.FindIndex(account => account.customer.getAadharNumber() == aadharNumber);
        if (account == -1)
        {
            ConsoleDisplay.WriteColorLine("You do not have any account in our bank!", ConsoleColor.Red);
            return -1;
        }
        return account;
    }

    public bool applyCreditCard(ulong aadharNumber, ushort cardType)
    {
        int accountIndex = accounts.FindIndex(account => account.customer.getAadharNumber() == aadharNumber);
        if (accountIndex == -1)
        {
            Console.WriteLine("You donot have an account in our bank!");
            return false;
        }
        Account account = accounts[accountIndex];
        if (account.customer.cardCount == 5)
        {
            Console.WriteLine("You have reached maximum number of cards");
            return false;
        }
        Card newCard = (Card)Activator.CreateInstance(null, ((CardType)cardType).ToString()).Unwrap();
        cards.Add(newCard);
        account.addCard(newCard);
        account.customer.cardCount++;
        ConsoleDisplay.WriteColorLine("Your new credit card details are:", ConsoleColor.DarkCyan);
        Console.WriteLine($"Card Type: {newCard.cardType}");
        Console.WriteLine($"Card Number: {newCard.cardNumber}");
        Console.WriteLine($"Card Pin: {newCard.PinNumber}");
        Console.WriteLine($"Card Status: {newCard.status}");
        return true;
    }

    public void issueCreditCard()
    {
        List<Card> inactiveCards = cards.FindAll(card => card.status == CardStatus.INACTIVE);
        if (inactiveCards.Count == 0)
        {
            ConsoleDisplay.WriteColorLine("No card has been requested to issue!", ConsoleColor.Red);
            return;
        }
        int index = 1;
        foreach (Card card in inactiveCards)
        {
            Console.WriteLine($"{index++}. {card.cardNumber}");
        }
        ConsoleDisplay.WriteColorLine("Enter Index of the card to issue:", ConsoleColor.DarkCyan);
        Int32.TryParse(Console.ReadLine(), out index);
        if (index < 1 || index > inactiveCards.Count)
        {
            ConsoleDisplay.WriteColorLine("Invalid Input", ConsoleColor.Red);
            return;
        }
        inactiveCards[index - 1].status = CardStatus.ACTIVE;
        ConsoleDisplay.WriteColorLine("You have successfully issued a card!", ConsoleColor.Green);
    }

    public void viewAllIssuedCardsInfo()
    {
        List<Card> activeCards = cards.FindAll(card => card.status == CardStatus.ACTIVE);
        if (activeCards.Count == 0)
        {
            ConsoleDisplay.WriteColorLine("No Cards exist to show!", ConsoleColor.Red);
            return;
        }
        Console.WriteLine($"Total Cards Issued: {activeCards.Count}");
        int index = 1;
        foreach (Card card in activeCards)
        {
            Console.WriteLine($"{index++}. {card.cardNumber}");
        }


    }

    public void viewAllAccountInfo()
    {
        if (accounts.Count == 0)
        {
            ConsoleDisplay.WriteColorLine("No Customers exist to show!", ConsoleColor.Red);
            return;
        }

        Console.WriteLine("Total Customers: " + accounts.Count);
        foreach (Account account in accounts)
        {
            Console.WriteLine(account.customer);
        }
    }

    public void viewAllBlockedCardsInfo()
    {
        List<Card> blockedCards = cards.FindAll(card => card.status == CardStatus.BLOCKED);
        if (blockedCards.Count == 0)
        {
            ConsoleDisplay.WriteColorLine("No cards have been blocked yet!", ConsoleColor.Red);
            return;
        }
        int index = 0;
        foreach (Card card in blockedCards)
        {
            Console.WriteLine($"{++index}. {card.cardNumber} - {card.cardType}");
        }

    }

    public void blockCard()
    {
        ulong cardNumber;
        ConsoleDisplay.WriteColor("Enter the card number to block: ", ConsoleColor.DarkCyan);
        UInt64.TryParse(Console.ReadLine(), out cardNumber);

        int index = cards.FindIndex(card => card.cardNumber == cardNumber);
        if (index == -1)
        {
            ConsoleDisplay.WriteColorLine("No card found!", ConsoleColor.Red);
            return;
        }

        cards[index].status = CardStatus.BLOCKED;
        ConsoleDisplay.WriteColorLine($"You have successfully blocked the card with card number: {cardNumber}", ConsoleColor.Green);
    }

    public void blockCardWithPin(ulong cardNumber, ushort pin)
    {
        int index = cards.FindIndex(card => card.cardNumber == cardNumber);
        if (index == -1)
        {
            ConsoleDisplay.WriteColorLine("No card found!", ConsoleColor.Red);
            return;
        }
        if (cards[index].PinNumber != pin)
        {
            ConsoleDisplay.WriteColorLine("Invalid Pin number!", ConsoleColor.Red);
            return;
        }
        cards[index].status = CardStatus.BLOCKED;
    }

}