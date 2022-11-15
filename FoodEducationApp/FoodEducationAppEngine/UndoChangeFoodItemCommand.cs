// <copyright file="UndoChangeFoodItemCommand.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuelGibsonFoodApp
{
    public class UndoChangeFoodItemCommand : ICommand
    {
        private List<string> properties;

        private FoodItem edited;

        public UndoChangeFoodItemCommand(List<string> original, FoodItem updated)
        {
            this.properties = original;
            this.edited = updated;
        }

        public void Execute(FoodEducationApp app)
        {
            FoodItem food = app.GetFoodItem(this.edited.FoodName);
            food.FoodName = this.properties[0];
            food.Color = int.Parse(this.properties[1]);
            food.Shape = this.properties[2];
            food.Texture = this.properties[3];
            food.Size = this.properties[4];
            food.Taste = this.properties[5];
        }
    }
}
