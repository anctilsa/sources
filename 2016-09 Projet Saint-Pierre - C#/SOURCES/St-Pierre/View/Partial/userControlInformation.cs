using System;
using System.Drawing;
using System.Windows.Forms;
using StPierre.database;
using StPierre.models;
using Type = StPierre.models.Type;

namespace StPierre.View.Partial
{
    public partial class UserControlInformation : UserControl
    {
        public int ItemId;

        private SqlManager _db;
        private Item _item;
        private bool _error;
        private Type[] _typeIdToCategory;

        private User _user;                      // Session user

        public UserControlInformation(int id)
        {
            InitializeComponent();
            _db = SqlManager.GetSqlManager();
            _typeIdToCategory = _db.SelectAllTypes();
            ItemId = id;
            _user = null;
        }

        private void UserControlInformation_Load(object sender, EventArgs e)
        {
            PopulateComboBox();

            //Selects the item and the FK in the combo box's and text fields
            _item = this._db.SelectItemFromId(ItemId);

            if (_item == null)
            {
                NewItem();
                return;
            }

            /*
             * Common content
             */
            textBoxName.Text = _item.Name;
            textBoxNo.Text = _item.Number;
            textBoxModel.Text = _item.Model;
            richTextBoxNotes.Text = _item.Description;
            numericUpDownYear.Value = _item.Year ?? 1900;
            textBoxValue.Text = _item?.Value.ToString();
            richTextBoxComments.Text = _item?.Comments;
            //Brand
            if (_item.BrandId.HasValue)
            {
                comboBoxBrand.SelectedValue = _item.BrandId;
            }
            //Provider
            if (_item.ProviderId.HasValue)
            {
                comboBoxProvider.SelectedValue = _item.Provider.Id;
            }
            //Type
            if (_item.TypeId.HasValue)
            {
                comboBoxType.SelectedValue = _item.TypeId.Value;
            }
            //location
            if (_item.LocationId.HasValue)
            {
                comboBoxLocation.SelectedValue = _item.LocationId.Value;
            }
            //Company
            if (_item.CompanyId.HasValue)
            {
                comboBoxCompany.SelectedValue = _item.CompanyId.Value;
            }
            //Unit
            if(_item.UnitId.HasValue)
            {
                comboBoxUnit.SelectedValue = _item.UnitId.Value;
            }

            labelYear.Visible = false;
            numericUpDownYear.Visible = false;

            labelQuantity.Visible = false;
            numericUpDownQuantity.Visible = false;

            labelUnit.Visible = false;
            comboBoxUnit.Visible = false;

            labelSerialNo.Visible = false;
            textBoxSerialNo.Visible = false;

            labelLicense.Visible = false;
            textBoxLicense.Visible = false;

            if (_item.BrandId.HasValue)
            {
                int index = _item.BrandId.Value;
                int? categoryId = _typeIdToCategory[index].CategoryId;
                FieldVisibleTypeId(categoryId.Value);
            }

            ApplyUserRight();
        }

        /// <summary>
        /// Populate the comboboxes with the other models
        ///     *  And set the selected index to the item FK
        ///     * This includes:
        ///     *   - Brand
        ///     *   - Model
        ///     *   - Provider
        ///     *   - Company
        ///     *   - Type
        ///     *   - Location
        ///     *   - Unit(for pieces)
        /// </summary>
        public void PopulateComboBox()
        {
            //Populating the BRAND combobox
            Brand[] brands = _db.SelectAllBrands();
            comboBoxBrand.DisplayMember = "name";
            comboBoxBrand.ValueMember = "id";
            comboBoxBrand.DataSource = brands;

            //Populating the provider combobox
            Provider[] providers = _db.SelectAllProviders();
            comboBoxProvider.DisplayMember = "name";
            comboBoxProvider.ValueMember = "id";
            comboBoxProvider.DataSource = providers;

            //Populating the type combobox
            Type[] types = _db.SelectAllTypes();
            comboBoxType.DisplayMember = "name";
            comboBoxType.ValueMember = "id";
            comboBoxType.DataSource = types;

            //Populating the location combobox
            Location[] locations = _db.SelectAllLocations();
            comboBoxLocation.DisplayMember = "name";
            comboBoxLocation.ValueMember = "id";
            comboBoxLocation.DataSource = locations;

            //Populating the company combobox
            Company[] companies = _db.SelectAllCompanies();
            comboBoxCompany.DisplayMember = "name";
            comboBoxCompany.ValueMember = "id";
            comboBoxCompany.DataSource = companies;

            //Populating the unit combobox
            Unit[] unit = _db.SelectAllUnits();
            comboBoxUnit.DisplayMember = "name";
            comboBoxUnit.ValueMember = "id";
            comboBoxUnit.DataSource = unit;
        }

