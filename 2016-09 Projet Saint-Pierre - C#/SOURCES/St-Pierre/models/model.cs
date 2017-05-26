using StPierre.database;

namespace StPierre.models
{
    public abstract class Model
    {
        protected static SqlManager Db;

        protected Model()
        {
            if(Db == null)
            {
                Db = SqlManager.GetSqlManager();
            }
        }
        public int? Id { get; protected set; }
        public string ObjectName { get; protected set; }
        public void SetId(int newId)
        {
            this.Id = newId;
        }
        public override string ToString()
        {
            return this.ObjectName + " with ID " + (Id?.ToString() ?? "null");
        }
    }
}
