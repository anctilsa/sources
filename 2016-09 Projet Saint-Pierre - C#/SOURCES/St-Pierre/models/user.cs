using System;

namespace StPierre.models
{
    public class User : Model
    {
        public string Username {get; set; }
        public string FirstName {get; set; }
        public string LastName {get; set; }
        public string Email {get; set; }
        public string Phone {get; set; }
        public string PhoneAlt {get; set; }
        public string Code {get; set; }
        public string PasswordHash {get; set; }
        public string Note {get; set; }
        public DateTime? CreationDate {get; set; }
        public DateTime? ArchiveDate {get; set; }
        public bool IsActive {get; set; }
        public int? RoleId {get; set; }

        public User() : base()
        {
            this.ObjectName = "User";
            this.Id = null;
            this.Username = null;
            this.FirstName = null;
            this.LastName = null;
            this.Email = null;
            this.Phone = null;
            this.PhoneAlt = null;
            this.Code = null;
            this.PasswordHash = null;
            this.Note = null;
            this.CreationDate = null;
            this.ArchiveDate = null;
            this.IsActive = false;
            this.RoleId = null;
        }

        public User(
            int id,
            string username,
            string firstName,
            string lastName,
            string email,
            string phone,
            string code,
            string passwordHash,
            DateTime creationDate,
            bool isActive,
            int roleId,
            string note = null,
            string phoneAlt = null,
            DateTime? archiveDate = null )
        {
            this.ObjectName = "User";
            this.Id = id;
            this.Username = username;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            this.PhoneAlt = phoneAlt;
            this.Code = code;
            this.PasswordHash = passwordHash;
            this.Note = note;
            this.CreationDate = creationDate;
            this.ArchiveDate = archiveDate;
            this.IsActive = isActive;
            this.RoleId = roleId;
        }
    }
}