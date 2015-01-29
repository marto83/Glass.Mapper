using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace Glass.Mapper.Tests
{
    [TestFixture]
    public class ConcurrencyFixture
    {
        [Test]
        public void ConcurrentDictionarySpeed()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ConcurrentDictionary<int, int> dictionary = new ConcurrentDictionary<int, int>();
            for (var i = 0; i < 10000; i++)
            {
                dictionary.TryAdd(i, i);
            }

            Console.WriteLine("Items added: {0}", sw.ElapsedTicks);
            sw.Restart();
            for (var i = 0; i < 10000; i++)
            {
                var j = dictionary.ContainsKey(i);
            }

            Console.WriteLine("Items contained: {0}", sw.ElapsedTicks);
            sw.Restart();

            for (var i = 0; i < 10000; i++)
            {
                var j = dictionary[i];
            }

            Console.WriteLine("Items retrieved: {0}", sw.ElapsedTicks);
        }

        [Test]
        public void LockedHashsetSpeed()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            HashSet<int> dictionary = new HashSet<int>();
            for (var i = 0; i < 10000; i++)
            {
                lock (dictionary)
                {
                    dictionary.Add(i);
                }
            }

            Console.WriteLine("Items added: {0}", sw.ElapsedTicks);
            sw.Restart();
            for (var i = 0; i < 10000; i++)
            {
                lock (dictionary)
                {
                    var j = dictionary.Contains(i);
                }
            }

            Console.WriteLine("Items contained: {0}", sw.ElapsedTicks);
            sw.Restart();

            lock (dictionary)
            {
                foreach (int i in dictionary)
                {
                    var j = i;
                }
            }

            Console.WriteLine("Items retrieved: {0}", sw.ElapsedTicks);
        }
    }
}
