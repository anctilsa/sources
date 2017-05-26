namespace StPierre.models
{
    public class Category : Model
    {
        public string Name { set; get; }
        public string Description { set; get; }

        public Category() : base()
        {
            this.ObjectName = "Category";
            this.Id = null;
            this.Name = null;
            this.Description = null;
        }

        public Category(
            int id,
            string name,
            string description
        ) : base()
        {
            this.ObjectName = "Category";
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }
    }
}