using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class SaveInfoTests
    {
        SaveInfo test = new SaveInfo("test1");
        [TestMethod()]
        public void AuthPasswordTest()
        {
            test.SetPassword("123four");

            bool t1 = test.AuthPassword("123four");
            bool t2 = test.AuthPassword("123five");
            bool t3 = test.AuthPassword("123FOUR");

            Assert.IsTrue(t1);
            Assert.IsFalse(t2);
            Assert.IsFalse(t3);
        }

        [TestMethod()]
        public void SetPasswordTest()
        {
            test.SetPassword("123four");
            bool t1 = test.AuthPassword("123four");

            Assert.IsTrue(t1);
        }

        
    }
}