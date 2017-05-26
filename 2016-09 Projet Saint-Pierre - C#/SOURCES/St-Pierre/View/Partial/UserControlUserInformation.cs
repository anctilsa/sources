using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StPierre.database;
using StPierre.manager;
using StPierre.models;

namespace StPierre.View.Partial
{
    // User manager controller
    public partial class UserControlUserInformation : UserControl
    {
        private SqlManager _db;                  // Database manager
        private UserManager _userManager;        // User manager

        private List<User> _users;               // List of the users on the list
        private Role[] _roles;                   // Array of the role for the combo box
        private string _errorMsg;                // Error message to show

        private int _selectedIndexState;                     // State used in for the selected index event to manage the exeption                       
        private const int NormalState = 0;                  // Use as default value
        private const int UnsavedNewItemState = 1;          // Use when an item has just been created and is not saved

        private const string AdminRoleName = "Administration";
        private const string MechanicianRoleName = "Mécanicien";
        private const string AskForSaveMsg = "Les modifications n'ont pas été sauvegarder. Voulez-vous sauvegarder les modifications?";

        private static readonly Color IsOfflineLabelColor = Color.Red;
        private static readonly Color IsOnlineLabelColor = Color.Green;
        private static readonly Color NeededFieldBackColor = Color.LightCoral;
        private static readonly Color NormalFieldBackColor = SystemColors.Window;

