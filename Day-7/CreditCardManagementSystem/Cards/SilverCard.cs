

class SilverCard : Card
{

    public override CardType cardType { get; }

    protected override ulong cardSequence => 932940736094516151;

    public SilverCard()
    {
        spendingLimit = 50000;
        cardType = CardType.SilverCard;
    }
}