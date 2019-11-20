using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ИНН
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = new List<int>();
            Console.Write("Enter 9-digit number ");
            Console.WriteLine();
            int count = 0;
            while (count < 9)
            {
                Console.Write("Enter {0} digit : ", count+1);
                nums.Add(Convert.ToInt32(Console.ReadLine()));
                count++;
            }
            int n10 = ((2*nums[0] + 4*nums[1] + 10*nums[2] + 3*nums[3] + 5*nums[4] + 9*nums[5] + 4*nums[6] + 6*nums[7] + 8*nums[8])%11)%10;
            Console.Write("10-th digit is: " + n10);
            Console.WriteLine();
            Console.Write("RESULT: " + n10);
            for (int i = 0; i < nums.Count; i++)
            {
                Console.Write(nums[i]);
            }
            Console.ReadKey();
        }
    }
}
