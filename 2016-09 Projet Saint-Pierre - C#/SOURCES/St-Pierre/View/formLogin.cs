using System;
using System.Drawing;
using System.Windows.Forms;
using StPierre.database;
using StPierre.manager;
using StPierre.models;

namespace StPierre.View
{
    // Login interface that allow to access the program
    public partial class FormLogin : Form
    {
        private SqlManager _db;                  // Database manager
        private UserManager _userManager;        // User manager
        private User _user;                      // Contain the infos of the connected user

        /// <summary>
        /// Initialise the variables
        /// </summary>
        public FormLogin()
        {
            InitializeComponent();

            _db = SqlManager.GetSqlManager();
            _userManager = new UserManager();

            CreatePlaceholderTextBox(userCode, userCode.Text);
            CreatePlaceholderTextBox(adminCode, adminCode.Text);
            CreatePlaceholderTextBox(adminPassword, adminPassword.Text, true);

            _user = null;

            this.ActiveControl = userCode;
            this.Closed += (s, a)=> Application.Exit();
        }

        /// <summary>
        /// Modify a standard textbox in placeholder textbox
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="isPassword">Indicate if the text box is for a password</param>
        private void CreatePlaceholderTextBox(TextBox textBox, string placeholder, bool isPassword = false)
        {
            textBox.GotFocus += (sender, e) => EraseText(sender, e, isPassword);
            textBox.LostFocus += (sender, e) => NewText(sender, e, placeholder, isPassword);
        }

        /// <summary>
        /// Erase the placeholder text if the textbox is inactive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="isPassword">Indicate if the text box is for a password</param>
        /// <param name="passwordChar">The password char to use</param>
        private void EraseText(object sender, EventArgs e, bool isPassword = false, char passwordChar = '*')
        {
            TextBox textBox = ((TextBox)sender);

            // Change the fore color to black the text to empty if the  textbox is inactive
            if ((string)textBox.Tag == "inactive")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
                textBox.Tag = "active";

                if(isPassword)
                {
                    textBox.PasswordChar = passwordChar;
                }
            }
        }

        /// <summary>
        /// Add the placeholder text if the textbox text property is empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="placeholder">Text show in the textbox as label (placeholder)</param>
        private void NewText(object sender, EventArgs e, string placeholder, bool isPassword = false)
        {
            TextBox textBox = ((TextBox)sender);

            // Add a placeholder and change the fore color to gray of the textbox is empty
            if (textBox.Text == "")
            {
                textBox.Text = placeholder;
                textBox.ForeColor = Color.Gray;
                textBox.Tag = "inactive";

                if (isPassword)
                {
                    textBox.PasswordChar = '\0';
                }
            }
        }

        /// <summary>
        /// On click on a login button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_Click(object sender, EventArgs e)
        {
            Connection(sender as Button);
        }

        /// <summary>
        /// Match the data of the admin code and password with the users in the database
        /// </summary>
        private void Connection(Button clickedButton)
        {
            // The user is an not admin
            if (clickedButton == userLoginButton)
            {
                _user = _db.Connection(userCode.Text, _userManager.Hash(userCode.Text));
                VerifyUser("Le numéro d'employé est invalide.");
            }
            // The user is an admin
            else if (clickedButton == adminLoginButton)
            {
                _user = _db.Connection(adminCode.Text, _userManager.Hash(adminCode.Text + adminPassword.Text));
                VerifyUser("Le numéro d'employé ou le mot de passe est invalide.");
            }
        }

        /// <summary>
        /// Verify if the user informations match
        /// </summary>
        private void VerifyUser(string error)
        {
            // The user does not exist
            if (_user == null)
            {

                MessageBox.Show(error, "Erreur");
            }
            // The session is open
            else
            {
                _user.IsActive = true;
                _db.CloseUserSession(_user);

                ShowMainForm();
            }
        }
        
        /// <summary>
        /// Close the admin form and show the main form
        /// </summary>
        private void ShowMainForm()
        {
            this.Hide();
            Form form = new FormMain(_user);
            form.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginButton_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBox textBox = sender as TextBox;

                // The user is not an admin
                if(textBox == userCode)
                {
                    Connection(userLoginButton);
                }
                // The user is an admin
                else if(textBox == adminCode || textBox == adminPassword)
                {
                    Connection(adminLoginButton);
                }
            }
        }
    }
}
