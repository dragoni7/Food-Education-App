// <copyright file="UndoAddFoodItemToContainerCommand.cs" company="Samuel Gibson 011773716">
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
    /// Class for undoing add food to container.
    /// </summary>
    public class UndoAddFoodItemToContainerCommand : ICommand
    {
        /// <summary>
        /// The food that was added.
        /// </summary>
        private FoodItem addedFood;

        /// <summary>
        /// The container the food was added to.
        /// </summary>
        private ContainerItem containerAddedTo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoAddFoodItemToContainerCommand"/> class.
        /// </summary>
        /// <param name="food"> food added.</param>
        /// <param name="container">container added to.</param>
        public UndoAddFoodItemToContainerCommand(FoodItem food, ContainerItem container)
        {
            this.addedFood = food;
            this.containerAddedTo = container;
        }

        /// <inheritdoc/>
        public void Execute(FoodEducationApp app)
        {
            app.RemoveFromContainer(this.containerAddedTo, this.addedFood);
        }
    }
}
