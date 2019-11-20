using System;
using System.IO;
using System.Security.Cryptography;
using System.Linq;

namespace 25
{
    class DES
    {
        public static void Main()
        {
            Console.WriteLine("Ââåäèòå òåêñò: ");
            string data = Console.ReadLine();
            Apply3DES(data);
            Console.ReadLine();
        }
        static void Apply3DES(string raw)
        {
            try
            {
                using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
                {
                    byte[] encrypted = Encrypt(raw, tdes.Key, tdes.IV);
                    Console.WriteLine("Key: " + System.Text.Encoding.UTF8.GetString(tdes.Key));
                    Console.WriteLine("IV " + System.Text.Encoding.UTF8.GetString(tdes.IV));
                    Console.WriteLine("Áëîêè: ");
                    blocks(System.Text.Encoding.UTF8.GetString(encrypted));
                    Console.WriteLine( "Çàøèôðîâàíî: " + System.Text.Encoding.UTF8.GetString(encrypted));
                    string decrypted = Decrypt(encrypted, tdes.Key, tdes.IV);
                    Console.WriteLine( "Ðàñøèôðîâàíî: " + decrypted);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            Console.ReadKey();
        }
        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            { 
                ICryptoTransform encryptor = tdes.CreateEncryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }
        public static void blocks(string s) {
           
            var split = s.Select((c, index) => new { c, index })
                .GroupBy(x => x.index / 3)
                .Select(group => group.Select(elem => elem.c))
                .Select(chars => new string(chars.ToArray()));
foreach (var str in split)
                Console.WriteLine(str + "");
        }
        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null; 
            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                ICryptoTransform decryptor = tdes.CreateDecryptor(Key, IV); 
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        private static void EncryptFile(String inName, String outName, byte[] desKey, byte[] desIV)
        {
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);
            byte[] bin = new byte[100];
            long rdlen = 0; 
            long totlen = fin.Length;   
            int len;  
            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);
            Console.WriteLine("Encrypting..."); 
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
                Console.WriteLine("{0} bytes processed", rdlen);
            }
            encStream.Close();
            fout.Close();
            fin.Close();
        }
    }
}