        /// <summary>
        /// Initialize the variable
        /// </summary>
        public UserControlUserInformation()
        {
            InitializeComponent();

            usersListBox.SelectedIndexChanged += (sender, e) => SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// Initialize the variable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControlUserInformation_Load(object sender, EventArgs e)
        {
            _db = SqlManager.GetSqlManager();
            _userManager = new UserManager();

            _users = _db.SelectAllUsers().ToList().Where(u => u.ArchiveDate == null).ToList();
            usersListBox.Items.AddRange(GetItemsList().ToArray());

            if (_users.Count > 0)
            {
                usersListBox.SelectedIndex = 0;
            }

            _roles = _db.SelectAllRoles();
            roleComboBox.Items.AddRange(GetNamesFromRole());

            _errorMsg = "";

            RefreshView();
        }

        /// <summary>
        /// Return an array of all the role for the combo box
        /// </summary>
        /// <returns></returns>
        public string[] GetNamesFromRole()
        {
            string[] names = new string[_roles.Length];

            for (int i = 0; i < _roles.Length; i++)
            {
                names[i] = _roles[i].Name;
            }

            return names;
        }

        /// <summary>
        /// Return a list of list view items
        /// </summary>
        /// <returns></returns>
        public List<string> GetItemsList()
        {
            List<string> itemsList = new List<string>();

            for (int i = 0; i < _users.Count; i++)
            {
                string item = null;

                item = _users[i].FirstName + " " + _users[i].LastName;
                itemsList.Add(item);
            }

            return itemsList;
        }

        /// <summary>
        /// Update the view in function of the user role
        /// </summary>
        private void UpdatePasswordFieldsFromSelectedRole()
        {
            switch (roleComboBox.Text)
            {
                case AdminRoleName:
                    SetAdminSetting(true);

                    if (highlightOptionsCheckBox.Checked)
                    {
                        SetPasswordFielColor(NeededFieldBackColor);
                    }

                    break;

                case MechanicianRoleName:
                    SetAdminSetting(false);

                    if (highlightOptionsCheckBox.Checked)
                    {
                        SetPasswordFielColor(NormalFieldBackColor);
                    }

                    break;
            }
        }

        /// <summary>
        /// Enable or disable all text field
        /// </summary>
        /// <param name="isEnable"></param>
        private void EnableInformations(bool isEnable)
        {
            lastNameTextBox.Enabled = isEnable;
            firstNameTextBox.Enabled = isEnable;
            emailTextBox.Enabled = isEnable;
            telephoneTextBox.Enabled = isEnable;
            telephoneAltTextBox.Enabled = isEnable;
            userCodeTextBox.Enabled = isEnable;
            noteTextBox.Enabled = isEnable;
            roleComboBox.Enabled = isEnable;

            UpdatePasswordFieldsFromSelectedRole();
        }

        /// <summary>
        /// Refresh the list of users from the database
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void RefreshList()
        {
            _users = _db.SelectAllUsers().ToList().Where(u => u.ArchiveDate == null).ToList();
            UpdateList(0);
        }

        /// <summary>
        /// Update the list from local users
        /// </summary>
        /// <param name="selectedIndex"></param>
        private void UpdateList(int selectedIndex)
        {
            if (_users.Count > 0)
            {
                usersListBox.Items.Clear();
                usersListBox.Items.AddRange(GetItemsList().ToArray());
                usersListBox.SelectedIndex = selectedIndex;
            }
            else
            {
                usersListBox.Items.Clear();
            }
        }

        /// <summary>
        /// Update the information of the view from the local users
        /// </summary>
        private void UpdateInformations()
        {
            if (_users.Count > 0)
            {
                int index = usersListBox.SelectedIndex;

                noTextBox.Text = _users[index].Id.Value.ToString();
                lastNameTextBox.Text = _users[index].LastName;
                firstNameTextBox.Text = _users[index].FirstName;
                emailTextBox.Text = _users[index].Email;
                telephoneTextBox.Text = _users[index].Phone;
                telephoneAltTextBox.Text = _users[index].PhoneAlt;
                usernameTextBox.Text = _users[index].Username;
                roleComboBox.SelectedIndex = roleComboBox.FindStringExact(_db.SelectRoleFromId(_users[index].RoleId.Value).Name);
                userCodeTextBox.Text = _users[index].Code;
                noteTextBox.Text = _users[index].Note;
                DateTime creationDate = _users[index].CreationDate.Value;
                creationDateTextBox.Text = creationDate.ToString("yyyy-MM-dd");

                if (_users[index].ArchiveDate == null)
                {
                    archiveDateLabel.Visible = false;
                    archiveDateTextBox.Visible = false;
                }
                else
                {
                    archiveDateLabel.Visible = true;
                    archiveDateTextBox.Visible = true;
                    DateTime archiveDate = _users[index].ArchiveDate.Value;
                    archiveDateTextBox.Text = archiveDate.ToString("yyyy-MM-dd");
                }

                if (_users[index].IsActive == true)
                {
                    isActiveLabel.Text = "En ligne";
                    isActiveLabel.ForeColor = IsOnlineLabelColor;
                }
                else
                {
                    isActiveLabel.Text = "Hors ligne";
                    isActiveLabel.ForeColor = IsOfflineLabelColor;
                }
            }
            else
            {
                noTextBox.Text = "";
                lastNameTextBox.Text = "";
                firstNameTextBox.Text = "";
                emailTextBox.Text = "";
                telephoneTextBox.Text = "";
                telephoneAltTextBox.Text = "";
                usernameTextBox.Text = "";
                roleComboBox.SelectedIndex = 1;
                userCodeTextBox.Text = "";
                noteTextBox.Text = "";
                creationDateTextBox.Text = "";

                archiveDateLabel.Visible = false;
                archiveDateTextBox.Visible = false;
                isActiveLabel.Visible = false;

                EnableInformations(false);
            }
        }

        /// <summary>
        /// Refresh the informations of the view with the database
        /// </summary>
        private void RefreshView()
        {
            RefreshList();
            UpdateInformations();
        }

        /// <summary>
        /// Update the informations of the view with local datas
        /// </summary>
        private void UpdateView(int selectedIndex)
        {
            UpdateList(selectedIndex);
            UpdateInformations();
        }

        /// <summary>
        /// Get the information of the view
        /// </summary>
        private void UpdateUsersFromView()
        {
            User user = _users[usersListBox.SelectedIndex];
            List<Role> roles = _db.SelectAllRoles().Where(r => r.Name == roleComboBox.Text).ToList();

            user.SetId(Int32.Parse(noTextBox.Text));
            user.LastName = lastNameTextBox.Text;
            user.FirstName = firstNameTextBox.Text;
            user.Email = emailTextBox.Text;
            user.Phone = telephoneTextBox.Text;
            user.PhoneAlt = telephoneAltTextBox.Text;
            user.Username = usernameTextBox.Text;
            user.Code = userCodeTextBox.Text;
            user.Note = noteTextBox.Text;
            user.RoleId = roles[0].Id;

            switch (roleComboBox.Text)
            {
                case AdminRoleName:
                    if (confPasswordTextBox.Text == passwordTextBox.Text && passwordTextBox.Text != "")
                    {
                        _users[usersListBox.SelectedIndex].PasswordHash = _userManager.Hash(user.Code + confPasswordTextBox.Text);
                    }

                    break;

                case MechanicianRoleName:
                    _users[usersListBox.SelectedIndex].PasswordHash = _userManager.Hash(user.Code);

                    break;
            }
        }

        /// <summary>
        /// Enable or disable the news users setting
        /// </summary>
        private void SetAdminSetting(bool isAdmin)
        {
            passwordTextBox.Enabled = isAdmin;
            confPasswordTextBox.Enabled = isAdmin;

            if (!isAdmin)
            {
                passwordTextBox.Text = "";
                confPasswordTextBox.Text = "";
            }
        }

        /// <summary>
        /// When an item of the list has been clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            UpdateInformations();

            switch(_selectedIndexState)
            {
                case NormalState:
                    break;

                case UnsavedNewItemState:
                    if (listBox.SelectedIndex != listBox.Items.Count - 1 && _users.Count != 0)
                    {
                        WarningUnsavedNew();
                    }
                    break;
            }
        }

