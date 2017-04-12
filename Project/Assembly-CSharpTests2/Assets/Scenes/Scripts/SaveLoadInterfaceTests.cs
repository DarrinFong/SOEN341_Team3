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
            bool nullPassword = test.CheckPasswords(null, null);
            bool tooShort = test.CheckPasswords("foo", "foo");
            bool hasSpace = test.CheckPasswords("foo bar", "foo bar");
            bool passwordMismatch = test.CheckPasswords("foobar", "foobar1");
            bool valid = test.CheckPasswords("foobar123", "foobar123");

            Assert.IsFalse(nullPassword);
            Assert.IsFalse(tooShort);
            Assert.IsFalse(hasSpace);
            Assert.IsFalse(passwordMismatch);
            Assert.IsTrue(valid);
        }
    }
}