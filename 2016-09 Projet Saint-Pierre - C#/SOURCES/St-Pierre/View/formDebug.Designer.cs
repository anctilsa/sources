namespace StPierre.View
{
    partial class FormDebug
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxResult = new System.Windows.Forms.RichTextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonFetchItem = new System.Windows.Forms.Button();
            this.radioButtonItemFromId = new System.Windows.Forms.RadioButton();
            this.radioButtonAllItems = new System.Windows.Forms.RadioButton();
            this.groupBoxType = new System.Windows.Forms.GroupBox();
            this.groupBoxTable = new System.Windows.Forms.GroupBox();
            this.radioButtonBrand = new System.Windows.Forms.RadioButton();
            this.radioButtonCategory = new System.Windows.Forms.RadioButton();
            this.radioButtonCompany = new System.Windows.Forms.RadioButton();
            this.radioButtonItem = new System.Windows.Forms.RadioButton();
            this.radioButtonItemCompatibility = new System.Windows.Forms.RadioButton();
            this.radioButtonLocation = new System.Windows.Forms.RadioButton();
            this.radioButtonProvider = new System.Windows.Forms.RadioButton();
            this.radioButtonRole = new System.Windows.Forms.RadioButton();
            this.radioButtonType = new System.Windows.Forms.RadioButton();
            this.radioButtonUnit = new System.Windows.Forms.RadioButton();
            this.radioButtonUser = new System.Windows.Forms.RadioButton();
            this.labelInfo = new System.Windows.Forms.Label();
            this.radioButtonItemCreation = new System.Windows.Forms.RadioButton();
            this.numericUpDownItemFromId = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxType.SuspendLayout();
            this.groupBoxTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownItemFromId)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxResult
            // 
            this.richTextBoxResult.Enabled = false;
            this.richTextBoxResult.Location = new System.Drawing.Point(397, 36);
            this.richTextBoxResult.Name = "richTextBoxResult";
            this.richTextBoxResult.Size = new System.Drawing.Size(350, 335);
            this.richTextBoxResult.TabIndex = 1;
            this.richTextBoxResult.Text = "";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(29, 22);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(73, 13);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Database test";
            // 
            // buttonFetchItem
            // 
            this.buttonFetchItem.Location = new System.Drawing.Point(105, 348);
            this.buttonFetchItem.Name = "buttonFetchItem";
            this.buttonFetchItem.Size = new System.Drawing.Size(75, 23);
            this.buttonFetchItem.TabIndex = 3;
            this.buttonFetchItem.Text = "Confirm";
            this.buttonFetchItem.UseVisualStyleBackColor = true;
            this.buttonFetchItem.Click += new System.EventHandler(this.buttonFetchItem_Click);
            // 
            // radioButtonItemFromId
            // 
            this.radioButtonItemFromId.AutoSize = true;
            this.radioButtonItemFromId.Location = new System.Drawing.Point(18, 48);
            this.radioButtonItemFromId.Name = "radioButtonItemFromId";
            this.radioButtonItemFromId.Size = new System.Drawing.Size(114, 17);
            this.radioButtonItemFromId.TabIndex = 4;
            this.radioButtonItemFromId.Text = "Select item from id:";
            this.radioButtonItemFromId.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllItems
            // 
            this.radioButtonAllItems.AutoSize = true;
            this.radioButtonAllItems.Checked = true;
            this.radioButtonAllItems.Location = new System.Drawing.Point(18, 71);
            this.radioButtonAllItems.Name = "radioButtonAllItems";
            this.radioButtonAllItems.Size = new System.Drawing.Size(95, 17);
            this.radioButtonAllItems.TabIndex = 5;
            this.radioButtonAllItems.TabStop = true;
            this.radioButtonAllItems.Text = "Select all items";
            this.radioButtonAllItems.UseVisualStyleBackColor = true;
            // 
            // groupBoxType
            // 
            this.groupBoxType.Controls.Add(this.numericUpDownItemFromId);
            this.groupBoxType.Controls.Add(this.radioButtonItemCreation);
            this.groupBoxType.Controls.Add(this.radioButtonAllItems);
            this.groupBoxType.Controls.Add(this.radioButtonItemFromId);
            this.groupBoxType.Location = new System.Drawing.Point(22, 230);
            this.groupBoxType.Name = "groupBoxType";
            this.groupBoxType.Size = new System.Drawing.Size(307, 112);
            this.groupBoxType.TabIndex = 6;
            this.groupBoxType.TabStop = false;
            // 
            // groupBoxTable
            // 
            this.groupBoxTable.Controls.Add(this.radioButtonUser);
            this.groupBoxTable.Controls.Add(this.radioButtonUnit);
            this.groupBoxTable.Controls.Add(this.radioButtonType);
            this.groupBoxTable.Controls.Add(this.radioButtonRole);
            this.groupBoxTable.Controls.Add(this.radioButtonProvider);
            this.groupBoxTable.Controls.Add(this.radioButtonLocation);
            this.groupBoxTable.Controls.Add(this.radioButtonItemCompatibility);
            this.groupBoxTable.Controls.Add(this.radioButtonItem);
            this.groupBoxTable.Controls.Add(this.radioButtonCompany);
            this.groupBoxTable.Controls.Add(this.radioButtonCategory);
            this.groupBoxTable.Controls.Add(this.radioButtonBrand);
            this.groupBoxTable.Location = new System.Drawing.Point(22, 71);
            this.groupBoxTable.Name = "groupBoxTable";
            this.groupBoxTable.Size = new System.Drawing.Size(306, 148);
            this.groupBoxTable.TabIndex = 7;
            this.groupBoxTable.TabStop = false;
            // 
            // radioButtonBrand
            // 
            this.radioButtonBrand.AutoSize = true;
            this.radioButtonBrand.Location = new System.Drawing.Point(10, 20);
            this.radioButtonBrand.Name = "radioButtonBrand";
            this.radioButtonBrand.Size = new System.Drawing.Size(53, 17);
            this.radioButtonBrand.TabIndex = 0;
            this.radioButtonBrand.TabStop = true;
            this.radioButtonBrand.Text = "Brand";
            this.radioButtonBrand.UseVisualStyleBackColor = true;
            // 
            // radioButtonCategory
            // 
            this.radioButtonCategory.AutoSize = true;
            this.radioButtonCategory.Location = new System.Drawing.Point(10, 43);
            this.radioButtonCategory.Name = "radioButtonCategory";
            this.radioButtonCategory.Size = new System.Drawing.Size(67, 17);
            this.radioButtonCategory.TabIndex = 1;
            this.radioButtonCategory.TabStop = true;
            this.radioButtonCategory.Text = "Category";
            this.radioButtonCategory.UseVisualStyleBackColor = true;
            // 
            // radioButtonCompany
            // 
            this.radioButtonCompany.AutoSize = true;
            this.radioButtonCompany.Location = new System.Drawing.Point(10, 66);
            this.radioButtonCompany.Name = "radioButtonCompany";
            this.radioButtonCompany.Size = new System.Drawing.Size(69, 17);
            this.radioButtonCompany.TabIndex = 2;
            this.radioButtonCompany.TabStop = true;
            this.radioButtonCompany.Text = "Company";
            this.radioButtonCompany.UseVisualStyleBackColor = true;
            // 
            // radioButtonItem
            // 
            this.radioButtonItem.AutoSize = true;
            this.radioButtonItem.Location = new System.Drawing.Point(10, 89);
            this.radioButtonItem.Name = "radioButtonItem";
            this.radioButtonItem.Size = new System.Drawing.Size(45, 17);
            this.radioButtonItem.TabIndex = 3;
            this.radioButtonItem.TabStop = true;
            this.radioButtonItem.Text = "Item";
            this.radioButtonItem.UseVisualStyleBackColor = true;
            // 
            // radioButtonItemCompatibility
            // 
            this.radioButtonItemCompatibility.AutoSize = true;
            this.radioButtonItemCompatibility.Location = new System.Drawing.Point(10, 113);
            this.radioButtonItemCompatibility.Name = "radioButtonItemCompatibility";
            this.radioButtonItemCompatibility.Size = new System.Drawing.Size(107, 17);
            this.radioButtonItemCompatibility.TabIndex = 4;
            this.radioButtonItemCompatibility.TabStop = true;
            this.radioButtonItemCompatibility.Text = "item_compatibility";
            this.radioButtonItemCompatibility.UseVisualStyleBackColor = true;
            // 
            // radioButtonLocation
            // 
            this.radioButtonLocation.AutoSize = true;
            this.radioButtonLocation.Location = new System.Drawing.Point(157, 20);
            this.radioButtonLocation.Name = "radioButtonLocation";
            this.radioButtonLocation.Size = new System.Drawing.Size(66, 17);
            this.radioButtonLocation.TabIndex = 5;
            this.radioButtonLocation.TabStop = true;
            this.radioButtonLocation.Text = "Location";
            this.radioButtonLocation.UseVisualStyleBackColor = true;
            // 
            // radioButtonProvider
            // 
            this.radioButtonProvider.AutoSize = true;
            this.radioButtonProvider.Location = new System.Drawing.Point(156, 43);
            this.radioButtonProvider.Name = "radioButtonProvider";
            this.radioButtonProvider.Size = new System.Drawing.Size(64, 17);
            this.radioButtonProvider.TabIndex = 6;
            this.radioButtonProvider.TabStop = true;
            this.radioButtonProvider.Text = "Provider";
            this.radioButtonProvider.UseVisualStyleBackColor = true;
            // 
            // radioButtonRole
            // 
            this.radioButtonRole.AutoSize = true;
            this.radioButtonRole.Location = new System.Drawing.Point(156, 66);
            this.radioButtonRole.Name = "radioButtonRole";
            this.radioButtonRole.Size = new System.Drawing.Size(47, 17);
            this.radioButtonRole.TabIndex = 7;
            this.radioButtonRole.TabStop = true;
            this.radioButtonRole.Text = "Role";
            this.radioButtonRole.UseVisualStyleBackColor = true;
            // 
            // radioButtonType
            // 
            this.radioButtonType.AutoSize = true;
            this.radioButtonType.Location = new System.Drawing.Point(157, 89);
            this.radioButtonType.Name = "radioButtonType";
            this.radioButtonType.Size = new System.Drawing.Size(49, 17);
            this.radioButtonType.TabIndex = 8;
            this.radioButtonType.TabStop = true;
            this.radioButtonType.Text = "Type";
            this.radioButtonType.UseVisualStyleBackColor = true;
            // 
            // radioButtonUnit
            // 
            this.radioButtonUnit.AutoSize = true;
            this.radioButtonUnit.Location = new System.Drawing.Point(156, 113);
            this.radioButtonUnit.Name = "radioButtonUnit";
            this.radioButtonUnit.Size = new System.Drawing.Size(44, 17);
            this.radioButtonUnit.TabIndex = 9;
            this.radioButtonUnit.TabStop = true;
            this.radioButtonUnit.Text = "Unit";
            this.radioButtonUnit.UseVisualStyleBackColor = true;
            // 
            // radioButtonUser
            // 
            this.radioButtonUser.AutoSize = true;
            this.radioButtonUser.Location = new System.Drawing.Point(237, 113);
            this.radioButtonUser.Name = "radioButtonUser";
            this.radioButtonUser.Size = new System.Drawing.Size(47, 17);
            this.radioButtonUser.TabIndex = 10;
            this.radioButtonUser.TabStop = true;
            this.radioButtonUser.Text = "User";
            this.radioButtonUser.UseVisualStyleBackColor = true;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(29, 55);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(184, 13);
            this.labelInfo.TabIndex = 8;
            this.labelInfo.Text = "Select the items you would like to test";
            // 
            // radioButtonItemCreation
            // 
            this.radioButtonItemCreation.AutoSize = true;
            this.radioButtonItemCreation.Location = new System.Drawing.Point(18, 25);
            this.radioButtonItemCreation.Name = "radioButtonItemCreation";
            this.radioButtonItemCreation.Size = new System.Drawing.Size(110, 17);
            this.radioButtonItemCreation.TabIndex = 7;
            this.radioButtonItemCreation.TabStop = true;
            this.radioButtonItemCreation.Text = "Create a new item";
            this.radioButtonItemCreation.UseVisualStyleBackColor = true;
            // 
            // numericUpDownItemFromId
            // 
            this.numericUpDownItemFromId.Location = new System.Drawing.Point(164, 48);
            this.numericUpDownItemFromId.Name = "numericUpDownItemFromId";
            this.numericUpDownItemFromId.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownItemFromId.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Select an action to perform";
            // 
            // formDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 407);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.groupBoxTable);
            this.Controls.Add(this.groupBoxType);
            this.Controls.Add(this.buttonFetchItem);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.richTextBoxResult);
            this.Name = "FormDebug";
            this.Text = "Form1";
            this.groupBoxType.ResumeLayout(false);
            this.groupBoxType.PerformLayout();
            this.groupBoxTable.ResumeLayout(false);
            this.groupBoxTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownItemFromId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxResult;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonFetchItem;
        private System.Windows.Forms.RadioButton radioButtonItemFromId;
        private System.Windows.Forms.RadioButton radioButtonAllItems;
        private System.Windows.Forms.GroupBox groupBoxType;
        private System.Windows.Forms.GroupBox groupBoxTable;
        private System.Windows.Forms.RadioButton radioButtonUser;
        private System.Windows.Forms.RadioButton radioButtonUnit;
        private System.Windows.Forms.RadioButton radioButtonType;
        private System.Windows.Forms.RadioButton radioButtonRole;
        private System.Windows.Forms.RadioButton radioButtonProvider;
        private System.Windows.Forms.RadioButton radioButtonLocation;
        private System.Windows.Forms.RadioButton radioButtonItemCompatibility;
        private System.Windows.Forms.RadioButton radioButtonItem;
        private System.Windows.Forms.RadioButton radioButtonCompany;
        private System.Windows.Forms.RadioButton radioButtonCategory;
        private System.Windows.Forms.RadioButton radioButtonBrand;
        private System.Windows.Forms.RadioButton radioButtonItemCreation;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.NumericUpDown numericUpDownItemFromId;
        private System.Windows.Forms.Label label1;
    }
}