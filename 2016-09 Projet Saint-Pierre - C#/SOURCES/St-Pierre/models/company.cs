namespace StPierre.models
{
    public class Company : Model
    {
        public string Name{get;set;}
        public string Description{get;set;}

        /// <summary>
        /// Creates a new company with empty values
        /// </summary>
        public Company() : base()
        {
            this.ObjectName = "Company";
            this.Id = null;
            this.Name = null;
            this.Description = null;
        }

        /// <summary>
        /// Creates a new company with the given values
        /// </summary>
        /// <param name="id">Company id</param>
        /// <param name="name">Company name</param>
        /// <param name="description">Company notes</param>
        public Company(
            int id,
            string name,
            string description
            ) : base()
        {
            this.ObjectName = "Company";
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }
    }
}