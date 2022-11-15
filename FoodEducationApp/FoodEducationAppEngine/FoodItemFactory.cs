// <copyright file="FoodItemFactory.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SamuelGibsonFoodApp
{
    /// <summary>
    /// Class for creating food items.
    /// </summary>
    internal class FoodItemFactory
    {
        /// <summary>
        /// Dictionary of all current food types.
        /// </summary>
        private readonly Dictionary<string, Type> foodTypes = new Dictionary<string, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodItemFactory"/> class.
        /// </summary>
        public FoodItemFactory()
        {
            this.TraverseAvailableFoodTypes((foodType, type) => this.foodTypes.Add(foodType, type));
        }

        /// <summary>
        /// Delegate for creating functions with a foodtype and type.
        /// </summary>
        /// <param name="foodType"> the food type.</param>
        /// <param name="type"> the type.</param>
        private delegate void OnFoodType(string foodType, Type type);

        /// <summary>
        /// Gets the food type dictionary.
        /// </summary>
        /// <value>
        /// <Dictionary<string, Type>>The food type dictionary.</Dictionary<string, Type>>
        /// </value>
        public Dictionary<string, Type> FoodTypes
        {
            get
            {
                return this.foodTypes;
            }
        }

        /// <summary>
        /// Creates a food item.
        /// </summary>
        /// <param name="foodType"> the food type.</param>
        /// <returns> a new food item.</returns>
        public FoodItem CreateFoodItem(string foodType)
        {
            if (this.foodTypes.ContainsKey(foodType))
            {
                object foodItemObject = System.Activator.CreateInstance(this.foodTypes[foodType]);
                if (foodItemObject is FoodItem)
                {
                    return (FoodItem)foodItemObject;
                }
            }

            return null;
        }

        /// <summary>
        /// Traverses available subclasses of food item and gathers types.
        /// </summary>
        /// <param name="onFoodType">function to run.</param>
        private void TraverseAvailableFoodTypes(OnFoodType onFoodType)
        {
            Type foodItemType = typeof(FoodItem);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> foodTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(foodItemType));

                foreach (var type in foodTypes)
                {
                    PropertyInfo foodTypeField = type.GetProperty("Type");

                    if (foodTypeField != null)
                    {
                        object value = foodTypeField.GetValue(type);

                        if (value is string)
                        {
                            string foodType = (string)value;

                            onFoodType(foodType, type);
                        }
                    }
                }
            }
        }
    }
}
