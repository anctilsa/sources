namespace StPierre.models
{
    public class Type : Model
    {
        public string Name { set; get; }
        public int? CategoryId { private set; get; }
        private Category _category;
        public Category Category {
            get {
                if(_category == null && CategoryId.HasValue)
                {
                    _category = Db.SelectCategoryFromId(CategoryId.Value);
                }
                return _category;
            } set {
                if(value != null)
                {
                    CategoryId = value.Id;
                }
                _category = value;
            }
        }

        public Type() : base()
        {
            this.ObjectName = "Type";
            this.Id = null;
            this.Name = null;
            this.CategoryId = null;
        }
        public Type(
            int id,
            string name,
            int categoryId
            ) : base()
        {
            this.ObjectName = "Type";
            this.Id = id;
            this.Name = name;
            this.CategoryId = categoryId;
        }
    }
}