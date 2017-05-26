using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DGVPrinterHelper;
using System.Drawing.Printing;
using Excel = Microsoft.Office.Interop.Excel;
using StPierre.database;
using StPierre.manager;
using StPierre.models;
using Point = System.Drawing.Point;
using Type = StPierre.models.Type;
using System.Drawing;

namespace StPierre.View.Partial
{
    public partial class UserControlInventory : UserControl
    {
        SqlManager _db;                          // Database manager
        Item[] _items;                           // Items to show in the inventory
        private InventoryManager _manager;       // Inventory manager
        int _typeId;                             // Type id

        private User _user;                      // Session user

        /// <summary>
        /// Initialize the variables
        /// </summary>
        public UserControlInventory()
        {
            this._manager = new InventoryManager();
            InitializeComponent();
            _db = SqlManager.GetSqlManager();
            _typeId = 0;
        }

        /// <summary>
        /// Event launched when loading the inventory usercontrol
        /// Populates the list with items fetched from the database
        /// </summary>
        private void UserControlInventory_Load(object sender, EventArgs e)
        {
            ToolStripMenuItem[] toolStripMenuItems;
            ToolStripMenuItem[] subToolStripMenuItems;

            Category[] categories = _db.SelectAllCategories();

            _items = _db.SelectAllItems();

            if (categories != null)
                toolStripMenuItems = new ToolStripMenuItem[categories.Length + 1];
            else
                toolStripMenuItems = new ToolStripMenuItem[1];

            toolStripMenuItems[0] = new ToolStripMenuItem();
            toolStripMenuItems[0].Name = "dynamicItem0";
            toolStripMenuItems[0].Tag = "0";
            toolStripMenuItems[0].Text = "Tout types";
            toolStripMenuItems[0].Click += subMenuItemClickHandler;

            if (categories != null)
            {
                for (int i = 0; i < categories.Length; i++)
                {
                    toolStripMenuItems[i + 1] = new ToolStripMenuItem();
                    toolStripMenuItems[i + 1].Name = "dynamicItem" + i.ToString();
                    toolStripMenuItems[i + 1].Tag = categories[i].Id;
                    toolStripMenuItems[i + 1].Text = categories[i].Name;

                    Type[] types = _db.SelectAllTypes();
                    types = (types.Where(n => n.CategoryId == categories[i].Id)).ToArray();

                    //If there are no valid types, skip this category
                    if (types.Length == 0) continue;

                    subToolStripMenuItems = new ToolStripMenuItem[types.Length];

                    for (int j = 0; j < types.Length; j++)
                    {
                        subToolStripMenuItems[j] = new ToolStripMenuItem();
                        subToolStripMenuItems[j].Name = "dynamicSubItem" + i.ToString() + j.ToString();
                        subToolStripMenuItems[j].Tag = types[j].Id;
                        subToolStripMenuItems[j].Text = types[j].Name;
                        subToolStripMenuItems[j].Click += subMenuItemClickHandler;
                    }

                    toolStripMenuItems[i + 1].DropDownItems.AddRange(subToolStripMenuItems);
                }

                this.toolStripMenuItemTousTypes.DropDownItems.AddRange(toolStripMenuItems);
            }

            List<Provider> providers = _db.SelectAllProviders().ToList();
            Provider blankProvider = new Provider();
            blankProvider.SetId(0);
            blankProvider.Name = "Tous les fournisseurs";
            providers.Insert(0, blankProvider);
            providerComboBox.ValueMember = "id";
            providerComboBox.DisplayMember = "name";
            providerComboBox.DataSource = providers;

            List<Brand> brands = _db.SelectAllBrands().ToList();
            Brand blankBrand = new Brand();
            blankBrand.SetId(0);
            blankBrand.Name = "Tous les marques";
            brands.Insert(0, blankBrand);
            brandComboBox.ValueMember = "id";

            brandComboBox.DisplayMember = "name";
            brandComboBox.DataSource = brands;

            List<Company> companies = _db.SelectAllCompanies().ToList();
            Company blankCompany = new Company();
            blankCompany.SetId(0);
            blankCompany.Name = "Tous les compagnies";
            companies.Insert(0, blankCompany);
            companyComboBox.DisplayMember = "name";
            companyComboBox.ValueMember = "id";
            companyComboBox.DataSource = companies;

            UpdateInventory();
            dataGridViewInventory.Rows.Clear();
            DisplayItemsGrid(_manager.SelectAllItem());

            ApplyUserRight();
        }

