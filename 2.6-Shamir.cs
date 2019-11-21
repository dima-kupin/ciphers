using System;
using System.Linq;
using Shamir_s_Secret_Sharing.Models;

namespace Shamir_s_Secret_Sharing
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const int minimum = 3;
            const int maximum = 5;

            var secret = SecretSharing.Create(minimum, maximum);

            Console.WriteLine($"Secret: {secret.Secret}");

            Console.WriteLine();
            Console.WriteLine("Shares:");
            foreach (var share in secret.Shares) Console.WriteLine($"\t{share}");

            var random = new Random();
            var randomShares = secret.Shares.OrderBy(x => random.Next()).Take(minimum).OrderBy(x => x.X).ToArray();

            Console.WriteLine();
            Console.WriteLine("Used random secrets:");
            foreach (var share in randomShares) Console.WriteLine($"\t{share}");

            var reconstructed = Secret.ReconstructFrom(randomShares);
            Console.WriteLine();
            Console.WriteLine($"Reconstructed secret: {reconstructed.Value}");

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}



using System.Collections.Generic;
using System.IO;
using Shamir_s_Secret_Sharing.Models;
using ShamirsSecretSharingScheme.Services;

namespace Shamir_s_Secret_Sharing
{
    public class SecretSharing
    {
        private SecretSharing(Secret secret, IEnumerable<Share> shares)
        {
            Secret = secret;
            Shares = shares;
        }

        public Secret Secret { get; }
        public IEnumerable<Share> Shares { get; }

        public static SecretSharing Create(int minimumShares, int allShares)
        {
            if (minimumShares > allShares)
                throw new InvalidDataException("Minimum required shares must be lower than number of generated shares");

            if (minimumShares <= 0 || allShares <= 0)
                throw new InvalidDataException("Number of shares must be higher than 0");

            var polynomial = Polynomial.Create((uint) minimumShares, Common.BasePrime);
            var secret = Secret.CreateFrom(polynomial);
            var shares = Share.GenerateMultipleFrom(polynomial, (uint) allShares);

            return new SecretSharing(secret, shares);
        }
    }
}