        /// <summary>
        /// Apply the user right depending of his role id
        /// </summary>
        private void ApplyUserRight()
        {
            FormMain formMain = (ParentForm as FormMain);

            _user = formMain.GetUser();

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
        /// Update or add an item in the database
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_item.Id.HasValue&& _item.Id != 0)
            {
                _db.UpdateItem(_item);
                (ParentForm as FormMain).BreadCrumbs.Peek().Item2.Text = _item.Name;
                (ParentForm as FormMain).BreadCrumbs.Peek().Item2.Width = (ParentForm as FormMain).BreadCrumbs.Peek().Item2.PreferredWidth;
            }
            else
            {
                _error = false;

                ValideName();
                if (_error) { return; }
                ValideType();
                if (_error) { return; }
                ValideNumber();
                if (_error) { return; }
                ValideDescription();
                if (_error) { return; }
                ValideModel();

                if (_error == false)
                {
                    _item.CreationDate = DateTime.Now;
                    _db.InsertItem(ref _item);

                    buttonDelete.Enabled = true;
                    buttonCopy.Enabled = true;
                    buttonCompatibility.Enabled = true;

                    (ParentForm as FormMain).BreadCrumbs.Peek().Item2.Text = _item.Name;
                    (ParentForm as FormMain).BreadCrumbs.Peek().Item2.Width = (ParentForm as FormMain).BreadCrumbs.Peek().Item2.PreferredWidth;
                }

            }
        }

        /// <summary>
        /// Display an error message with the specific message
        /// </summary>
        private static void ShowErrorMessage(string message, string title = "Erreur!")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Validating and updating the name of the item
        /// </summary>
        private void ValidateName(object sender, EventArgs e)
        {
            ValideName();
        }

        /// <summary>
        /// Validating and updating the number of a given item
        /// </summary>
        private void ValidateNo(object sender, EventArgs e)
        {
            ValideNumber();
        }

