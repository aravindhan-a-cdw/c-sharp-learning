using System.Collections.Generic;

class Account
{
    uint accountId;
    public Customer customer;

    static uint accountCount = 0;

    public List<Card> cards = new List<Card>();

    public Account(Customer accountHolder)
    {
        customer = accountHolder;
        accountId = getUniqueAccountNumber();
        ++accountCount;
    }

    uint getUniqueAccountNumber()
    {
        return 709551615 + accountCount;
    }

    public List<Card> getCards()
    {
        return cards;
    }

    public void addCard(Card card)
    {
        cards.Add(card);
    }
}