// <copyright file="UndoCreateContainerCommand.cs" company="Samuel Gibson 011773716">
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
    /// Class for undoing creation of container.
    /// </summary>
    public class UndoCreateContainerCommand : ICommand
    {
        /// <summary>
        /// Item that was created.
        /// </summary>
        private ContainerItem createdContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoCreateContainerCommand"/> class.
        /// </summary>
        /// <param name="container">the target container item.</param>
        public UndoCreateContainerCommand(ContainerItem container)
        {
            this.createdContainer = container;
        }

        /// <inheritdoc/>
        public void Execute(FoodEducationApp app)
        {
            app.RemoveContainer(this.createdContainer);
        }
    }
}
