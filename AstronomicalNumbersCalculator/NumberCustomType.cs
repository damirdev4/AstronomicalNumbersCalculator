namespace AstronomicalNumbersCalculator
{
    public class NumberCustomType : IComparable<NumberCustomType>
    {

        public Dictionary<int, int> DigitsOfNumbers { get; set; }

        public int Rank
        {
            get
            {
                return DigitsOfNumbers.Count;
            }
        }

        public bool Negative { get; init; }

        public static bool TryParse(string input, out NumberCustomType? result)
        {
            Dictionary<int, int> digOfNumber = new();
            if (IsNumber(input, digOfNumber))
            {
                result = new NumberCustomType(digOfNumber);
                return true;
            }
            result = null;
            return false;
        }

        static bool IsNumber(string inputNumberForChecking, Dictionary<int, int> digitsOfNumber)
        {
            if (string.IsNullOrEmpty(inputNumberForChecking))
            {
                return false;
            }

            for (int i = inputNumberForChecking.Length - 1, j = 0; i >= 0; i--, j++)
            {
                if (!char.IsDigit(inputNumberForChecking[i]))
                {
                    digitsOfNumber.Clear();
                    return false;
                }
                else
                {
                    digitsOfNumber.Add(j, int.Parse(inputNumberForChecking[i].ToString()));
                }
            }

            RemoveUselessZeroDigits(digitsOfNumber);

            return true;
        }

        static void RemoveUselessZeroDigits(Dictionary<int, int> digitsOfNumber)
        {
            for (int i = digitsOfNumber.Count - 1; i >= 0 && digitsOfNumber.Count > 1; i--)
            {
                if (digitsOfNumber[i] != 0)
                {
                    break;
                }
                else
                {
                    digitsOfNumber.Remove(i);
                }
            }
        }

        public static NumberCustomType operator +(NumberCustomType left, NumberCustomType right)
        {
            var additionResultDict = left.DigitsOfNumbers.Concat(right.DigitsOfNumbers)
                      .GroupBy(d => d.Key)
                      .ToDictionary(g => g.Key, g => g.Sum(d => d.Value));

            for (int i = 0; i < additionResultDict.Count; i++)
            {
                if (additionResultDict[i] > 9 && i + 1 < additionResultDict.Count)
                {
                    additionResultDict[i] = additionResultDict[i] - 10;
                    additionResultDict[i + 1]++;
                }
            }

            return new NumberCustomType(additionResultDict);
        }

        public static NumberCustomType operator -(NumberCustomType left, NumberCustomType right)
        {
            Dictionary<int, int> subtractionResultDict;
            bool negative = false;
            if (left.CompareTo(right) > 0)
            {
                subtractionResultDict = left.DigitsOfNumbers.ToDictionary(d => d.Key, d => d.Value - (right.DigitsOfNumbers.TryGetValue(d.Key, out _) ? right.DigitsOfNumbers[d.Key] : 0));
            }
            else if (left.CompareTo(right) < 0)
            {
                subtractionResultDict = right.DigitsOfNumbers.ToDictionary(d => d.Key, d => d.Value - (left.DigitsOfNumbers.TryGetValue(d.Key, out _) ? left.DigitsOfNumbers[d.Key] : 0));
                negative = true;
            }
            else
            {
                subtractionResultDict = new Dictionary<int, int>() { { 0, 0 } };
            }

            for (int i = 0; i < subtractionResultDict.Count; i++)
            {
                if (subtractionResultDict[i] < 0)
                {
                    subtractionResultDict[i] = 10 + subtractionResultDict[i];
                    subtractionResultDict[i + 1]--;
                }
            }

            return new NumberCustomType(subtractionResultDict) { Negative = negative ? true : false };
        }

        public static NumberCustomType operator *(NumberCustomType left, NumberCustomType right)
        {
            Dictionary<int, int> multiplicationResultDict = new Dictionary<int, int>() { { 0, 0 } };
            NumberCustomType multiplicationResult = new NumberCustomType(multiplicationResultDict);
            for (int i = 0; i < right.Rank; i++)
            {
                multiplicationResultDict = left.DigitsOfNumbers.ToDictionary(d => d.Key + i, d => d.Value * right.DigitsOfNumbers[i]);
                for (int m = 0; m < i; m++)
                {
                    multiplicationResultDict.Add(m, 0);
                }
                for (int j = 0; j < multiplicationResultDict.Count; j++)
                {
                    if (multiplicationResultDict[j] > 9 && j + 1 < multiplicationResultDict.Count)
                    {
                        multiplicationResultDict[j + 1] += multiplicationResultDict[j] / 10;
                        multiplicationResultDict[j] = multiplicationResultDict[j] % 10;
                    }
                }
                multiplicationResult += new NumberCustomType(multiplicationResultDict);
            }

            return multiplicationResult;
        }

        public int CompareTo(NumberCustomType? other)
        {
            if (other == null) return 1;
            if (Rank > other.Rank)
            {
                return 1;
            }
            else if (Rank < other.Rank)
            {
                return -1;
            }
            else
            {
                for (int i = other.Rank - 1; i >= 0; i--)
                {
                    if (DigitsOfNumbers[i] > other.DigitsOfNumbers[i])
                    {
                        return 1;
                    }
                    else if (DigitsOfNumbers[i] < other.DigitsOfNumbers[i])
                    {
                        return -1;
                    }
                }
            }

            return 0;
        }

        public override string ToString()
        {
            string result = string.Empty;
            RemoveUselessZeroDigits(DigitsOfNumbers);

            result = string.Join("", DigitsOfNumbers.Values.Reverse());

            if (Negative)
            {
                result = $"-{result}";
            }

            return result;
        }

        public NumberCustomType(Dictionary<int, int> digitsOfNumber)
        {
            DigitsOfNumbers = digitsOfNumber;
        }
    }
}
