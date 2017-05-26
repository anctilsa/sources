using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using StPierre.helpers;
using StPierre.models;
using Type = StPierre.models.Type;

namespace StPierre.database
{
    //Singleton class
    public class SqlManager
    {
        private static SqlManager _manager;
        private Connection _db;
        public int NbResult { get; private set; }

        private SqlManager()
        {
            Credentials creds = Encryption.GetEncryptedObjectFromFile<Credentials>("database.bin");
            if(creds == null)
            {
                return;
            }
            _db = new Connection(creds);
            _db.Open();
        }

        public static SqlManager GetSqlManager()
        {
            return _manager ?? (_manager = new SqlManager());
        }
		
        /// <summary>
        /// Executes a MySqlCommand and returns the attached MySqlDataReader
        /// </summary>
        /// <param name="command">The MySqlCommand which will be executed</param>
        /// <returns>The datareader with the results of the command OR null if the command failed</returns>
        private MySqlDataReader ExecuteCommand(MySqlCommand command)
        {
            command.Connection = _db.MySqlConnection;
            try {
                return command.ExecuteReader();
            } catch(Exception ex) {
                //Todo: Log exception?
                return null;
            }
        }

        /// <summary>
        /// Executes a mysqlCommand
        /// </summary>
        /// <param name="command">The MySqlCommand which will be executed</param>
        /// <returns>The status of the command. True if it worked and had more than 0 rows affected, False if it didnt.</returns>
        private bool ExecuteCommandWithoutResult(ref MySqlCommand command)
        {
            command.Connection = _db.MySqlConnection;
            try {
                return command.ExecuteNonQuery() > 0;
            } catch(Exception ex) {
                //Todo: Log exception?
                return false;
            }
        }

        #region User operations
        public User GetUserFromResult(MySqlDataReader result)
        {
            String firstName = (!result.IsDBNull(result.GetOrdinal("firstName")) ? result.GetString("firstName") : null);
            String lastName = (!result.IsDBNull(result.GetOrdinal("lastName")) ? result.GetString("lastName") : null);
            String email = (!result.IsDBNull(result.GetOrdinal("email")) ? result.GetString("email") : null);
            String phone = (!result.IsDBNull(result.GetOrdinal("phone")) ? result.GetString("phone") : null);
            String note = (!result.IsDBNull(result.GetOrdinal("note")) ? result.GetString("note") : null);
            String phoneAlt = (!result.IsDBNull(result.GetOrdinal("phoneAlt")) ? result.GetString("phoneAlt") : null);

            DateTime? archiveDate = null;

            if(!result.IsDBNull(result.GetOrdinal("archiveDate")))
            {
                archiveDate = result.GetDateTime("archiveDate");
            }

            //Creating the new user
            User user = new User(
                result.GetInt32("id"),
                result.GetString("username"),
                firstName,
                lastName,
                email,
                phone,
                result.GetString("userCode"),
                result.GetString("passwordHash"),
                result.GetDateTime("creationDate"),
                (result.GetInt32("isActive") == 1),
                result.GetInt32("FK_Role_id"),
                note,
                phoneAlt,
                archiveDate
            );

            return user;
        }

        /// <summary>
        /// Match a password and a usercode with the a user of the database
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public User Connection(string userCode, string passwordHash)
        {
            MySqlCommand command = null;
            MySqlDataReader result = null;
            User user = null;

            command = new MySqlCommand("SELECT * FROM user WHERE userCode = '" + userCode + "' AND passwordHash = '" + passwordHash + "' AND archiveDate IS NULL");
            //command = new MySqlCommand("SELECT * FROM user WHERE userCode = '" + userCode + "' AND passwordHash = '" + passwordHash + "'");
            result = ExecuteCommand(command);

            //Either the query failed or no users exist with this id, return null
            if (result != null)
            {
                if (result.Read())
                {
                    user = GetUserFromResult(result);
                }

                result.Close();

                return user;
            }

            return null;
        }

