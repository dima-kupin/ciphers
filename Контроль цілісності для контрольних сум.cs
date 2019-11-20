using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public enum CRC8_POLY
    {
        CRC8 = 0xd5,
        CRC8_CCITT = 0x07,
        CRC8_DALLAS_MAXIM = 0x31,
        CRC8_SAE_J1850 = 0x1D,
        CRC_8_WCDMA = 0x9b,
    };

    class Program
    {
        private byte[] table = new byte[256];

        public byte Checksum(params byte[] val)
        {
            if (val == null)
                throw new ArgumentNullException("val");

            byte c = 0;

            foreach (byte b in val)
            {
                c = table[c ^ b];
            }

            return c;
        }

        public byte[] Table
        {
            get
            {
                return this.table;
            }
            set
            {
                this.table = value;
            }
        }

        public byte[] GenerateTable(CRC8_POLY polynomial)
        {
            byte[] csTable = new byte[256];

            for (int i = 0; i < 256; ++i)
            {
                int curr = i;

                for (int j = 0; j < 8; ++j)
                {
                    if ((curr & 0x80) != 0)
                    {
                        curr = (curr << 1) ^ (int)polynomial;
                    }
                    else
                    {
                        curr <<= 1;
                    }
                }

                csTable[i] = (byte)curr;
            }

            return csTable;
        }

        public Program(CRC8_POLY polynomial)
        {
            this.table = this.GenerateTable(polynomial);
        }

        public static void RunSnippet()
        {
            System.Text.ASCIIEncoding AsciiEncoding = new System.Text.ASCIIEncoding();
            
            byte[] Checksum = AsciiEncoding.GetBytes("Checksum");

            byte checksum;
            byte[] testVal = Checksum;
            Program crc_dallas = new Program(CRC8_POLY.CRC8_DALLAS_MAXIM);
            checksum = crc_dallas.Checksum(testVal);
            WL(checksum);
            Program crc = new Program(CRC8_POLY.CRC8_CCITT);
            checksum = crc.Checksum(testVal);
            WL(checksum);
        }

        #region Helper methods

        public static void Main()
        {
            try
            {
                RunSnippet();
            }
            catch (Exception e)
            {
                string error = string.Format
                ("---\nThe following error occurred while executing  the snippet:\n{0}\n-- - ", e.ToString());
    
            Console.WriteLine(error);
            }
            finally
            {
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void WL(object text, params object[] args)
        {
            Console.WriteLine(text.ToString(), args);
        }
        #endregion
    }
}
