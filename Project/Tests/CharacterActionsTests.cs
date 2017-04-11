using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

namespace Tests
{
    [TestClass]
    public class CharacterActionsTests
    {
        Destination _dest = new Destination();
        CharacterActions _charAct = new CharacterActions();
        char[] DEFAULTACTIONSEQUENCE = new char[1000];

        [TestMethod]
        public void TestLevelSwitch()
        {
            //coordinates of destination are 1, 1, 1 for level 1
            GivenDestinationCoordinates(1.0f, 1.0f, 1.0f, 1);
        }
        #region GIVEN
        public void GivenDestinationCoordinates(float x, float y, float z, int level)
        {
            _dest.SetDestination(new Vector3(x, y, z), level);
        }
        public void GivenCharacterActions(char[] actionSequence, long startTime, int actionPointer = 0)
        {
            _charAct.SetActionSequence(actionSequence);
            _charAct.SetActionPointer(actionPointer);
            _charAct.SetStartTime(startTime);
            _charAct.levelDestination = _dest;
        }
        #endregion
        #region WHEN
        public void WhenCharacterReachesDestination()
        {
            _charAct.WinLevel();
        }
        #endregion
        #region THEN
        public void ThenAssertLevelSwitch(int level)
        {
            Assert.IsNotNull(GameObject.FindObjectOfType<Destination>());
            Assert.AreEqual(GameObject.FindObjectOfType<Destination>().lvl, level + 1);
        }
        #endregion
    }
}