        /// <summary>
        /// Close the user session
        /// </summary>
        /// <param name="user">The user with the new values.</param>
        /// <returns>Success status</returns>
        public bool CloseUserSession(User user)
        {
            string updateItemQuery = @"UPDATE `user` SET `isActive` = @isActive WHERE `id` = @id";

            MySqlCommand command = new MySqlCommand(updateItemQuery);

            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@isActive", user.IsActive);

            return ExecuteCommandWithoutResult(ref command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User SelectUserFromId(int id)
        {
            MySqlCommand command = null;
            MySqlDataReader result = null;
            User user = null;

            command = new MySqlCommand("SELECT * FROM user WHERE user.id = @id");
            command.Parameters.AddWithValue("@id", id);

            result = ExecuteCommand(command);

            //Either the query failed or no users exist with this id, return null
            if (result != null)
            {
                if(result.Read())
                    user = GetUserFromResult(result);
                result.Close();

                return user;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public User[] SelectAllUsers()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM user ORDER BY TRIM(lastName) ASC, TRIM(firstName) ASC");
            MySqlDataReader result = this.ExecuteCommand(command);

            //Either the query failed or no users exist with this id, return null
            if (result != null)
            {
                List<User> users = new List<User>();

                //Looping through all of the results and creating the user objects
                while (result.Read())
                {
                    users.Add(GetUserFromResult(result));
                }

                result.Close();
                return users.ToArray();
            }

            return new User[0];
        }

        public bool InsertUser(ref User user)
        {
            const string insertUserQuery = @"INSERT INTO `user` 
                    (`id`, 
                     `username`, 
                     `firstName`,
                     `lastName`,
                     `email`,
                     `phone`,
                     `phoneAlt`,
                     `userCode`,
                     `passwordHash`,
                     `note`,
                     `creationDate`,
                     `archiveDate`,
                     `isActive`,
                     `FK_Role_id`
                ) VALUES (
                     @id,
                     @username,
                     @firstName,
                     @lastName,
                     @email,
                     @phone,
                     @phoneAlt,
                     @userCode,
                     @passwordHash,
                     @note,
                     CURRENT_TIMESTAMP,
                     @archiveDate,
                     @isActive,
                     @FK_Role_id)";

            MySqlCommand command = new MySqlCommand(insertUserQuery);

            command.Parameters.AddWithValue("@id", null);
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@firstName", user.FirstName);
            command.Parameters.AddWithValue("@lastName", user.LastName);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@phone", user.Phone);
            command.Parameters.AddWithValue("@phoneAlt", user.PhoneAlt);
            command.Parameters.AddWithValue("@userCode", user.Code);
            command.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
            command.Parameters.AddWithValue("@note", user.Note);
            command.Parameters.AddWithValue("@archiveDate", user.ArchiveDate);
            command.Parameters.AddWithValue("@isActive", user.IsActive);
            command.Parameters.AddWithValue("@FK_Role_id", user.RoleId);

            if (!ExecuteCommandWithoutResult(ref command)) return false;

            user.SetId(Convert.ToInt32(command.LastInsertedId));
            return true;
        }

        /// <summary>
        /// Update the given user values
        /// </summary>
        /// <param name="user">The user with the new values.</param>
        /// <returns>Success status</returns>
        public bool UpdateUser(User user)
        {
            string updateItemQuery = @"UPDATE `user` SET 
                     `firstName`= @firstName,
                     `lastName`= @lastName,
                     `email` = @email,
                     `phone` = @phone,
                     `phoneAlt` = @phoneAlt,
                     `userCode` = @userCode,
                     `passwordHash` = @passwordHash,
                     `note` = @note,
                     `isActive` = @isActive,
                     `FK_Role_id` = @FK_Role_id ,
                     `archiveDate` = @archiveDate
            WHERE `id` = @id";

            MySqlCommand command = new MySqlCommand(updateItemQuery);
            
            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@firstName", user.FirstName);
            command.Parameters.AddWithValue("@lastName", user.LastName);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@phone", user.Phone);
            command.Parameters.AddWithValue("@phoneAlt", user.PhoneAlt);
            command.Parameters.AddWithValue("@userCode", user.Code);
            command.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
            command.Parameters.AddWithValue("@note", user.Note);
            command.Parameters.AddWithValue("@isActive", user.IsActive);
            command.Parameters.AddWithValue("@FK_Role_id", user.RoleId);
            command.Parameters.AddWithValue("@archiveDate", user.ArchiveDate);

            return ExecuteCommandWithoutResult(ref command);
        }

        /// <summary>
        /// Delete a user in the database from a user object instance
        /// </summary>
        /// <param name="user">User object use to found the user in the database</param>
        /// <returns></returns>
        public bool DeleteUser(User user)
        {
            string deleteUserQuery = "DELETE FROM user WHERE id = @id";
            MySqlCommand command = new MySqlCommand(deleteUserQuery);
            command.Parameters.AddWithValue("@id", user.Id);
            return ExecuteCommandWithoutResult(ref command);
        }
        #endregion

        #region Location operations
        public Location SelectLocationFromId(int id)
        {
            string commandQuery = "SELECT * FROM location WHERE id = @id";
            MySqlCommand command = new MySqlCommand(commandQuery);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader result = ExecuteCommand(command);

            //Either the query failed or no users exist with this id, return null
            if(result == null || !result.HasRows) {
                result.Close();
                return null;
            }
            result.Read();
            Location location = new Location(
                result.GetInt32("id"),
                result.GetString("name"),
                result.GetString("description")
            );
            result.Close();
            return location;
        }

        public Location[] SelectAllLocations()
        {
            string commandQuery = "SELECT * FROM location";

            MySqlCommand command = new MySqlCommand(commandQuery);
            MySqlDataReader result = ExecuteCommand(command);
            //Either the query failed or no users exist
            if(result == null || !result.HasRows) {
                result.Close();
                return new Location[0];
            }
            List<Location> locations = new List<Location>();
            //Looping through all of the results and creating the user objects
            while (result.Read())
            {
                Location location = new Location(
                    result.GetInt32("id"),
                    result.GetString("name"),
                    result.GetString("description")
                );
                locations.Add(location);
            }
            result.Close();
            return locations.ToArray();
        }

        public bool InsertLocation(ref Location location)
        {
            const string insertLocationQuery = @"INSERT INTO `location` (
                    `id`, 
                    `name`,
                    `description`
                ) VALUES (
                    @id, 
                    @name,
                    @description
                )";
            MySqlCommand command = new MySqlCommand(insertLocationQuery);
            command.Parameters.AddWithValue("@id", null);
            command.Parameters.AddWithValue("@name", location.Name);
            command.Parameters.AddWithValue("@description", location.Description);

            if (ExecuteCommandWithoutResult(ref command))
            {
                location.SetId(Convert.ToInt32(command.LastInsertedId));
                return true;
            }
            return false;
        }

        public bool UpdateLocation(Location location)
        {
            const string updateLocationQuery = @"UPDATE `location` SET 
                    `name` = @name,
                    `description` = @description
                WHERE 
                    `id` = @id";
            MySqlCommand command = new MySqlCommand(updateLocationQuery);
            command.Parameters.AddWithValue("@id", location.Id);
            command.Parameters.AddWithValue("@name", location.Name);
            command.Parameters.AddWithValue("@description", location.Description);
            return ExecuteCommandWithoutResult(ref command);
        }

        public bool DeleteLocation(Location location)
        {
            const string deleteBrandQuery = "DELETE FROM location WHERE location.id = @id";
            MySqlCommand command = new MySqlCommand(deleteBrandQuery);
            command.Parameters.AddWithValue("@id", location.Id);
            return ExecuteCommandWithoutResult(ref command);
        }
        #endregion

        #region Item operations
        /// <summary>
        /// Selects an item from the given id
        /// </summary>
        /// <param name="id">Id of the desired item</param>
        /// <returns>The item from the given id. If the item does not exist, returns null</returns>
        public Item SelectItemFromId(int id)
        {
            string commandQuery = "SELECT * FROM item WHERE item.id = @id";
            MySqlCommand command = new MySqlCommand(commandQuery);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader result = ExecuteCommand(command);

            //Either the query failed or no users exist with this id, return null
            if(result == null || !result.HasRows) {
                result.Close();
                return null;
            }
            result.Read();
            Item item = CreateItemFromReader(result);
            result.Close();
            return item;
        }

        /// <summary>
        /// Select all of the items from the database
        /// Perhaps a paging system could be used? Otherwise with tons of items it may have a slow performance
        /// </summary>
        /// <returns>An array of items</returns>
        public Item[] SelectAllItems()
        {
            string commandQuery = "SELECT * FROM item";

            MySqlCommand command = new MySqlCommand(commandQuery);
            MySqlDataReader result = ExecuteCommand(command);
            //Either the query failed or no users exist
            if(result == null || !result.HasRows) {
                result.Close();
                return new Item[0];
            }
            List<Item> items = new List<Item>();
            //Looping through all of the results and creating the user objects
            while (result.Read())
            {
                items.Add(CreateItemFromReader(result));
            }
            result.Close();
            return items.ToArray();
        }

        private Item CreateItemFromReader(MySqlDataReader reader)
        {
            //Conditionnal values
            string[] columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToArray();
            int? year = null;
            if (!reader.IsDBNull(Array.IndexOf(columns, "year"))) {
                year = reader.GetInt32("year");
            }

            double? value = null;
            if (!reader.IsDBNull(Array.IndexOf(columns, "value"))) {
                value = reader.GetDouble("value");
            }

            DateTime? receptionDate = null;
            if (!reader.IsDBNull(Array.IndexOf(columns, "receptionDate"))) {
                receptionDate = reader.GetDateTime("receptionDate");
            }

            int? unitId = null;
            if (!reader.IsDBNull(Array.IndexOf(columns, "FK_Unit_id"))) {
                unitId = reader.GetInt32("FK_Unit_id");
            }

            int? quantity = null;
            if (!reader.IsDBNull(Array.IndexOf(columns, "quantity"))) {
                quantity = reader.GetInt32("quantity");
            }

            string comments = null;
            if (!reader.IsDBNull(Array.IndexOf(columns, "comments"))) {
                comments = reader.GetString("comments");
            }
            string matriculation = null;
            if (!reader.IsDBNull(Array.IndexOf(columns, "matriculation"))) {
                matriculation = reader.GetString("matriculation");
            }
            string serialNumber = null;
            if (!reader.IsDBNull(Array.IndexOf(columns, "serialNumber"))) {
                serialNumber = reader.GetString("serialNumber");
            }

            Item item = new Item(
                reader.GetInt32("id"),
                reader.GetString("name"),
                reader.GetString("description"),
                reader.GetString("comments"),
                reader.GetString("number"),
                reader.GetString("model"),
                reader.GetDateTime("creationDate"),
                reader.GetInt32("FK_Type_id"),
                reader.GetInt32("FK_Brand_id"),
                reader.GetInt32("FK_Location_id"),
                reader.GetInt32("FK_Provider_id"),
                reader.GetInt32("FK_Company_id"),
                year,
                value,
                comments,
                receptionDate,
                unitId,
                quantity,
                matriculation,
                serialNumber
            );
            return item;

        }
        
        /// <summary>
        /// Creates a new item with the given values
        /// </summary>
        /// <param name="item">The item to create. The id will be updated with the newly inserted id.</param>
        /// <returns>Success status</returns>
        public bool InsertItem(ref Item item)
        {
            string insertUserQuery = @"INSERT INTO `item` (
                    `id`, 
                    `name`,
                    `description`,
                    `number`,
                    `model`,
                    `year`,
                    `value`,
                    `comments`,
                    `receptionDate`,
                    `serialNumber`,
                    `matriculation`,
                    `quantity`,
                    `FK_Type_id`,
                    `FK_Brand_id`,
                    `FK_Location_id`,
                    `FK_Provider_id`,
                    `FK_Company_id`,
                    `FK_Unit_id`
                ) VALUES (
                    @id, 
                    @name,
                    @description,
                    @number,
                    @model,
                    @year,
                    @value,
                    @comments,
                    @receptionDate,
                    @serialNumber,
                    @matriculation,
                    @quantity,
                    @FK_Type_id,
                    @FK_Brand_id,
                    @FK_Location_id,
                    @FK_Provider_id,
                    @FK_Company_id,
                    @FK_Unit_id
                )";
            MySqlCommand command = new MySqlCommand(insertUserQuery);
            command.Parameters.AddWithValue("@id", null);
            command.Parameters.AddWithValue("@name", item.Name);
            command.Parameters.AddWithValue("@description", item.Description);
            command.Parameters.AddWithValue("@number", item.Number);
            command.Parameters.AddWithValue("@model", item.Model);
            command.Parameters.AddWithValue("@year", item.Year);
            command.Parameters.AddWithValue("@value", item.Value);
            command.Parameters.AddWithValue("@comments", item.Comments);
            command.Parameters.AddWithValue("@receptionDate", item.ReceptionDate);
            command.Parameters.AddWithValue("@serialNumber", item.SerialNumber);
            command.Parameters.AddWithValue("@matriculation", item.Matriculation);
            command.Parameters.AddWithValue("@quantity", item.Quantity);
            command.Parameters.AddWithValue("@FK_Type_id", item.TypeId);
            command.Parameters.AddWithValue("@FK_Brand_id", item.BrandId);
            command.Parameters.AddWithValue("@FK_Provider_id", item.ProviderId);
            command.Parameters.AddWithValue("@FK_Company_id", item.CompanyId);
            command.Parameters.AddWithValue("@FK_Location_id", item.LocationId);
            command.Parameters.AddWithValue("@FK_Unit_id", item.UnitId);

            if (ExecuteCommandWithoutResult(ref command))
            {
                item.SetId(Convert.ToInt32(command.LastInsertedId));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update the given item values
        /// </summary>
        /// <param name="item">The item with the new values.</param>
        /// <returns>Success status</returns>
        public bool UpdateItem(Item item)
        {
            string updateItemQuery = @"UPDATE `item` SET 
                    `name` = @name,
                    `description` = @description,
                    `number` = @number,
                    `model` = @model,
                    `year` = @year,
                    `value` = @value,
                    `comments` = @comments,
                    `receptionDate` = @receptionDate,
                    `creationDate` = @creationDate,
                    `serialNumber` = @serialNumber,
                    `matriculation` = @matriculation,
                    `quantity` = @quantity,
                    `FK_Type_id` = @FK_Type_id,
                    `FK_Brand_id` = @FK_Brand_id,
                    `FK_Location_id` = @FK_Location_id,
                    `FK_Provider_id` = @FK_Provider_id,
                    `FK_Company_id` = @FK_Company_id,
                    `FK_Unit_id` = @FK_Unit_id
                WHERE `id` = @id";
            MySqlCommand command = new MySqlCommand(updateItemQuery);
            command.Parameters.AddWithValue("@id", item.Id);
            command.Parameters.AddWithValue("@name", item.Name);
            command.Parameters.AddWithValue("@description", item.Description);
            command.Parameters.AddWithValue("@number", item.Number);
            command.Parameters.AddWithValue("@model", item.Model);
            command.Parameters.AddWithValue("@year", item.Year);
            command.Parameters.AddWithValue("@value", item.Value);
            command.Parameters.AddWithValue("@comments", item.Comments);
            command.Parameters.AddWithValue("@receptionDate", item.ReceptionDate);
            command.Parameters.AddWithValue("@creationDate", item.CreationDate);
            command.Parameters.AddWithValue("@serialNumber", item.SerialNumber);
            command.Parameters.AddWithValue("@matriculation", item.Matriculation);
            command.Parameters.AddWithValue("@quantity", item.Quantity);
            command.Parameters.AddWithValue("@FK_Type_id", item.TypeId);
            command.Parameters.AddWithValue("@FK_Brand_id", item.BrandId);
            command.Parameters.AddWithValue("@FK_Provider_id", item.ProviderId);
            command.Parameters.AddWithValue("@FK_Company_id", item.CompanyId);
            command.Parameters.AddWithValue("@FK_Location_id", item.LocationId);
            command.Parameters.AddWithValue("@FK_Unit_id", item.UnitId);
            return ExecuteCommandWithoutResult(ref command);
        }

        public bool DeleteItem(Item item)
        {
            string deleteItemQuery = "DELETE FROM item WHERE id = @id";
            MySqlCommand command = new MySqlCommand(deleteItemQuery);
            command.Parameters.AddWithValue("@id", item.Id);
            return ExecuteCommandWithoutResult(ref command);
        }

        /// <summary>
        /// Select all items compatible with the given item id
        /// </summary>
        /// <param name="id">The item id</param>
        /// <returns>An array of compatible items</returns>
        public Item[] SelectCompatibleItemsWithId(int id)
        {
            string selectCompatibleItems = "SELECT * FROM item_compatibility JOIN item on item.id = FK_Item1_id OR item.id = FK_Item2_id WHERE (FK_Item1_id = @id OR FK_Item2_id = @id) AND item.id != @id";
            MySqlCommand command = new MySqlCommand(selectCompatibleItems);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = ExecuteCommand(command);
            List<Item> items = new List<Item>();
            while (reader.Read())
            {
                items.Add(CreateItemFromReader(reader));
            }
            reader.Close();
            return items.ToArray();
        }

        public Item[] SelectNotCompatibleItemsWithId(int id)
        {
            string selectCompatibleItems = @"SELECT * FROM item WHERE item.id not in (
                    SELECT item.id FROM item_compatibility JOIN item on item.id = FK_Item1_id OR item.id = FK_Item2_id WHERE(FK_Item1_id = @id OR FK_Item2_id = @id)
                ) AND item.id != @id";
            MySqlCommand command = new MySqlCommand(selectCompatibleItems);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = ExecuteCommand(command);
            List<Item> items = new List<Item>();
            while (reader.Read())
            {
                items.Add(CreateItemFromReader(reader));
            }
            reader.Close();
            return items.ToArray();
        }

        public bool AddCompatibility(int item1, int item2)
        {
            string insertCompatibility = "INSERT INTO `item_compatibility` (`FK_Item1_id`, `FK_Item2_id`) VALUES (@id1, @id2);";
            MySqlCommand command = new MySqlCommand(insertCompatibility);
            command.Parameters.AddWithValue("@id1", item1);
            command.Parameters.AddWithValue("@id2", item2);
            return ExecuteCommandWithoutResult(ref command);
        }

        /// <summary>
        /// Removes the compatiblity between two items
        /// </summary>
        /// <param name="item1">The first item which has to be compatible with the second one</param>
        /// <param name="item2">The second item, compatible with the first one</param>
        /// <returns>Compatibility removal status</returns>
        public bool RemoveCompatibility(int item1, int item2)
        {
            //As both id's can be the ones of item1 or item2,
            // the query has [fk_1 = item1.id and fk_2 = item2.id] OR [fk_1 = item2.id and fk_2 = item1.id]
            //This prevents the non-removal of items in which the order is flipped.
            const string removeCompatibility = @"DELETE FROM `item_compatibility` WHERE 
                (`item_compatibility`.`FK_Item1_id` = @id1 AND 
                 `item_compatibility`.`FK_Item2_id` = @id2) 
                OR (`item_compatibility`.`FK_Item1_id` = @id2 AND 
                    `item_compatibility`.`FK_Item2_id` = @id1)";
            MySqlCommand command = new MySqlCommand(removeCompatibility);
            command.Parameters.AddWithValue("@id1", item1);
            command.Parameters.AddWithValue("@id2", item2);
            return ExecuteCommandWithoutResult(ref command);
        }
        #endregion

        #region Provider operations

        /// <summary>
        /// Creates a new provider from a MySqlDataReader object
        /// </summary>
        /// <param name="reader">The MySqlDataReader containing the information</param>
        /// <returns>The provider from the given reader</returns>
        private Provider CreateProviderFromResult(MySqlDataReader reader)
        {
            string phone = (!reader.IsDBNull(reader.GetOrdinal("phone"))) ? reader.GetString("phone") : null;
            string phoneAlt = (!reader.IsDBNull(reader.GetOrdinal("phoneAlt"))) ? reader.GetString("phoneAlt") : null;
            string email = (!reader.IsDBNull(reader.GetOrdinal("email"))) ? reader.GetString("email") : null;
            string contact = (!reader.IsDBNull(reader.GetOrdinal("contact"))) ? reader.GetString("contact") : null;
            string website = (!reader.IsDBNull(reader.GetOrdinal("website"))) ? reader.GetString("website") : null;
            string city = (!reader.IsDBNull(reader.GetOrdinal("city"))) ? reader.GetString("city") : null;
            string notes = (!reader.IsDBNull(reader.GetOrdinal("notes"))) ? reader.GetString("notes") : null;

            return new Provider(
                reader.GetInt32("id"),
                reader.GetDateTime("creationDate"),
                reader.GetString("name"),
                phone,
                phoneAlt,
                email,
                contact,
                website,
                city,
                notes );
        }

        public Provider SelectProviderFromId(int id)
        {
            string commandQuery = "SELECT * FROM provider WHERE id = @id";
            MySqlCommand command = new MySqlCommand(commandQuery);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader result = ExecuteCommand(command);

            //Either the query failed or no users exist with this id, return null
            if(result == null) return null;
            if(!result.HasRows) {
                result.Close();
                return null;
            }
            result.Read();

            Provider provider = CreateProviderFromResult(result);

            result.Close();
            return provider;
        }

        public Provider[] SelectAllProviders()
        {
            string commandQuery = "SELECT * FROM provider";

            MySqlCommand command = new MySqlCommand(commandQuery);
            MySqlDataReader result = ExecuteCommand(command);
            //Either the query failed or no users exist
            if(result == null || !result.HasRows) {
                result.Close();
                return new Provider[0];
            }
            List<Provider> providers = new List<Provider>();
            //Looping through all of the results and creating the user objects
            while (result.Read())
            {
                providers.Add(CreateProviderFromResult(result));
            }
            result.Close();
            return providers.ToArray();
        }

        /// <summary>
        /// Insert a provider into the database
        /// </summary>
        /// <param name="provider">The provider to insert.
        /// The id will be updated to reflect the newly inserted provider.</param>
        /// <returns>Success status of the insert</returns>
        public bool InsertProvider(ref Provider provider)
        {
            string insertProviderQuery = @"INSERT INTO `provider` (
                    `name`,
                    `phone`,
                    `phoneAlt`,
                    `email`,
                    `contact`,
                    `website`,
                    `city`,
                    `notes`
                ) VALUES (
                    @name,
                    @phone,
                    @phoneAlt,
                    @email,
                    @contact,
                    @website,
                    @city,
                    @notes 
                )";
            MySqlCommand command = new MySqlCommand(insertProviderQuery);
            command.Parameters.AddWithValue("@name", provider.Name);
            command.Parameters.AddWithValue("@phone", provider.Phone);
            command.Parameters.AddWithValue("@phoneAlt", provider.PhoneAlt);
            command.Parameters.AddWithValue("@email", provider.Email);
            command.Parameters.AddWithValue("@contact", provider.Contact);
            command.Parameters.AddWithValue("@website", provider.Website);
            command.Parameters.AddWithValue("@city", provider.City);
            command.Parameters.AddWithValue("@notes", provider.Notes);
            if (ExecuteCommandWithoutResult(ref command))
            {
                provider.SetId(Convert.ToInt32(command.LastInsertedId));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update a provider: Will set the database values to the ones of the current provider object.
        /// </summary>
        /// <param name="provider">Provider to be updated</param>
        /// <returns>Update status</returns>
        public bool UpdateProvider(Provider provider)
        {
			string updateProviderQuery = @"UPDATE `provider` SET 
                    `name` = @name,
                    `phone` = @phone,
                    `phoneAlt` = @phoneAlt,
                    `email` = @email,
                    `contact` = @contact,
                    `website` = @website,
                    `city` = @city,
                    `notes` = @notes
                WHERE `id` = @id";

            MySqlCommand command = new MySqlCommand(updateProviderQuery);
            command.Parameters.AddWithValue("@name", provider.Name);
            command.Parameters.AddWithValue("@phone", provider.Phone);
            command.Parameters.AddWithValue("@phoneAlt", provider.PhoneAlt);
            command.Parameters.AddWithValue("@email", provider.Email);
            command.Parameters.AddWithValue("@contact", provider.Contact);
            command.Parameters.AddWithValue("@website", provider.Website);
            command.Parameters.AddWithValue("@city", provider.City);
            command.Parameters.AddWithValue("@notes", provider.Notes);
            command.Parameters.AddWithValue("@id", provider.Id);
            return ExecuteCommandWithoutResult(ref command);
        }

        /// <summary>
        /// Deletes a given provider
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public bool DeleteProvider(Provider provider)
        {
            string deleteProviderQuery = "DELETE FROM PROVIDER WHERE provider.id = @id";
            MySqlCommand command = new MySqlCommand(deleteProviderQuery);
            command.Parameters.AddWithValue("@id", provider.Id);
            return ExecuteCommandWithoutResult(ref command);
        }
        #endregion

        #region Category operations
        public Category SelectCategoryFromId(int id)
        {
            string commandQuery = "SELECT * FROM category WHERE id = @id";
            MySqlCommand command = new MySqlCommand(commandQuery);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader result = ExecuteCommand(command);

            //Either the query failed or no users exist with this id, return null
            if(result == null || !result.HasRows) {
                result.Close();
                return null;
            }
            result.Read();
            Category category = new Category(
                result.GetInt32("id"),
                result.GetString("name"),
                result.GetString("description") 
            );

            result.Close();
            return category;
        }

        public Category[] SelectAllCategories()
        {
            string commandQuery = "SELECT * FROM category";

            MySqlCommand command = new MySqlCommand(commandQuery);
            MySqlDataReader result = ExecuteCommand(command);
            //Either the query failed or no users exist
            if(result == null || !result.HasRows) {
                result.Close();
                return new Category[0];
            }
            List<Category> categories = new List<Category>();
            //Looping through all of the results and creating the user objects
            while (result.Read())
            {
                Category category = new Category(
                    result.GetInt32("id"),
                    result.GetString("name"),
                    result.GetString("description") 
                );
                categories.Add(category);
            }
            result.Close();
            return categories.ToArray();
        }

        public bool InsertCategory(ref Category category)
        {
            return false;
        }

        public bool UpdateCategory(Category category)
        {
            return false;
        }

        public bool DeleteCategory(Category category)
        {
            const string deleteCategoryQuery = "DELETE FROM Category WHERE category.id = @id";
            MySqlCommand command = new MySqlCommand(deleteCategoryQuery);
            command.Parameters.AddWithValue("@id", category.Id);
            return ExecuteCommandWithoutResult(ref command);
        }
        #endregion

        #region Type operations
        /// <summary>
        /// Selects a type from the given id
        /// </summary>
        /// <param name="id">Id of the desired type</param>
        /// <returns>The type from the given id. If the type does not exist, returns null</returns>
        public Type SelectTypeFromId(int id)
        {
            string commandQuery = "SELECT * FROM type WHERE type.id = @id";
            MySqlCommand command = new MySqlCommand(commandQuery);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader result = ExecuteCommand(command);

            //Either the query failed or no users exist with this id, return null
            if(result == null || !result.HasRows) {
                result.Close();
                return null;
            }
            result.Read();

            Type type = new Type(
                result.GetInt32("id"),
                result.GetString("name"),
                result.GetInt32("FK_Category_id")
            );
            result.Close();
            return type;
        }

        /// <summary>
        /// Select all of the types from the database
        /// Perhaps a paging system could be used? Otherwise with tons of types it may have a slow performance
        /// </summary>
        /// <returns>An array of types</returns>
        public Type[] SelectAllTypes()
        {
            string commandQuery = "SELECT * FROM type";

            MySqlCommand command = new MySqlCommand(commandQuery);
            MySqlDataReader result = ExecuteCommand(command);
            //Either the query failed or no users exist
            if(result == null || !result.HasRows) {
                result.Close();
                return new Type[0];
            }
            List<Type> types = new List<Type>();
            //Looping through all of the results and creating the user objects
            while (result.Read())
            {
                Type type = new Type(
                    result.GetInt32("id"),
                    result.GetString("name"),
                    result.GetInt32("FK_Category_id")
                );
                types.Add(type);
            }
            result.Close();
            return types.ToArray();
        }

        /// <summary>
        /// Creates a type type with the given values
        /// </summary>
        /// <param name="type">The type to create. The id will be updated with the newly inserted id.</param>
        /// <returns>Success status</returns>
        public bool InsertType(ref Type type)
        {
            string insertUserQuery = @"INSERT INTO `type` (
                    `id`, 
                    `name`,
                    `FK_Category_id`
                ) VALUES (
                    @id, 
                    @name,
                    @categoryId
                )";
            MySqlCommand command = new MySqlCommand(insertUserQuery);
            command.Parameters.AddWithValue("@id", null);
            command.Parameters.AddWithValue("@name", type.Name);
            command.Parameters.AddWithValue("@categoryId", type.CategoryId);

            if (ExecuteCommandWithoutResult(ref command))
            {
                type.SetId(Convert.ToInt32(command.LastInsertedId));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update the given type values
        /// </summary>
        /// <param name="type">The type with the new values.</param>
        /// <returns>Success status</returns>
        public bool UpdateType(Type type)
        {
            string insertUserQuery = @"UPDATE `type` SET 
                    `name` = @name,
                    `FK_Category_id` = @categoryId
                WHERE 
                    `id` = @id";
            MySqlCommand command = new MySqlCommand(insertUserQuery);
            command.Parameters.AddWithValue("@id", type.Id);
            command.Parameters.AddWithValue("@name", type.Name);
            command.Parameters.AddWithValue("@categoryId", type.CategoryId);
            return ExecuteCommandWithoutResult(ref command);
        }

        public bool DeleteType(Type type)
        {
            const string deleteTypeQuery = "DELETE FROM TYPE WHERE type.id = @id";
            MySqlCommand command = new MySqlCommand(deleteTypeQuery);
            command.Parameters.AddWithValue("@id", type.Id);
            return ExecuteCommandWithoutResult(ref command);
        }
        #endregion

        #region Brand operations

        private Brand CreateBrandFromResult(MySqlDataReader reader)
        {
            string phone = (!reader.IsDBNull(reader.GetOrdinal("phone"))) ? reader.GetString("phone") : null;
            string contact = (!reader.IsDBNull(reader.GetOrdinal("contact"))) ? reader.GetString("contact") : null;
            string website = (!reader.IsDBNull(reader.GetOrdinal("website"))) ? reader.GetString("website") : null;
            string note = (!reader.IsDBNull(reader.GetOrdinal("note"))) ? reader.GetString("note") : null;

            return new Brand(
                reader.GetInt32("id"),
                reader.GetString("name"),
                phone,
                contact,
                website,
                note
            );
        }

        /// <summary>
        /// Selects a brand from the given id
        /// </summary>
        /// <param name="id">Id of the desired brand</param>
        /// <returns>The brand from the given id. If the brand does not exist, returns null</returns>
        public Brand SelectBrandFromId(int id)
        {
            string commandQuery = "SELECT * FROM brand WHERE brand.id = @id";
            MySqlCommand command = new MySqlCommand(commandQuery);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader result = ExecuteCommand(command);

            //Either the query failed or no users exist with this id, return null
            if(result == null || !result.HasRows) {
                result.Close();
                return null;
            }
            result.Read();

            Brand brand = CreateBrandFromResult(result);

            result.Close();
            return brand;
        }

        /// <summary>
        /// Select all of the brands from the database
        /// Perhaps a paging system could be used? Otherwise with tons of brands it may have a slow performance
        /// </summary>
        /// <returns>An array of brands</returns>
        public Brand[] SelectAllBrands()
        {
            string commandQuery = "SELECT * FROM brand";

            MySqlCommand command = new MySqlCommand(commandQuery);
            MySqlDataReader result = ExecuteCommand(command);
            //Either the query failed or no users exist
            if (result == null || !result.HasRows)
            {
                result.Close();
                return new Brand[0];
            }
            List<Brand> brands = new List<Brand>();
            //Looping through all of the results and creating the user objects
            while (result.Read())
            {
                brands.Add(CreateBrandFromResult(result));
            }
            result.Close();
            return brands.ToArray();
        }

        /// <summary>
        /// Creates a brand with the given values
        /// </summary>
        /// <param name="brand">The brand to create. The id will be updated with the newly inserted id.</param>
        /// <returns>Success status</returns>
        public bool InsertBrand(ref Brand brand)
        {
            const string insertBrandQuery = @"INSERT INTO `brand` (
                    `id`, 
                    `name`,
                    `phone`,
                    `contact`,
                    `website`,
                    `note`
                ) VALUES (
                    @id, 
                    @name,
                    @phone,
                    @contact,
                    @website,
                    @note
                )";
            MySqlCommand command = new MySqlCommand(insertBrandQuery);
            command.Parameters.AddWithValue("@id", null);
            command.Parameters.AddWithValue("@name", brand.Name);
            command.Parameters.AddWithValue("@phone", brand.Phone);
            command.Parameters.AddWithValue("@contact", brand.Contact);
            command.Parameters.AddWithValue("@website", brand.Website);
            command.Parameters.AddWithValue("@note", brand.Notes);

            if (ExecuteCommandWithoutResult(ref command))
            {
                brand.SetId(Convert.ToInt32(command.LastInsertedId));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update the given brand values
        /// </summary>
        /// <param name="brand">The brand with the new values.</param>
        /// <returns>Success status</returns>
        public bool UpdateBrand(Brand brand)
        {
            const string updateBrandQuery = @"UPDATE `Brand` SET 
                    `name` = @name,
                    `phone` = @phone,
                    `contact` = @contact,
                    `website` = @website,
                    `note` = @note
                WHERE 
                    `id` = @id";
            MySqlCommand command = new MySqlCommand(updateBrandQuery);
            command.Parameters.AddWithValue("@id", brand.Id);
            command.Parameters.AddWithValue("@name", brand.Name);
            command.Parameters.AddWithValue("@phone", brand.Phone);
            command.Parameters.AddWithValue("@contact", brand.Contact);
            command.Parameters.AddWithValue("@website", brand.Website);
            command.Parameters.AddWithValue("@note", brand.Notes);
            return ExecuteCommandWithoutResult(ref command);
        }

        public bool DeleteBrand(Brand brand)
        {
            const string deleteBrandQuery = "DELETE FROM BRAND WHERE brand.id = @id";
            MySqlCommand command = new MySqlCommand(deleteBrandQuery);
            command.Parameters.AddWithValue("@id", brand.Id);
            return ExecuteCommandWithoutResult(ref command);
        }
        #endregion

        #region Role operations
        public Role SelectRoleFromId(int id)
        {
            string commandQuery = "SELECT * FROM role WHERE id = @id";
            MySqlCommand command = new MySqlCommand(commandQuery);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader result = ExecuteCommand(command);

            //Either the query failed or no users exist with this id, return null
            if(result == null || !result.HasRows) {
                result.Close();
                return null;
            }
            result.Read();
            Role role = new Role(
                result.GetInt32("id"),
                result.GetString("name"),
                result.GetString("description") 
            );

            result.Close();
            return role;
        }

        public Role[] SelectAllRoles()
        {
            string commandquery = "select * from role";

            MySqlCommand command = new MySqlCommand(commandquery);
            MySqlDataReader result = ExecuteCommand(command);
            //either the query failed or no users exist
            if(result == null || !result.HasRows) {
                result.Close();
                return new Role[0];
            }
            List<Role> roles = new List<Role>();
            //looping through all of the results and creating the user objects
            while (result.Read())
            {
                Role role = new Role(
                    result.GetInt32("id"),
                    result.GetString("name"),
                    result.GetString("description") 
                );
                roles.Add(role);
            }
            result.Close();
            return roles.ToArray();
        }

        public bool InsertRole(ref Role role)
        {
            return false;
        }

        public bool UpdateRole(Role role)
        {
            return false;
        }

        public bool DeleteRole(Role role)
        {
            return false;
        }
        #endregion

        #region Unit operations
        public Unit SelectUnitFromId(int id)
        {
            string commandQuery = "SELECT * FROM unit WHERE id = @id";
            MySqlCommand command = new MySqlCommand(commandQuery);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader result = ExecuteCommand(command);

            //Either the query failed or no users exist with this id, return null
            if(result == null || !result.HasRows) {
                result.Close();
                return null;
            }
            result.Read();
            Unit unit = new Unit(
                result.GetInt32("id"),
                result.GetString("name"),
                result.GetString("shortName"),
                result.GetString("description") 
            );

            result.Close();
            return unit;
        }

        public Unit[] SelectAllUnits()
        {
            string commandquery = "select * from unit";

            MySqlCommand command = new MySqlCommand(commandquery);
            MySqlDataReader result = ExecuteCommand(command);
            //either the query failed or no users exist
            if(result == null || !result.HasRows) {
                result.Close();
                return new Unit[0];
            }
            List<Unit> units = new List<Unit>();
            //looping through all of the results and creating the user objects
            while (result.Read())
            {
                Unit unit = new Unit(
                    result.GetInt32("id"),
                    result.GetString("name"),
                    result.GetString("shortName"),
                    result.GetString("description") 
                );
                units.Add(unit);
            }
            result.Close();
            return units.ToArray();
        }

        public bool InsertUnit(ref Unit unit)
        {
            const string insertUnitQuery = @"INSERT INTO `unit` (
                    `id`, 
                    `name`,
                    `shortName`,
                    `description`
                ) VALUES (
                    @id, 
                    @name,
                    @shortName,
                    @description
                )";
            MySqlCommand command = new MySqlCommand(insertUnitQuery);
            command.Parameters.AddWithValue("@id", null);
            command.Parameters.AddWithValue("@name", unit.Name);
            command.Parameters.AddWithValue("@shortName", unit.ShortName);
            command.Parameters.AddWithValue("@description", unit.Description);

            if (ExecuteCommandWithoutResult(ref command))
            {
                unit.SetId(Convert.ToInt32(command.LastInsertedId));
                return true;
            }
            return false;
        }

        public bool UpdateUnit(Unit unit)
        {
            string updateUnitQuery = @"UPDATE `unit` SET 
                    `name` = @name,
                    `shortName` = @shortName,
                    `description` = @description
                WHERE 
                    `id` = @id";
            MySqlCommand command = new MySqlCommand(updateUnitQuery);
            command.Parameters.AddWithValue("@id", unit.Id);
            command.Parameters.AddWithValue("@name", unit.Name);
            command.Parameters.AddWithValue("@shortName", unit.ShortName);
            command.Parameters.AddWithValue("@description", unit.Description);
            return ExecuteCommandWithoutResult(ref command);
        }

        public bool DeleteUnit(Unit unit)
        {
            const string deleteUnitQuery = "DELETE FROM UNIT WHERE unit.id = @id";
            MySqlCommand command = new MySqlCommand(deleteUnitQuery);
            command.Parameters.AddWithValue("@id", unit.Id);
            return ExecuteCommandWithoutResult(ref command);
        }
        #endregion

        #region Company operations
        public Company SelectCompanyFromId(int id)
        {
            string commandQuery = "SELECT * FROM company WHERE id = @id";
            MySqlCommand command = new MySqlCommand(commandQuery);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader result = ExecuteCommand(command);

            //Either the query failed or no users exist with this id, return null
            if(result == null || !result.HasRows) {
                result.Close();
                return null;
            }
            result.Read();
            Company company = new Company(
                result.GetInt32("id"),
                result.GetString("name"),
                result.GetString("description") 
            );

            result.Close();
            return company;
        }

        public Company[] SelectAllCompanies()
        {
            string commandquery = "select * from company";

            MySqlCommand command = new MySqlCommand(commandquery);
            MySqlDataReader result = ExecuteCommand(command);
            //either the query failed or no users exist
            if(result == null || !result.HasRows) {
                result.Close();
                return new Company[0];
            }
            List<Company> companies = new List<Company>();
            //looping through all of the results and creating the user objects
            while (result.Read())
            {
                Company company = new Company(
                    result.GetInt32("id"),
                    result.GetString("name"),
                    result.GetString("description") 
                );
                companies.Add(company);
            }
            result.Close();
            return companies.ToArray();
        }

        public bool InsertCompany(ref Company company)
        {
            return false;
        }

        public bool UpdateCompany(Company company)
        {
            return false;
        }

        public bool DeleteCompany(Company company)
        {
            return false;
        }
        #endregion

    }
}
