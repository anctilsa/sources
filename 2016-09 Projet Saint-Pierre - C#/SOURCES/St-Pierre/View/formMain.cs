using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using StPierre.database;
using StPierre.helpers;
using StPierre.models;
using StPierre.View.Partial;
using Type = System.Type;

namespace StPierre.View
{
    public partial class FormMain : Form
    {
        /**************************************************/
        /* VARIABLES                                      */
        /**************************************************/

        public Stack<Tuple<UserControl, LinkLabel>> BreadCrumbs;
        private List<Tuple<UserControl, Stack<Tuple<UserControl, LinkLabel>>>> _history;
        private int _iterator;
        private User _user;

        private const int Admin = 1;

        /**************************************************/
        /* PUBLIC METHODS                                 */
        /**************************************************/
        /// <summary>
        /// FormMain: Creates the main form and instanciate the breadcrumbs
        /// </summary>
        public FormMain(User user)
        {
            InitializeComponent();

            //Breadcrumbs
            BreadCrumbs = new Stack<Tuple<UserControl, LinkLabel>>();
            //breadCrumbsUserControl = new Stack<UserControl>();
            //breadCrumbsLinkLabel = new Stack<LinkLabel>();

            //History
            _history = new List<Tuple<UserControl, Stack<Tuple<UserControl, LinkLabel>>>>();
            _iterator = -1;

            this._user = user;
            this.Closed += (s, a) => Application.Exit();
        }

        // Return the user session
        public User GetUser()
        {
            return _user;
        }

        /// <summary>
        /// AdvanceTo
        /// Advance the main view to the next userControl
        /// Updates the breadcrumbs menu with the given control name
        /// </summary>
        /// <param name="userControl">UserControl which will be printed on the screen</param>
        /// <param name="controlName">Usercontrol name (string) which will be printed in the breadcrumbs</param>
        public void AdvanceTo(UserControl userControl, string controlName)
        {
            LinkLabel linkLabel = new LinkLabel
            {
                Text = controlName,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12),
                AutoSize = false,
                Height = 36
            };
            linkLabel.Width = linkLabel.PreferredWidth;

            //Creating event on the fly (lambda)
            linkLabel.Click += (s, e) =>
            {
                /*
                 * The reason we use if then do/while is for the edge case
                 * of clicking on the current item in the breadcrumbs menu.
                 * If we replace the if/do/while with while, the previous/forward
                 * menu would add an entry with the current userControl, which 
                 * would be useless considering backward would bring us to
                 * the exact same place.
                 */
                if (BreadCrumbs.Peek().Item1 != userControl)
                {
                    do
                    {
                        panelHeader.Controls.Remove(BreadCrumbs.Pop().Item2);
                    } while (BreadCrumbs.Peek().Item1 != userControl);
                    GoToUserControl(userControl);
                }
            };

            //Add the breadcrumb menu
            BreadCrumbs.Push(new Tuple<UserControl, LinkLabel>(userControl, linkLabel));
            panelHeader.Controls.Add(linkLabel);

            //Navigate to the next userControl
            GoToUserControl(userControl);
        }

        /// <summary>
        /// void GoToUserControl
        /// Goes to the given usercontrol now
        /// Does not update the breadcrumbs unlike AdvanceTo
        /// </summary>
        /// <param name="userControl">Usercontrol we must advance to</param>
        public void GoToUserControl(UserControl userControl)
        {
            //Show the userControl
            ChangeDisplay(userControl);
            //Manages the previous/forward buttons
            _iterator++;
            if (_iterator != _history.Count)
            {
                _history.RemoveRange(_iterator, _history.Count - _iterator);
                buttonForward.Enabled = false;
            }
            _history.Add(new Tuple<UserControl, Stack<Tuple<UserControl, LinkLabel>>>(userControl, new Stack<Tuple<UserControl, LinkLabel>>(BreadCrumbs)));

            if (_iterator != 0)
            {
                buttonBack.Enabled = true;
            }
        }

        /**************************************************/
        /* PRIVATE METHODS                                */
        /**************************************************/

