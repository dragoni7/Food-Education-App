// <copyright file="UndoCollection.cs" company="Samuel Gibson 011773716">
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
    /// Class for collection of undos.
    /// </summary>
    public class UndoCollection
    {
        /// <summary>
        /// collection title.
        /// </summary>
        private string title;

        /// <summary>
        /// The collection's commands.
        /// </summary>
        private List<ICommand> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="UndoCollection"/> class.
        /// </summary>
        /// <param name="cmds">command list.</param>
        /// <param name="newTitle">title.</param>
        public UndoCollection(List<ICommand> cmds, string newTitle)
        {
            this.title = newTitle;
            this.commands = cmds;
        }

        /// <summary>
        /// Gets or sets the collection title.
        /// </summary>
        /// <value>
        /// <string> The collection title.</string>
        /// </value>
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="app">app to execute in.</param>
        public void Execute(FoodEducationApp app)
        {
            foreach (ICommand cmd in this.commands)
            {
                cmd.Execute(app);
            }
        }
    }
}
