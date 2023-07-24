

class PlatinumCard : Card
{

    public override CardType cardType { get; }

    protected override ulong cardSequence => 6594342609451615;

    public PlatinumCard()
    {
        spendingLimit = 150000;
        cardType = CardType.PlatinumCard;
    }
}