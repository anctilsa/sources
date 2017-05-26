using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using StPierre.database;
using StPierre.manager;
using StPierre.models;
using Type = StPierre.models.Type;

namespace StPierre.View.Partial
{
    public partial class UserControlCompatibility : UserControl
    {
        public Item Item;

        private User _user;                      // Session user

        private InventoryManager _manager;
        private SqlManager _db;
        private Item[] _items;
        private Item[] _itemsCompatible;
        private Brand[] _brands;
        private Type[] _types;
        private CultureInfo _culture;

        public UserControlCompatibility()
        {
            InitializeComponent();
            
            _manager = new InventoryManager();
        }

        public UserControlCompatibility(Item item)
        {
            InitializeComponent();

            _manager = new InventoryManager();

            this.Item = item;
            labelRight.Text = this.Item.Name;
        }

        private void UserControlCompatibility_Load(object sender, EventArgs e)
        {
            this._db = SqlManager.GetSqlManager();
            _items = _db.SelectNotCompatibleItemsWithId(Item.Id.Value);
            _itemsCompatible = _db.SelectCompatibleItemsWithId(Item.Id.Value);
            _types = _db.SelectAllTypes();
            _brands = _db.SelectAllBrands();
            _culture = CultureInfo.CurrentCulture;
            DisplayGrid(dataGridViewLeft, _db.SelectNotCompatibleItemsWithId(Item.Id.Value));
            DisplayGrid(dataGridViewRight, _db.SelectCompatibleItemsWithId(Item.Id.Value));

            ApplyUserRight();
        }

        private void DisplayGrid(DataGridView dataGridView, Item[] items)
        {
            dataGridView.Rows.Clear();
            /*
             * For each item taken from the db, ordered by reception date newest first
             * Add the item in the list
             */
            Array.ForEach(
                items.OrderByDescending(i => i.ReceptionDate).ToArray(),
                item => dataGridView.Rows.Add(item.Id, item.Type.Name, item.Number, item.Name, item.Brand.Name)
            );
            dataGridView.ClearSelection();
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
        /// Disable some field and hide some if the user isnt an admin
        /// </summary>
        private void UserEnabled()
        {
            buttonAdd.Visible = false;
            buttonRemove.Visible = false;
            buttonSave.Visible = false;
            
            dataGridViewLeft.Enabled = false;
            dataGridViewLeft.ClearSelection();

            dataGridViewRight.Enabled = false;
            dataGridViewRight.ClearSelection();
        }

        /// <summary>
        /// Disable some field and hide some if the user isnt an admin
        /// </summary>
        private void AdminEnabled()
        {
            buttonAdd.Visible = true;
            buttonRemove.Visible = true;
            buttonSave.Visible = true;

            dataGridViewLeft.Enabled = true;
            dataGridViewRight.Enabled = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow selRow in dataGridViewLeft.SelectedRows.OfType<DataGridViewRow>().ToArray())
            {
                dataGridViewLeft.Rows.Remove(selRow);
                dataGridViewRight.Rows.Add(selRow);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selRow in dataGridViewRight.SelectedRows.OfType<DataGridViewRow>().ToArray())
            {
                dataGridViewRight.Rows.Remove(selRow);
                dataGridViewLeft.Rows.Add(selRow);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Item[] items = _db.SelectCompatibleItemsWithId(Item.Id.Value);

            Array.ForEach(
                items.OrderByDescending(i => i.ReceptionDate).ToArray(),
                citem => _db.RemoveCompatibility(Item.Id.Value, citem.Id.Value)
            );
            
            for (int i = 0; i < dataGridViewRight.RowCount; i++)
            {
                _db.AddCompatibility(Item.Id.Value, (int) dataGridViewRight.Rows[i].Cells[0].Value);
            }
        }

        private void textBoxSearchLeft_TextChanged(object sender, EventArgs e)
        {
            if(_items!=null)
            {
                DisplayGrid(dataGridViewLeft, ResearchitemList(textBoxSearchLeft.Text, _items));
            }
        }
        private void textBoxSearchRight_TextChanged(object sender, EventArgs e)
        {
            if(_itemsCompatible != null)
            {
                DisplayGrid(dataGridViewRight,ResearchitemList(textBoxSearchRight.Text, _itemsCompatible));
            }
        }
        private Item[] ResearchitemList(string research, Item[] compatibleItems)
        {
            return compatibleItems.Where(i => 
                    i.Name != null && _culture.CompareInfo.IndexOf(i.Name, research, CompareOptions.IgnoreCase) >= 0 
                    || i.Number != null && _culture.CompareInfo.IndexOf(i.Number, research, CompareOptions.IgnoreCase) >= 0 
                    || i.Type != null && _culture.CompareInfo.IndexOf(i.Type.Name, research, CompareOptions.IgnoreCase) >= 0 
                    || i.Brand != null && _culture.CompareInfo.IndexOf(i.Brand.Name, research, CompareOptions.IgnoreCase) >= 0
                ).ToArray();
        }

        private void textBoxSearchLeft_Enter(object sender, EventArgs e)
        {
            if (textBoxSearchLeft.Text.Equals("Mot clés, # de modèle ou # d'objet"))
            {
                textBoxSearchLeft.Text = "";
            }
        }

        private void textBoxSearchLeft_Leave(object sender, EventArgs e)
        {
            if(textBoxSearchLeft.Text.Equals(""))
            {
                textBoxSearchLeft.Text = "Mot clés, # de modèle ou # d'objet";
                DisplayGrid(dataGridViewLeft, _items);
            }
        }

        private void textBoxSearchRight_Enter(object sender, EventArgs e)
        {
            if (textBoxSearchRight.Text.Equals("Mot clés, # de modèle ou # d'objet"))
            {
                textBoxSearchRight.Text = "";
            }
        }

        private void textBoxSearchRight_Leave(object sender, EventArgs e)
        {
            if (textBoxSearchRight.Text.Equals(""))
            {
                textBoxSearchRight.Text = "Mot clés, # de modèle ou # d'objet";
                DisplayGrid(dataGridViewRight, _itemsCompatible);
            }
        }
    }
}
