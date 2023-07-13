

enum CardStatus
{
    ACTIVE,
    INACTIVE,
    BLOCKED
}
class Card
{
    ulong cardNumber;

    CardStatus status;

    uint spendingLimit = 0;

    uint amountSpent = 0;

    ushort pin;

    public static uint cardCount = 0;

    public Card()
    {
        ++cardCount;
        cardNumber = getUniqueCardNumber();
        status = CardStatus.INACTIVE;
    }

    public ulong getCardNumber()
    {
        return cardNumber;
    }

    ulong getUniqueCardNumber()
    {
        return (ulong)6744073709551615 + cardCount;
    }

    public CardStatus getStatus()
    {
        return status;
    }

    public void setStatus(CardStatus newStatus)
    {
        status = newStatus;
    }


}