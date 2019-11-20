using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna
{   
    class Program
    {
        public static List<int> list = new List<int>();
        public static List<int> listEven = new List<int>();
        public static List<int> listFinal = new List<int>();
        
        public static Dictionary<char, int> alphabet = new Dictionary<char, int>
        {
                    { 'А', 1 },  { 'Б', 2 },  { 'В', 3 },  { 'Г', 4 },  { 'Д', 5 },  { 'Е', 6 },  { 'Ё', 7 },  { 'Ж', 8 },
                    { 'З', 9 },  { 'И', 10 }, { 'Й', 11 }, { 'К', 12 }, { 'Л', 13 }, { 'М', 14 }, { 'Н', 15 }, { 'О', 16 },
                    { 'П', 17 }, { 'Р', 18 }, { 'С', 19 }, { 'Т', 20 }, { 'У', 21 }, { 'Ф', 22 }, { 'Х', 23 }, { 'Ц', 24 },
                    { 'Ч', 25 }, { 'Ш', 26 }, { 'Щ', 27 }, { 'Ъ', 28 }, { 'Ы', 29 }, { 'Ь', 30 }, { 'Э', 31 }, { 'Ю', 32 },
                    { 'Я', 33 }, { ' ', 1111 }
        };
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
        public static void Display(int[] arr) {
            Console.Write("Позиции букв в соответствии с алфавитом: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }
        public static void OddOrEven(int [] arr) {
            if (arr.Length % 2 == 0)
            {
                Even(arr);

            }
            else if (arr.Length % 2 == 1)
            {
                Odd(arr);
            }
        }
        public static void Even(int[] arr) {
            int Sum = 0;
            int evenSum = 0;
            int CD = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (i % 2 ==1)
                {
                    list.Add((arr[i-1] * 2) % 9);
                }
                else if (i % 2 == 0)
                {
                    listEven.Add(arr[i + 1]);
                }
            }
            Console.WriteLine();
            Console.Write("1) (i*2) mod 9: ");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i] + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < list.Count; i++)
            {
                Sum += list[i];
            }
            Console.WriteLine("2) Sum is: " + Sum);
            for (int i = 0; i < listEven.Count - 1; i++)
            {
                evenSum += listEven[i];
            }
            Console.WriteLine("3) Sum of even numbers is: " + evenSum);
            if ((Sum + evenSum + CD) % 10 == 0)
            {
                Console.WriteLine("4) RESULT CD: " + CD);
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if ((Sum + evenSum + i) % 10 == 0)
                    {
                        CD = i;
                        break;
                    }
                }
                Console.Write("4) RESULT CD: " + CD + " ");
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(arr[i] + " ");
                }
            }
            
            
        }

        public static void Odd(int [] arr) {
            int Sum = 0;
            int evenSum = 0;
            int CD = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (i % 2 == 1)
                {
                    listEven.Add(arr[i]);
                }
                else if (i % 2 == 0)
                {
                    list.Add((arr[i -1] * 2) % 9);
                }
            }
            Console.WriteLine();
            Console.Write("1) (i*2) mod 9: ");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i] + " ");
            }
            for (int i = 0; i < list.Count; i++)
            {
                Sum += list[i];
            }
            Console.WriteLine();
            Console.WriteLine("2) Sum is: " + Sum);
            for (int i = 0; i < listEven.Count - 1; i++)
            {
                evenSum += listEven[i];
            }
            Console.WriteLine("3) Sum of even numbers is: " + evenSum);
            if ((Sum + evenSum + CD) % 10 == 0)
            {
                Console.WriteLine("4) RESULT CD: " + CD);
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if ((Sum + evenSum + i) % 10 == 0)
                    {
                        CD = i;
                        break;
                    }
                }
                Console.Write("4) RESULT CD: " + CD + " ");
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
            Display(arr);
            OddOrEven(arr);

            Console.ReadKey();
        }
    }
}
