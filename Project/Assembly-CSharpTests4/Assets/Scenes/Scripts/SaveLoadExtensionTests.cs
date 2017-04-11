using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaveLoadExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveLoadExtension.Tests
{
    [TestClass()]
    public class SaveLoadExtensionTests
    {
        [TestMethod()]
        public void ValidateUsernameTest()
        {

            SaveLoadExtension test = new SaveLoadExtension();
            bool t1 = test.ValidateUsername("bob");
            bool t2 = test.ValidateUsername("bo123 5 235b");

            Assert.AreEqual(true, t1);
            Assert.AreNotEqual(true, t2);
        }
    }
}