        /// <summary>
        /// Update the userControl when the back/forward button is pressed
        /// </summary>
        private void update()
        {
            UserControl userControl = _history.ElementAt(_iterator).Item1;
            ChangeDisplay(userControl);

            Stack<Tuple<UserControl, LinkLabel>> newBreadCrumbs = new Stack<Tuple<UserControl, LinkLabel>>(_history.ElementAt(_iterator).Item2);
            Stack<Tuple<UserControl, LinkLabel>> tempBreadCrumbs = new Stack<Tuple<UserControl, LinkLabel>>(_history.ElementAt(_iterator).Item2);
            Stack<LinkLabel> linkStack = new Stack<LinkLabel>();

            //Empty the links
            while (BreadCrumbs.Count != 0)
            {
                panelHeader.Controls.Remove(BreadCrumbs.Pop().Item2);
            }

            //Flip the links
            while (tempBreadCrumbs.Count != 0)
            {
                linkStack.Push(tempBreadCrumbs.Pop().Item2);
            }

            //Add the new links
            while (linkStack.Count != 0)
            {
                panelHeader.Controls.Add(linkStack.Pop());
            }

            BreadCrumbs = newBreadCrumbs;
        }

        /// <summary>
        /// changeDisplay
        /// Updates the display with the given userControl
        /// Does not validate or update the breadcrumbs menu unlike advanceTo and goToUserControl
        /// </summary>
        /// <param name="userControl">UserControl which will be showed</param>
        private void ChangeDisplay(UserControl userControl)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(userControl);
            userControl.Dock = DockStyle.Fill;
        }

        private void refresh()
        {
            UserControl userControl = (UserControl) panelMain.Controls[0];

            Type t = userControl.GetType();
            panelMain.Controls.Clear();

            if (Type.Equals(typeof(UserControlGeneralManager), t))
            {
                panelMain.Controls.Add(userControl = (UserControl)Activator.CreateInstance(t, ((UserControlGeneralManager)userControl).Manager));
            }
            else if (Type.Equals(typeof(UserControlInformation), t))
            {
                panelMain.Controls.Add(userControl = (UserControl)Activator.CreateInstance(t, ((UserControlInformation)userControl).ItemId));
            }
            else if (Type.Equals(typeof(UserControlCompatibility), t))
            {
                panelMain.Controls.Add(userControl = (UserControl)Activator.CreateInstance(t, ((UserControlCompatibility)userControl).Item));
            }
            else
            {
                panelMain.Controls.Add(userControl = (UserControl)Activator.CreateInstance(t));
            }

            userControl.Dock = DockStyle.Fill;
        }

        /**************************************************/
        /* EVENTS                                         */
        /**************************************************/

        /// <summary>
        /// Event launched when the main form is created
        /// </summary>
        private void FormMain_Load(object sender, EventArgs e)
        {
            //Checking the database connection
            Credentials creds = Encryption.GetEncryptedObjectFromFile<Credentials>("database.bin");
            //If the creds file doesn't exist or the connection isn't working,
            //Show the config form if it doesn,t work
            if(creds == null || !new Connection(creds).TestConnection() )
            {
                new FormDatabaseConfig().ShowDialog();
            }

            // If the user is an admin, he is redirect to the home view
            if(_user.RoleId == Admin)
            {
                AdvanceTo(new UserControlHome(), "Accueil");
            }
            else
            {
                AdvanceTo(new UserControlInventory(), "Inventaire");
            }
        }

        /// <summary>
        /// Event launched when the "previous" button is pressed
        /// Goes to the previous userControl and enable/disables the button according to our position
        /// </summary>
        private void buttonBack_Click(object sender, EventArgs e)
        {
            _iterator--;
            update();
            if (_iterator == 0)
            {
                buttonBack.Enabled = false;
            }

            buttonForward.Enabled = true;
        }

        /// <summary>
        /// Event launched when the "forward" button is pressed
        /// Goes to the forward userControl and enable/disables the button according to our position
        /// </summary>
        private void buttonForward_Click(object sender, EventArgs e)
        {
            _iterator++;

            update();

            if (_iterator == _history.Count - 1)
            {
                buttonForward.Enabled = false;
            }

            buttonBack.Enabled = true;
        }

        /// <summary>
        /// Close the session
        /// </summary>
        private void CloseSession()
        {
            SqlManager db = SqlManager.GetSqlManager();

            _user.IsActive = false;
            db.CloseUserSession(_user);

            _user = null;
        }

        private void logout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CloseSession();

            this.Hide();
            Form form = new FormLogin();
            form.ShowDialog();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                CloseSession();
            }
        }
    }
}