        /// <summary>
        /// Apply the user right depending of his role id
        /// </summary>
        private void ApplyUserRight()
        {
            _user = (ParentForm as FormMain).GetUser();

            switch (_user.RoleId)
            {
                case 1:
                    AdminEnabled();
                    break;

                case 2:
                    UserEnabled();
                    break;
            }
        }

        /// <summary>
        /// Disable some field and hide some if the user isnt an admin
        /// </summary>
        private void UserEnabled()
        {
            nouveauToolStripMenuItem.Visible = false;
            toolStripMenuItemSeparator.Visible = false;

            valueLabel.Visible = false;
            
            minValueLabel.Visible = false;
            minValueTextBox.Visible = false;

            maxValueLabel.Visible = false;
            maxValueTextBox.Visible = false;

            applyButton.Location = new Point(85, 222);
        }

        /// <summary>
        /// Enable some field and show some others if the user is an admin
        /// </summary>
        private void AdminEnabled()
        {
            nouveauToolStripMenuItem.Visible = true;
            toolStripMenuItemSeparator.Visible = true;

            valueLabel.Visible = true;

            minValueLabel.Visible = true;
            minValueTextBox.Visible = true;

            maxValueLabel.Visible = true;
            maxValueTextBox.Visible = true;

            applyButton.Location = new Point(85, 297);
        }

        /// <summary>
        /// Filter the item when choosing a type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subMenuItemClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            _typeId = Convert.ToInt32(clickedItem.Tag);
            ApplyFilter();
            UpdateInventory();
        }

        /// <summary>
        /// Apply the filter and update the inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyButton_Click(object sender, EventArgs e)
        {
            ApplyFilter();
            UpdateInventory();
        }

        /// <summary>
        /// Apply the filters on the inventory list
        /// </summary>
        public void ApplyFilter()
        {
            if (_typeId != 0)
            {
                _items = _items.Where(n => n.TypeId == _typeId).ToArray();
            }

            if (providerComboBox.SelectedIndex != 0)
            {
                int providerId = (int)providerComboBox.SelectedValue;
                _items = (_items.Where(n => n.ProviderId.Equals(providerId))).ToArray();
            }

            if (brandComboBox.SelectedIndex != 0)
            {
                int brandId = (int)brandComboBox.SelectedValue;
                _items = (_items.Where(n => n.BrandId.Equals(brandId))).ToArray();
            }

            if (companyComboBox.SelectedIndex != 0)
            {
                int companyId = (int)companyComboBox.SelectedValue;
                _items = (_items.Where(n => n.CompanyId.Equals(companyId))).ToArray();
            }

            _items = (_items.Where(n => n.ReceptionDate > minReceptionDateDateTimePicker.Value.Date || n.ReceptionDate == null)).ToArray();
            _items = (_items.Where(n => n.ReceptionDate < maxReceptionDateDateTimePicker.Value.Date || n.ReceptionDate == null)).ToArray();

            if (minValueTextBox.Text != "")
            {
                _items = (_items.Where(n => n.Value > Int32.Parse(minValueTextBox.Text))).ToArray();
            }

            if (maxValueTextBox.Text != "")
            {
                _items = (_items.Where(n => n.Value < Int32.Parse(maxValueTextBox.Text))).ToArray();
            }

            labelResult.Text = _items.Length + " résultat(s)";
        }

        /// <summary>
        /// Clear the inventory list then display the actual items
        /// </summary>
        public void UpdateInventory()
        {
            dataGridViewInventory.Rows.Clear();
            DisplayItemsGrid(_items);
            _items = _db.SelectAllItems();
        }

        /// <summary>
        /// Modify the inventory list in function of the value in the searsh bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridViewInventory.Rows.Clear();
            _items = _manager.UpdateItemsResearch(textBoxSearch.Text);

