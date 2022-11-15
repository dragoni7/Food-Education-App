// <copyright file="FoodItem.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SamuelGibsonFoodApp
{
    /// <summary>
    /// Base object for food items.
    /// </summary>
    public abstract class FoodItem : INotifyPropertyChanged
    {
        /// <summary>
        /// The name of the food item.
        /// </summary>
        private string name;

        /// <summary>
        /// The color of the food.
        /// </summary>
        private int color;

        /// <summary>
        /// The shape of the food.
        /// </summary>
        private string shape;

        /// <summary>
        /// The texture of the food.
        /// </summary>
        private string texture;

        /// <summary>
        /// The size of the food.
        /// </summary>
        private string size;

        /// <summary>
        /// The taste of the food.
        /// </summary>
        private string taste;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// <string>The name.</string>
        /// </value>
        public string FoodName
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("FoodName"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// <int>The color.</int>
        /// </value>
        public int Color
        {
            get
            {
                return this.color;
            }

            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Color"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the shape.
        /// </summary>
        /// <value>
        /// <string>The shape.</string>
        /// </value>
        public string Shape
        {
            get
            {
                return this.shape;
            }

            set
            {
                if (this.shape != value)
                {
                    this.shape = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Shape"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        /// <value>
        /// <string>The texture.</string>
        /// </value>
        public string Texture
        {
            get
            {
                return this.texture;
            }

            set
            {
                if (this.texture != value)
                {
                    this.texture = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Texture"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// <string>The size.</string>
        /// </value>
        public string Size
        {
            get
            {
                return this.size;
            }

            set
            {
                if (this.size != value)
                {
                    this.size = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Size"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the taste.
        /// </summary>
        /// <value>
        /// <string>The taste.</string>
        /// </value>
        public string Taste
        {
            get
            {
                return this.taste;
            }

            set
            {
                if (this.taste != value)
                {
                    this.taste = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Taste"));
                }
            }
        }
    }
}
