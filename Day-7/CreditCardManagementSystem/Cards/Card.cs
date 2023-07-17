using System;

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

    uint getBalance()
    {
        return spendingLimit - amountSpent;
    }

    uint debit(uint amount)
    {
        if (amount < getBalance())
            amountSpent += amount;
        return amount;
    }


}