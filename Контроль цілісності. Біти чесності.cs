using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace integtity_control
{
    class Program
    {
        public static int countOnes = 0;
        
        static string UTF8ToWin1251(string sourceStr)
        {
            Encoding utf8 = Encoding.UTF8;
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");
            byte[] utf8Bytes = utf8.GetBytes(sourceStr);
            byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);
            return win1251.GetString(win1251Bytes);
        }
        static private string Win1251ToUTF8(string source)
        {
            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Encoding win1251 = Encoding.GetEncoding("windows-1251");
            byte[] utf8Bytes = win1251.GetBytes(source);
            byte[] win1251Bytes = Encoding.Convert(win1251, utf8, utf8Bytes);
            source = win1251.GetString(win1251Bytes);
            return source;
        }
   
        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }
        public static void OrrOrEven(string binary) {
            
            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[i] == '1')
                {
                    countOnes++;
                }
                
            }
            if (countOnes % 2 == 1)
            {
                AddOne(binary);
            }
            else if (countOnes % 2 == 0)
            {
                AddZero(binary);
            }
        }
        public static void AddOne(string binary) {
            Console.WriteLine("Кол-во единиц: " + countOnes);
            Console.WriteLine("Паритетный бит: 1");
            binary = "1" + binary.PadLeft(9, '0');
            Console.WriteLine("Итог: " + binary);
        }
        public static void AddZero(string binary) {
            Console.WriteLine("Кол-во единиц: " + countOnes);
            Console.WriteLine("Паритетный бит: 0");
            binary = "0" + binary.PadLeft(9, '0');
            Console.WriteLine("Итог: " + binary);
        }
        static void Main(string[] args)
        {
            string insert = Console.ReadLine();
            string res = UTF8ToWin1251(insert);
            string binaryres = StringToBinary(res);
            Console.WriteLine("Строка в бинарном пердставлении: " + binaryres);
            OrrOrEven(binaryres);
            
            Console.ReadKey();
        }
    }
}
