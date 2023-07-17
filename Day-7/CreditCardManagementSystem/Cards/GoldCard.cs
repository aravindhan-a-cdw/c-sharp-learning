

class GoldCard : Card
{
    public override CardType cardType { get; }

    protected override ulong cardSequence => 29407360945161564;

    public GoldCard()
    {
        spendingLimit = 100000;
        cardType = CardType.GoldCard;
    }
}