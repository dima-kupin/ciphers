using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЖД
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = new List<int>();
            Console.Write("Enter 5-digit number ");
            Console.WriteLine();
            int count = 0;
            while (count < 5)
            {
                Console.Write("Enter {0} digit : ", count + 1);
                nums.Add(Convert.ToInt32(Console.ReadLine()));
                count++;
            }
            int n6 = (1 * nums[0] + 2 * nums[1] + 3 * nums[2] + 4 * nums[3] + 5 * nums[4]) % 11;
            Console.Write("6-th digit is: " + n6);
            Console.WriteLine();
            Console.Write("RESULT " + n6);
            for (int i = 0; i < nums.Count; i++)
            {
                Console.Write(nums[i]);
            }
            Console.ReadKey();
            //БОЛЬШЕ ЦИФР И НЕ НАДО ПО ДОКУ СУПРУН (ТОЛЬКО 5)
        }
    }
}
