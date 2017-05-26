using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using StPierre.database;
using StPierre.models;
using DGVPrinterHelper;
using Type = StPierre.models.Type;

namespace StPierre.View.Partial
{
    public partial class UserControlGeneralManager : UserControl
    {
        public byte Manager;           //Current Manager
        
        private List<string> _errorList;
        private Provider[] _providers;
        private Brand[] _brands;
        private Model[] _models;
        private Type[] _types;
        private Location[] _locations;
        private Unit[] _units;
        private CultureInfo _culture;
        private SqlManager _db;

        public UserControlGeneralManager()
        {
            InitializeComponent();
            _errorList = new List<string>();
            //Defaults on manager 0, which is nothing
            Manager = 0;
            this._db = SqlManager.GetSqlManager();
            dataGridView.Columns.Add("columnId", "ID");
            dataGridView.Columns["columnId"].ReadOnly = true;
        }
        public UserControlGeneralManager(byte manager)
        {
            InitializeComponent();
            _errorList = new List<string>();
            this.Manager = manager;
            this._db = SqlManager.GetSqlManager();
            dataGridView.Columns.Add("columnDelete", "Delete");
            dataGridView.Columns["columnDelete"].ReadOnly = true;
            dataGridView.Columns["columnDelete"].Visible = false;
            dataGridView.Columns["columnDelete"].ValueType = System.Type.GetType("bool");
            dataGridView.Columns.Add("columnId", "ID");
            dataGridView.Columns["columnId"].ReadOnly = true;
            //Adds column depending on which manager is selected
            switch (this.Manager)
            {
                case Managers.Providers:
                    dataGridView.Columns.Add("columnName",         "Nom");
                    dataGridView.Columns.Add("columnPhone",        "Tél.");
                    dataGridView.Columns.Add("columnPhoneAlt",     "Tél. Alt.");
                    dataGridView.Columns.Add("columnEmail",        "Email");
                    dataGridView.Columns.Add("columnContact",      "Contact");
                    dataGridView.Columns.Add("columnWebsite",      "Site Web");
                    dataGridView.Columns.Add("columnCity",         "Ville");
                    dataGridView.Columns.Add("columnNotes",        "Notes");
                    dataGridView.Columns.Add("columnCreationDate", "Date de Création");
                    dataGridView.Columns["columnCreationDate"].ReadOnly = true;
                    break;
                case Managers.Brands:
                    dataGridView.Columns.Add("columnName", "Nom");
                    dataGridView.Columns.Add("columnPhone", "Tél.");
                    dataGridView.Columns.Add("columnContact", "Contact");
                    dataGridView.Columns.Add("columnWebsite", "Site Web");
                    dataGridView.Columns.Add("columnNotes", "Notes");
                    break;
                case Managers.Models:
                    //Exists in case we decide to add that fonctionality
                    break;
                case Managers.Types:
                    dataGridView.Columns.Add("columnName", "Nom");

                    DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
                    cmb.DisplayMember = "name";
                    cmb.ValueMember = "id";
                    cmb.HeaderText = "Catégorie";
                    cmb.Name = "columnCategory";
                    Category[] categories = _db.SelectAllCategories();
                    cmb.DataSource = categories;
                    dataGridView.Columns.Add(cmb);
                    break;
                case Managers.Locations:
                    dataGridView.Columns.Add("columnName", "Nom");
                    dataGridView.Columns.Add("columnDescription", "Description");
                    break;
                case Managers.Unity:
                    dataGridView.Columns.Add("columnName", "Nom");
                    dataGridView.Columns.Add("columnShortName", "Nom abrégé");
                    dataGridView.Columns.Add("columnDescription", "Description");
                    break;
            }
        }
        private void UserControlGeneralManager_Load(object sender, EventArgs e)
        {
            _culture = CultureInfo.CurrentCulture;
            switch (this.Manager)
            {
                case Managers.Providers:
                    _providers = _db.SelectAllProviders();
                    UpdateListProvider(_providers);
                    break;
                case Managers.Brands:
                    _brands = _db.SelectAllBrands();
                    UpdateListBrand(_brands);
                    break;
                case Managers.Models:
                    UpdateListModel(_models);
                    break;
                case Managers.Types:
                    _types = _db.SelectAllTypes();
                    UpdateListType(_types);
                    break;
                case Managers.Locations:
                    _locations = _db.SelectAllLocations();
                    UpdateListLocation(_locations);
                    break;
                case Managers.Unity:
                    _units = _db.SelectAllUnits();
                    UpdateListUnit(_units);
                    break;
            }
        }
        private void ShowErrorMessage(string message, string title = "Erreur!")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void LoopThroughGrid(Action<DataGridViewRow> rowAction)
        {
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.White;

                if ((bool)dataGridView.Rows[i].Cells["columnDelete"].Value && dataGridView.Rows[i].Cells["columnId"].Value == null)
                {
                    dataGridView.Rows.RemoveAt(i);
                    i--;
                }
                else
                {
                    int pastRowCount = dataGridView.RowCount;
                    rowAction(dataGridView.Rows[i]);
                    if (pastRowCount > dataGridView.RowCount) i--;
                }
            }
            ShowErrorList();
        }
        private void ValidateProvider(DataGridViewRow row)
        {
            Provider provider = new Provider(
                (int) (row.Cells["columnId"].Value ?? 0),
                (DateTime) (row.Cells["columnCreationDate"].Value ?? DateTime.Now),
                (string) row.Cells["columnName"].Value,
                (string) row.Cells["columnPhone"].Value,
                (string) row.Cells["columnPhoneAlt"].Value,
                (string) row.Cells["columnEmail"].Value,
                (string) row.Cells["columnContact"].Value,
                (string) row.Cells["columnWebsite"].Value,
                (string) row.Cells["columnCity"].Value,
                (string) row.Cells["columnNotes"].Value
            );
            
            if ((bool)row.Cells["columnDelete"].Value)
            {
                _db.DeleteProvider(provider);
                dataGridView.Rows.Remove(row);
            }
            else
            {
                //If there is an error, add the appropriate message to the error list and don't update
                if (string.IsNullOrEmpty(provider.Name))
                {
                    AddToErrorList(row.Index, "Besoin d'un nom");
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    if (provider.Id != 0)
                    {
                        _db.UpdateProvider(provider);
                    }
                    else
                    {
                        row.Cells["columnCreationDate"].Value = provider.CreationDate = DateTime.Now;

                        _db.InsertProvider(ref provider);
                        row.Cells["columnId"].Value = provider.Id;
                    }
                }
            }
        }
        private void ValidateBrand(DataGridViewRow row)
        {
            Brand brand = new Brand(
                (int) (row.Cells["columnId"].Value ?? 0),
                (string) row.Cells["columnName"].Value,
                (string) row.Cells["columnPhone"].Value,
                (string) row.Cells["columnContact"].Value,
                (string) row.Cells["columnWebsite"].Value,
                (string) row.Cells["columnNotes"].Value
            );
            
            if ((bool) row.Cells["columnDelete"].Value)
            {
                _db.DeleteBrand(brand);
                dataGridView.Rows.Remove(row);
            }
            else
            {
                //If there is an error, add the appropriate message to the error list and don't update
                if (string.IsNullOrEmpty(brand.Name))
                {
                    AddToErrorList(row.Index, "Besoin d'un nom");
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    if (brand.Id != 0)
                    {
                        _db.UpdateBrand(brand);
                    }
                    else
                    {
                        _db.InsertBrand(ref brand);
                        row.Cells["columnId"].Value = brand.Id;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        private void ValidateType(DataGridViewRow row)
        {
            Type type = new Type(
                (int) (row.Cells["columnId"].Value ?? 0),
                (string) row.Cells["columnName"].Value,
                (int) (row.Cells["columnCategory"].Value ?? -1)
            );
            
            if ((bool)row.Cells["columnDelete"].Value)
            {
                _db.DeleteType(type);
                dataGridView.Rows.Remove(row);
            }
            else
            {
                //If there is an error, add the appropriate message to the error list and don't update
                if (string.IsNullOrEmpty(type.Name) || type.CategoryId == -1)
                {
                    if (string.IsNullOrEmpty(type.Name))
                    {
                        AddToErrorList(row.Index, "Besoin d'un nom");
                    }

                    if (type.CategoryId == -1)
                    {
                        AddToErrorList(row.Index, "Besoin d'une catégorie");
                    }

                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    if (type.Id != 0)
                    {
                        _db.UpdateType(type);
                    }
                    else
                    {
                        _db.InsertType(ref type);
                        row.Cells["columnId"].Value = type.Id;
                    }
                }
            }
        }
        private void ValidateLocation(DataGridViewRow row)
        {
            Location locationn = new Location(
                (int)(row.Cells["columnId"].Value ?? 0),
                (string)row.Cells["columnName"].Value,
                (string)row.Cells["columnDescription"].Value
            );
            if ((bool)row.Cells["columnDelete"].Value)
            {
                _db.DeleteLocation(locationn);
                dataGridView.Rows.Remove(row);
            }
            else
            {
                if (string.IsNullOrEmpty(locationn.Name) || locationn.Name.Length >= 127 || locationn.Description == null || locationn.Description == "")
                {
                    if (string.IsNullOrEmpty(locationn.Name)|| locationn.Name.Length >= 127)
                    {
                        AddToErrorList(row.Index, "Besoin d'un nom d'un maximum de 126 caractère");
                    }

                    if (string.IsNullOrEmpty(locationn.Description))
                    {
                        AddToErrorList(row.Index, "Besoin d'une description");
                    }

                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    if (locationn.Id != 0)
                    {
                        _db.UpdateLocation(locationn);
                    }
                    else
                    {
                        _db.InsertLocation(ref locationn);
                        row.Cells["columnId"].Value = locationn.Id;
                    }
                }
            }
        }
        private void ValidateUnit(DataGridViewRow row)
        {
            Unit unit = new Unit(
                (int)(row.Cells["columnId"].Value ?? 0),
                (string)row.Cells["columnName"].Value,
                (string)row.Cells["columnShortName"].Value,
                (string)row.Cells["columnDescription"].Value
            );
            if ((bool)row.Cells["columnDelete"].Value)
            {
                _db.DeleteUnit(unit);
                dataGridView.Rows.Remove(row);
            }
            else
            {
                if (string.IsNullOrEmpty(unit.Name) || unit.Name.Length >= 127 || unit.Description == null || unit.Description == "" || unit.ShortName == null || unit.ShortName == "")
                {
                    if (string.IsNullOrEmpty(unit.Name) || unit.Name.Length >= 127)
                    {
                        AddToErrorList(row.Index, "Besoin d'un nom d'un maximum de 126 caractère");
                    }

                    if (string.IsNullOrEmpty(unit.Description))
                    {
                        AddToErrorList(row.Index, "Besoin d'une description");
                    }
                    if(string.IsNullOrEmpty(unit.ShortName))
                    {
                        AddToErrorList(row.Index, "Besoin d'une abréviation");
                    }

                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    if (unit.Id != 0)
                    {
                        _db.UpdateUnit(unit);
                    }
                    else
                    {
                        _db.InsertUnit(ref unit);
                        row.Cells["columnId"].Value = unit.Id;
                    }
                }
            }
        }
        private void AddToErrorList(int index, string message)
        {
            _errorList.Add("Colonne " + (index+1) + ": " + message);
        }
        private void ShowErrorList()
        {
            string errors = "";

            foreach (string error in _errorList)
            {
                errors += error + "\n";
            }

            if(errors != "") ShowErrorMessage(errors);
            _errorList.RemoveRange(0, _errorList.Count);
        }
        private void buttonNew_Click(object sender, EventArgs e)
        {
            int newIndex = dataGridView.Rows.Add();
            dataGridView.Rows[newIndex].Cells["columnDelete"].Value = false;
        }
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            switch (this.Manager)
            {
                case Managers.Providers:
                    for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                    {
                        dataGridView.Rows.Add(
                            false,
                            null,
                            dataGridView.SelectedRows[i].Cells["columnName"].Value,
                            dataGridView.SelectedRows[i].Cells["columnPhone"].Value,
                            dataGridView.SelectedRows[i].Cells["columnPhoneAlt"].Value,
                            dataGridView.SelectedRows[i].Cells["columnEmail"].Value,
                            dataGridView.SelectedRows[i].Cells["columnContact"].Value,
                            dataGridView.SelectedRows[i].Cells["columnWebsite"].Value,
                            dataGridView.SelectedRows[i].Cells["columnCity"].Value,
                            dataGridView.SelectedRows[i].Cells["columnNotes"].Value,
                            null
                        );
                    }
                    break;
                case Managers.Brands:
                    for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                    {
                        dataGridView.Rows.Add(
                            false,
                            null,
                            dataGridView.SelectedRows[i].Cells["columnName"].Value,
                            dataGridView.SelectedRows[i].Cells["columnPhone"].Value,
                            dataGridView.SelectedRows[i].Cells["columnContact"].Value,
                            dataGridView.SelectedRows[i].Cells["columnWebsite"].Value,
                            dataGridView.SelectedRows[i].Cells["columnNotes"].Value
                        );
                    }
                    break;
                case Managers.Models:
                    break;
                case Managers.Types:
                    for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                    {
                        dataGridView.Rows.Add(
                            false,
                            null,
                            dataGridView.SelectedRows[i].Cells["columnName"].Value,
                            dataGridView.SelectedRows[i].Cells["columnCategory"].Value
                        );
                    }
                    break;
                case Managers.Locations:
                    for(int i=0;i<dataGridView.SelectedRows.Count;i++)
                    {
                        dataGridView.Rows.Add(
                            false,
                            null,
                            dataGridView.SelectedRows[i].Cells["columnName"].Value,
                            dataGridView.SelectedRows[i].Cells["columnDescription"].Value
                        );
                    }
                    break;
                case Managers.Unity:
                    for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
                    {
                        dataGridView.Rows.Add(
                            false,
                            null,
                            dataGridView.SelectedRows[i].Cells["columnName"].Value,
                            dataGridView.SelectedRows[i].Cells["columnShortName"].Value,
                            dataGridView.SelectedRows[i].Cells["columnDescription"].Value
                        );
                    }
                    break;
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            switch (this.Manager)
            {
                case Managers.Providers:
                    LoopThroughGrid(ValidateProvider);
                    break;

                case Managers.Brands:
                    LoopThroughGrid(ValidateBrand);
                    break;

                case Managers.Models:
                    break;

                case Managers.Types:
                    LoopThroughGrid(ValidateType);
                    break;
                case Managers.Locations:
                    LoopThroughGrid(ValidateLocation);
                    break;
                case Managers.Unity:
                    LoopThroughGrid(ValidateUnit);
                    break;
            }
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            for(int i=0; i<dataGridView.SelectedRows.Count; i++)
            {
                if((int) dataGridView.SelectedRows[i].Cells["columnId"].Value != 1)
                {
                    dataGridView.SelectedRows[i].DefaultCellStyle.BackColor = Color.Red;
                    dataGridView.SelectedRows[i].Cells["columnDelete"].Value = true;
                }
            }
        }
        private Provider[] ResearchProvidersList(string research)
        {
            //Returning an array of providers where a field matches the searched string
            return _providers.Where(
                    t => t.Name != null && 
                    _culture.CompareInfo.IndexOf(t.Name, research, CompareOptions.IgnoreCase) >= 0 ||
                    t.Phone != null && _culture.CompareInfo.IndexOf(t.Phone, research, CompareOptions.IgnoreCase) >= 0 || 
                    t.PhoneAlt != null && _culture.CompareInfo.IndexOf(t.PhoneAlt, research, CompareOptions.IgnoreCase) >= 0 || 
                    t.Email != null && _culture.CompareInfo.IndexOf(t.Email, research, CompareOptions.IgnoreCase) >= 0 || 
                    t.Contact != null && _culture.CompareInfo.IndexOf(t.Contact, research, CompareOptions.IgnoreCase) >= 0 || 
                    t.Website != null && _culture.CompareInfo.IndexOf(t.Website, research, CompareOptions.IgnoreCase) >= 0 || 
                    t.City != null && _culture.CompareInfo.IndexOf(t.City, research, CompareOptions.IgnoreCase) >= 0 || 
                    t.Notes != null && _culture.CompareInfo.IndexOf(t.Notes, research, CompareOptions.IgnoreCase) >= 0
                ).ToArray();
        }
        private Brand[] ResearchBrandsList(string research)
        {
            //Returns an array of brands where a field matches the searched string
            return _brands.Where(t => t.Name != null && _culture.CompareInfo.IndexOf(t.Name, research, CompareOptions.IgnoreCase) >= 0 
                    || t.Phone != null && _culture.CompareInfo.IndexOf(t.Phone, research, CompareOptions.IgnoreCase) >= 0 
                    || t.Contact != null && _culture.CompareInfo.IndexOf(t.Contact, research, CompareOptions.IgnoreCase) >= 0 
                    || t.Website != null && _culture.CompareInfo.IndexOf(t.Website, research, CompareOptions.IgnoreCase) >= 0 
                    || t.Notes != null && _culture.CompareInfo.IndexOf(t.Notes, research, CompareOptions.IgnoreCase) >= 0
                ).ToArray();
        }
        private Model[] ResearchModelsList(string research)
        {
            return new Model[0];
            //TODO
            /*
            Model[] modelsFiltered;
            List<Model> modelToMerge = new List<Model>();
            for (int i = 0; i < models.Length; i++)
            {
                bool foundResearch = false;
                //Not yet implemented but could make a research here by making some if
                if (foundResearch)
                {
                    modelToMerge.Add(models[i]);
                }
            }
            modelsFiltered = modelToMerge.ToArray();
            return modelsFiltered;
            */
        }
        private Type[] ResearchTypeList(string research)
        {
            return _types.Where(t => t.Name != null && _culture.CompareInfo.IndexOf(t.Name, research, CompareOptions.IgnoreCase) >= 0).ToArray();
        }
        private Location[] ResearchLocationList(string research)
        {
            return _locations.Where(t => t.Name != null && _culture.CompareInfo.IndexOf(t.Name, research, CompareOptions.IgnoreCase) >= 0 
                    || t.Description != null && _culture.CompareInfo.IndexOf(t.Description, research, CompareOptions.IgnoreCase) >= 0
                ).ToArray();
        }
        private Unit[] ResearchUnitList(string research)
        {
            return _units.Where(t => t.Name != null && _culture.CompareInfo.IndexOf(t.Name, research, CompareOptions.IgnoreCase) >= 0 
                    || t.Description != null && _culture.CompareInfo.IndexOf(t.Description, research, CompareOptions.IgnoreCase) >= 0 
                    || t.ShortName != null && _culture.CompareInfo.IndexOf(t.ShortName, research, CompareOptions.IgnoreCase) >= 0
                ).ToArray();
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
            string textSearched = textBoxSearch.Text;
            switch (this.Manager)
            {
                case Managers.Providers:
                    UpdateListProvider(ResearchProvidersList(textSearched));
                    break;
                case Managers.Brands:
                    UpdateListBrand(ResearchBrandsList(textSearched));
                    break;
                case Managers.Models:
                    UpdateListModel(ResearchModelsList(textSearched));
                    break;
                case Managers.Types:
                    UpdateListType(ResearchTypeList(textSearched));
                    break;
                case Managers.Locations:
                    UpdateListLocation(ResearchLocationList(textSearched));
                    break;
                case Managers.Unity:
                    UpdateListUnit(ResearchUnitList(textSearched));
                    break;
            }
        }
        private void UpdateListProvider(Provider[] providerr)
        {
            Array.ForEach
                (
                    providerr.OrderBy(i => i.Id).ToArray(),
                    item => dataGridView.Rows.Add(new object[] {
                        false,
                        item.Id,
                        item.Name,
                        item.Phone,
                        item.PhoneAlt,
                        item.Email,
                        item.Contact,
                        item.Website,
                        item.City,
                        item.Notes,
                        item.CreationDate
                    })
                );
        }
        private void UpdateListBrand(Brand[] brand)
        {
            Array.ForEach
                (
                    brand.OrderBy(i => i.Id).ToArray(),
                    item => dataGridView.Rows.Add(new object[]
                    {
                        false,
                        item.Id,
                        item.Name,
                        item.Phone,
                        item.Contact,
                        item.Website,
                        item.Notes
                    })
                );
        }
        private void UpdateListModel(Model[] model)
        {
            //Code here to make the view of models
        }
        private void UpdateListType(Type[] typee)
        {
            Array.ForEach
                (
                    typee.OrderBy(i => i.Id).ToArray(),
                    item => dataGridView.Rows.Add(
                        false,
                        item.Id,
                        item.Name,
                        item.Category.Id
                    )
                );
        }
        private void UpdateListLocation(Location[] locationn)
        {
            Array.ForEach
                (
                    locationn.OrderBy(i => i.Id).ToArray(),
                    item => dataGridView.Rows.Add(
                        false,
                        item.Id,
                        item.Name,
                        item.Description
                    )
                );
        }
        private void UpdateListUnit(Unit[] unitt)
        {
            Array.ForEach
                (
                    unitt.OrderBy(i => i.Id).ToArray(),
                    item => dataGridView.Rows.Add(
                        false,
                        item.Id,
                        item.Name,
                        item.ShortName,
                        item.Description
                    )
                );
        }
        private void textBoxSearch_Enter(object sender, EventArgs e)
        {
            if(textBoxSearch.Text.Equals("Mot clés, # de modèle ou # d'objet"))
            {
                textBoxSearch.Text = "";
            }
        }
        private void textBoxSearch_Leave(object sender, EventArgs e)
        {
            if(textBoxSearch.Text.Equals(""))
            {
                textBoxSearch.Text = "Mot clés, # de modèle ou # d'objet";
                switch (this.Manager)
                {
                    case Managers.Providers:
                        UpdateListProvider(_providers);
                        break;

                    case Managers.Brands:
                        UpdateListBrand(_brands);
                        break;

                    case Managers.Models:
                        break;

                    case Managers.Types:
                        UpdateListType(_types);
                        break;

                    case Managers.Locations:
                        UpdateListLocation(_locations);
                        break;

                    case Managers.Unity:
                        UpdateListUnit(_units);
                        break;
                }
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int startCol = 0;
                int startRow = 1;
                int j = 1, i = 0;

                //Write Headers
                for (j = 1; j < dataGridView.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[startRow, startCol + j];
                    myRange.Value2 = dataGridView.Columns[j].HeaderText;
                }
                startRow++;
                //Write datagridview content
                for (i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[startRow + i, startCol + j];
                            myRange.Value2 = dataGridView[j, i].Value == null ? "" : dataGridView[j, i].Value;
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
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            var mainForm = ParentForm;
            bool isMaximised = mainForm.WindowState == FormWindowState.Maximized;
            mainForm.WindowState = FormWindowState.Normal;
            int oldWidth = mainForm.Width;
            mainForm.Width = 900;
            mainForm.Visible = false;

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Rapport";


            switch (this.Manager)
            {
                case Managers.Providers:
                    printer.Title = "Rapport des fournisseurs";
                    break;

                case Managers.Brands:
                    printer.Title = "Rapport des marques";
                    break;

                case Managers.Models:
                    printer.Title = "Rapport des modèles";
                    break;

                case Managers.Types:
                    printer.Title = "Rapport des types";
                    break;

                case Managers.Locations:
                    printer.Title = "Rapport des lieux";
                    break;

                case Managers.Unity:
                    printer.Title = "Rapport des unités de mesure";
                    break;
            }
            printer.SubTitle = $"Date: {DateTime.Now}";
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = false;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "©Excavation St-Pierre";
            printer.FooterSpacing = 15;
            printer.PrintMargins = new Margins(5, 5, 40, 40);
            printer.PrintDataGridView(dataGridView);

            if (isMaximised)
            {
                mainForm.WindowState = FormWindowState.Maximized;
                mainForm.Visible = true;
            }
        }
        
    }

    public static class Managers
    {
        public const byte Providers = 1;
        public const byte Brands = 2;
        public const byte Models = 3;
        public const byte Types = 4;
        public const byte Locations = 5;
        public const byte Unity = 6;
    }
}
