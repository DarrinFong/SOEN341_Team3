using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaveLoadInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveLoadInterface.Tests
{
    [TestClass()]
    public class SaveLoadInterfaceTests
    {
        SaveLoadInterface test = new SaveLoadInterface();
        [TestMethod()]
        public void ValidateUsernameTest()
        {
            
            bool t1 = test.ValidateUsername("bob");
            bool t2 = test.ValidateUsername("bo123 5 235b");
            bool t3 = test.ValidateUsername("_!@#$");
            Assert.IsTrue(t1);
            Assert.IsFalse(t2);
            Assert.IsFalse(t3);
        }

        [TestMethod()]
        public void CheckPasswordsTest()
        {
            bool t1 = test.CheckPasswords("bob123","bob123");
            Assert.IsTrue(t1);
        }
    }
}