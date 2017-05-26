using System.Globalization;
using System.Linq;
using StPierre.database;
using StPierre.models;

namespace StPierre.manager
{
    class InventoryManager
    {
        private Item[] _items;
        SqlManager _db;
    // UserControlFilter filter;

        public InventoryManager()
        {
            _db = SqlManager.GetSqlManager();
            _items = SelectAllItem();
        }

        public Item[] UpdateItemsResearch(string research)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            return _items.Where(i => culture.CompareInfo.IndexOf(i.Name, research, CompareOptions.IgnoreCase) >= 0
                    || culture.CompareInfo.IndexOf(i.Description, research, CompareOptions.IgnoreCase) >= 0
                    || culture.CompareInfo.IndexOf(i.Number, research, CompareOptions.IgnoreCase) >= 0
                    || culture.CompareInfo.IndexOf(i.Model, research, CompareOptions.IgnoreCase) >= 0
                    || (i.Note != null && culture.CompareInfo.IndexOf(i.Note, research, CompareOptions.IgnoreCase) >= 0)
                    || (i.Matriculation != null && culture.CompareInfo.IndexOf(i.Matriculation, research, CompareOptions.IgnoreCase) >= 0)
                    || (i.SerialNumber != null && culture.CompareInfo.IndexOf(i.SerialNumber, research, CompareOptions.IgnoreCase) >= 0)
                    || (i.Year.HasValue && culture.CompareInfo.IndexOf(i.Year.Value.ToString(), research, CompareOptions.IgnoreCase) >= 0)).ToArray();
        }
        //Get all the item from the database
        public Item[] SelectAllItem()
        {
            return _db.SelectAllItems();
        }
        //Get the variable item of this class
        public Item[] GetAllitems()
        {
            return _items;
        }
    }
}
