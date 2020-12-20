using System;

namespace OperatorsOverloading
{
    /// <summary>
    ///     The runnable entrypoint of the exercise.
    /// </summary>
    public class Program
    {
        /// <inheritdoc cref="Program" />
        public static void Main()
        {
            var lst = List.From(1, 2, 3, 4, 5);

            int[] res1 = {3, 4, 5, 4, 3};

            // here { ... } is the same as new int[] { ... }
            int[] res2 = {3, 4, 4, 3};

            var lst1 = List.Append(lst.Tail.Tail, List.From(4, 3));

            // Look at this cast!
            // It is possible because of the conversion operator implemented in list
            // Because it is implicit, we can also remove the cast here
            if (lst1 != res1) throw new Exception("Wrong implementation !=");

            var lst2 = lst;
            lst2 += List.From(4, 3);

            if (lst2.Tail.Tail != lst1) throw new Exception("Wrong implementation");

            var lst3 = lst2;

            // Look at this assignment!
            // It is possible because of the implicit conversion operator implemented in list
            lst3 -= 5;

            if (lst3.Tail.Tail != res2)  throw new Exception("Wrong implementation");

            if (lst3 <= lst1) throw new Exception("Wrong implementation");

            if (lst1 >= lst2) throw new Exception("Wrong implementation");

            Console.WriteLine("Ok");
        }
    }
}
