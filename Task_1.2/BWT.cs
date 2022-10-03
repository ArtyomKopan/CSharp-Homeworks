using System.Runtime.InteropServices;

namespace Task_1._2
{
    public class BWT
    {
        private static string getUniqueSymbols(string s)
        {
            var uniqueSymbols = new SortedSet<Char>();
            foreach (var ch in s)
                uniqueSymbols.Add(ch);
            var new_string = "";
            foreach (var ch in uniqueSymbols)
            {
                new_string += ch;
            }

            return new_string;
        }

        private static string getSortedString(string s)
        {
            var list = s.ToList();
            list.Sort();
            var sortedString = "";
            foreach (var ch in list)
                sortedString += ch;
            return sortedString;
        }

        private static int countSymbolInSortedString(string sortedString, char symbol)
        {
            var count = 0;
            var startIndex = sortedString.IndexOf(symbol);
            for (var i = startIndex; i < sortedString.Length; ++i)
            {
                if (sortedString[i] == symbol)
                    count++;
                else
                    break;
            }

            return count;
        }

        // прямой BWT
        public static KeyValuePair<string, int> StraightBWT(String s)
        {
            var n = s.Length;
            var suffixArray = new SuffixArray(s);

            var resultString = "";
            var position = 0;
            for (var i = 0; i < n; ++i)
            {
                resultString += s[(suffixArray.Array[i] + n - 1) % n];
                if (suffixArray.Array[i] == 0)
                    position = i;
            }

            return new KeyValuePair<string, int>(resultString, position);
        }

        // обратный BWT
        public static string InverseBWT(KeyValuePair<string, int> result)
        {
            var resultString = result.Key;
            var position = result.Value;
            var n = resultString.Length;

            var uniqueSymbols = getUniqueSymbols(resultString);
            var sortedResultString = getSortedString(resultString);

            var l = new List<int>(); // количество вхождений каждого символа
            var a = new List<int>(); // индекс первого вхождения символа в ОТСОРТИРОВАННОЙ строке
            for (var i = 0; i < uniqueSymbols.Length; ++i)
            {
                l.Add(countSymbolInSortedString(sortedResultString, uniqueSymbols[i]));
                a.Add(sortedResultString.IndexOf(uniqueSymbols[i]));
            }

            var p = new List<int>(); // список для построения исходной строки
            for (var i = 0; i < n; ++i)
            {
                p.Add(a[uniqueSymbols.IndexOf(resultString[i])]);
                a[uniqueSymbols.IndexOf(resultString[i])]++;
            }

            // восстанавливаем исходную строку
            var s = "";
            for (var i = 0; i < n; ++i)
            {
                position = p[position];
                s = sortedResultString[position] + s;
            }

            return s;
        }

        public static void Main()
        {
            Console.Write(
                "Введите ПРЯМОЕ или STRAIGHT для прямого преобразования BWT, и ОБРАТНОЕ или INVERSE для обратного: ");
            var mode = Console.ReadLine();
            if (mode.ToUpper() == "ПРЯМОЕ" || mode.ToUpper() == "STRAIGHT")
            {
                Console.Write("Введите строку: ");
                var s = Console.ReadLine();
                var bwt = StraightBWT(s);
                Console.WriteLine($"Результат прямого преобразования BWT: ({bwt.Key}, {bwt.Value})");
            }
            else
            {
                Console.Write("Введите строку: ");
                var result = Console.ReadLine();
                Console.Write("Введите позицию символа конца строки: ");
                var position = int.Parse(Console.ReadLine());
                var argument = new KeyValuePair<string, int>(result, position);
                Console.WriteLine($"Результат обратного преобразования BWT: {InverseBWT(argument)}");
            }
        }
    }
}