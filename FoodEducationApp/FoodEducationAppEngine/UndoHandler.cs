// <copyright file="UndoHandler.cs" company="Samuel Gibson 011773716">
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
    /// Class for handling undos.
    /// </summary>
    public class UndoHandler
    {
        /// <summary>
        /// Stack of undo collections.
        /// </summary>
        private Stack<UndoCollection> undos = new Stack<UndoCollection>();

        /// <summary>
        /// Amount of Undo Collections in stack.
        /// </summary>
        /// <returns> int count. </returns>
        public int Undos()
        {
            return this.undos.Count();
        }

        /// <summary>
        /// Gets the title of the top item in the Undo Collection stack.
        /// </summary>
        /// <returns> title.</returns>
        public string GetUndoTitle()
        {
            if (this.Undos() > 0)
            {
                return this.undos.Peek().Title;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Executes the Undo Collection.
        /// </summary>
        /// <param name="app"> the active app.</param>
        public void Undo(FoodEducationApp app)
        {
            UndoCollection undoCommands = this.undos.Pop();
            undoCommands.Execute(app);
        }

        /// <summary>
        /// Clears the undo stacks.
        /// </summary>
        public void Clear()
        {
            this.undos.Clear();
        }

        /// <summary>
        /// Adds a undo collection to the Undo Collection stack.
        /// </summary>
        /// <param name="undo"> new undo collection. </param>
        internal void AddUndo(UndoCollection undo)
        {
            this.undos.Push(undo);
        }
    }
}
