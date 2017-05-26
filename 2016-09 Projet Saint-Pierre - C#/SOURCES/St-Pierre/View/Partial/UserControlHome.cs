using System;
using System.Windows.Forms;

namespace StPierre.View.Partial
{
    public partial class UserControlHome : UserControl
    {
        public UserControlHome()
        {
            InitializeComponent();
        }

        private void buttonInventory_Click(object sender, EventArgs e)
        {
            (ParentForm as FormMain).AdvanceTo(new UserControlInventory(),"Inventaire");
        }

        private void buttonProviders_Click(object sender, EventArgs e)
        {
            (ParentForm as FormMain).AdvanceTo(new UserControlGeneralManager(Managers.Providers), "Fournisseurs");
        }

        private void buttonBrands_Click(object sender, EventArgs e)
        {
            (ParentForm as FormMain).AdvanceTo(new UserControlGeneralManager(Managers.Brands), "Marques");
        }

        private void buttonModels_Click(object sender, EventArgs e)
        {
            (ParentForm as FormMain).AdvanceTo(new UserControlGeneralManager(Managers.Models), "Modèles");
        }

        private void buttonTypes_Click(object sender, EventArgs e)
        {
            (ParentForm as FormMain).AdvanceTo(new UserControlGeneralManager(Managers.Types), "Types");
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            (ParentForm as FormMain).AdvanceTo(new UserControlUserInformation(), "Utilisateurs");
        }

        private void buttonLocation_Click(object sender, EventArgs e)
        {
            (ParentForm as FormMain).AdvanceTo(new UserControlGeneralManager(Managers.Locations), "Locations");
        }

        private void buttonUnity_Click(object sender, EventArgs e)
        {
            (ParentForm as FormMain).AdvanceTo(new UserControlGeneralManager(Managers.Unity), "Unités de mesure");
        }
    }
}
