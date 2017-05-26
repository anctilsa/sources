using System;
using System.Drawing;
using System.Windows.Forms;
using StPierre.database;
using StPierre.helpers;
using StPierre.models;

namespace StPierre.View
{
    public partial class FormDatabaseConfig : Form
    {
        public FormDatabaseConfig()
        {
            InitializeComponent();
            Credentials creds = Encryption.GetEncryptedObjectFromFile<Credentials>("database.bin");
            if (Connection.TestConnection(creds))
            {
                AdvanceToLogin();
            }
            this.Closed += (s, a)=> Application.Exit();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            string host = textBoxHost.Text;
            string database = textBoxDatabase.Text;
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            if (!Connection.TestConnection(database, username, password, host))
            {
                labelDescription.Text = "Les informations semblent eronnées!\nVeuillez réessayer.";
                labelDescription.ForeColor = Color.DarkRed;
            }
            else
            {
                Credentials creds = new Credentials(host,database, username, password);
                Encryption.EncryptObjectInFile(creds, "database.bin");
                AdvanceToLogin();
            }
        }

        private void AdvanceToLogin()
        {
                this.Hide();
                Form form = new FormLogin();
                form.ShowDialog();
        }
    }
}
