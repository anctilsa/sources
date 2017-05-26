namespace StPierre.models
{
    public class Location : Model
    {
        public string Name { set; get; }
        public string Description { set; get; }

        public Location() : base()
        {
            this.ObjectName = "Location";
            this.Id = null;
            this.Name = null;
            this.Description = null;
        }

        public Location(
            int id,
            string name,
            string description
        ) : base()
        {
            this.ObjectName = "Location";
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }
    }
}
