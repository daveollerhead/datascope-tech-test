using System;
using System.Collections.Generic;
using System.Linq;
using DatascopeTest.Models;

namespace DatascopeTest.Tests.TestHelpers
{
    public static class GameFactory
    {
        public static Game Random()
        {
            return new Game(
                Randomise.String(10),
                Randomise.String(10),
                DateTime.UtcNow,
                Randomise.Rating());
        }

        public static IEnumerable<Game> Random(int count)
        {
            return Enumerable.Range(1, count).Select(x => Random());
        }
    }

}
