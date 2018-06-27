using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace PrimeDating.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var date = "2019";

            if (DateTime.TryParse(date, out var result))
            {
                
            }

            if (int.TryParse(date, out var year))
            {
                if (year >= 1900 && year <= DateTime.Now.Year)
                {
                    
                }
            }
        }

    }

    public class Daily
    {
        public int Id { get; set; }

        public int SomeName { get; set; }

        public List<int> Names { get; set; }

        public DateTime date { get; set; }
    }
}
