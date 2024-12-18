using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CellTests
    {

        // A Test behaves as an ordinary method
        [Test]
        public void CellSetGameObjectPass()
        {
            Cell cell = new Cell();
            cell.SetConstruction(new GameObject());
            Assert.IsTrue(cell.IsTaken);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void CellSetGameObjectNullFail()
        {
            Cell cell = new Cell();
            cell.SetConstruction(null);
            Assert.IsFalse(cell.IsTaken);
        }


    }
}