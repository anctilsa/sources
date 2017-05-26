using System;

namespace StPierre.models
{
    public class Item : Model
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public string Number {get; set; }
        public string Model {get; set; }
        public int? Year {get; set; }
        public double? Value {get; set; }
        public string Note {get; set; }
        public DateTime? ReceptionDate {get; set; }
        public DateTime? CreationDate {get; set; }
        //TODO: When setting a new typeId, set the type object to null
        public int? TypeId {get; set; }
        private Type _type;
        public Type Type {
            get {
                if(_type == null && TypeId.HasValue)
                {
                    _type = Db.SelectTypeFromId(TypeId.Value);
                }
                return _type;
            } set {
                if(value != null)
                {
                    TypeId = value.Id;
                }
                _type = value;
            }
        }
        public int? BrandId {get; set; }
        private Brand _brand;
        public Brand Brand {
            get {
                if(_brand == null && BrandId.HasValue)
                {
                    _brand = Db.SelectBrandFromId(BrandId.Value);
                }
                return _brand;
            } set {
                if(value != null)
                {
                    BrandId = value.Id;
                }
                _brand = value;
            }
        }
        public int? LocationId {get; set; }
        private Location _location;
        public Location Location {
            get {
                if(_location == null && LocationId.HasValue)
                {
                    _location = Db.SelectLocationFromId(LocationId.Value);
                }
                return _location;
            } set {
                if(value != null)
                {
                    LocationId = value.Id;
                }
                _location = value;
            }
        }
        public int? ProviderId {get; set; }
        private Provider _provider;
        public Provider Provider {
            get {
                if(_provider == null && ProviderId.HasValue)
                {
                    _provider = Db.SelectProviderFromId(ProviderId.Value);
                }
                return _provider;
            } set {
                if(value != null)
                {
                    ProviderId = value.Id;
                }
                _provider = value;
            }
        }
        public int? CompanyId {get; set; }
        private Company _company;
        public Company Company {
            get {
                if(_company == null && CompanyId.HasValue)
                {
                    _company = Db.SelectCompanyFromId(CompanyId.Value);
                }
                return _company;
            } set {
                if(value != null)
                {
                    CompanyId = value.Id;
                }
                _company = value;
            }
        }
        public int? UnitId {get; set; }
        private Unit _unit;
        public Unit Unit {
            get {
                if(_unit == null && UnitId.HasValue)
                {
                    _unit = Db.SelectUnitFromId(UnitId.Value);
                }
                return _unit;
            } set {
                if(value != null)
                {
                    UnitId = value.Id;
                }
                _unit = value;
            }
        }
        public int? Quantity {get; set; }
        public string Matriculation {get; set; }
        public string SerialNumber {get; set; }

        public Item() : base()
        {
            this.ObjectName = "Item";
            this.Id = null;
            this.Name = null;
            this.Description = null;
            this.Comments = null;
            this.Number = null;
            this.Model = null;
            this.Year = null;
            this.Value = null;
            this.Note = null;
            this.ReceptionDate = null;
            this.CreationDate = null;
            this.TypeId = null;
            this.BrandId = null;
            this.LocationId = null;
            this.ProviderId = null;
            this.CompanyId = null;
            this.UnitId = null;
            this.Quantity = null;
            this.Matriculation = null;
            this.SerialNumber = null;
        }

        public Item(
            int id,
            string name,
            string description,
            string comments,
            string number,
            string model,
            DateTime creationDate,
            int typeId,
            int brandId,
            int locationId,
            int providerId,
            int companyId,
            int? year,
            double? value,
            string note, 
            DateTime? receptionDate,
            int? unitId,
            int? quantity,
            string matriculation,
            string serialNumber
        ) : base() {
            this.ObjectName = "Item";
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Comments = comments;
            this.Number = number;
            this.Model = model;
            this.Year = year;
            this.Value = value;
            this.Note = note;
            this.ReceptionDate = receptionDate;
            this.CreationDate = creationDate;
            this.TypeId = typeId;
            this.BrandId = brandId;
            this.LocationId = locationId;
            this.ProviderId = providerId;
            this.CompanyId = companyId;
            this.UnitId = unitId;
            this.Quantity = quantity;
            this.Matriculation = matriculation;
            this.SerialNumber = serialNumber;
        }
    }
}