            if (_items != null)
            {
                ApplyFilter();
                DisplayItemsGrid(_items);
                labelResult.Text = _items.Length + " résultat(s)";
            }
            else
            {
                ApplyFilter();
                DisplayItemsGrid(_manager.GetAllitems());
                labelResult.Text = "";
            }

        }

        /// <summary>
        /// Display the items on the inventory
        /// </summary>
        /// <param name="items"></param>
        private void DisplayItemsGrid(Item[] items)
        {
            /*
             * For each item taken from the db, ordered by reception date newest first
             * Add the item in the list
             */
            Array.ForEach(
                items.OrderByDescending(i => i.ReceptionDate).ToArray(),
                item => dataGridViewInventory.Rows.Add(new object[] {
                    item.Id,
                    item.Type.Name,
                    item.Number,
                    item.Name,
                    item.Brand.Name,
                    item.Model,
                    item.Year,
                    item.ReceptionDate,
                    item.Quantity
                })
            );

            items = _db.SelectAllItems();
        }

        /// <summary>
        /// Event launched when clicking on an item in the gridview
        /// Shows the specific details about a given item
        /// </summary>
        private void dataGridViewInventory_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int itemId = Convert.ToInt32(dataGridViewInventory.Rows[e.RowIndex].Cells[0].Value);
            UserControl informationControl = new UserControlInformation(itemId);
            (ParentForm as FormMain).AdvanceTo(informationControl, (string) dataGridViewInventory.Rows[e.RowIndex].Cells["columnName"].Value);
        }

        /// <summary>
        /// Update all the inventory without category or type filter and apply the filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemTousTypes_Click(object sender, EventArgs e)
        {
            _typeId = 0;
        }
        /// <summary>
        /// Remove the place older
        /// </summary>
        private void textBoxSearch_Enter(object sender, EventArgs e)
        {
            if(textBoxSearch.Text == "Mot clés, # de modèle ou # d'objet")
            {
                textBoxSearch.Text = "";
            }
        }

        private void textBoxSearch_Leave(object sender, EventArgs e)
        {
            if(textBoxSearch.Text == "Mot clés, # de modèle ou # d'objet" || textBoxSearch.Text.Length <= 0)
            {
                textBoxSearch.Text = "Mot clés, # de modèle ou # d'objet";
                _items = _manager.UpdateItemsResearch("");
                ApplyFilter();
                DisplayItemsGrid(_items);
                labelResult.Text = "";
            }
        }

        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserControl informationControl = new UserControlInformation(0);
            (ParentForm as FormMain).AdvanceTo(informationControl, "Nouvel Objet");
        }

        private void exportercsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int startCol = 1;
                int startRow = 1;
                int j = 0, i = 0;

                //Write Headers
                for (j = 0; j < dataGridViewInventory.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[startRow, startCol + j];
                    myRange.Value2 = dataGridViewInventory.Columns[j].HeaderText;
                }
                startRow++;
                //Write datagridview content
                for (i = 0; i < dataGridViewInventory.Rows.Count; i++)
                {
                    for (j = 0; j < dataGridViewInventory.Columns.Count; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[startRow + i, startCol + j];
                            myRange.Value2 = dataGridViewInventory[j, i].Value == null ? "" : dataGridViewInventory[j, i].Value;
                        }
                        catch
                        {
                            ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = ParentForm;
            bool isMaximised = mainForm.WindowState == FormWindowState.Maximized;
            mainForm.WindowState = FormWindowState.Normal;
            int oldWidth = mainForm.Width;
            mainForm.Width = 900;
            mainForm.Visible = false;
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Rapport Inventaire";

            printer.SubTitle = string.Format("Date: {0}", DateTime.Now);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "©Excavation St-Pierre";
            printer.FooterSpacing = 15;
            printer.PrintDataGridView(dataGridViewInventory);
            mainForm.Width = oldWidth;
            if (isMaximised)
            {
                mainForm.WindowState = FormWindowState.Maximized;
                mainForm.Visible = true;
            }
        }
    }
}