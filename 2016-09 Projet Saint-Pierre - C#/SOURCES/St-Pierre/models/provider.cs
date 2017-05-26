using System;
namespace StPierre.models
{
    public class Provider : Model
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PhoneAlt { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Website { get; set; }
        public string City { get; set; }
        public string Notes { get; set; }
        public DateTime? CreationDate { get; set; }
        public Provider() : base()
        {
            this.ObjectName = "Provider";
            this.Id = null;
            this.Name = null;
            this.Phone = null;
            this.PhoneAlt = null;
            this.Email = null;
            this.Contact = null;
            this.Website = null;
            this.City = null;
            this.Notes = null;
            this.CreationDate = null;
        }
        public Provider(
            int id,
            DateTime creationDate,
            string name = null,
            string phone = null,
            string phoneAlt = null,
            string email = null,
            string contact = null,
            string website = null,
            string city = null,
            string notes = null
            ) : base()
        {
            this.ObjectName = "Provider";
            this.Id = id;
            this.Name = name;
            this.Phone = phone;
            this.PhoneAlt = phoneAlt;
            this.Email = email;
            this.Contact = contact;
            this.Website = website;
            this.City= city;
            this.Notes= notes;
            this.CreationDate = creationDate;
        }
    }
}