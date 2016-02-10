using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicList.Tests
{
    using CustomLinkedList;

    [TestClass]
    public class DynamicListTests
    {
        [TestMethod]
        public void Add_ShouldIncrementCount()
        {
            var list = new DynamicList<int>();
            list.Add(2);
            list.Add(3);

            var expected = 2;
            var actual = list.Count;

            Assert.AreEqual(expected, actual, "Incorrect count");
        }

        [TestMethod]
        [ExpectedException(typeof (FormatException))]
        public void Add_IncorrectType_ShouldThrow()
        {
            var list = new DynamicList<double>();
            string incorrectType = "try to add this";
            list.Add(double.Parse(incorrectType));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveAt_InvalidIndex_ShoudThrow()
        {
            var list = new DynamicList<int>();
            list.Add(5);

            list.RemoveAt(-1);
        }

        [TestMethod]
        public void RemoveAt_Remove_ShouldDecrementCount()
        {
            var list = new DynamicList<int>();
            list.Add(5);
            list.Add(6);
            list.Add(7);

            list.RemoveAt(1);
            var epected = 2;
            var actual = list.Count;

            Assert.AreEqual(epected, actual);
        }

        [TestMethod]
        public void Remove_RemoveItem_ShouldDecrementCount()
        {
            var list = new DynamicList<int>();
            list.Add(5);
            list.Add(6);
            list.Add(7);

            list.Remove(6);
            var expected = 2;
            var actual = list.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Remove_NotFound_ShloudReturnNegativeOne()
        {
            var list = new DynamicList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            
            var expected = -1;
            var actual = list.Remove(34);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Remove_RemoveItem_ShloudRemoveSelectedItem()
        {
            var list = new DynamicList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            list.Remove(2);

            var expected = -1;
            var actual = list.IndexOf(2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IndexOf_GetIndexOf_ShouldReturnIndexOfSelectedItem()
        {
            var list = new DynamicList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            var expected = 0;
            var actual = list.IndexOf(1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IndexOf_NotFound_ShouldReturnNegativeOne()
        {
            var list = new DynamicList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            var expected = -1;
            var actual = list.IndexOf(6);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Contains_CheckForExistingElement_ShouldReturnTrue()
        {
            var list = new DynamicList<char>();
            list.Add('s');
            list.Add('j');
            list.Add('m');

            bool expected = true;
            bool actual = list.Contains('j');

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Contains_CheckForNonExistingElement_ShouldReturnFalse()
        {
            var list = new DynamicList<string>();
            list.Add("Test");
            list.Add("All");
            list.Add("Methods!");

            bool expected = false;
            bool actual = list.Contains("What?");

            Assert.AreEqual(expected, actual);
        }
    }
}
