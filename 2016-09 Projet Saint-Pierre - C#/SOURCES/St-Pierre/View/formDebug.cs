using System;
using System.Windows.Forms;
using StPierre.database;
using StPierre.models;
using Type = StPierre.models.Type;

namespace StPierre.View
{
    public partial class FormDebug : Form
    {
        private SqlManager _manager;
        public FormDebug()
        {
            InitializeComponent();
            _manager = SqlManager.GetSqlManager();
        }

        private void PerformAction(Model item) {
            if(item == null || item.Id == 0)
            {
                richTextBoxResult.Text = "Failed!";
            } else {
                richTextBoxResult.Text = "Success!\n" + item;
            }
        }
        private void PerformAction(Model[] items)
        {
            richTextBoxResult.Text = items != null ? string.Join("\n", Array.ConvertAll(items, i => i.ToString())) : "Failed!";
        }

        private void buttonFetchItem_Click(object sender, EventArgs e)
        {
            string name = "name: " + DateTime.Now.ToString();
            int id = Convert.ToInt32(numericUpDownItemFromId.Value);
            if (radioButtonBrand.Checked) {
                //Actions for brands
                if (radioButtonItemCreation.Checked)
                {
                    Brand brand = new Brand(0, name, "Phone", "contact", "website", "notes");
                    _manager.InsertBrand(ref brand);
                    this.PerformAction(brand);
                } else if (radioButtonItemFromId.Checked)
                {
                    PerformAction(_manager.SelectBrandFromId(id));
                } else
                {
                    PerformAction(_manager.SelectAllBrands());
                }
            } else if (radioButtonCategory.Checked) {
            //Actions for category
                if (radioButtonItemCreation.Checked)
                {
                    Category cat = new Category(0, name, "description");
                    _manager.InsertCategory(ref cat);
                    this.PerformAction(cat);
                } else if (radioButtonItemFromId.Checked)
                {
                    PerformAction(_manager.SelectCategoryFromId(id));
                } else
                {
                    PerformAction(_manager.SelectAllCategories());
                }

            } else if (radioButtonCompany.Checked) {
            //Actions for company
                if (radioButtonItemCreation.Checked)
                {
                    Company company = new Company(0,name,"Description");
                    _manager.InsertCompany(ref company);
                    this.PerformAction(company);
                } else if (radioButtonItemFromId.Checked)
                {
                    PerformAction(_manager.SelectCompanyFromId(id));
                } else
                {
                    PerformAction(_manager.SelectAllCompanies());
                }
            } else if (radioButtonItem.Checked) {
            //Actions for Item
                if (radioButtonItemCreation.Checked) {
                    //Create a new item
                    Item item = new Item(
                        0,
                        name,
                        "description",
                        "comments",
                        "N" + new Random().Next(999999),
                        "model",
                        DateTime.Now,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0.0,
                        "notes",
                        DateTime.Now,
                        0,
                        0,
                        "m" + new Random().Next(99999),
                        "Serial");
                    _manager.InsertItem(ref item);
                    PerformAction(item);
                }
                else if (radioButtonItemFromId.Checked) {
                    //Select an item from an id
                    Item item = _manager.SelectItemFromId(id);
                    PerformAction(item);
                }
                else {
                    //Select all items
                    Item[] items = _manager.SelectAllItems();
                    PerformAction(items);
                }

            } else if(radioButtonLocation.Checked) {
            //Actions for Location
                if (radioButtonItemCreation.Checked)
                {
                    Location location = new Location(0, name, "description");
                    _manager.InsertLocation(ref location);
                    this.PerformAction(location);
                } else if (radioButtonItemFromId.Checked)
                {
                    PerformAction(_manager.SelectLocationFromId(id));
                } else
                {
                    PerformAction(_manager.SelectAllLocations());
                }
            } else if (radioButtonProvider.Checked) {
            //Actions for Provider
                if (radioButtonItemCreation.Checked)
                {
                    Provider provider = new Provider(0, DateTime.Now, name, "phone",null,"email","contact","website","city","notes");
                    _manager.InsertProvider(ref provider);
                    this.PerformAction(provider);
                } else if (radioButtonItemFromId.Checked)
                {
                    PerformAction(_manager.SelectProviderFromId(id));
                } else
                {
                    PerformAction(_manager.SelectAllProviders());
                }

            } else if (radioButtonRole.Checked) {
            //Actions for Role
                if (radioButtonItemCreation.Checked)
                {
                    Role role = new Role(0,name,"Description");
                    _manager.InsertRole(ref role);
                    this.PerformAction(role);
                } else if (radioButtonItemFromId.Checked)
                {
                    PerformAction(_manager.SelectRoleFromId(id));
                } else
                {
                    PerformAction(_manager.SelectAllRoles());
                }
            } else if (radioButtonType.Checked) {
            //Actions for Type
                if (radioButtonItemCreation.Checked)
                {
                    Type type = new Type(0,name,0);
                    _manager.InsertType(ref type);
                    this.PerformAction(type);
                } else if (radioButtonItemFromId.Checked)
                {
                    PerformAction(_manager.SelectTypeFromId(id));
                } else
                {
                    PerformAction(_manager.SelectAllTypes());
                }

            } else if (radioButtonUnit.Checked) {
            //Actions for Unit
                if (radioButtonItemCreation.Checked)
                {
                    Unit unit = new Unit(0, name, "", "desc");
                    _manager.InsertUnit(ref unit);
                    this.PerformAction(unit);
                } else if (radioButtonItemFromId.Checked)
                {
                    PerformAction(_manager.SelectUnitFromId(id));
                } else
                {
                    PerformAction(_manager.SelectAllUnits());
                }

            } else if (radioButtonUser.Checked) {
            //Actions for User
                if (radioButtonItemCreation.Checked)
                {
                    User user = new User(0, name, "first", "last", "email", "phone", "code", "hash", DateTime.Now, false, 0);
                    _manager.InsertUser(ref user);
                    this.PerformAction(user);
                } else if (radioButtonItemFromId.Checked)
                {
                    PerformAction(_manager.SelectUserFromId(id));
                } else
                {
                    PerformAction(_manager.SelectAllUsers());
                }

            }
        }
    }
}
