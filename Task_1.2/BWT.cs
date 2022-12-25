namespace Task_1._2;

public static class Bwt
{
    private static string GetUniqueSymbols(string s)
    {
        var uniqueSymbols = new SortedSet<char>();
        foreach (var ch in s)
            uniqueSymbols.Add(ch);

        return uniqueSymbols.Aggregate("", (current, ch) => current + ch);
    }

    private static int GetLessSymbolsCount(string s, char symbol) => s.Count(ch => ch < symbol);

    private static string GetSortedString(string s)
    {
        var list = s.ToList();
        list.Sort();
        return list.Aggregate("", (current, ch) => current + ch);
    }

    private static int CountSymbolInString(string s, char symbol) => s.Count(ch => ch == symbol);

    // прямой BWT
    public static (string transformedString, int position) StraightBwt(string s)
    {
        var inputStringLength = s.Length;
        var suffixArray = new SuffixArray(s);

        var transformedString = "";
        var position = 0;
        for (var i = 0; i < inputStringLength; ++i)
        {
            transformedString += s[(suffixArray.Array[i] + inputStringLength - 1) % inputStringLength];
            if (suffixArray.Array[i] == 0)
                position = i;
        }

        return (transformedString, position);
    }

    // обратный BWT
    public static string InverseBwt((string transformedString, int position) result)
    {
        var transformedString = result.transformedString;
        var position = result.position;
        var transformedStringLength = transformedString.Length;

        var uniqueSymbols = GetUniqueSymbols(transformedString);
        var sortedResultString = GetSortedString(transformedString);

        var firstCharOccurrence =
            uniqueSymbols.Select(t => sortedResultString.IndexOf(t))
                .ToList(); // индекс первого вхождения символа в ОТСОРТИРОВАННОЙ строке

        var transformedStringCharsPositions = new List<int>(); // список для построения исходной строки
        for (var i = 0; i < transformedStringLength; ++i)
        {
            transformedStringCharsPositions.Add(firstCharOccurrence[uniqueSymbols.IndexOf(transformedString[i])]);
            firstCharOccurrence[uniqueSymbols.IndexOf(transformedString[i])]++;
        }

        // восстанавливаем исходную строку
        var originalString = "";
        for (var i = 0; i < transformedStringLength; ++i)
        {
            position = transformedStringCharsPositions[position];
            originalString = sortedResultString[position] + originalString;
        }

        return originalString;
    }

    public static string LinearInverseBwt((string transformedString, int position) result)
    {
        var resultString = result.transformedString;
        var position = result.position;
        var resultStringLength = resultString.Length;

        var firstCharOccurrence = new int[resultStringLength];
        for (var i = 0; i < resultStringLength; ++i)
            firstCharOccurrence[i] = CountSymbolInString(resultString[..i], resultString[i]);
        var lessSymbolsCount = new Dictionary<char, int>();
        foreach (var ch in GetUniqueSymbols(resultString))
            lessSymbolsCount[ch] = GetLessSymbolsCount(resultString, ch);

        var originalString = "" + resultString[position];
        while (originalString.Length != resultStringLength)
        {
            position = firstCharOccurrence[position] + lessSymbolsCount[originalString[0]];
            originalString = resultString[position] + originalString;
        }

        return originalString;
    }

    public static void Main()
    {
        Console.Write(
            "Введите ПРЯМОЕ или STRAIGHT для прямого преобразования BWT, и ОБРАТНОЕ или INVERSE для обратного: ");
        var mode = Console.ReadLine();
        if (mode?.ToUpper() == "ПРЯМОЕ" || mode?.ToUpper() == "STRAIGHT")
        {
            Console.Write("Введите строку: ");
            var inputString = Console.ReadLine();
            if (inputString == null) return;
            var bwt = StraightBwt(inputString);
            Console.WriteLine($"Результат прямого преобразования BWT: ({bwt.transformedString}, {bwt.position})");
        }
        else
        {
            Console.Write("Введите строку: ");
            var result = Console.ReadLine();
            Console.Write("Введите позицию символа конца строки: ");
            var position = int.Parse(Console.ReadLine() ?? string.Empty);
            var argument = (result, position);
            Console.WriteLine($"Результат обратного преобразования BWT: {LinearInverseBwt(argument)}");
        }
    }
}