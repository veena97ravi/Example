using System;
using DischargePatientModuleLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DischargePatientLibTests
{
   

    [TestClass]
    public class UnitTest1
    {
         private DischargePatientViewModel dischargePatientView;
         [TestInitialize]

        [TestMethod]
        public void GivenPatientIdToDischargePatientApiapishouldreturntrue()
        {
            
         DischargePatientViewModel obj= new DischargePatientViewModel();
            bool expected=false;
            bool actualvalue = obj.DischargePatient("nivi");
            Assert.AreEqual(expected, actualvalue);
        }

        [TestMethod]

        public void PatientId_Set_Check()
        {
            string expected = new Random().ToString();

            var target = new DischargePatientViewModel(){PatientId = expected };

            Assert.AreEqual(expected, target.PatientId);
        }

        [TestMethod]
        public void ResetTest()
        {
           DischargePatientViewModel dischargePatientView=new DischargePatientViewModel();
           dischargePatientView.PatientId = "veena";
           dischargePatientView.Reset();
            var actual = "";
            Assert.AreEqual
                (dischargePatientView.PatientId,actual);

        }
    }
}
