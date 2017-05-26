namespace StPierre.models
{
    public class Brand : Model
    {
        public string Name{get;set;}
        public string Phone{get;set;}
        public string Contact{get;set;}
        public string Website{get;set;}
        public string Notes{get;set;}

        /// <summary>
        /// Creates a new brand with empty values
        /// </summary>
        public Brand() : base()
        {
            ObjectName = "Brand";
            Id = null;
            Name = null;
            Phone = null;
            Contact = null;
            Website = null;
            Notes = null;
        }

        /// <summary>
        /// Creates a new brand with the given values
        /// </summary>
        /// <param name="id">Brand id</param>
        /// <param name="name">Brand name</param>
        /// <param name="phone">Brand phone number</param>
        /// <param name="contact">Brand contact name</param>
        /// <param name="website">Brand website</param>
        /// <param name="notes">Brand notes</param>
        public Brand(
            int id,
            string name,
            string phone,
            string contact,
            string website,
            string notes
            ) : base()
        {
            ObjectName = "Brand";
            Id = id;
            Name = name;
            Phone = phone;
            Contact = contact;
            Website = website;
            Notes = notes;
        }
    }
}
