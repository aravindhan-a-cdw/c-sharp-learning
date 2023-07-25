using System;
using System.Collections.Generic;

enum CardStatus
{
    ACTIVE,
    INACTIVE,
    BLOCKED
}

enum CardType
{
    SilverCard = 1,
    GoldCard = 2,
    PlatinumCard = 3
}

abstract class Card
{
    public ulong cardNumber { get; }

    public CardStatus status { get; set; }

    public uint spendingLimit { get; protected set; }

    abstract public CardType cardType { get; }

    public uint amountSpent { get; set; }

    public ushort PinNumber { get; set; }

    public static uint cardCount = 0;

    protected abstract ulong cardSequence { get; }

    protected List<Purchase> purchases;

    public Card()
    {
        ++cardCount;
        cardNumber = getUniqueCardNumber();
        status = CardStatus.INACTIVE;
        Random rand = new Random();
        PinNumber = (ushort)(rand.NextSingle() * 10000);
    }

    ulong getUniqueCardNumber()
    {
        return cardSequence + cardCount;
    }

    public uint getBalance()
    {
        return spendingLimit - amountSpent;
    }

    public uint debit(string name, uint amount)
    {
        if (amount < getBalance())
        {
            purchases.Add(new Purchase(name, amount));
            amountSpent += amount;
            return amount;
        }
        throw new Exception("Low balance exception");
    }


}