namespace StPierre.View.Partial
{
    partial class UserControlInventory
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemTousTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.brandLabel = new System.Windows.Forms.Label();
            this.brandComboBox = new System.Windows.Forms.ComboBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.maxReceptionDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.minReceptionDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.maxReceptionDateLabel = new System.Windows.Forms.Label();
            this.minReceptionDateLabel = new System.Windows.Forms.Label();
            this.receptionDateLabel = new System.Windows.Forms.Label();
            this.companyLabel = new System.Windows.Forms.Label();
            this.companyComboBox = new System.Windows.Forms.ComboBox();
            this.providerLabel = new System.Windows.Forms.Label();
            this.providerComboBox = new System.Windows.Forms.ComboBox();
            this.maxValueTextBox = new System.Windows.Forms.TextBox();
            this.minValueTextBox = new System.Windows.Forms.TextBox();
            this.maxValueLabel = new System.Windows.Forms.Label();
            this.minValueLabel = new System.Windows.Forms.Label();
            this.valueLabel = new System.Windows.Forms.Label();
            this.dataGridViewInventory = new System.Windows.Forms.DataGridView();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBrand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReceptionDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nouveauToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportercsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelResult = new System.Windows.Forms.Panel();
            this.labelResult = new System.Windows.Forms.Label();
            this.panelSearch.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInventory)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.panelResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSearch
            // 
            this.panelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSearch.Controls.Add(this.menuStrip);
            this.panelSearch.Controls.Add(this.textBoxSearch);
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1089, 37);
            this.panelSearch.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemTousTypes});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(71, 28);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItemTousTypes
            // 
            this.toolStripMenuItemTousTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItemTousTypes.Name = "toolStripMenuItemTousTypes";
            this.toolStripMenuItemTousTypes.Size = new System.Drawing.Size(63, 24);
            this.toolStripMenuItemTousTypes.Text = "Types";
            this.toolStripMenuItemTousTypes.Click += new System.EventHandler(this.toolStripMenuItemTousTypes_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(208, 1);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(877, 26);
            this.textBoxSearch.TabIndex = 0;
            this.textBoxSearch.Text = "Mot clés, # de modèle ou # d\'objet";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            this.textBoxSearch.Enter += new System.EventHandler(this.textBoxSearch_Enter);
            this.textBoxSearch.Leave += new System.EventHandler(this.textBoxSearch_Leave);
            // 
            // panelBottom
            // 
            this.panelBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelBottom.Controls.Add(this.panelFilters);
            this.panelBottom.Controls.Add(this.dataGridViewInventory);
            this.panelBottom.Location = new System.Drawing.Point(0, 35);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1089, 372);
            this.panelBottom.TabIndex = 2;
            // 
            // panelFilters
            // 
            this.panelFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelFilters.AutoScroll = true;
            this.panelFilters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelFilters.Controls.Add(this.brandLabel);
            this.panelFilters.Controls.Add(this.brandComboBox);
            this.panelFilters.Controls.Add(this.applyButton);
            this.panelFilters.Controls.Add(this.maxReceptionDateDateTimePicker);
            this.panelFilters.Controls.Add(this.minReceptionDateDateTimePicker);
            this.panelFilters.Controls.Add(this.maxReceptionDateLabel);
            this.panelFilters.Controls.Add(this.minReceptionDateLabel);
            this.panelFilters.Controls.Add(this.receptionDateLabel);
            this.panelFilters.Controls.Add(this.companyLabel);
            this.panelFilters.Controls.Add(this.companyComboBox);
            this.panelFilters.Controls.Add(this.providerLabel);
            this.panelFilters.Controls.Add(this.providerComboBox);
            this.panelFilters.Controls.Add(this.maxValueTextBox);
            this.panelFilters.Controls.Add(this.minValueTextBox);
            this.panelFilters.Controls.Add(this.maxValueLabel);
            this.panelFilters.Controls.Add(this.minValueLabel);
            this.panelFilters.Controls.Add(this.valueLabel);
            this.panelFilters.Location = new System.Drawing.Point(0, 6);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(210, 366);
            this.panelFilters.TabIndex = 0;
            // 
            // brandLabel
            // 
            this.brandLabel.AutoSize = true;
            this.brandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.brandLabel.Location = new System.Drawing.Point(3, 50);
            this.brandLabel.Name = "brandLabel";
            this.brandLabel.Size = new System.Drawing.Size(62, 17);
            this.brandLabel.TabIndex = 2;
            this.brandLabel.Text = "Marque";
            // 
            // brandComboBox
            // 
            this.brandComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.brandComboBox.FormattingEnabled = true;
            this.brandComboBox.Location = new System.Drawing.Point(3, 70);
            this.brandComboBox.Name = "brandComboBox";
            this.brandComboBox.Size = new System.Drawing.Size(183, 24);
            this.brandComboBox.TabIndex = 3;
            // 
            // applyButton
            // 
            this.applyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyButton.Location = new System.Drawing.Point(85, 297);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(101, 34);
            this.applyButton.TabIndex = 14;
            this.applyButton.Text = "Appliquer";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // maxReceptionDateDateTimePicker
            // 
            this.maxReceptionDateDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.maxReceptionDateDateTimePicker.Location = new System.Drawing.Point(52, 193);
            this.maxReceptionDateDateTimePicker.Name = "maxReceptionDateDateTimePicker";
            this.maxReceptionDateDateTimePicker.Size = new System.Drawing.Size(134, 23);
            this.maxReceptionDateDateTimePicker.TabIndex = 8;
            // 
            // minReceptionDateDateTimePicker
            // 
            this.minReceptionDateDateTimePicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.minReceptionDateDateTimePicker.Location = new System.Drawing.Point(52, 164);
            this.minReceptionDateDateTimePicker.Name = "minReceptionDateDateTimePicker";
            this.minReceptionDateDateTimePicker.Size = new System.Drawing.Size(134, 23);
            this.minReceptionDateDateTimePicker.TabIndex = 7;
            this.minReceptionDateDateTimePicker.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // maxReceptionDateLabel
            // 
            this.maxReceptionDateLabel.AutoSize = true;
            this.maxReceptionDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.maxReceptionDateLabel.Location = new System.Drawing.Point(22, 198);
            this.maxReceptionDateLabel.Name = "maxReceptionDateLabel";
            this.maxReceptionDateLabel.Size = new System.Drawing.Size(24, 17);
            this.maxReceptionDateLabel.TabIndex = 16;
            this.maxReceptionDateLabel.Text = "au";
            // 
            // minReceptionDateLabel
            // 
            this.minReceptionDateLabel.AutoSize = true;
            this.minReceptionDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.minReceptionDateLabel.Location = new System.Drawing.Point(18, 169);
            this.minReceptionDateLabel.Name = "minReceptionDateLabel";
            this.minReceptionDateLabel.Size = new System.Drawing.Size(26, 17);
            this.minReceptionDateLabel.TabIndex = 15;
            this.minReceptionDateLabel.Text = "Du";
            // 
            // receptionDateLabel
            // 
            this.receptionDateLabel.AutoSize = true;
            this.receptionDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.receptionDateLabel.Location = new System.Drawing.Point(3, 144);
            this.receptionDateLabel.Name = "receptionDateLabel";
            this.receptionDateLabel.Size = new System.Drawing.Size(138, 17);
            this.receptionDateLabel.TabIndex = 6;
            this.receptionDateLabel.Text = "Date de réception";
            // 
            // companyLabel
            // 
            this.companyLabel.AutoSize = true;
            this.companyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.companyLabel.Location = new System.Drawing.Point(3, 3);
            this.companyLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.companyLabel.Name = "companyLabel";
            this.companyLabel.Size = new System.Drawing.Size(88, 17);
            this.companyLabel.TabIndex = 0;
            this.companyLabel.Text = "Compagnie";
            // 
            // companyComboBox
            // 
            this.companyComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.companyComboBox.FormattingEnabled = true;
            this.companyComboBox.Location = new System.Drawing.Point(3, 23);
            this.companyComboBox.Name = "companyComboBox";
            this.companyComboBox.Size = new System.Drawing.Size(183, 24);
            this.companyComboBox.TabIndex = 1;
            // 
            // providerLabel
            // 
            this.providerLabel.AutoSize = true;
            this.providerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.providerLabel.Location = new System.Drawing.Point(3, 97);
            this.providerLabel.Name = "providerLabel";
            this.providerLabel.Size = new System.Drawing.Size(94, 17);
            this.providerLabel.TabIndex = 4;
            this.providerLabel.Text = "Fournisseur";
            // 
            // providerComboBox
            // 
            this.providerComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.providerComboBox.FormattingEnabled = true;
            this.providerComboBox.Location = new System.Drawing.Point(3, 117);
            this.providerComboBox.Name = "providerComboBox";
            this.providerComboBox.Size = new System.Drawing.Size(183, 24);
            this.providerComboBox.TabIndex = 5;
            // 
            // maxValueTextBox
            // 
            this.maxValueTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.maxValueTextBox.Location = new System.Drawing.Point(52, 268);
            this.maxValueTextBox.Name = "maxValueTextBox";
            this.maxValueTextBox.Size = new System.Drawing.Size(134, 23);
            this.maxValueTextBox.TabIndex = 13;
            // 
            // minValueTextBox
            // 
            this.minValueTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.minValueTextBox.Location = new System.Drawing.Point(52, 239);
            this.minValueTextBox.Name = "minValueTextBox";
            this.minValueTextBox.Size = new System.Drawing.Size(134, 23);
            this.minValueTextBox.TabIndex = 11;
            // 
            // maxValueLabel
            // 
            this.maxValueLabel.AutoSize = true;
            this.maxValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.maxValueLabel.Location = new System.Drawing.Point(11, 271);
            this.maxValueLabel.Name = "maxValueLabel";
            this.maxValueLabel.Size = new System.Drawing.Size(37, 17);
            this.maxValueLabel.TabIndex = 12;
            this.maxValueLabel.Text = "à $C";
            // 
            // minValueLabel
            // 
            this.minValueLabel.AutoSize = true;
            this.minValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.minValueLabel.Location = new System.Drawing.Point(23, 242);
            this.minValueLabel.Name = "minValueLabel";
            this.minValueLabel.Size = new System.Drawing.Size(25, 17);
            this.minValueLabel.TabIndex = 10;
            this.minValueLabel.Text = "$C";
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.valueLabel.Location = new System.Drawing.Point(1, 219);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(55, 17);
            this.valueLabel.TabIndex = 9;
            this.valueLabel.Text = "Valeur";
            // 
            // dataGridViewInventory
            // 
            this.dataGridViewInventory.AllowUserToAddRows = false;
            this.dataGridViewInventory.AllowUserToDeleteRows = false;
            this.dataGridViewInventory.AllowUserToResizeRows = false;
            this.dataGridViewInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewInventory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInventory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnType,
            this.ColumnNo,
            this.ColumnName,
            this.ColumnBrand,
            this.ColumnModel,
            this.ColumnYear,
            this.ColumnReceptionDate});
            this.dataGridViewInventory.ContextMenuStrip = this.contextMenuStrip;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewInventory.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewInventory.Location = new System.Drawing.Point(210, 6);
            this.dataGridViewInventory.MultiSelect = false;
            this.dataGridViewInventory.Name = "dataGridViewInventory";
            this.dataGridViewInventory.ReadOnly = true;
            this.dataGridViewInventory.RowHeadersVisible = false;
            this.dataGridViewInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInventory.Size = new System.Drawing.Size(879, 366);
            this.dataGridViewInventory.TabIndex = 0;
            this.dataGridViewInventory.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewInventory_CellContentDoubleClick);
            // 
            // ColumnId
            // 
            this.ColumnId.HeaderText = "Id";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.ReadOnly = true;
            this.ColumnId.Visible = false;
            // 
            // ColumnType
            // 
            this.ColumnType.FillWeight = 94.23405F;
            this.ColumnType.HeaderText = "Type";
            this.ColumnType.Name = "ColumnType";
            this.ColumnType.ReadOnly = true;
            // 
            // ColumnNo
            // 
            this.ColumnNo.FillWeight = 98.61937F;
            this.ColumnNo.HeaderText = "No.";
            this.ColumnNo.Name = "ColumnNo";
            this.ColumnNo.ReadOnly = true;
            // 
            // ColumnName
            // 
            this.ColumnName.FillWeight = 99.22027F;
            this.ColumnName.HeaderText = "Nom";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            // 
            // ColumnBrand
            // 
            this.ColumnBrand.FillWeight = 101.175F;
            this.ColumnBrand.HeaderText = "Marque";
            this.ColumnBrand.Name = "ColumnBrand";
            this.ColumnBrand.ReadOnly = true;
            // 
            // ColumnModel
            // 
            this.ColumnModel.FillWeight = 100.6784F;
            this.ColumnModel.HeaderText = "Modèle";
            this.ColumnModel.Name = "ColumnModel";
            this.ColumnModel.ReadOnly = true;
            // 
            // ColumnYear
            // 
            this.ColumnYear.FillWeight = 106.8192F;
            this.ColumnYear.HeaderText = "Année";
            this.ColumnYear.Name = "ColumnYear";
            this.ColumnYear.ReadOnly = true;
            // 
            // ColumnReceptionDate
            // 
            this.ColumnReceptionDate.FillWeight = 99.25372F;
            this.ColumnReceptionDate.HeaderText = "Date de Réception";
            this.ColumnReceptionDate.Name = "ColumnReceptionDate";
            this.ColumnReceptionDate.ReadOnly = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nouveauToolStripMenuItem,
            this.toolStripMenuItemSeparator,
            this.printToolStripMenuItem,
            this.exportercsvToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(166, 76);
            // 
            // nouveauToolStripMenuItem
            // 
            this.nouveauToolStripMenuItem.Name = "nouveauToolStripMenuItem";
            this.nouveauToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.nouveauToolStripMenuItem.Text = "Nouvel objet";
            this.nouveauToolStripMenuItem.Click += new System.EventHandler(this.nouveauToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSeparator
            // 
            this.toolStripMenuItemSeparator.Name = "toolStripMenuItemSeparator";
            this.toolStripMenuItemSeparator.Size = new System.Drawing.Size(162, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.printToolStripMenuItem.Text = "Imprimer";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // exportercsvToolStripMenuItem
            // 
            this.exportercsvToolStripMenuItem.Name = "exportercsvToolStripMenuItem";
            this.exportercsvToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.exportercsvToolStripMenuItem.Text = "Exporter sur Excel";
            this.exportercsvToolStripMenuItem.Click += new System.EventHandler(this.exportercsvToolStripMenuItem_Click);
            // 
            // panelResult
            // 
            this.panelResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelResult.Controls.Add(this.labelResult);
            this.panelResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelResult.Location = new System.Drawing.Point(0, 413);
            this.panelResult.Name = "panelResult";
            this.panelResult.Size = new System.Drawing.Size(1089, 20);
            this.panelResult.TabIndex = 2;
            // 
            // labelResult
            // 
            this.labelResult.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelResult.Location = new System.Drawing.Point(0, 0);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(1089, 20);
            this.labelResult.TabIndex = 0;
            // 
            // UserControlInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelResult);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelSearch);
            this.Name = "UserControlInventory";
            this.Size = new System.Drawing.Size(1089, 433);
            this.Load += new System.EventHandler(this.UserControlInventory_Load);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInventory)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.panelResult.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.DataGridView dataGridViewInventory;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReceptionDate;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTousTypes;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Label brandLabel;
        private System.Windows.Forms.ComboBox brandComboBox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.DateTimePicker maxReceptionDateDateTimePicker;
        private System.Windows.Forms.DateTimePicker minReceptionDateDateTimePicker;
        private System.Windows.Forms.Label maxReceptionDateLabel;
        private System.Windows.Forms.Label minReceptionDateLabel;
        private System.Windows.Forms.Label receptionDateLabel;
        private System.Windows.Forms.Label companyLabel;
        private System.Windows.Forms.ComboBox companyComboBox;
        private System.Windows.Forms.Label providerLabel;
        private System.Windows.Forms.ComboBox providerComboBox;
        private System.Windows.Forms.TextBox maxValueTextBox;
        private System.Windows.Forms.TextBox minValueTextBox;
        private System.Windows.Forms.Label maxValueLabel;
        private System.Windows.Forms.Label minValueLabel;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem nouveauToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportercsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSeparator;
    }
}
