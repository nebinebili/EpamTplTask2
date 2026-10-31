using System.Collections.Concurrent;

namespace PalindromeNumberFiltering
{
    /// <summary>
    /// A static class containing methods for filtering palindrome numbers from a collection of integers.
    /// </summary>
    public static class Selector
    {
        /// <summary>
        /// Retrieves a collection of palindrome numbers from the given list of integers using sequential filtering.
        /// </summary>
        /// <param name="numbers">The list of integers to filter.</param>
        /// <returns>A collection of palindrome numbers.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input list 'numbers' is null.</exception>
        public static IList<int> GetPalindromeInSequence(IList<int>? numbers)
        {
            ArgumentNullException.ThrowIfNull(numbers);

            List<int> list = new List<int>();

            foreach (var number in numbers)
            {
                if (IsPalindrome(number))
                {
                    list.Add(number);
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// Retrieves a collection of palindrome numbers from the given list of integers using parallel filtering.
        /// </summary>
        /// <param name="numbers">The list of integers to filter.</param>
        /// <returns>A collection of palindrome numbers.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input list 'numbers' is null.</exception>
        public static IList<int> GetPalindromeInParallel(IList<int> numbers)
        {
            ArgumentNullException.ThrowIfNull(numbers);

            ConcurrentBag<int> result = new ConcurrentBag<int>();

            _ = Parallel.ForEach(numbers, number =>
            {
                if (IsPalindrome(number))
                {
                    result.Add(number);
                }
            });

            return result.ToArray();
        }

        /// <summary>
        /// Checks whether the given integer is a palindrome number.
        /// </summary>
        /// <param name="number">The integer to check.</param>
        /// <returns>True if the number is a palindrome, otherwise false.</returns>
        private static bool IsPalindrome(int number)
        {
            if (number < 0)
            {
                return false;
            }

            return IsPositiveNumberPalindrome(number, 0);
        }

        /// <summary>
        /// Recursively checks whether a positive number is a palindrome.
        /// </summary>
        /// <param name="number">The positive number to check.</param>
        /// <param name="divider">The divider used in the recursive check.</param>
        /// <returns>True if the positive number is a palindrome, otherwise false.</returns>
        private static bool IsPositiveNumberPalindrome(int number, int divider)
        {
            byte length = GetLength(number);

            if (divider >= length / 2)
            {
                return true;
            }

            int right = (number / (int)Math.Pow(10, divider)) % 10;

            int left = (number / (int)Math.Pow(10, length - divider - 1)) % 10;

            if (left != right)
            {
                return false;
            }

            return IsPositiveNumberPalindrome(number, divider + 1);
        }

        /// <summary>
        /// Gets the number of digits in the given integer.
        /// </summary>
        /// <param name="number">The integer to count digits for.</param>
        /// <returns>The number of digits in the integer.</returns>
        private static byte GetLength(int number)
        {
            switch (number)
            {
                case var n when n < 10:
                    return 1;
                case var n when n < 100:
                    return 2;
                case var n when n < 1000:
                    return 3;
                case var n when n < 10000:
                    return 4;
                case var n when n < 100000:
                    return 5;
                case var n when n < 1000000:
                    return 6;
                case var n when n < 10000000:
                    return 7;
                case var n when n < 100000000:
                    return 8;
                case var n when n < 1000000000:
                    return 9;
                default:
                    return 10;
            }
        }
    }
}
