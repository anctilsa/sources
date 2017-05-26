namespace StPierre.models
{
    public class Unit : Model
    {
        public string Name { get; set; }
        public string ShortName;
        public string Description;
        public Unit() : base()
        {
            this.ObjectName = "Unit";
            this.Name = null;
            this.ShortName = null;
            this.Description = null;
        }
        public Unit(int id, string name, string shortName, string description) : base()
        {
            this.ObjectName = "Unit";
            this.Id = id;
            this.Name = name;
            this.ShortName = shortName;
            this.Description = description;
        }
    }
}