// <copyright file="Form1.cs" company="Samuel Gibson 011773716">
// Copyright (c) Samuel Gibson 011773716. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SamuelGibsonFoodApp
{
#pragma warning disable SA1601 // Generated code
    public partial class Form1 : Form
#pragma warning restore SA1601 // Generated code
    {
        /// <summary>
        /// Logic handler for the application.
        /// </summary>
        private FoodEducationApp app;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// On form load event.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.app = new FoodEducationApp(10);

            // Add all food types to the type combo boxes
            this.comboBoxType.Items.AddRange(this.app.GetTypes());
            this.comboBoxContainerType.Items.AddRange(this.app.GetTypes());
            this.comboBoxFilterContainerType.Items.AddRange(this.app.GetTypes());
            this.comboBoxFoodFilterType.Items.AddRange(this.app.GetTypes());

            this.buttonEditFoodItem.Enabled = false;
            this.buttonFillContainer.Enabled = false;
            this.buttonRemoveContainer.Enabled = false;
            this.buttonRemoveFood.Enabled = false;
            this.buttonRemoveFromContainer.Enabled = false;
            this.undoToolStripMenuItem.Enabled = false;

            this.app.PropertyChanged += this.AppPropertyChanged;
        }

        /// <summary>
        /// Event handler for change engine property changes.
        /// </summary>
        /// <param name="sender"> the objected changed.</param>
        /// <param name="e"> the property changed.</param>
        private void AppPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            FoodItem food = sender as FoodItem;
            ContainerItem container = sender as ContainerItem;
            int foodIndex;

            // When contents of a container item is changed.
            if (e.PropertyName.Equals("contents"))
            {
                int index = this.app.GetContainerIndex(container) + 1;
                this.treeViewContainers.Nodes[index].Nodes[2].Nodes.Clear();

                int i = 0;
                foreach (FoodItem item in this.app.GetContainerContents(container))
                {
                    this.treeViewContainers.Nodes[index].Nodes[2].Nodes.Add(item.FoodName).ForeColor = Color.FromArgb(item.Color);
                    this.treeViewContainers.Nodes[index].Nodes[2].Nodes[i].Nodes.Add(this.app.GetFoodType(item));
                    this.treeViewContainers.Nodes[index].Nodes[2].Nodes[i].Nodes.Add(item.Shape);
                    this.treeViewContainers.Nodes[index].Nodes[2].Nodes[i].Nodes.Add(item.Texture);
                    this.treeViewContainers.Nodes[index].Nodes[2].Nodes[i].Nodes.Add(item.Size);
                    this.treeViewContainers.Nodes[index].Nodes[2].Nodes[i].Nodes.Add(item.Taste);

                    i++;
                }
            }

            // If the container list is changed.
            else if (e.PropertyName.Equals("containers"))
            {
                if (sender is ContainerItem)
                {
                    int index = this.app.GetContainerCount();

                    this.treeViewContainers.Nodes.Add(container.ContainerName);
                    this.treeViewContainers.Nodes[index].Nodes.Add("Type: " + container.Type);
                    this.treeViewContainers.Nodes[index].Nodes.Add("Capacity: " + container.Limit.ToString());
                    this.treeViewContainers.Nodes[index].Nodes.Add("items: ");
                }
                else if (sender is int)
                {
                    int i = (int)sender;
                    this.treeViewContainers.Nodes[i].Remove();
                }
            }

            // If the foodItems list is changed.
            else if (e.PropertyName.Equals("foodItems"))
            {
                if (sender is FoodItem)
                {
                    int index = this.app.GetFoodItemCount();

                    this.treeViewFoodItems.Nodes.Add(food.FoodName).ForeColor = Color.FromArgb(food.Color);
                    this.treeViewFoodItems.Nodes[index].Nodes.Add(this.app.GetFoodType(food));
                    this.treeViewFoodItems.Nodes[index].Nodes.Add(food.Shape);
                    this.treeViewFoodItems.Nodes[index].Nodes.Add(food.Texture);
                    this.treeViewFoodItems.Nodes[index].Nodes.Add(food.Size);
                    this.treeViewFoodItems.Nodes[index].Nodes.Add(food.Taste);
                }
                else if (sender is int)
                {
                    int i = (int)sender;
                    this.treeViewFoodItems.Nodes[i].Remove();
                }
            }

            // When a food item's property is changed.
            else if (e.PropertyName.Equals("FoodName"))
            {
                foodIndex = this.app.GetFoodIndex(food) + 1;
                this.treeViewFoodItems.Nodes[foodIndex].Text = food.FoodName;
            }
            else if (e.PropertyName.Equals("Color"))
            {
                foodIndex = this.app.GetFoodIndex(food) + 1;
                this.treeViewFoodItems.Nodes[foodIndex].ForeColor = Color.FromArgb(food.Color);
            }
            else if (e.PropertyName.Equals("Shape"))
            {
                foodIndex = this.app.GetFoodIndex(food) + 1;
                this.treeViewFoodItems.Nodes[foodIndex].Nodes[1].Text = food.Shape;
            }
            else if (e.PropertyName.Equals("Texture"))
            {
                foodIndex = this.app.GetFoodIndex(food) + 1;
                this.treeViewFoodItems.Nodes[foodIndex].Nodes[2].Text = food.Texture;
            }
            else if (e.PropertyName.Equals("Size"))
            {
                foodIndex = this.app.GetFoodIndex(food) + 1;
                this.treeViewFoodItems.Nodes[foodIndex].Nodes[3].Text = food.Size;
            }
            else if (e.PropertyName.Equals("Taste"))
            {
                foodIndex = this.app.GetFoodIndex(food) + 1;
                this.treeViewFoodItems.Nodes[foodIndex].Nodes[4].Text = food.Taste;
            }
        }

        /// <summary>
        /// Triggers when the create food item is clicked.
        /// </summary>
        /// <param name="sender"> object.</param>
        /// <param name="e"> eventargs.</param>
        private void ButtonCreateFoodItem_Click(object sender, EventArgs e)
        {
            if (this.app.AddFoodItem(this.comboBoxType.Text, this.textBoxFoodName.Text, this.buttonFoodColor.BackColor.ToArgb(), this.comboBoxShape.Text, this.comboBoxTexture.Text, this.comboBoxSize.Text, this.comboBoxTaste.Text))
            {
                List<ICommand> undos = new List<ICommand>();
                this.textBoxFoodName.Clear();

                int index = this.app.GetFoodItemCount();
                FoodItem item = this.app.GetFoodItem(index - 1);

                undos.Add(new UndoCreateFoodItemCommand(item));

                this.app.AddUndo(undos, "Creating Food Item...");

                this.UpdateButtonStatus();
            }
        }

        /// <summary>
        /// Triggered when the color option is selected.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void ButtonFoodColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.buttonFoodColor.BackColor = colorDialog.Color;
                this.buttonFoodColor.ForeColor = colorDialog.Color;
            }
        }

        /// <summary>
        /// Handles clicking the create container button.
        /// </summary>
        /// <param name="sender"> the button.</param>
        /// <param name="e"> eventargs.</param>
        private void ButtonCreateContainer_Click(object sender, EventArgs e)
        {
            int size = -1;
            if (int.TryParse(this.textBoxContainerLimit.Text, out size) && this.app.AddContainer(this.textBoxContainerName.Text, this.comboBoxContainerType.Text, size))
            {
                List<ICommand> undos = new List<ICommand>();
                this.textBoxContainerName.Clear();
                this.textBoxContainerLimit.Clear();
                this.comboBoxContainerType.ResetText();

                int index = this.app.GetContainerCount();
                ContainerItem item = this.app.GetContainer(index - 1);

                undos.Add(new UndoCreateContainerCommand(item));

                this.app.AddUndo(undos, "Creating Container...");

                this.UpdateButtonStatus();
            }
        }

        /// <summary>
        /// Handles clicking the fill container button.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void ButtonFillContainer_Click(object sender, EventArgs e)
        {
            TreeNode selectedContainer = null;
            TreeNode selectedFood = null;
            List<ICommand> undos = new List<ICommand>();

            if (this.treeViewContainers.GetNodeCount(true) > 1 && this.treeViewContainers.SelectedNode != null && this.treeViewFoodItems.SelectedNode != null)
            {
                selectedContainer = this.treeViewContainers.SelectedNode;
                selectedFood = this.treeViewFoodItems.SelectedNode;
            }

            if (selectedContainer != null && selectedFood != null && this.app.GetContainer(selectedContainer.Text) != null)
            {
                ContainerItem container = this.app.GetContainer(selectedContainer.Text);
                FoodItem food = this.app.GetFoodItem(selectedFood.Text);
                if (this.app.AddToContainer(container, food))
                {
                    selectedFood.Remove();
                    undos.Add(new UndoAddFoodItemToContainerCommand(food, container));
                }

                this.app.AddUndo(undos, "Filling Container...");
                this.UpdateButtonStatus();
            }
        }

        /// <summary>
        /// Handles clicking the edit food item button.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void ButtonEditFoodItem_Click(object sender, EventArgs e)
        {
            if (this.treeViewFoodItems.SelectedNode != null)
            {
                if (this.app.GetFoodItem(this.treeViewFoodItems.SelectedNode.Text) != null)
                {
                    List<ICommand> undos = new List<ICommand>();
                    FoodItem target = this.app.GetFoodItem(this.treeViewFoodItems.SelectedNode.Text);
                    List<string> properties = new List<string>(7);
                    properties.Add(target.FoodName);
                    properties.Add(target.Color.ToString());
                    properties.Add(target.Shape);
                    properties.Add(target.Texture);
                    properties.Add(target.Size);
                    properties.Add(target.Taste);

                    if (this.textBoxFoodName.Text != null && this.textBoxFoodName.Text != string.Empty)
                    {
                        target.FoodName = this.textBoxFoodName.Text;
                    }

                    if (this.buttonFoodColor.BackColor.ToArgb() != -1)
                    {
                        target.Color = this.buttonFoodColor.BackColor.ToArgb();
                    }

                    if (this.comboBoxShape.Text != null && this.comboBoxShape.Text != string.Empty)
                    {
                        target.Shape = this.comboBoxShape.Text;
                    }

                    if (this.comboBoxTexture.Text != null && this.comboBoxTexture.Text != string.Empty)
                    {
                        target.Texture = this.comboBoxTexture.Text;
                    }

                    if (this.comboBoxSize.Text != null && this.comboBoxSize.Text != string.Empty)
                    {
                        target.Size = this.comboBoxSize.Text;
                    }

                    if (this.comboBoxTaste.Text != null && this.comboBoxTaste.Text != string.Empty)
                    {
                        target.Taste = this.comboBoxTaste.Text;
                    }

                    undos.Add(new UndoChangeFoodItemCommand(properties, target));
                    this.app.AddUndo(undos, "Edit Food Item...");
                }

                this.UpdateButtonStatus();
            }
        }

        /// <summary>
        /// Handles clicking the remove food from container button.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void ButtonRemoveFromContainer_Click(object sender, EventArgs e)
        {
            TreeNode selectedContainerFood = null;
            TreeNode selectedContainer = null;
            List<ICommand> undos = new List<ICommand>();

            if (this.treeViewContainers.GetNodeCount(true) > 1 && this.treeViewContainers.SelectedNode != null && this.treeViewContainers.SelectedNode.Parent != null && this.treeViewContainers.SelectedNode.Parent.Parent != null)
            {
                selectedContainerFood = this.treeViewContainers.SelectedNode;
                selectedContainer = this.treeViewContainers.SelectedNode.Parent.Parent;
            }

            string foodName;
            string containerName;
            FoodItem food = null;

            if (selectedContainer != null && selectedContainerFood != null)
            {
                foodName = selectedContainerFood.Text;
                containerName = selectedContainer.Text;

                ContainerItem container = this.app.GetContainer(containerName);

                foreach (FoodItem item in this.app.GetContainerContents(container))
                {
                    if (foodName == item.FoodName)
                    {
                        food = item;
                    }
                }

                if (food != null)
                {
                    if (container != null)
                    {
                        undos.Add(new UndoRemoveFoodItemFromContainerCommand(food, container));
                        this.app.RemoveFromContainer(container, food);
                        this.app.AddUndo(undos, "Removing Food from Container...");
                    }
                }
            }

            this.UpdateButtonStatus();
        }

        /// <summary>
        /// Handles clicking the remove food item button.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void ButtonRemoveFood_Click(object sender, EventArgs e)
        {
            TreeNode selectedContainerFood = null;
            TreeNode selectedContainer = null;
            TreeNode selectedFood = null;

            List<ICommand> undos = new List<ICommand>();

            if (this.treeViewContainers.GetNodeCount(true) > 1 && this.treeViewContainers.SelectedNode != null && this.treeViewContainers.SelectedNode.Parent != null && this.treeViewContainers.SelectedNode.Parent.Parent != null)
            {
                selectedContainerFood = this.treeViewContainers.SelectedNode;
                selectedContainer = this.treeViewContainers.SelectedNode.Parent.Parent;
            }

            if (this.treeViewFoodItems.GetNodeCount(true) > 1 && this.treeViewFoodItems.SelectedNode != null)
            {
                selectedFood = this.treeViewFoodItems.SelectedNode;
            }

            string foodName;
            string containerName;
            FoodItem food = null;

            if (selectedContainer != null && selectedContainerFood != null)
            {
                foodName = selectedContainerFood.Text;
                containerName = selectedContainer.Text;

                ContainerItem container = this.app.GetContainer(containerName);

                foreach (FoodItem item in this.app.GetContainerContents(container))
                {
                    if (foodName == item.FoodName)
                    {
                        food = item;
                    }
                }

                if (food != null)
                {
                    if (container != null)
                    {
                        undos.Add(new UndoDeleteFoodItemCommand(food, container));
                        container.RemoveItem(food);
                        this.app.AddUndo(undos, "Deleting Food from Container...");
                    }
                }
            }
            else if (selectedFood != null)
            {
                foodName = selectedFood.Text;
                food = this.app.GetFoodItem(foodName);

                if (food != null)
                {
                    undos.Add(new UndoDeleteFoodItemCommand(food));
                    this.app.RemoveFoodItem(food);
                    this.app.AddUndo(undos, "Deleting Food...");
                }
            }

            this.UpdateButtonStatus();
        }

        /// <summary>
        /// Handles clicking the remove container button.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void ButtonRemoveContainer_Click(object sender, EventArgs e)
        {
            TreeNode selectedContainer = null;
            List<ICommand> undos = new List<ICommand>();

            if (this.treeViewContainers.SelectedNode != null)
            {
                selectedContainer = this.treeViewContainers.SelectedNode;
            }

            if (this.app.GetContainer(selectedContainer.Text) != null)
            {
                undos.Add(new UndoDeleteContainerCommand(this.app.GetContainer(selectedContainer.Text)));
                this.app.RemoveContainer(this.app.GetContainer(selectedContainer.Text));
            }

            this.app.AddUndo(undos, "Deleting Container...");
            this.UpdateButtonStatus();
        }

        /// <summary>
        /// Updates the status of the buttons in the create tab.
        /// </summary>
        private void UpdateButtonStatus()
        {
            if (this.app.GetFoodItemCount() > 0)
            {
                this.buttonEditFoodItem.Enabled = true;
                this.buttonRemoveFood.Enabled = true;

                if (this.app.GetContainerCount() > 0)
                {
                    this.buttonFillContainer.Enabled = true;
                }
                else
                {
                    this.buttonFillContainer.Enabled = false;
                }
            }
            else
            {
                this.buttonEditFoodItem.Enabled = false;
            }

            if (this.app.GetContainerCount() > 0)
            {
                this.buttonRemoveContainer.Enabled = true;
                this.buttonRemoveFromContainer.Enabled = true;
                this.buttonRemoveFood.Enabled = true;
            }
            else
            {
                this.buttonRemoveContainer.Enabled = false;
                this.buttonRemoveFromContainer.Enabled = false;
            }

            if (this.app.GetFoodItemCount() > 0 || this.app.GetContainerCount() > 0)
            {
                this.buttonRemoveFood.Enabled = true;
            }
            else
            {
                this.buttonRemoveFood.Enabled = false;
            }

            this.treeViewFoodItems.SelectedNode = null;
            this.treeViewContainers.SelectedNode = null;

            // Update the Undo status
            ToolStripMenuItem group = this.menuStrip1.Items[0] as ToolStripMenuItem;
            ToolStripItem item = group.DropDownItems[0]; // Undo
            item.Enabled = this.app.Undos.Undos() > 0;
            item.Text = "Undo " + this.app.Undos.GetUndoTitle();
        }

        /// <summary>
        /// Handles clicking between tabs.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Filter Containers
            if (this.tabControl1.SelectedTab == this.tabControl1.TabPages[1])
            {
                this.treeViewFoodItems.Hide();

                this.treeViewFilteredContainers.Show();
                this.treeViewFilteredFoods.Hide();
            }

            // Filter Food Items
            if (this.tabControl1.SelectedTab == this.tabControl1.TabPages[2])
            {
                this.treeViewFoodItems.Hide();

                this.treeViewFilteredContainers.Hide();
                this.treeViewFilteredFoods.Show();
            }

            // Create
            if (this.tabControl1.SelectedTab == this.tabControl1.TabPages[0])
            {
                this.treeViewContainers.Show();
                this.treeViewFoodItems.Show();

                this.treeViewFilteredContainers.Hide();
                this.treeViewFilteredFoods.Hide();
            }
        }

        /// <summary>
        /// Handles clicking the filter containers button.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void ButtonFilterContainers_Click(object sender, EventArgs e)
        {
            // reset the filter view
            this.buttonContainerFoodColor.BackColor = Color.Empty;
            this.buttonContainerFoodColor.ForeColor = Color.Empty;
            this.treeViewFilteredContainers.Nodes.Clear();
            this.treeViewFilteredContainers.Nodes.Add("Filtered Containers");

            List<string> filters = new List<string>(10); // filter parameters

            filters.Add(this.textBoxContainerFilterName.Text);
            filters.Add(this.comboBoxFilterContainerType.Text);
            filters.Add(this.textBoxMoreThan.Text);
            filters.Add(this.textBoxLessThan.Text);

            filters.Add(this.textBoxContainerFoodName.Text);
            filters.Add(this.buttonContainerFoodColor.ForeColor.ToArgb().ToString());
            filters.Add(this.comboBoxContainerFoodShape.Text);
            filters.Add(this.comboBoxContainerFoodTexture.Text);
            filters.Add(this.comboBoxContainerFoodSize.Text);
            filters.Add(this.comboBoxContainerFoodTaste.Text);

            this.app.FilterContainers(filters);
            int j = 1;
            int k;

            foreach (ContainerItem container in this.app.GetFilterContainersResult())
            {
                this.treeViewFilteredContainers.Nodes.Add(container.ContainerName);
                this.treeViewFilteredContainers.Nodes[j].Nodes.Add("Type: " + container.Type);
                this.treeViewFilteredContainers.Nodes[j].Nodes.Add("Capacity: " + container.Limit.ToString());
                this.treeViewFilteredContainers.Nodes[j].Nodes.Add("items: ");

                k = 0;
                foreach (FoodItem food in this.app.GetContainerContents(container))
                {
                    this.treeViewFilteredContainers.Nodes[j].Nodes[2].Nodes.Add(food.FoodName).ForeColor = Color.FromArgb(food.Color);
                    this.treeViewFilteredContainers.Nodes[j].Nodes[2].Nodes[k].Nodes.Add(this.app.GetFoodType(food));
                    this.treeViewFilteredContainers.Nodes[j].Nodes[2].Nodes[k].Nodes.Add(food.Shape);
                    this.treeViewFilteredContainers.Nodes[j].Nodes[2].Nodes[k].Nodes.Add(food.Texture);
                    this.treeViewFilteredContainers.Nodes[j].Nodes[2].Nodes[k].Nodes.Add(food.Size);
                    this.treeViewFilteredContainers.Nodes[j].Nodes[2].Nodes[k].Nodes.Add(food.Taste);

                    k++;
                }

                j++;
            }
        }

        /// <summary>
        /// Handles clicking the filter containers color button.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void ButtonContainerFoodColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.buttonContainerFoodColor.BackColor = colorDialog.Color;
                this.buttonContainerFoodColor.ForeColor = colorDialog.Color;
            }
        }

        /// <summary>
        /// Handles clicking the filter food items button.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void ButtonFilterFoodItems_Click(object sender, EventArgs e)
        {
            // Reset the filter view.
            this.buttonFoodFilterColor.BackColor = Color.Empty;
            this.buttonFoodFilterColor.ForeColor = Color.Empty;

            this.treeViewFilteredFoods.Nodes.Clear();
            this.treeViewFilteredFoods.Nodes.Add("Filtered Foods");

            List<string> filters = new List<string>(7); // filter parameters
            filters.Add(this.comboBoxFoodFilterType.Text);
            filters.Add(this.textBoxFoodFilterName.Text);
            filters.Add(this.buttonFoodFilterColor.ForeColor.ToArgb().ToString());
            filters.Add(this.comboBoxFoodFilterShape.Text);
            filters.Add(this.comboBoxFoodFilterTexture.Text);
            filters.Add(this.comboBoxFoodFilterSize.Text);
            filters.Add(this.comboBoxFoodFilterTaste.Text);

            this.app.FilterFoodItems(filters);

            int i = 1;
            foreach (FoodItem food in this.app.GetFilterFoodItemsResult())
            {
                this.treeViewFilteredFoods.Nodes.Add(food.FoodName).ForeColor = Color.FromArgb(food.Color);
                this.treeViewFilteredFoods.Nodes[i].Nodes.Add(this.app.GetFoodType(food));
                this.treeViewFilteredFoods.Nodes[i].Nodes.Add(food.Shape);
                this.treeViewFilteredFoods.Nodes[i].Nodes.Add(food.Texture);
                this.treeViewFilteredFoods.Nodes[i].Nodes.Add(food.Size);
                this.treeViewFilteredFoods.Nodes[i].Nodes.Add(food.Taste);

                i++;
            }
        }

        /// <summary>
        /// Handles clicking the filter foods color button.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void ButtonFoodFilterColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.buttonFoodFilterColor.BackColor = colorDialog.Color;
                this.buttonFoodFilterColor.ForeColor = colorDialog.Color;
            }
        }

        /// <summary>
        /// Handles clicking the undo button.
        /// </summary>
        /// <param name="sender">object.</param>
        /// <param name="e">eventargs.</param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.app.Undos.Undo(this.app);
            this.UpdateButtonStatus();
        }
    }
}
