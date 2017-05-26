using System;
using System.Windows.Forms;
using StPierre.View;

namespace St_Pierre
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormDatabaseConfig());
        }
    }
}
