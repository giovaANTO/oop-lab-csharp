using System;
using System.Collections.Generic;
using System.Linq;

namespace Iterators
{
    /// <summary>
    ///     The runnable entrypoint of the exercise.
    /// </summary>
    public class Program
    {
        /// <inheritdoc cref="Program" />
        public static void Main()
        {
            const int len = 50;
            var numbers = new int?[len];
            var rand = new Random();
            for (var i = 0; i < len; i++)
                if (rand.NextDouble() > 0.2)
                    numbers[i] = rand.Next(len);

            // TODO rewrite using methods from Java8StreamOperations
            IDictionary<int, int> occurrences = numbers
                .Select(optN =>
                {
                    Console.Write(optN + ",");
                    return optN;
                })
                .Skip(1)
                .Take(len - 2)
                .Where(optN => optN.HasValue)
                .Select(optN => optN.Value)
                .Aggregate(new Dictionary<int, int>(), (d, n) =>
                {
                    if (!d.ContainsKey(n))
                        d[n] = 1;
                    else
                        d[n]++;

                    return d;
                });

            Console.WriteLine();

            foreach (var kv in occurrences) Console.WriteLine(kv);
        }
    }
}
