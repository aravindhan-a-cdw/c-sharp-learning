using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

class Application : ICensusOperations
{
    public static List<Census> FilterByRegion(List<Census> censusList, string region)
    {
        return censusList.FindAll(item => item.region == region);
    }

    public static Dictionary<CensusType, uint> FilterByYear(List<Census> censusList, ushort year)
    {
        List<Census> filteredByRegion = censusList.FindAll(item => item.year == year);
        Dictionary<CensusType, uint> result = new();
        result.Add(CensusType.Birth, 0);
        result.Add(CensusType.Death, 0);
        foreach (Census item in filteredByRegion)
        {
            result[item.type] += item.count;
        }
        return result;
    }

    public static List<string> GetAllRegions(List<Census> censusList)
    {
        HashSet<string> result = new();
        foreach (Census item in censusList)
        {
            result.Add(item.region);
        }
        return new List<string>(result);
    }

    public static List<ushort> GetAllYears(List<Census> censusList)
    {
        HashSet<ushort> result = new();
        foreach (Census item in censusList)
        {
            result.Add(item.year);
        }
        return new List<ushort>(result);
    }

    public static Dictionary<ushort, Dictionary<CensusType, uint>> TotalCountByYear(List<Census> censusList)
    {
        Dictionary<ushort, Dictionary<CensusType, uint>> result = new();
        foreach (Census item in censusList)
        {
            if (!result.ContainsKey(item.year))
            {
                result[item.year] = new();
                result[item.year][CensusType.Birth] = 0;
                result[item.year][CensusType.Death] = 0;
            }
            result[item.year][item.type] += item.count;
        }
        return result;
    }

    public static List<Census> LoadCensusFromFile(string path)
    {
        List<string> allLines = new(File.ReadAllLines(path));
        List<Census> census = new();

        foreach (string item in allLines)
        {
            string[] data = item.Split(',');
            ushort year;
            UInt16.TryParse(data[0], out year);
            uint count;
            UInt32.TryParse(data[3], out count);
            if (data[1] == "Births")
            {
                census.Add(new Census(CensusType.Birth, data[2], year, count));
            }
            else if (data[1] == "Deaths")
            {
                census.Add(new Census(CensusType.Death, data[2], year, count));
            }
            else
            {
                Console.WriteLine($"{data[1]} is not available in census type");
            }
        }
        return census;
    }

    public static ushort FindYearWithMax(List<Census> censusList, CensusType type)
    {
        IEnumerable<Census> typeList = censusList.Where(census => census.type == type);
        uint max = typeList.Max(x => x.count);
        return typeList.Where(x => x.count == max).First().year;
    }

    static Dictionary<string, Dictionary<CensusType, uint>> FindMaxOfAllRegionsWithYear(List<Census> censusList)
    {
        Dictionary<string, Dictionary<CensusType, uint>> result = new();
        foreach (Census census in censusList)
        {
            if (!result.ContainsKey(census.region))
            {
                result[census.region] = new();
                result[census.region][CensusType.Birth] = 0;
                result[census.region][CensusType.Death] = 0;
            }
            if (result[census.region][census.type] < census.count)
            {
                result[census.region][census.type] = census.count;
            }
        }
        return result;
    }

    static void PrintUserOptions()
    {
        ConsoleDisplay.WriteColorLine("Select an option to proceed: ", ConsoleColor.DarkMagenta);
        Console.WriteLine("1. Display all regions");
        Console.WriteLine("2. Display all years");
        Console.WriteLine("3. Display overall Birth and Death Rate");
        Console.WriteLine("4. Filter by year");
        Console.WriteLine("5. Filter by region");
        Console.WriteLine("6. Display year with highest Birth and Death Rate");
        Console.WriteLine("7. Display the highest Birth and Death Rate for each region along with the year");
    }

    static void Print(List<string> list)
    {
        int index = 0;
        foreach (var item in list)
        {
            Console.WriteLine($"{++index}. {item}");
        }
    }

    static void Print(List<ushort> list)
    {
        int index = 0;
        foreach (var item in list)
        {
            Console.WriteLine($"{++index}. {item}");
        }
    }

    static void Print(Dictionary<ushort, Dictionary<string, uint>> data)
    {
        foreach (ushort key in data.Keys)
        {
            ConsoleDisplay.WriteColor($"{key}: ", ConsoleColor.DarkCyan);
            Console.WriteLine($"Birth: {data[key]["Birth"]}, Death: {data[key]["Death"]}");
        }
    }

    static void Print(Dictionary<string, Dictionary<CensusType, uint>> data)
    {
        foreach (string key in data.Keys)
        {
            Dictionary<CensusType, uint> census = data[key];
            Console.WriteLine($"The Max value for the Region {key} are: ");
            Console.WriteLine($"Birth: {census[CensusType.Birth]}, Death: {census[CensusType.Death]}");
        }
    }

    static void Print(Dictionary<CensusType, uint> data)
    {
        foreach (CensusType key in data.Keys)
        {
            ConsoleDisplay.WriteColor($"{key}: {data[key]} ", ConsoleColor.DarkCyan);
        }
    }

    private static void Print(Dictionary<ushort, Dictionary<CensusType, uint>> data)
    {
        foreach (ushort key in data.Keys)
        {
            Dictionary<CensusType, uint> census = data[key];
            Console.WriteLine($"The Count for the {key} are: ");
            Console.WriteLine($"Birth: {census[CensusType.Birth]}, Death: {census[CensusType.Death]}");
        }
    }

    static void Print(List<Census> census)
    {
        foreach (Census item in census)
        {
            Console.WriteLine($"{item.type} Count for year {item.year} is {item.count}");
        }
    }

    static void Main()
    {
        Console.Write("Enter the file location: ");
        string? path = Console.ReadLine();
        if (path == null)
        {
            Console.WriteLine("The path you entered is null!");
            return;
        }
        List<Census> census = LoadCensusFromFile(path);
        while (true)
        {
            PrintUserOptions();
            int userSelection = ConsoleDisplay.OptionSelection(max: 7);

            switch (userSelection)
            {
                case 1:
                    {
                        Print(GetAllRegions(census));
                        break;
                    }
                case 2:
                    {
                        Print(GetAllYears(census));
                        break;
                    }
                case 3:
                    {
                        Print(TotalCountByYear(census));
                        break;
                    }
                case 4:
                    {
                        ushort year;
                        ConsoleDisplay.WriteColor("Enter the year to filter: ", ConsoleColor.DarkMagenta);
                        UInt16.TryParse(Console.ReadLine(), out year);
                        Print(FilterByYear(census, year));
                        break;
                    }
                case 5:
                    {
                        Console.Write("Enter the region to search: ");
                        string? region = Console.ReadLine();
                        if (region == null) break;
                        Console.WriteLine($"The result for your search of {region} are: ");
                        Print(FilterByRegion(census, region));
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine($"Highest Birth: {FindYearWithMax(census, CensusType.Birth)}");
                        Console.WriteLine($"Highest Death: {FindYearWithMax(census, CensusType.Death)}");
                        break;
                    }
                case 7:
                    {
                        Print(FindMaxOfAllRegionsWithYear(census));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            ConsoleDisplay.WaitForConfirmation();
        }
        // var result = FilterByRegion(census, "Northland region");
        // Console.WriteLine(result["Birth"]);
        // Console.WriteLine(result["Death"]);
    }

}