        /// <summary>
        /// Updates the brand based on the new selected value
        /// </summary>
        private void comboBoxBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_item != null)
            {
                _item.BrandId = (int)comboBoxBrand.SelectedValue;
            }
        }

        /// <summary>
        /// Validate and update the date of an item
        /// </summary>
        private void numericUpDownYear_ValueChanged(object sender, EventArgs e)
        {
            _item.Year = Convert.ToInt32(numericUpDownYear.Value);
        }

        /// <summary>
        /// Updates the description based on the new selected value
        /// </summary>
        private void richTextBoxNotes_Leave(object sender, EventArgs e)
        {
            ValideDescription();
        }

        /// <summary>
        /// Updates the year based on the new selected value
        /// </summary>
        private void numericUpDownQuantity_ValueChanged(object sender, EventArgs e)
        {
            _item.Quantity = Convert.ToInt32(numericUpDownQuantity.Value);
        }

        /// <summary>
        /// Updates the provider based on the new selected value
        /// </summary>
        private void comboBoxProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_item != null)
            {
                _item.ProviderId = (int)comboBoxProvider.SelectedValue;
            }
        }

        /// <summary>
        /// Updates the company based on the new selected value
        /// </summary>
        private void comboBoxCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_item != null)
            {
                _item.CompanyId = (int)comboBoxCompany.SelectedValue;
            }

        }

        /// <summary>
        /// Updates the unit based on the new selected value
        /// </summary>
        private void comboBoxUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_item!=null)
            {
                _item.UnitId = (int)comboBoxUnit.SelectedValue;
            }
        }

        /// <summary>
        /// Updates the type based on the new selected value
        /// </summary>
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_item != null)
            {
                ValideType();

                int index = comboBoxType.SelectedIndex;
                int? sendId = _typeIdToCategory[index].CategoryId;
                FieldVisibleTypeId((int)sendId);
            }
        }

        /// <summary>
        /// Updates the location of an item based on the new value
        /// </summary>
        private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_item != null)
            {
                _item.LocationId = (int)comboBoxLocation.SelectedValue;
            }
        }

        /// <summary>
        /// Validate the license when we leave the textfield
        /// </summary>
        private void textBoxLicense_Leave(object sender, EventArgs e) {
            string newLicense = textBoxLicense.Text;
            //Can be null but has to be under 10 chars
            if (newLicense.Length > 10) {
                ShowErrorMessage("La license doit être au maximum 10 caractères!");
            } else {
                _item.Matriculation = newLicense;
            }
        }
        /// <summary>
        /// Validate the serial number when we leave the text vox
        /// </summary>
        private void textBoxSerialNo_Leave(object sender, EventArgs e)
        {
            string newSerialNumber = textBoxSerialNo.Text;
            if (newSerialNumber.Length > 255) {
                ShowErrorMessage("Le numéro de série doit être au maximum 255 caractères!");
            } else {
                _item.SerialNumber = newSerialNumber;
            }
        }
        /// <summary>
        /// Erase all the combobox and text field and create a new item
        /// </summary>
        private void buttonNew_Click(object sender, EventArgs e)
        {
            NewItem();
        }

        /// <summary>
        /// Create a second item in the database with the same properties
        /// </summary>
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            _item.SetId(0);
            _item.Number = null;
            textBoxNo.Text = "";
            _db.InsertItem(ref _item);
        }

        /// <summary>
        /// Validate the textBoxModel if it is to long or not
        /// </summary>
        private void textBoxModel_Leave(object sender, EventArgs e)
        {
            ValideModel();
        }

        /// <summary>
        /// Delete the current item displayed
        /// </summary>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Êtes-vous sur de vouloir supprimer l'objet?",
            "Important Question",
            MessageBoxButtons.YesNo);
            Console.WriteLine(result1);
            if (result1 != DialogResult.Yes) return;
            _db.DeleteItem(_item);
            NewItem();
        }

        /// <summary>
        /// Validate the text field No
        /// </summary>
        private void ValideNumber()
        {
            string newNo = textBoxNo.Text;
            //Can be null but has to be under 10 chars
            if (newNo.Length > 10)
            {
                ShowErrorMessage("Le numéro doit être entre 0 et 10 caractères OU omis!");
                _error = true;
                labelNo.ForeColor = Color.DarkRed;
            }
            else
            {
                _item.Number = newNo;
                CheckboxChecked(labelNo);
            }
        }
        /// <summary>
        /// Validate the text field Name
        /// </summary>
        private void ValideName()
        {
            string newName = textBoxName.Text;
            if (newName.Length == 0 || newName.Length > 50)
            {
                ShowErrorMessage("Le nom doit être entre 1 et 50 caractères!");
                _error = true;
                labelName.ForeColor = Color.DarkRed;
            }
            else
            {
                _item.Name = newName;
                CheckboxChecked(labelName);
            }
        }
        /// <summary>
        /// Valide the type if it is entered or not
        /// </summary>
        private void ValideType()
        {
            if (comboBoxType.Text.Length <= 0)
            {
                ShowErrorMessage("Vous devez sélectionner un type avant l'ajout d'un objet!");
                _error = true;
                labelType.ForeColor = Color.DarkRed;
            }
            else
            {
                CheckboxChecked(labelType);
                _item.TypeId = (int)comboBoxType.SelectedValue;
            }
        }

        /// <summary>
        /// Valide the description if there is one or not
        /// </summary>
        private void ValideDescription()
        {
            if (richTextBoxNotes.Text.Length == 0)
            {
                ShowErrorMessage("Une petite description de l'objet est nécessaire pour l'ajout d'un objet!");
                _error = true;
                labelNotes.ForeColor = Color.DarkRed;
            }
            else
            {
                _item.Description = richTextBoxNotes.Text;
                CheckboxChecked(labelNotes);
            }
        }
        /// <summary>
        /// Verify if the model is null
        /// </summary>
        private void ValideModel()
        {
            if (textBoxModel.Text.Length == 0)
            {
                ShowErrorMessage("Un modèle de l'objet est nécessaire pour l'ajout d'un objet!");
                _error = true;
                labelModel.ForeColor = Color.DarkRed;
            }
            else
            {
                _item.Model = textBoxModel.Text;
                CheckboxChecked(labelModel);
            }
        }
        /// <summary>
        /// When the option highlight change, it apply all the green or black label
        /// </summary>
        private void checkBoxOptionHighlight_CheckedChanged(object sender, EventArgs e)
        {
            CheckboxChecked(labelModel);
            CheckboxChecked(labelNotes);
            CheckboxChecked(labelType);
            CheckboxChecked(labelName);
            CheckboxChecked(labelNo);
        }
        /// <summary>
        /// If the option checked is on, the label will turn green instead of black and if it is not, it will turn in black
        /// </summary>
        private void CheckboxChecked(Label labelToChange)
        {
            labelToChange.ForeColor = checkBoxOptionHighlight.Checked ? Color.Green : Color.Black;
        }
        /// <summary>
        /// Function if we want to reset all the fields and an item
        /// </summary>
        private void NewItem()
        {
            (ParentForm as FormMain).BreadCrumbs.Peek().Item2.Text = "Nouvel Objet";
            (ParentForm as FormMain).BreadCrumbs.Peek().Item2.Width = (ParentForm as FormMain).BreadCrumbs.Peek().Item2.PreferredWidth;

            textBoxName.Text = "";
            textBoxNo.Text = "";
            richTextBoxNotes.Text = "";
            textBoxModel.Text = "";
            comboBoxProvider.SelectedValue = 1;
            comboBoxBrand.SelectedValue = 1;
            numericUpDownYear.Value = 1900;
            comboBoxCompany.SelectedValue = 1;
            textBoxValue.Text = "";
            comboBoxUnit.Text = "";
            numericUpDownQuantity.Value = 0;
            textBoxSerialNo.Text = "";
            textBoxLicense.Text = "";
            comboBoxType.SelectedValue = 1;
            comboBoxLocation.SelectedValue = 1;
            dateTimePickerReception.Value = new DateTime(1900, 1, 1);
            dateTimePickerCreation.Value = DateTime.Now;
            richTextBoxComments.Text = "";

            ItemId = 0;
            _item = new Item
            {
                CompanyId = 1,
                CreationDate = dateTimePickerCreation.Value,
                BrandId = 1,
                ProviderId = 1,
                LocationId = 1,
                Comments = ""
            };


            buttonDelete.Enabled = false;
            buttonCopy.Enabled = false;
            buttonCompatibility.Enabled = false;
        }
        /// <summary>
        /// Add a comment in the rich text box and in the bd if the text field isnt 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonComment_Click(object sender, EventArgs e)
        {
            if(textBoxComment.Text.Length != 0 && _item.Id.HasValue)
            {
                string addComment;
                addComment = DateTime.Now.ToString("d-MM-yyyy");
                addComment += " : ";
                addComment += textBoxComment.Text;
                addComment += "\n";
                addComment += richTextBoxComments.Text;
                richTextBoxComments.Text = addComment;
                textBoxComment.Text = "";
                _item.Comments = richTextBoxComments.Text;
                _db.UpdateItem(_item);
            }
        }
        /// <summary>
        /// Update de comment of an item if the admin edit it
        /// </summary>
        private void richTextBoxComments_Leave(object sender, EventArgs e)
        {
            _item.Comments = richTextBoxComments.Text;
        }
        /// <summary>
        /// Disable some field and hide some if the user isnt an admin
        /// </summary>
        private void UserEnabled()
        {
            textBoxName.Enabled = false;
            textBoxNo.Enabled = false;
            richTextBoxNotes.Enabled = false;
            comboBoxBrand.Enabled = false;
            textBoxModel.Enabled = false;
            numericUpDownQuantity.Enabled = false;
            comboBoxCompany.Enabled = false;
            numericUpDownQuantity.Enabled = false;
            comboBoxUnit.Enabled = false;
            textBoxSerialNo.Enabled = false;
            textBoxLicense.Enabled = false;
            checkBoxOptionHighlight.Enabled = false;
            comboBoxType.Enabled = false;
            comboBoxLocation.Enabled = false;
            dateTimePickerReception.Enabled = false;
            richTextBoxComments.Enabled = false;
            numericUpDownYear.Enabled = false;
            buttonNew.Visible = false;
            buttonSave.Visible = false;
            buttonDelete.Visible = false;
            buttonCopy.Visible = false;
            labelValue.Visible = false;
            textBoxValue.Visible = false;
        }
        /// <summary>
        /// Enable some field and show some others if the user is an admin
        /// </summary>
        private void AdminEnabled()
        {
            textBoxName.Enabled = true;
            textBoxNo.Enabled = true;
            richTextBoxNotes.Enabled = true;
            comboBoxBrand.Enabled = true;
            textBoxModel.Enabled = true;
            numericUpDownQuantity.Enabled = true;
            comboBoxCompany.Enabled = true;
            numericUpDownQuantity.Enabled = true;
            comboBoxUnit.Enabled = true;
            textBoxSerialNo.Enabled = true;
            textBoxLicense.Enabled = true;
            checkBoxOptionHighlight.Enabled = true;
            comboBoxType.Enabled = true;
            comboBoxLocation.Enabled = true;
            dateTimePickerReception.Enabled = true;
            richTextBoxComments.Enabled = true;
            buttonNew.Visible = true;
            buttonSave.Visible = true;
            buttonDelete.Visible = true;
            buttonCopy.Visible = true;
            labelValue.Visible = true;
            textBoxValue.Visible = true;
        }

        private void buttonCompatibility_Click(object sender, EventArgs e)
        {
            UserControl userControlCompatibility = new UserControlCompatibility(_item);
            (ParentForm as FormMain).AdvanceTo(userControlCompatibility, "Compatibilité");
        }

        private void FieldVisibleTypeId(int indexType)
        {
            labelYear.Visible = false;
            numericUpDownYear.Visible = false;

            labelQuantity.Visible = false;
            numericUpDownQuantity.Visible = false;

            labelUnit.Visible = false;
            comboBoxUnit.Visible = false;

            labelSerialNo.Visible = false;
            textBoxSerialNo.Visible = false;

            labelLicense.Visible = false;
            textBoxLicense.Visible = false;

            switch (indexType)
            {
                case 1: //Equipment
                    labelLicense.Visible = true;
                    textBoxLicense.Visible = true;
                    textBoxLicense.Text = _item.Matriculation;
                    goto case 2;
                case 2: //Accessory
                    labelValue.Visible = true;
                    textBoxValue.Visible = true;
                    labelYear.Visible = true;
                    numericUpDownYear.Visible = true;
                    labelSerialNo.Visible = true;
                    textBoxSerialNo.Visible = true;
                    textBoxSerialNo.Text = _item.SerialNumber;
                    break;
                case 3: //Other
                    labelValue.Visible = true;
                    textBoxValue.Visible = true;
                    labelUnit.Visible = true;
                    comboBoxUnit.Visible = true;
                    labelQuantity.Visible = true;
                    numericUpDownQuantity.Visible = true;

                    numericUpDownQuantity.Value = _item.Quantity.HasValue ? _item.Quantity.Value : 0;
                    break;
            }
        }
    }
 }
