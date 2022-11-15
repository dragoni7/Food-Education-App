// <copyright file="UndoDeleteContainerCommand.cs" company="Samuel Gibson 011773716">
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
    /// Class for undoing deleting container.
    /// </summary>
    public class UndoDeleteContainerCommand : ICommand
    {
        /// <summary>
        /// Deleted container.
        /// </summary>
        private ContainerItem deletedContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoDeleteContainerCommand"/> class.
        /// </summary>
        /// <param name="container"> deleted container.</param>
        public UndoDeleteContainerCommand(ContainerItem container)
        {
            this.deletedContainer = container;
        }

        /// <inheritdoc/>
        public void Execute(FoodEducationApp app)
        {
            app.AddContainer(this.deletedContainer.ContainerName, this.deletedContainer.Type, this.deletedContainer.Limit);

            foreach (FoodItem food in this.deletedContainer.GetContents())
            {
                app.AddToContainer(app.GetContainer(this.deletedContainer.ContainerName), food);
            }
        }
    }
}
