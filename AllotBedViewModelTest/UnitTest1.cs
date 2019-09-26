using System;
using System.Collections.Generic;
using System.Net.Http;
using AllotBedModuleLib;
using EnableVitalsClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VitalSigns;

namespace AllotBedViewModelTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
       
        public void Given_PatientId_And_Vitals_To_api_should_return_Success()
        {
            AllotBedViewModel allotBed = new AllotBedViewModel();
            SetVitals set = new SetVitals();
            set.PatientId = "veena";
            set.VitalsSigns = new List<VitalSign>();
            set.VitalsSigns.Add(new VitalSign { IsEnabled = true, Type = 0 });
            bool expected = true;
            HttpClient client = new HttpClient();
            var response = client.PostAsJsonAsync
                ("http://localhost:58905/api/BedConfiguration/ConfigureBed", 234).Result;
            bool actual = allotBed.AllotBed(set.PatientId, set);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]

        public void Test_EnableVitals1()
        {
            AllotBedViewModel bed = new AllotBedViewModel();
            bed.PatientId = "boo";
            VitalSign vital = new VitalSign
            {
                IsEnabled = true,
                Type = 0
            };
            bed.EnableVitals1();
            Assert.AreEqual("boo", bed.PatientId);
        }
    }
}
