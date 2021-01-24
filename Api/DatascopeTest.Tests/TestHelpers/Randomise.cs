using System;
using System.Linq;

namespace DatascopeTest.Tests.TestHelpers
{
    public static class Randomise
    {
        private static readonly Random Rng = new Random();

        public static byte Rating()
        {
            return (byte) Rng.Next(1, 10);
        }

        public static string String(int length)
        {
            const string chars = "qwertyuiopasdfghjklzxcvbnm";

            return new string(Enumerable.Range(1, length).Select(_ => chars[Rng.Next(chars.Length)]).ToArray());
        }
    }
}