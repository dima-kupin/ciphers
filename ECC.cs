using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace hammingCode
{
    class Program
    {
        public const bool t = true;
        public const bool f = false;
        public const int startWith = 2;
        static int length;

        static bool[] Encode(bool[] code)
        {
            var encoded = new bool[length];

            int i = startWith, j = 0;
            while (i < length)
            {
                if (i == 3 || i == 7) i++;
                encoded[i] = code[j];

                i++;
                j++;
            }

            encoded[0] = Helpers.doXoringForPosition(encoded, length, 1);
            encoded[1] = Helpers.doXoringForPosition(encoded, length, 2);
            encoded[3] = Helpers.doXoringForPosition(encoded, length, 4);
            if (length > 7)
                encoded[7] = Helpers.doXoringForPosition(encoded, length, 8);

            return encoded;
        }

        static bool[] Decode(bool[] encoded)
        {
            var decoded = new bool[11];

            int i = startWith, j = 0;
            while (i < length)
            {
                if (i == 3 || i == 7) i++;
                decoded[j] = encoded[i];

                i++;
                j++;
            }

            return decoded;
        }

        static int ErrorSyndrome(bool[] encoded)
        {
            int syndrome =
                (Convert.ToInt32(Helpers.doXoringForPosition(encoded, length, 1) ^ encoded[0])) +
                (Convert.ToInt32(Helpers.doXoringForPosition(encoded, length, 2) ^ encoded[1]) << 1) +
                (Convert.ToInt32(Helpers.doXoringForPosition(encoded, length, 4) ^ encoded[3]) << 2);
            if (length > 7) syndrome +=
               (Convert.ToInt32(Helpers.doXoringForPosition(encoded, length, 8) ^ encoded[7]) << 3);

            return syndrome;
        }

        static void MixinSingleError(bool[] encoded, int pos)
        {
            encoded[pos - 1] = !encoded[pos - 1];
        }

        static void Main(string[] args)
        {
            length = 15;
            int errorPosition = 10;
            string codeString = "01010101111";

            var code = Helpers.prettyStringToBoolArray(codeString);
            var encoded = Encode(code);
            
            Console.WriteLine(Helpers.boolArrayToPrettyString(code));
            Console.WriteLine(Helpers.boolArrayToPrettyString(encoded));

            MixinSingleError(encoded, errorPosition);
            Console.WriteLine(Helpers.boolArrayToPrettyString(encoded));

            Console.WriteLine(ErrorSyndrome(encoded));
            encoded[errorPosition-1] = !encoded[errorPosition-1];

            var decoded = Decode(encoded);
            Console.WriteLine(Helpers.boolArrayToPrettyString(decoded));

            Console.WriteLine(Enumerable.SequenceEqual(code, decoded));
                   
            Console.WriteLine();

            Console.ReadLine();
        }
    }

    public class Helpers
    {

        public static String boolArrayToPrettyString(bool[] arr)
        {
            return String.Join("", arr.Select(x => Convert.ToInt32(x)));
        }

        public static bool[] prettyStringToBoolArray(String s)
        {
            return s.ToArray().Select(x => ((Convert.ToInt32(x) - 48) > 0)).ToArray();
        }

        public static bool notPowerOf2(int x)
        {
            return !(x == 1 || x == 2 || x == 4 || x == 8);
        }

        public static int[] getPositionsForXoring(int length, int currentHammingPosition)
        {
            var positions = new List<int>();
            for (int i = 1; i <= length; i++)
            {
                if ((i & currentHammingPosition) > 0 && notPowerOf2(i))
                    positions.Add(i);

            }
            return positions.ToArray();
        }

        public static bool doXoringForPosition(bool[] vector, int length, int currentHammingPosition)
        {
            return getPositionsForXoring(length, currentHammingPosition)
                .Select(x => vector[x - 1])
                .Aggregate((x, y) => x ^ y);
        }
    }
}