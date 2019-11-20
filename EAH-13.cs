using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAH_13
{
    class Program
    {
        public static Dictionary<char, int> alphabet = new Dictionary<char, int>
        {
                    { 'А', 1 },  { 'Б', 2 },  { 'В', 3 },  { 'Г', 4 },  { 'Д', 5 },  { 'Е', 6 },  { 'Ё', 7 },  { 'Ж', 8 },
                    { 'З', 9 },  { 'И', 10 }, { 'Й', 11 }, { 'К', 12 }, { 'Л', 13 }, { 'М', 14 }, { 'Н', 15 }, { 'О', 16 },
                    { 'П', 17 }, { 'Р', 18 }, { 'С', 19 }, { 'Т', 20 }, { 'У', 21 }, { 'Ф', 22 }, { 'Х', 23 }, { 'Ц', 24 },
                    { 'Ч', 25 }, { 'Ш', 26 }, { 'Щ', 27 }, { 'Ъ', 28 }, { 'Ы', 29 }, { 'Ь', 30 }, { 'Э', 31 }, { 'Ю', 32 },
                    { 'Я', 33 }, { ' ', 1111 }
        };
        public static int[] RemoveIndices(int[] IndicesArray, int RemoveAt)
        {
            int[] newIndicesArray = new int[IndicesArray.Length - 1];

            int i = 0;
            int j = 0;
            while (i < IndicesArray.Length)
            {
                if (i != RemoveAt)
                {
                    newIndicesArray[j] = IndicesArray[i];
                    j++;
                }

                i++;
            }

            return newIndicesArray;
        }
        public static int[] StringToArrayNumberPos(string _stringchar)
        {

            int[] resarray = new int[_stringchar.Length];
            uint count = 0;
            try
            {
                foreach (char literal in _stringchar.ToUpperInvariant())
                    if (literal != ' ')
                    {
                        resarray[count] = alphabet[literal];
                        count++;
                    }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            for (int i = 0; i < resarray.Length; i++)
            {
                if (resarray[i] == 0)
                {
                    resarray = RemoveIndices(resarray, i);
                }
            }
            return resarray;
        }
        public static void Even(int[] arr) {
            int Sum = 0;
            int TripleSum = 0;
            int CD = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (i % 2 == 1)
                {
                    Sum += arr[i-1];
                }
                else if (i % 2 == 0)
                {
                    TripleSum += 3 * arr[i+1];
                }
            }
            Console.Write("Сумма чисел на четных позициях: " + Sum);
            Console.WriteLine();
            Console.Write("Утроенная сумма чисел, стоящих на нечетных позициях: " + TripleSum);


            if ((Sum + TripleSum + CD) % 10 == 0)
            {
                Console.WriteLine("RESULT CD: " + CD);
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if ((Sum + TripleSum + i) % 10 == 0)
                    {
                        CD = i;
                        break;
                    }
                }
                Console.WriteLine();
                Console.Write("RESULT Final: " + CD + " ");
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(arr[i] + " ");
                }
            }
        }
       
        static void Main(string[] args)
        {
            string name = Console.ReadLine();
            int[] arr = StringToArrayNumberPos(name);
            Even(arr);
            Console.ReadKey();
        }
    }
}
