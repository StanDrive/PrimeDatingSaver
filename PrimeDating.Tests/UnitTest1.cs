using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PrimeDating.BusinessLayer;
using PrimeDating.DataAccess;
using PrimeDating.DataAccess.Interfaces;
using PrimeDating.Reports;

namespace PrimeDating.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var reportService = new ReportsData();

            var a = reportService.GetAdminAreasStatistics();
        }

    }
}
