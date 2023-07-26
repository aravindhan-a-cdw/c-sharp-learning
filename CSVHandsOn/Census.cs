
enum CensusType
{
    Birth,
    Death
}

class Census
{
    public ushort year { get; }

    public CensusType type { get; }

    public string region { get; }

    public uint count { get; }

    public Census(CensusType type, string region, ushort year, uint count)
    {
        this.type = type;
        this.region = region;
        this.year = year;
        this.count = count;
    }

    public override string ToString()
    {
        return $"Census<Type: {type.ToString()}, Region: {region}, Year: {year}, Count: {count}>";
    }
}