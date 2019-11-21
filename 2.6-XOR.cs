using System;

namespace XORCipher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("XOR Cipher\n");
            var x = new XORCipher();
            Console.Write("Enter text: ");
            var message = Console.ReadLine();
            while (true)
            {
                Console.Write(
                    "Choose the encryption method:\n1) Repeat the keyword until the gamma is equal to the length of the message;\n2) Generate a sequence of pseudorandom numbers equal in length to the message body.\n(\"1\" or \"2\"): ");
                var answer = Console.ReadLine();
                if (answer == "1")
                {
                    Console.Write("Enter password: ");
                    var pass = Console.ReadLine();
                    var encryptedMessageByPass = x.Encrypt(message, pass);
                    Console.WriteLine("Encrypted message: {0}", encryptedMessageByPass);
                    Console.WriteLine("Decrypted message: {0}", x.Decrypt(encryptedMessageByPass, pass));
                    break;
                }

                if (answer == "2")
                {
                    Console.Write("Enter key: ");
                    var key = Convert.ToInt32(Console.ReadLine());
                    var encryptedMessageByKey = x.EncryptRandomKey(message, key);
                    Console.WriteLine("Encrypted message: {0}", encryptedMessageByKey);
                    Console.WriteLine("Decrypted message: {0}", x.DecryptRandomKey(encryptedMessageByKey, key));
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}




using System;

namespace XORCipher
{
    internal class XORCipher
    {
        //key generator
        private string GetRandomKey(int k, int len)
        {
            var gamma = string.Empty;
            var rnd = new Random(k);
            for (var i = 0; i < len; i++) gamma += ((char) rnd.Next(35, 126)).ToString();
            return gamma;
        }

        //pseudorandom number encryption/decryption method
        private string EncryptDecrypt(string text, int key)
        {
            var secretKey = GetRandomKey(key, text.Length);
            var result = string.Empty;
            for (var i = 0; i < text.Length; i++) result += ((char) (text[i] ^ secretKey[i])).ToString();
            return result;
        }

        //text encryption
        public string EncryptRandomKey(string plainText, int key)
        {
            return EncryptDecrypt(plainText, key);
        }

        //text decryption
        public string DecryptRandomKey(string encryptedText, int key)
        {
            return EncryptDecrypt(encryptedText, key);
        }

        //key replay generator
        private string GetRepeatKey(string p, int len)
        {
            var r = p;
            while (r.Length < len) r += r;
            return r.Substring(0, len);
        }

        //encryption/decryption method
        private string EncryptDecrypt(string text, string password)
        {
            var secretPassword = GetRepeatKey(password, text.Length);
            var result = string.Empty;
            for (var i = 0; i < text.Length; i++) result += ((char) (text[i] ^ secretPassword[i])).ToString();
            return result;
        }

        //text encryption
        public string Encrypt(string plainText, string password)
        {
            return EncryptDecrypt(plainText, password);
        }

        //text decryption
        public string Decrypt(string encryptedText, string password)
        {
            return EncryptDecrypt(encryptedText, password);
        }
    }
}
