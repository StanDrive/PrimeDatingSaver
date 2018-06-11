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
            var json = "0.2";

            decimal value;

            var res = decimal.TryParse(json, NumberStyles.Any, new CultureInfo("en-US"), out value);


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
