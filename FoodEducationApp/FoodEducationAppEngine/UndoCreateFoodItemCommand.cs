// <copyright file="UndoCreateFoodItemCommand.cs" company="Samuel Gibson 011773716">
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
    /// Class for undo create food item command.
    /// </summary>
    public class UndoCreateFoodItemCommand : ICommand
    {
        /// <summary>
        /// Item that was created.
        /// </summary>
        private FoodItem createdItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoCreateFoodItemCommand"/> class.
        /// </summary>
        /// <param name="food">the target food item.</param>
        public UndoCreateFoodItemCommand(FoodItem food)
        {
            this.createdItem = food;
        }

        /// <inheritdoc/>
        public void Execute(FoodEducationApp app)
        {
            app.RemoveFoodItem(this.createdItem);
        }
    }
}
