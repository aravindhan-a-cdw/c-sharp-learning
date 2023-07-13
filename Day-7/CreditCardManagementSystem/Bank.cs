
using System;
using System.Collections.Generic;

enum Info
{
    cards,
    customers,
    blocked_cards

}

class Bank
{

    string name;

    List<Account> accounts = new List<Account>();
    List<Card> cards = new List<Card>();
    // ArrayList customers = new ArrayList();

    public List<Account> getAccounts()
    {
        return accounts;
    }

    public Customer addNewCustomer(string name, uint aadharNumber)
    {
        Customer newCustomer = Customer.getCustomer(name, aadharNumber);
        Account newAccount = new Account(newCustomer);
        newCustomer.addAccount(newAccount);
        accounts.Add(newAccount);
        return newCustomer;
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

    public bool applyCreditCard(ulong aadharNumber)
    {
        int accountIndex = accounts.FindIndex(account => account.customer.getAadharNumber() == aadharNumber);
        if (accountIndex == -1) return false;
        Account account = accounts[accountIndex];
        if (account.customer.cardCount == 5) return false;
        Card newCard = new Card();
        cards.Add(newCard);
        account.addCard(newCard);
        account.customer.cardCount++;
        return true;
    }

    public void issueCreditCard()
    {
        List<Card> inactiveCards = cards.FindAll(card => card.getStatus() == CardStatus.INACTIVE);
        if (inactiveCards.Count == 0)
        {
            ConsoleDisplay.WriteColorLine("No card has been requested to issue!", ConsoleColor.Red);
            return;
        }
        int index = 1;
        foreach (Card card in inactiveCards)
        {
            Console.WriteLine($"{index}. {card}");
        }
        ConsoleDisplay.WriteColorLine("Enter Index of the card to issue:", ConsoleColor.DarkCyan);
        Int32.TryParse(Console.ReadLine(), out index);
        if (index - 1 >= inactiveCards.Count)
        {
            ConsoleDisplay.WriteColorLine("Invalid Input", ConsoleColor.Red);
            return;
        }
        inactiveCards[index - 1].setStatus(CardStatus.ACTIVE);
    }

    public void viewAllIssuedCardsInfo()
    {
        List<Card> activeCards = cards.FindAll(card => card.getStatus() == CardStatus.ACTIVE);
        if (activeCards.Count == 0)
        {
            ConsoleDisplay.WriteColorLine("No Cards exist to show!", ConsoleColor.Red);
            return;
        }
        Console.WriteLine($"Total Cards Issued: {activeCards.Count}");
        int index = 1;
        foreach (Card card in activeCards)
        {
            Console.WriteLine($"{index++}. {card}");
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
        List<Card> blockedCards = cards.FindAll(card => card.getStatus() == CardStatus.BLOCKED);
        if (blockedCards.Count == 0)
        {
            ConsoleDisplay.WriteColorLine("No cards have been blocked yet!", ConsoleColor.Red);
            return;
        }
        foreach (Card card in blockedCards)
        {
            Console.WriteLine(card);
        }

    }

    public void blockCard(ulong cardNumber)
    {
        int index = cards.FindIndex(card => card.getCardNumber() == cardNumber);
        if (index == -1)
        {
            ConsoleDisplay.WriteColorLine("No card found!", ConsoleColor.Red);
            return;
        }
        cards[index].setStatus(CardStatus.BLOCKED);
    }
}