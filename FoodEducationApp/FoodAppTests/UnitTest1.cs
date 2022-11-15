// <copyright file="UnitTest1.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SamuelGibsonFoodApp
{
    /// <summary>
    /// Class for testing methods.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Tests for the FoodEducationApp FoodItem methods.
        /// </summary>
        [TestMethod]
        public void FooditemManipulationTests()
        {
            FoodEducationApp engine = new FoodEducationApp(10);

            Assert.IsTrue(engine.AddFoodItem("fruit", "apple", 12800, "circle", "crunchy", "medium", "sweet"));
            Assert.IsTrue(engine.AddFoodItem("vegetable", "carrot", 12800, "circle", "crunchy", "medium", "sweet"));
            Assert.IsFalse(engine.AddFoodItem("weirdtype", "carrot", 12800, "circle", "crunchy", "medium", "sweet"));
            Assert.AreEqual(engine.GetFoodItemCount(), 2);
            Assert.IsNotNull(engine.GetFoodItem("apple"));

            Assert.AreEqual(engine.GetFoodType(engine.GetFoodItem("apple")), "fruit");
            Assert.AreEqual(engine.GetFoodType(engine.GetFoodItem("carrot")), "vegetable");

            engine.GetFoodItem("carrot").Shape = "triangle";
            Assert.IsNotNull(engine.GetFoodItem("carrot"));
            Assert.AreEqual(engine.GetFoodItem("carrot").Shape, "triangle");

            Assert.IsTrue(engine.RemoveFoodItem(engine.GetFoodItem("apple")));

            Assert.IsFalse(engine.AddFoodItem("fruit", string.Empty, 12800, "circle", "crunchy", "medium", "sweet"));
            Assert.IsFalse(engine.AddFoodItem("fruit", "apple", 12800, string.Empty, "crunchy", "medium", "sweet"));
            Assert.IsFalse(engine.AddFoodItem("fruit", "apple", -1, "circle", "crunchy", "medium", "sweet"));

            Assert.IsNull(engine.GetFoodItem("test"));
        }

        /// <summary>
        /// Tests for FoodEducationApp ContainerItem methods.
        /// </summary>
        [TestMethod]
        public void ContainerItemManipulationTests()
        {
            FoodEducationApp engine = new FoodEducationApp(10);
            Assert.IsTrue(engine.AddContainer("c1", "fruit", 6));
            Assert.IsFalse(engine.AddContainer("c2", "fruit", 11));
            Assert.IsFalse(engine.AddContainer("c3", "fruit", -1));
            Assert.IsTrue(engine.AddContainer("c4", "vegetable", 10));

            Assert.AreEqual(engine.GetContainerCount(), 2);

            Assert.IsNotNull(engine.GetContainer("c1"));
            Assert.IsNull(engine.GetContainer("c2"));

            engine.AddFoodItem("fruit", "apple", 12800, "circle", "crunchy", "medium", "sweet");
            engine.AddFoodItem("vegetable", "carrot", 12800, "circle", "crunchy", "medium", "sweet");
            Assert.IsTrue(engine.AddToContainer(engine.GetContainer("c1"), engine.GetFoodItem("apple")));
            Assert.IsTrue(engine.AddToContainer(engine.GetContainer("c4"), engine.GetFoodItem("carrot")));
            Assert.IsFalse(engine.AddToContainer(engine.GetContainer("c1"), engine.GetFoodItem("carrot")));
            Assert.IsFalse(engine.AddToContainer(engine.GetContainer("c2"), engine.GetFoodItem("apple")));
            Assert.IsFalse(engine.AddToContainer(engine.GetContainer("c1"), engine.GetFoodItem("test")));

            List<string> filter1 = new List<string>(10);
            filter1.Add(string.Empty);
            filter1.Add(string.Empty);
            filter1.Add(string.Empty);
            filter1.Add(string.Empty);
            filter1.Add("apple");
            filter1.Add(string.Empty);
            filter1.Add(string.Empty);
            filter1.Add(string.Empty);
            filter1.Add(string.Empty);
            filter1.Add(string.Empty);

            Assert.IsTrue(engine.FilterContainers(filter1));
            engine.GetFilterContainersResult().Contains(engine.GetContainer("c1"));

            List<string> filter2 = new List<string>(7);
            filter2.Add(string.Empty);
            filter2.Add("apple");
            filter2.Add(string.Empty);
            filter2.Add(string.Empty);
            filter2.Add(string.Empty);
            filter2.Add(string.Empty);
            filter2.Add(string.Empty);

            List<string> filter3 = new List<string>(7);
            filter3.Add(string.Empty);
            filter3.Add("test");
            filter3.Add(string.Empty);
            filter3.Add(string.Empty);
            filter3.Add(string.Empty);
            filter3.Add(string.Empty);
            filter3.Add(string.Empty);

            List<string> filter4 = new List<string>(7);
            filter4.Add("fruit");
            filter4.Add(string.Empty);
            filter4.Add(string.Empty);
            filter4.Add(string.Empty);
            filter4.Add(string.Empty);
            filter4.Add(string.Empty);
            filter4.Add(string.Empty);

            Assert.IsTrue(engine.FilterFoodItems(filter2));
            Assert.IsFalse(engine.FilterFoodItems(filter3));
            Assert.IsTrue(engine.FilterFoodItems(filter4));

            Assert.IsTrue(engine.GetFilterFoodItemsResult().Count > 0);
            Assert.AreEqual(engine.GetFilterFoodItemsResult()[0].FoodName, "apple");

            Assert.IsTrue(engine.RemoveContainer(engine.GetContainer("c1")));
            Assert.IsFalse(engine.RemoveContainer(engine.GetContainer("c2")));
        }

        /// <summary>
        /// Tests for undo commands.
        /// </summary>
        [TestMethod]
        public void TestUndos()
        {
            FoodEducationApp engine = new FoodEducationApp(10);
            engine.AddFoodItem("fruit", "apple", 12800, "circle", "crunchy", "medium", "sweet");
            engine.AddFoodItem("fruit", "apple2", 12800, "circle", "crunchy", "medium", "sweet");

            UndoCreateFoodItemCommand u1 = new UndoCreateFoodItemCommand(engine.GetFoodItem("apple"));
            u1.Execute(engine);

            Assert.IsNull(engine.GetFoodItem("apple"));
            Assert.IsNotNull(engine.GetFoodItem("apple2"));

            UndoDeleteFoodItemCommand u2 = new UndoDeleteFoodItemCommand(engine.GetFoodItem("apple2"));
            engine.RemoveFoodItem(engine.GetFoodItem("apple2"));
            Assert.IsNull(engine.GetFoodItem("apple2"));
            u2.Execute(engine);

            Assert.IsNotNull(engine.GetFoodItem("apple2"));

            engine.AddContainer("c1", "fruit", 5);

            UndoAddFoodItemToContainerCommand u3 = new UndoAddFoodItemToContainerCommand(engine.GetFoodItem("apple2"), engine.GetContainer("c1"));
            engine.AddToContainer(engine.GetContainer("c1"), engine.GetFoodItem("apple2"));
            Assert.IsNull(engine.GetFoodItem("apple2"));
            u3.Execute(engine);
            Assert.IsNotNull(engine.GetFoodItem("apple2"));
            Assert.AreEqual(engine.GetContainerContents(engine.GetContainer("c1")).Count, 0);
        }
    }
}
