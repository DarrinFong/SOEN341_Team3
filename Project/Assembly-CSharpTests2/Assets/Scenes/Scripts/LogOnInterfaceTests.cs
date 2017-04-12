using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogOnInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOnInterface.Tests
{
    [TestClass()]
    public class LogOnInterfaceTests
    {
        [TestMethod()]
        public void checkpassTest()
        {
            LogOnInterface test = new LogOnInterface();
            bool t1 = test.checkpass("soen341");
            bool t2 = test.checkpass("SOEN341");
            bool t3 = test.checkpass("Soen341");
            bool t4 = test.checkpass("wofiwefoiweh22434234f");
            Assert.IsTrue(t1);
            Assert.IsFalse(t2);
            Assert.IsFalse(t3);
            Assert.IsFalse(t4);
        }
    }
}