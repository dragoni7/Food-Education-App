// <copyright file="ICommand.cs" company="Samuel Gibson 011773716">
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
    /// Interface for creating commands.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command in the app.
        /// </summary>
        /// <param name="app">the active app.</param>
        void Execute(FoodEducationApp app);
    }
}
