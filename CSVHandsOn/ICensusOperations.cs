
using System.Collections.Generic;

interface ICensusOperations
{
    public static abstract List<string> GetAllRegions(List<Census> censusList);

    public static abstract List<ushort> GetAllYears(List<Census> censusList);

    public static abstract Dictionary<ushort, Dictionary<CensusType, uint>> TotalCountByYear(List<Census> censusList);

    public static abstract Dictionary<CensusType, uint> FilterByYear(List<Census> censusList, ushort year);

    public static abstract List<Census> FilterByRegion(List<Census> censusList, string region);
}