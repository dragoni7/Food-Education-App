// <copyright file="ContainerItem.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuelGibsonFoodApp
{
    /// <summary>
    /// Class for container items.
    /// </summary>
    public class ContainerItem : INotifyPropertyChanged
    {
        /// <summary>
        /// the container name.
        /// </summary>
        private string name;

        /// <summary>
        /// The container type.
        /// </summary>
        private string type;

        /// <summary>
        /// The container's contents.
        /// </summary>
        private List<FoodItem> contents;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerItem"/> class.
        /// </summary>
        /// <param name="t"> container type.</param>
        /// <param name="n"> container name.</param>
        public ContainerItem(string t, string n)
        {
            this.type = t;
            this.name = n;
            this.contents = new List<FoodItem>(10);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerItem"/> class.
        /// </summary>
        /// <param name="t">container type.</param>
        /// <param name="n"> container name.</param>
        /// <param name="capacity">Capacity cannot exceed 10.</param>
        public ContainerItem(string t, string n, int capacity)
        {
            if (capacity > 10)
            {
                this.contents = new List<FoodItem>(10);
            }
            else
            {
                this.contents = new List<FoodItem>(capacity);
            }

            this.type = t;
            this.name = n;
        }

        /// <summary>
        /// Gathers property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets the container's name.
        /// </summary>
        /// <value>
        /// <string>The container's name.</string>
        /// </value>
        public string ContainerName
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the container's type.
        /// </summary>
        /// <value>
        /// <string>The container's type.</string>
        /// </value>
        public string Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>
        /// Gets the container's limit.
        /// </summary>
        /// <value>
        /// <int>The container's limit.</int>
        /// </value>
        public int Limit
        {
            get { return this.contents.Capacity; }
        }

        /// <summary>
        /// Removes an item from the container.
        /// </summary>
        /// <param name="item"> the food item to remove.</param>
        public void RemoveItem(FoodItem item)
        {
            this.contents.Remove(item);
            this.PropertyChanged(this, new PropertyChangedEventArgs("contents"));
        }

        /// <summary>
        /// Removes an item from the container.
        /// </summary>
        /// <param name="index"> index to remove from.</param>
        public void RemoveItem(int index)
        {
            this.contents.RemoveAt(index);
            this.PropertyChanged(this, new PropertyChangedEventArgs("contents"));
        }

        /// <summary>
        /// Gets the contents of the container.
        /// </summary>
        /// <returns>list of food items.</returns>
        internal List<FoodItem> GetContents()
        {
            return this.contents;
        }

        /// <summary>
        /// Adds an item to the container.
        /// </summary>
        /// <param name="item">food item to add.</param>
        internal void AddItem(FoodItem item)
        {
            this.contents.Add(item);
            this.PropertyChanged(this, new PropertyChangedEventArgs("contents"));
        }
    }
}
