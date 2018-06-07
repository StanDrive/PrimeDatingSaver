using System;
using System.Collections.Generic;
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
            var json = "{ id: \"1a\", date: \"2018-05-17 19:36:43\", someName: \"testname\", names: [ \"one\", \"two\" ] }";

            var result = JsonConvert.DeserializeObject<Daily>(json);
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