        /// <summary>
        /// Warning the user to save the unsaved new user
        /// </summary>
        private void WarningUnsavedNew()
        {
            DialogResult dialogResult = MessageBox.Show(AskForSaveMsg, "Sauvegarde", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                usersListBox.SelectedIndex = usersListBox.Items.Count - 1;
                UpdateUser(usersListBox.Items.Count - 1);
            }
            else
            {
                _users.RemoveAt(usersListBox.Items.Count - 1);
                usersListBox.Items.RemoveAt(usersListBox.Items.Count - 1);
                buttonNew.Enabled = true;
                _selectedIndexState = NormalState;
            }
        }

        /// <summary>
        /// Warning the user to save the unsaved user
        /// </summary>
        private void WarningUnsaved()
        {
            DialogResult dialogResult = MessageBox.Show(AskForSaveMsg, "Sauvegarde", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {

            }
        }

        /// <summary>
        /// Verify if the password match with the conf password
        /// </summary>
        /// <returns></returns>
        public void VerifyMatchPassword(User user)
        {
            if (passwordTextBox.Text != "" || confPasswordTextBox.Text != "")
            {
                if (passwordTextBox.Text != confPasswordTextBox.Text)
                {
                    _errorMsg = "Les mot de passes ne corespondent pas.";
                }
            }
        }

        /// <summary>
        /// Verify the input user code is not in the database
        /// </summary>
        /// <returns></returns>
        public void VerifyCode(User user)
        {
            List<User> allUsers = _db.SelectAllUsers().ToList().Where(u => u.ArchiveDate == null).ToList();

            if (user.Code == "")
            {
                _errorMsg = "Le numéro d'employé est requis.";
            }
            else
            {
                for (int i = 0; i < allUsers.Count; i++)
                {
                    if (user.Code == allUsers[i].Code && user.Id != allUsers[i].Id)
                    {
                        _errorMsg = "Le numéro d'employé existe déjà, vueillez en choisir un différent.";
                    }
                }
            }
        }

        /// <summary>
        /// Verify the informations and set an error message
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool VerirfyInformation(User user)
        {
            _errorMsg = "";

            if (user.FirstName == "") _errorMsg = "Le prénom est requis.";
            if (user.LastName == "") _errorMsg = "Le nom est requis.";
            if (user.Code == "") _errorMsg = "Le numéro d'employé est requis.";

            VerifyCode(user);
            VerifyMatchPassword(user);

            if (_errorMsg != "")
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Return a new unique user id
        /// </summary>
        /// <returns>The new user id</returns>
        public int GetNewUserId()
        {
            List<User> allUsers = _db.SelectAllUsers().ToList();
            int? id = allUsers.Max(obj => obj.Id) + 1;
            int newId = (id != null ? id.Value : 0);

            return newId;
        }

        /// <summary>
        /// Insert a new user in the database
        /// </summary>
        private void InsertNewUser(User user)
        {
            if (_users.Count == 0)
            {
                buttonDelete.Enabled = true;
                buttonSave.Enabled = true;
                EnableInformations(true);
            }

            _users.Add(user);
            usersListBox.Items.Add(user.FirstName + " " + user.LastName);

            UpdateView(usersListBox.Items.Count - 1);
            buttonNew.Enabled = false;
            _selectedIndexState = UnsavedNewItemState;
        }

        /// <summary>
        /// Update an existing user in the databse
        /// </summary>
        private void UpdateUser(int selectedIndex)
        {
            UpdateUsersFromView();

            if (VerirfyInformation(_users[selectedIndex]))
            {
                if (_selectedIndexState == UnsavedNewItemState)
                {
                    User user = _users[selectedIndex];
                    _db.InsertUser(ref user);
                }

                _db.UpdateUser(_users[selectedIndex]);

                passwordTextBox.Text = "";
                confPasswordTextBox.Text = "";

                UpdateView(usersListBox.SelectedIndex);
                DialogResult dialogResult = MessageBox.Show("L'utilisateur a été sauvegardé", "Sauvegarde", MessageBoxButtons.OK);

                buttonNew.Enabled = true;
                _selectedIndexState = NormalState;
            }

            if (_errorMsg != "")
            {
                DialogResult dialogResult = MessageBox.Show(_errorMsg, "Sauvegarde", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Archive the current user
        /// </summary>
        private void ArchiveSelectedUser()
        {
            UpdateUsersFromView();
            _users[usersListBox.SelectedIndex].ArchiveDate = DateTime.Now;

            passwordTextBox.Text = "";
            confPasswordTextBox.Text = "";

            _db.UpdateUser(_users[usersListBox.SelectedIndex]);

            _users.RemoveAt(usersListBox.SelectedIndex);

            if(_users.Count == 0)
            {
                buttonDelete.Enabled = false;
                buttonSave.Enabled = false;
                buttonNew.Enabled = true;

                usersListBox.SelectedIndex = -1;
            }
            else
            {
                buttonNew.Enabled = true;
            }

            if(_selectedIndexState == UnsavedNewItemState)
            {
                _selectedIndexState = NormalState;
            }

            RefreshView();
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNew_Click(object sender, EventArgs e)
        {
            int newId = GetNewUserId();
            User user = new User(newId, "user" + newId, "Nouveau", "", "", "", "", "", DateTime.Now, false, 2, "", "", null);

            InsertNewUser(user);
        }

        /// <summary>
        /// Save the current user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            UpdateUser(usersListBox.SelectedIndex);
        }

        /// <summary>
        ///  Delete the current user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Voulez-vous vraiment supprimer " + _users[usersListBox.SelectedIndex].FirstName + " " + _users[usersListBox.SelectedIndex].LastName + "?",
                "Mise en garde", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                ArchiveSelectedUser();
            }
        }

        /// <summary>
        /// Set the color of the password label fields
        /// </summary>
        /// <param name="color"></param>
        private void SetPasswordFielColor(Color color)
        {
            passwordTextBox.BackColor = color;
            confPasswordTextBox.BackColor = color;
        }

        /// <summary>
        /// Set the color of the needed label fields
        /// </summary>
        /// <param name="color"></param>
        private void SetNeededFieldColor(Color color)
        {
            firstNameTextBox.BackColor = color;
            lastNameTextBox.BackColor = color;
            userCodeTextBox.BackColor = color;

            if (roleComboBox.Text != AdminRoleName)
            {
                SetPasswordFielColor(NormalFieldBackColor);
            }
            else
            {
                SetPasswordFielColor(color);
            }
        }

        /// <summary>
        /// Role changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void roleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePasswordFieldsFromSelectedRole();
        }

        /// <summary>
        /// To show needed field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void highlightOptionsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox highlightOptions = (CheckBox)sender;

            if (highlightOptions.Checked)
            {
                SetNeededFieldColor(NeededFieldBackColor);
            }
            else
            {
                SetNeededFieldColor(NormalFieldBackColor);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// *** N'est pas implémenter *** 
        /// Devrait permettre d'afficher les utilisateurs archivés
        /// Devrait également permettre de pouvoir réintégrer ou de supprimer définitivement les utilisateurs archivés
        private void showArchiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}