namespace StPierre.models
{
    public class Role : Model
    {
        public string Name { set; get; }
        public string Description { set; get; }

        public Role() : base()
        {
            this.ObjectName = "Role";
            this.Id = null;
            this.Name = null;
            this.Description = null;
        }

        public Role(
            int id,
            string name,
            string description
        ) : base()
        {
            this.ObjectName = "Role";
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }
    }
}