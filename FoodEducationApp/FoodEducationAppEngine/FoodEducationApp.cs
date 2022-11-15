// <copyright file="FoodEducationApp.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SamuelGibsonFoodApp
{
    /// <summary>
    /// Handles the logic behind creating, editing, and filtering food items and containers.
    /// </summary>
    public class FoodEducationApp
    {
        /// <summary>
        /// The capacity of the lists.
        /// </summary>
        private readonly int listCapacity;

        /// <summary>
        /// Stores created food items.
        /// </summary>
        private readonly List<FoodItem> foodItems;

        /// <summary>
        /// Stores created containers.
        /// </summary>
        private readonly List<ContainerItem> containers;

        /// <summary>
        /// Stores result of filtered food items.
        /// </summary>
        private List<FoodItem> filteredFoodItems;

        /// <summary>
        /// Stores result of filtered containers.
        /// </summary>
        private List<ContainerItem> filteredContainers;

        /// <summary>
        /// Factory for creating food items.
        /// </summary>
        private FoodItemFactory factory;

        /// <summary>
        /// The undo handler for the app.
        /// </summary>
        private UndoHandler undos = new UndoHandler();

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodEducationApp"/> class.
        /// </summary>
        /// <param name="capacity"> amount of items for the lists.</param>
        public FoodEducationApp(int capacity)
        {
            this.listCapacity = capacity;
            this.foodItems = new List<FoodItem>(this.listCapacity);
            this.containers = new List<ContainerItem>(this.listCapacity);
            this.filteredFoodItems = new List<FoodItem>(this.listCapacity);
            this.filteredContainers = new List<ContainerItem>(this.listCapacity);
            this.factory = new FoodItemFactory();
        }

        /// <summary>
        /// Event for gathering item changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets the app's undos handler.
        /// </summary>
        /// <value>
        /// <UndoHandler>The app's undos handler.</UndoHandler>
        /// </value>
        public UndoHandler Undos
        {
            get { return this.undos; }
        }

        /// <summary>
        /// Adds a new undo command collection to the app's undo handler.
        /// </summary>
        /// <param name="commands"> the command collection.</param>
        /// <param name="title"> the collection title.</param>
        public void AddUndo(List<ICommand> commands, string title)
        {
            this.undos.AddUndo(new UndoCollection(commands, title));
        }

        /// <summary>
        /// Adds a new food item to the list.
        /// </summary>
        /// <param name="foodType"> the type.</param>
        /// <param name="name"> the name.</param>
        /// <param name="color"> the color.</param>
        /// <param name="shape"> the shape.</param>
        /// <param name="texture"> the texture.</param>
        /// <param name="size"> the size.</param>
        /// <param name="taste"> the taste.</param>
        /// <returns> bool value indicating if a food item was successful added.</returns>
        public bool AddFoodItem(string foodType, string name, int color, string shape, string texture, string size, string taste)
        {
            // all parameters must not be empty.
            if (foodType != string.Empty && name != string.Empty && color != -1 && shape != string.Empty
                && texture != string.Empty && size != string.Empty && taste != string.Empty)
            {
                FoodItem item = this.factory.CreateFoodItem(foodType);

                if (item != null)
                {
                    item.FoodName = name;
                    item.Color = color;
                    item.Shape = shape;
                    item.Texture = texture;
                    item.Size = size;
                    item.Taste = taste;
                    item.PropertyChanged += this.FoodItemPropertyChanged;
                    this.foodItems.Add(item);
                    this.PropertyChanged(item, new PropertyChangedEventArgs("foodItems"));

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds an existing food item to the list.
        /// </summary>
        /// <param name="food"> the food item.</param>
        /// <returns> bool success value.</returns>
        public bool AddFoodItem(FoodItem food)
        {
            try
            {
                this.foodItems.Add(food);
            }
            catch (Exception)
            {
                return false;
            }

            this.PropertyChanged(food, new PropertyChangedEventArgs("foodItems"));
            return true;
        }

        /// <summary>
        /// Adds a new container to the list.
        /// </summary>
        /// <param name="name"> the name.</param>
        /// <param name="type"> the type.</param>
        /// <param name="size"> the size.</param>
        /// <returns> bool success value.</returns>
        public bool AddContainer(string name, string type, int size)
        {
            // All parameters must not be empty.
            if (name != string.Empty && type != string.Empty && size > 0 && size <= 10)
            {
                ContainerItem item = new ContainerItem(type, name, size);
                item.PropertyChanged += this.ContainerItemPropertyChanged;
                this.containers.Add(item);
                this.PropertyChanged(item, new PropertyChangedEventArgs("containers"));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the current food item types from FoodItem subclasses.
        /// </summary>
        /// <returns> array of types.</returns>
        public string[] GetTypes()
        {
            return this.factory.FoodTypes.Keys.ToArray();
        }

        /// <summary>
        /// Gets the food type of specific food item.
        /// </summary>
        /// <param name="item"> target food item.</param>
        /// <returns> the type.</returns>
        public string GetFoodType(FoodItem item)
        {
            if (item != null)
            {
                if (this.factory.FoodTypes.ContainsValue(item.GetType()))
                {
                    Type type = item.GetType();

                    PropertyInfo foodTypeField = type.GetProperty("Type");

                    if (foodTypeField != null)
                    {
                        object value = foodTypeField.GetValue(type);

                        if (value is string)
                        {
                            return (string)value;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the amount of items in foodItems.
        /// </summary>
        /// <returns> amount of food items.</returns>
        public int GetFoodItemCount()
        {
            return this.foodItems.Count();
        }

        /// <summary>
        /// Gets a food item from an index.
        /// </summary>
        /// <param name="index"> index of the food item.</param>
        /// <returns> the food item.</returns>
        public FoodItem GetFoodItem(int index)
        {
            return this.foodItems[index];
        }

        /// <summary>
        /// Gets a food item from it's name.
        /// </summary>
        /// <param name="name"> name of the food item.</param>
        /// <returns> the food item.</returns>
        public FoodItem GetFoodItem(string name)
        {
            foreach (FoodItem item in this.foodItems)
            {
                if (item.FoodName == name)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the amount of containers in the container list.
        /// </summary>
        /// <returns> the amount of containers.</returns>
        public int GetContainerCount()
        {
            return this.containers.Count();
        }

        /// <summary>
        /// Gets a container from an index.
        /// </summary>
        /// <param name="index"> container index.</param>
        /// <returns>the container.</returns>
        public ContainerItem GetContainer(int index)
        {
            return this.containers[index];
        }

        /// <summary>
        /// Gets a container from it's name.
        /// </summary>
        /// <param name="name"> container's name.</param>
        /// <returns> the container.</returns>
        public ContainerItem GetContainer(string name)
        {
            if (name != null && name != string.Empty)
            {
                foreach (ContainerItem container in this.containers)
                {
                    if (container.ContainerName == name)
                    {
                        return container;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the index of the container.
        /// </summary>
        /// <param name="container"> the container.</param>
        /// <returns> the container's index.</returns>
        public int GetContainerIndex(ContainerItem container)
        {
            return this.containers.IndexOf(container);
        }

        /// <summary>
        /// Gets the index of the food item.
        /// </summary>
        /// <param name="item"> the food item.</param>
        /// <returns> the food item's index.</returns>
        public int GetFoodIndex(FoodItem item)
        {
            return this.foodItems.IndexOf(item);
        }

        /// <summary>
        /// Gets the contents of a container.
        /// </summary>
        /// <param name="container"> the container.</param>
        /// <returns>list of food item contents.</returns>
        public List<FoodItem> GetContainerContents(ContainerItem container)
        {
            return container.GetContents();
        }

        /// <summary>
        /// Adds a food item to a container.
        /// </summary>
        /// <param name="container"> the container to add to.</param>
        /// <param name="item"> the food item to add.</param>
        /// <returns>bool success value.</returns>
        public bool AddToContainer(ContainerItem container, FoodItem item)
        {
            if (container != null && item != null)
            {
                // the food must have the same type as the container.
                if (this.GetFoodType(item) == container.Type)
                {
                    container.AddItem(item);
                    this.RemoveFoodItem(item);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes a food item from a container.
        /// </summary>
        /// <param name="container">container.</param>
        /// <param name="item">food item to remove.</param>
        /// <returns>bool success value.</returns>
        public bool RemoveFromContainer(ContainerItem container, FoodItem item)
        {
            if (container != null && item != null)
            {
                container.RemoveItem(item);
                this.AddFoodItem(item);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes a food item from the list.
        /// </summary>
        /// <param name="food"> food item to remove.</param>
        /// <returns>bool success value.</returns>
        public bool RemoveFoodItem(FoodItem food)
        {
            int i = this.GetFoodIndex(food) + 1;

            if (this.foodItems.Remove(food))
            {
                this.PropertyChanged(i, new PropertyChangedEventArgs("foodItems"));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes a container from the list.
        /// </summary>
        /// <param name="container"> the container to remove.</param>
        /// <returns>bool success value.</returns>
        public bool RemoveContainer(ContainerItem container)
        {
            int i = this.GetContainerIndex(container) + 1;

            if (this.containers.Remove(container))
            {
                this.PropertyChanged(i, new PropertyChangedEventArgs("containers"));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Filters the foods in the containers in container list.
        /// </summary>
        /// <param name="filters"> the parameters to filter by.</param>
        /// <returns>bool success value.</returns>
        public bool FilterFoodItems(List<string> filters)
        {
            this.filteredFoodItems.Clear();
            this.filteredFoodItems = new List<FoodItem>(this.listCapacity);

            List<FoodItem> temp = new List<FoodItem>(this.listCapacity);

            bool filtered = false;
            int color = -1;

            foreach (ContainerItem container in this.containers)
            {
                List<FoodItem> contents = container.GetContents();

                if (filters[0] != string.Empty)
                {
                    temp.AddRange(this.GetFoodByType(contents, filters[0]));
                    filtered = true;
                }

                if (filters[1] != string.Empty)
                {
                    temp.AddRange(this.GetFoodByName(contents, filters[1]));
                    filtered = true;
                }

                if (filters[2] != "-16777216" && filters[2] != string.Empty && int.TryParse(filters[2], out color) && color != -1)
                {
                    temp.AddRange(this.GetFoodByColor(contents, color));
                    filtered = true;
                }

                if (filters[3] != string.Empty)
                {
                    temp.AddRange(this.GetFoodByShape(contents, filters[3]));
                    filtered = true;
                }

                if (filters[4] != string.Empty)
                {
                    temp.AddRange(this.GetFoodByTexture(contents, filters[4]));
                    filtered = true;
                }

                if (filters[5] != string.Empty)
                {
                    temp.AddRange(this.GetFoodBySize(contents, filters[5]));
                    filtered = true;
                }

                if (filters[6] != string.Empty)
                {
                    temp.AddRange(this.GetFoodByTaste(contents, filters[6]));
                    filtered = true;
                }
            }

            if (filtered)
            {
                this.filteredFoodItems = temp;

                if (temp.Count() == 0)
                {
                    filtered = false;
                }
            }

            return filtered;
        }

        /// <summary>
        /// Filters the container list.
        /// </summary>
        /// <param name="filters">the parameters to filter by.</param>
        /// <returns>bool success value.</returns>
        public bool FilterContainers(List<string> filters)
        {
            this.filteredContainers.Clear();
            this.filteredContainers = new List<ContainerItem>(this.listCapacity);

            IEnumerable<ContainerItem> containerQuery;
            List<ContainerItem> temp = this.containers;
            bool filtered = false;

            int moreThan = -1;
            int lessThan = -1;
            bool compared = false;
            int color = -1;

            if (filters[0] != string.Empty)
            {
                containerQuery = from container in temp where container.ContainerName.Equals(filters[0]) select container;
                temp = containerQuery.ToList();
                filtered = true;
            }

            if (filters[1] != string.Empty)
            {
                containerQuery = from container in temp where container.Type.Equals(filters[1]) select container;
                temp = containerQuery.ToList();
                filtered = true;
            }

            if (filters[2] != string.Empty && int.TryParse(filters[2], out moreThan))
            {
                if (filters[3] != string.Empty && int.TryParse(filters[3], out lessThan))
                {
                    containerQuery = from container in temp where container.Limit > moreThan && container.GetContents().Count < lessThan select container;
                    temp = containerQuery.ToList();
                    compared = true;
                    filtered = true;
                }
                else
                {
                    containerQuery = from container in temp where container.GetContents().Count > moreThan select container;
                    temp = containerQuery.ToList();
                    filtered = true;
                }
            }

            if (!compared && filters[3] != string.Empty && int.TryParse(filters[3], out lessThan))
            {
                containerQuery = from container in temp where container.Limit < lessThan select container;
                temp = containerQuery.ToList();
                filtered = true;
            }

            // Container Contents Filters
            if (filters[4] != string.Empty)
            {
                containerQuery = from container in temp where this.GetFoodByName(container.GetContents(), filters[4]).Count > 0 select container;
                temp = containerQuery.ToList();
                filtered = true;
            }

            if (filters[5] != "-16777216" && filters[5] != string.Empty && int.TryParse(filters[5], out color) && color != -1)
            {
                containerQuery = from container in temp where this.GetFoodByColor(container.GetContents(), color).Count > 0 select container;
                temp = containerQuery.ToList();
                filtered = true;
            }

            if (filters[6] != string.Empty)
            {
                containerQuery = from container in temp where this.GetFoodByShape(container.GetContents(), filters[6]).Count > 0 select container;
                temp = containerQuery.ToList();
                filtered = true;
            }

            if (filters[7] != string.Empty)
            {
                containerQuery = from container in temp where this.GetFoodByTexture(container.GetContents(), filters[7]).Count > 0 select container;
                temp = containerQuery.ToList();
                filtered = true;
            }

            if (filters[8] != string.Empty)
            {
                containerQuery = from container in temp where this.GetFoodBySize(container.GetContents(), filters[8]).Count > 0 select container;
                temp = containerQuery.ToList();
                filtered = true;
            }

            if (filters[9] != string.Empty)
            {
                containerQuery = from container in temp where this.GetFoodByTaste(container.GetContents(), filters[9]).Count > 0 select container;
                temp = containerQuery.ToList();
                filtered = true;
            }

            if (filtered)
            {
                this.filteredContainers = temp;
            }

            return filtered;
        }

        /// <summary>
        /// Gets the list of filtered containers.
        /// </summary>
        /// <returns> list of filtered containers.</returns>
        public List<ContainerItem> GetFilterContainersResult()
        {
            return this.filteredContainers;
        }

        /// <summary>
        /// Gets the list of filtered food items.
        /// </summary>
        /// <returns> the list of filtered food items.</returns>
        public List<FoodItem> GetFilterFoodItemsResult()
        {
            return this.filteredFoodItems;
        }

        /// <summary>
        /// Handles changes to food items.
        /// </summary>
        /// <param name="sender"> the food item.</param>
        /// <param name="e"> property changed. </param>
        public void FoodItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            FoodItem item = sender as FoodItem;

            if (e.PropertyName.Equals("FoodName"))
            {
                this.PropertyChanged(item, new PropertyChangedEventArgs("FoodName"));
            }
            else if (e.PropertyName.Equals("Color"))
            {
                this.PropertyChanged(item, new PropertyChangedEventArgs("Color"));
            }
            else if (e.PropertyName.Equals("Shape"))
            {
                this.PropertyChanged(item, new PropertyChangedEventArgs("Shape"));
            }
            else if (e.PropertyName.Equals("Texture"))
            {
                this.PropertyChanged(item, new PropertyChangedEventArgs("Texture"));
            }
            else if (e.PropertyName.Equals("Size"))
            {
                this.PropertyChanged(item, new PropertyChangedEventArgs("Size"));
            }
            else if (e.PropertyName.Equals("Taste"))
            {
                this.PropertyChanged(item, new PropertyChangedEventArgs("Taste"));
            }
        }

        /// <summary>
        /// Handles changes to containers.
        /// </summary>
        /// <param name="sender"> container object.</param>
        /// <param name="e"> container properties.</param>
        public void ContainerItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ContainerItem container = sender as ContainerItem;

            if (e.PropertyName.Equals("contents"))
            {
                this.PropertyChanged(container, new PropertyChangedEventArgs("contents"));
            }
        }

        /// <summary>
        /// Gets a list of food items matching a type.
        /// </summary>
        /// <param name="list"> the list to search.</param>
        /// <param name="type"> the type.</param>
        /// <returns> list of found items.</returns>
        private List<FoodItem> GetFoodByType(List<FoodItem> list, string type)
        {
            IEnumerable<FoodItem> foods = null;
            foods = from foodItem in list where this.GetFoodType(foodItem).Equals(type) select foodItem;

            return foods.ToList();
        }

        /// <summary>
        /// Gets a list of food items matching a name.
        /// </summary>
        /// <param name="list">the list to search.</param>
        /// <param name="name"> the name.</param>
        /// <returns>list of found items.</returns>
        private List<FoodItem> GetFoodByName(List<FoodItem> list, string name)
        {
            IEnumerable<FoodItem> foods = null;
            foods = from foodItem in list where foodItem.FoodName.Equals(name) select foodItem;

            return foods.ToList();
        }

        /// <summary>
        /// Gets a list of items matching a color.
        /// </summary>
        /// <param name="list"> list to search.</param>
        /// <param name="color"> the color.</param>
        /// <returns>the list of found items.</returns>
        private List<FoodItem> GetFoodByColor(List<FoodItem> list, int color)
        {
            IEnumerable<FoodItem> foods = null;
            foods = from foodItem in list where foodItem.Color.Equals(color) select foodItem;

            return foods.ToList();
        }

        /// <summary>
        /// Gets a list of items matching shape.
        /// </summary>
        /// <param name="list">the list to search.</param>
        /// <param name="shape">the shape.</param>
        /// <returns>the list of found items.</returns>
        private List<FoodItem> GetFoodByShape(List<FoodItem> list, string shape)
        {
            IEnumerable<FoodItem> foods = null;
            foods = from foodItem in list where foodItem.Shape.Equals(shape) select foodItem;

            return foods.ToList();
        }

        /// <summary>
        /// Gets a list of items matching size.
        /// </summary>
        /// <param name="list"> the list to search.</param>
        /// <param name="size">the size.</param>
        /// <returns>list of found items.</returns>
        private List<FoodItem> GetFoodBySize(List<FoodItem> list, string size)
        {
            IEnumerable<FoodItem> foods = null;
            foods = from foodItem in list where foodItem.Size.Equals(size) select foodItem;

            return foods.ToList();
        }

        /// <summary>
        /// Gets a list of items matching texture.
        /// </summary>
        /// <param name="list">list to search.</param>
        /// <param name="texture">the texture.</param>
        /// <returns>list of found items.</returns>
        private List<FoodItem> GetFoodByTexture(List<FoodItem> list, string texture)
        {
            IEnumerable<FoodItem> foods = null;
            foods = from foodItem in list where foodItem.Texture.Equals(texture) select foodItem;

            return foods.ToList();
        }

        /// <summary>
        /// Gets a list of items matching taste.
        /// </summary>
        /// <param name="list">list to search.</param>
        /// <param name="taste">the taste.</param>
        /// <returns>list of found items.</returns>
        private List<FoodItem> GetFoodByTaste(List<FoodItem> list, string taste)
        {
            IEnumerable<FoodItem> foods = null;
            foods = from foodItem in list where foodItem.Taste.Equals(taste) select foodItem;

            return foods.ToList();
        }
    }
}
