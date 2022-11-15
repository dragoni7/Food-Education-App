// <copyright file="UndoRemoveFoodItemFromContainerCommand.cs" company="Samuel Gibson 011773716">
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
    /// Class for undoing removing food from container.
    /// </summary>
    public class UndoRemoveFoodItemFromContainerCommand : ICommand
    {
        /// <summary>
        /// Removed food.
        /// </summary>
        private FoodItem removedFood;

        /// <summary>
        /// Container removed from.
        /// </summary>
        private ContainerItem foodContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoRemoveFoodItemFromContainerCommand"/> class.
        /// </summary>
        /// <param name="food">deleted food.</param>
        /// <param name="container">container deleted from.</param>
        public UndoRemoveFoodItemFromContainerCommand(FoodItem food, ContainerItem container)
        {
            this.removedFood = food;
            this.foodContainer = container;
        }

        /// <inheritdoc/>
        public void Execute(FoodEducationApp app)
        {
            app.AddToContainer(this.foodContainer, this.removedFood);
        }
    }
}
