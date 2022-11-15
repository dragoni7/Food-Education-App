// <copyright file="UndoDeleteFoodItemCommand.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuelGibsonFoodApp
{
    /// <summary>
    /// Class for undoing deleting food items.
    /// </summary>
    public class UndoDeleteFoodItemCommand : ICommand
    {
        /// <summary>
        /// Deleted food.
        /// </summary>
        private FoodItem deletedFood;

        /// <summary>
        /// Container food was deleted from.
        /// </summary>
        private ContainerItem foodContainer = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoDeleteFoodItemCommand"/> class.
        /// </summary>
        /// <param name="food">deleted food.</param>
        public UndoDeleteFoodItemCommand(FoodItem food)
        {
            this.deletedFood = food;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoDeleteFoodItemCommand"/> class.
        /// </summary>
        /// <param name="food">deleted food.</param>
        /// <param name="container">container deleted from.</param>
        public UndoDeleteFoodItemCommand(FoodItem food, ContainerItem container)
        {
            this.deletedFood = food;
            this.foodContainer = container;
        }

        /// <inheritdoc/>
        public void Execute(FoodEducationApp app)
        {
            if (this.foodContainer != null)
            {
                app.AddToContainer(this.foodContainer, this.deletedFood);
            }
            else
            {
                app.AddFoodItem(this.deletedFood);
            }
        }
    }
}
