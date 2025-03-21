using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace HW_POS_Server.DataBaseData
{
    public class SQLCommandClass
    {
        SqlConnect SQLConnectClass = new SqlConnect();
        private SqlCommand Command { get; set; }
        private string CommandString { get; set; }
        private SqlDataReader reader { get; set; }

        #region Read Login DataBase
        public List<string> SelectLoginMeber()
        {
            SQLConnectClass.connectOpen();
            List<string> result = new List<string>();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT Member 
FROM LoginMember;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result.Add(reader.GetString(0));
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public List<string> SelectLoginPassword()
        {
            SQLConnectClass.connectOpen();
            List<string> result = new List<string>();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT Password 
FROM LoginMember;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result.Add(reader.GetString(0));
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public bool CheckLoginStaff(int ID, string Password)
        {
            SQLConnectClass.connectOpen();
            bool readerCheck;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ID, Password 
FROM StaffDataTable 
WHERE ID = '{ID}' and Password='{Password}' and Alive = 1;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                readerCheck = reader.Read();
                SQLConnectClass.connectionClose();
                if (readerCheck)
                    return true;
                else
                    return false;
            }
            else
            {
                System.Windows.MessageBox.Show("SQLConnection state is wrong", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return false;
            }
        }
        public string SelectStaffGender(int ID, string Password)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT Gender 
FROM StaffDataTable 
WHERE ID = '{ID}' and Password= '{Password}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    string readerString = reader.GetString(0);
                    SQLConnectClass.connectionClose();
                    return readerString;
                }
                else
                {
                    SQLConnectClass.connectionClose();
                    return null;
                }
            }
            else
                return null;
        }
        #endregion

        #region Read Staff DataBase
        public List<int> SelectStaffIDAndAlive()
        {
            SQLConnectClass.connectOpen();

            List<int> result = new List<int>();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ID 
FROM StaffDataTable 
WHERE Alive = 'True' ORDER BY ID;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result.Add(reader.GetInt32(0));
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public int SelectStaffIDFromMAX()
        {
            SQLConnectClass.connectOpen();

            int result = 0;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT max(ID) 
FROM StaffDataTable;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetInt32(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectStaffNameFromID(int ID)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT Name 
FROM StaffDataTable 
WHERE ID='{ID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectStaffGenderFromID(int ID)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT Gender 
FROM StaffDataTable 
WHERE ID='{ID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0).Trim();
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectStaffPerminssionFromID(int ID)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT Permission 
FROM StaffDataTable 
WHERE ID='{ID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectStaffNickNameFromID(int ID)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT NickName 
FROM StaffDataTable 
WHERE ID='{ID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectStaffPasswordFromID(int ID)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT Password 
FROM StaffDataTable 
WHERE ID='{ID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectStaffAddressFromID(int ID)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT Address 
FROM StaffDataTable 
WHERE ID='{ID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectStaffPhoneNumberFromID(int ID)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT PhoneNumber 
FROM StaffDataTable 
WHERE ID='{ID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectStaffPSFromID(int ID)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT PS 
FROM StaffDataTable 
WHERE ID='{ID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public int SelectStaffIDFromIsLogined()
        {
            SQLConnectClass.connectOpen();

            int result = 0;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ID 
FROM StaffDataTable 
WHERE IsLogined='True';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetInt32(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        #endregion

        #region Insert Staff Database
        public bool InsertStaffToDB(string Name, string Password, string Gender, string Permission, string NickName, string Address, string PhoneNumber, string PS)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
INSERT INTO StaffDataTable(Name, Password, Gender, Permission, NickName, Address, PhoneNumber, PS, Alive, IsLogined) 
VALUES (@Name, '{Password}', '{Gender}', '{Permission}', @NickName, @Address, '{PhoneNumber}', @PS, 'True', 'False')";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = Name;
                    Command.Parameters.Add("@NickName", System.Data.SqlDbType.NVarChar).Value = NickName;
                    Command.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar).Value = Address;
                    Command.Parameters.Add("@PS", System.Data.SqlDbType.NVarChar).Value = PS;
                    Command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException e)
                {
                    if (e.Number == 2627)
                    {
                        MessageBox.Show("密碼已重複, 請重新輸入", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else
                    {
                        MessageBox.Show("Error Generate. Details: " + e.ToString());
                        return false;
                    }

                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
            else
                return false;
        }
        #endregion

        #region Update Staff Database
        public bool UpdateStaffToDB(int ID, string Name, string Password, string Gender, string Permission, string NickName, string Address, string PhoneNumber, string PS, bool IsLogined)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
UPDATE StaffDataTable
SET 
Name = @Name, 
Password = '{Password}', 
Gender = '{Gender}', 
Permission = '{Permission}', 
NickName = @NickName, 
Address = @Address, 
PhoneNumber = '{PhoneNumber}', 
PS = @PS, 
Alive = 'True', 
IsLogined = '{IsLogined}' 
WHERE ID = {ID};";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = Name;
                    Command.Parameters.Add("@NickName", System.Data.SqlDbType.NVarChar).Value = NickName;
                    Command.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar).Value = Address;
                    Command.Parameters.Add("@PS", System.Data.SqlDbType.NVarChar).Value = PS;
                    Command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException e)
                {
                    if (e.Number == 2627)
                    {
                        MessageBox.Show("密碼已重複, 請重新輸入", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else
                    {
                        MessageBox.Show("Error Generate. Details: " + e.ToString());
                        return false;
                    }

                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
            else
                return false;
        }
        public void UpdateStaffIsLoginedToDB(int ID)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
UPDATE StaffDataTable 
SET IsLogined = 'True' 
WHERE ID = {ID};";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                    return;
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }

        public void UpdateStaffIsLogOutToDB()
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
UPDATE StaffDataTable 
SET IsLogined = 'False' 
WHERE IsLogined = 'True';";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                    return;
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }
        #endregion

        #region Delete Staff Database
        public void DeleteStaffToDB(int ID)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
UPDATE StaffDataTable 
SET Alive = 'False' 
WHERE ID = '{ID}';";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }
        #endregion

        #region Read Item Class Database
        public List<int> SelectItemClassIDAndAlive()
        {
            SQLConnectClass.connectOpen();

            List<int> result = new List<int>();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ID 
FROM ItemClass 
WHERE Alive = 'True' 
ORDER BY ID;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result.Add(reader.GetInt32(0));
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public List<int> SelectItemClassIDFromEnableAndAlive()
        {
            SQLConnectClass.connectOpen();

            List<int> result = new List<int>();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ID 
FROM ItemClass 
WHERE Enable = 1 and Alive = 1 
ORDER BY ID;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result.Add(reader.GetInt32(0));
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public int SelectItemClassIDFromItemClassName(string ItemClassName)
        {
            SQLConnectClass.connectOpen();

            int result = 0;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ID 
FROM ItemClass 
WHERE ItemClassName='{ItemClassName}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetInt32(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectItemClassNameFromID(int ID)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ItemClassName 
FROM ItemClass 
WHERE ID='{ID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public bool SelectItemClassEnableFromID(int ID)
        {
            SQLConnectClass.connectOpen();

            bool result = false;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT Enable 
FROM ItemClass 
WHERE ID='{ID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetBoolean(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectItemClassPSFromID(int ID)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT PS 
FROM ItemClass 
WHERE ID='{ID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        #endregion

        #region Insert Item Class Database
        public void InsertItemClassToDB(string ItemClassName, bool Enable, string PS)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
INSERT INTO ItemClass(ItemClassName, Enable, PS, Alive) 
VALUES ('{ItemClassName}', '{Enable}', '{PS}', 'True');";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }
        #endregion

        #region Update Item Class Database
        public void UpdateItemClassToDB(int ID, string ItemClassName, bool Enable, string PS)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
UPDATE ItemClass 
SET ItemClassName = '{ItemClassName}', Enable = '{Enable}', PS = '{PS}', Alive = 'True' 
WHERE ID = {ID};";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }
        #endregion

        #region Delete Item Class Database
        public void DeleteItemClassToDB(int ID)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
UPDATE ItemClass 
SET Alive = 'False' 
WHERE ID = '{ID}';";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }
        public void ItemClassAutoOrderReset(int ID)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
UPDATE ItemClass 
SET ID = '{ID}' 
WHERE ID = '{(ID + 1)}';";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }
        #endregion

        #region Read Item Database
        public List<int> SelectItemIDAndAlive()
        {
            SQLConnectClass.connectOpen();

            List<int> result = new List<int>();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ID 
FROM ItemDataTable 
WHERE Alive = 'True' 
ORDER BY ID;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result.Add(reader.GetInt32(0));
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public List<int> SelectItemIDFromItemClassIDAndAlive(int ItemClassID)
        {
            SQLConnectClass.connectOpen();

            List<int> result = new List<int>();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ID 
FROM ItemDataTable 
WHERE ItemClassID='{ItemClassID}' and Alive = 'True' 
ORDER BY ID;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result.Add(reader.GetInt32(0));
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public List<int> SelectItemIDFromItemClassIDAndEnableAndAlive(int ItemClassID)
        {
            SQLConnectClass.connectOpen();

            List<int> result = new List<int>();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ID 
FROM ItemDataTable 
WHERE ItemClassID='{ItemClassID}' and Enable = 1 and Alive = 1 
ORDER BY ID;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result.Add(reader.GetInt32(0));
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public int SelectItemIDFromItemName(string ItemName)
        {
            SQLConnectClass.connectOpen();

            int result = 0;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ID 
FROM ItemDataTable 
WHERE ItemName='{ItemName}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetInt32(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectItemNameFromID(int ID)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ItemName 
FROM ItemDataTable 
WHERE ID='{ID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectItemNameFromIDAndAlive(int ID)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ItemName 
FROM ItemDataTable 
WHERE ID='{ID}' and Alive = 'True';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public int SelectItemCashFromItemClassandItemID(int ItemClassID, int ItemID)
        {
            SQLConnectClass.connectOpen();

            int result = 0;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT Cash 
FROM ItemDataTable 
WHERE ItemClassID='{ItemClassID}' and ID='{ItemID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetInt32(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public bool SelectItemEnableFromItemClassandItemID(int ItemClassID, int ItemID)
        {
            SQLConnectClass.connectOpen();

            bool result = false;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT Enable 
FROM ItemDataTable 
WHERE ItemClassID='{ItemClassID}' and ID='{ItemID}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetBoolean(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectItemPSFromItemName(string ItemName)
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT PS 
FROM ItemDataTable 
WHERE ItemName='{ItemName}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        #endregion

        #region Insert Item Database
        public void InsertItemToDB(string ItemName, int ItemClassID, string ItemClass, int Cash, bool Enable, string PS)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
INSERT INTO ItemDataTable(ItemName, ItemClassID, ItemClass, Cash, Enable, PS, Alive) 
VALUES ('{ItemName}', '{ItemClassID}', '{ItemClass}', '{Cash}', '{Enable}', '{PS}', 'True');";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }
        public void ItemReSeedMaxVal()
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
DECLARE @maxVal 
INT SELECT @maxVal = ISNULL(max(ID), 0) 
FROM ItemDataTable 
DBCC CHECKIDENT('POS_System_Server.dbo.ItemDataTable', RESEED, @maxVal);";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }
        #endregion

        #region Update Item Database
        public void UpdateItemToDB(int ID, string ItemName, int ItemClassID, string ItemClass, int Cash, bool Enable, string PS)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
UPDATE ItemDataTable 
SET 
ItemName = '{ItemName}', 
ItemClassID = '{ItemClassID}', 
ItemClass = '{ItemClass}', 
Cash = '{Cash}', 
Enable = '{Enable}', 
PS = '{PS}', 
Alive = 'True' 
WHERE ID = {ID};";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }
        #endregion

        #region Delete Item Class Database
        public void DeleteItemToDB(string ItemName)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
UPDATE ItemDataTable 
SET Alive = 'False' 
WHERE ItemName = '{ItemName}';";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }
        #endregion

        #region Read Check Out List Database
        public List<string> SelectChekcOutListOrderNumber()
        {
            SQLConnectClass.connectOpen();

            List<string> result = new List<string>();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT DISTINCT OrderNumber 
FROM CheckOutList 
ORDER BY OrderNumber;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result.Add(reader.GetString(0));
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public List<string> SelectChekcOutListOrderNumberFromTrue()
        {
            SQLConnectClass.connectOpen();

            List<string> result = new List<string>();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT OrderNumber 
FROM CheckOutList 
WHERE ReportView = 'True' 
ORDER BY OrderNumber;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result.Add(reader.GetString(0));
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public DateTime SelectCheckOutListOrderDateFromOrderNumber(string OrderNumber)
        {
            SQLConnectClass.connectOpen();

            DateTime result = DateTime.UtcNow;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT DISTINCT OrderDate 
FROM CheckOutList 
WHERE OrderNumber = '{OrderNumber}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetDateTime(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public TimeSpan SelectCheckOutListOrderTimeFromOrderNumber(string OrderNumber)
        {
            SQLConnectClass.connectOpen();

            TimeSpan result = new TimeSpan(0, 0, 13, 0, 0);
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT DISTINCT OrderTime 
FROM CheckOutList 
WHERE OrderNumber = '{OrderNumber}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetTimeSpan(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public int SelectCheckOutListOrderTotalFromOrderNumber(string OrderNumber)
        {
            SQLConnectClass.connectOpen();

            int result = 0;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT DISTINCT OrderTotal 
FROM CheckOutList 
WHERE OrderNumber = '{OrderNumber}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetInt32(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public int SelectCheckOutListStaffIDFromOrderNumber(string OrderNumber)
        {
            SQLConnectClass.connectOpen();

            int result = 0;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT DISTINCT StaffID 
FROM CheckOutList 
WHERE OrderNumber = '{OrderNumber}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetInt32(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }

        #endregion

        #region Insert Check Out List Database
        public void InsertOrderListToDB(string OrderNumber, string OrderDate, string OrderTime, int OrderTotal, int StaffID)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
INSERT INTO CheckOutList(OrderNumber, OrderDate, OrderTime, OrderTotal, StaffID, ReportView) 
VALUES ('{OrderNumber}', '{OrderDate}', '{OrderTime}', '{OrderTotal}', '{StaffID}', 'False');";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                    return;
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }

        #endregion

        #region Update Check Out List Database
        public void UpdateCheckOutListReportViewToDB(bool ReportView, string StartTime, string EndTime)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
UPDATE CheckOutList 
SET ReportView = '{ReportView}' 
WHERE OrderDate BETWEEN '{StartTime}' AND '{EndTime}';";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }

        #endregion

        #region Read Check Out Data Database
        public List<string> SelectChekcOutDataOrderNumber()
        {
            SQLConnectClass.connectOpen();

            List<string> result = new List<string>();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT DISTINCT OrderNumber 
FROM CheckOutData 
ORDER BY OrderNumber;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result.Add(reader.GetString(0));
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public List<int> SelectCheckOutDataOrderItemNumberFromOrderNumber(string OrderNumber)
        {
            SQLConnectClass.connectOpen();

            List<int> result = new List<int>();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT OrderItemNumber 
FROM CheckOutData 
WHERE OrderNumber = {OrderNumber};";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result.Add(reader.GetInt32(0));
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public int SelectCheckOutDataItemIDFromOrderNumberAndOrderItemNumber(string OrderNumber, int OrderItemNumber)
        {
            SQLConnectClass.connectOpen();

            int result = 0;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ItemID 
FROM CheckOutData 
WHERE OrderNumber = '{OrderNumber}' and OrderItemNumber = '{OrderItemNumber}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetInt32(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public int SelectCheckOutDataItemAmountFromOrderNumberAndOrderItemNumber(string OrderNumber, int OrderItemNumber)
        {
            SQLConnectClass.connectOpen();

            int result = 0;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ItemAmount 
FROM CheckOutData 
WHERE OrderNumber = '{OrderNumber}' and OrderItemNumber = '{OrderItemNumber}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetInt32(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public int SelectCheckOutDataItemCashFromOrderNumberAndOrderItemNumber(string OrderNumber, int OrderItemNumber)
        {
            SQLConnectClass.connectOpen();

            int result = 0;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ItemCash 
FROM CheckOutData 
WHERE OrderNumber = '{OrderNumber}' and OrderItemNumber = '{OrderItemNumber}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetInt32(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public int SelectCheckOutDataItemSubTotalFromOrderNumberAndOrderItemNumber(string OrderNumber, int OrderItemNumber)
        {
            SQLConnectClass.connectOpen();

            int result = 0;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT ItemSubTotal 
FROM CheckOutData 
WHERE OrderNumber = '{OrderNumber}' and OrderItemNumber = '{OrderItemNumber}';";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetInt32(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        public string SelectCheckOutDataOrderNumberFromMax()
        {
            SQLConnectClass.connectOpen();

            string result = null;
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                CommandString = $@"
SELECT max(OrderNumber) 
FROM CheckOutData;";
                Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                reader = Command.ExecuteReader();
                while (reader.Read())
                    result = reader.GetString(0);
                SQLConnectClass.connectionClose();
                return result;
            }
            else
                return result;
        }
        #endregion

        #region Insert Check Out Data Database
        public void InsertOrderDataToDB(string OrderNumber, int OrderItemNumber, int ItemID, int ItemAmount, int ItemCash, int ItemSubTotal, string initTime)
        {
            SQLConnectClass.connectOpen();
            if (SQLConnectClass != null || SQLConnectClass.sqlConnection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    CommandString = $@"
INSERT INTO CheckOutData(OrderNumber, OrderItemNumber, ItemID, ItemAmount, ItemCash, ItemSubTotal, initTime) 
VALUES ('{OrderNumber}', '{OrderItemNumber}', '{ItemID}', '{ItemAmount}', '{ItemCash}', '{ItemSubTotal}', '{initTime}');";
                    Command = new SqlCommand(CommandString, SQLConnectClass.sqlConnection);
                    Command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Error Generate. Details: " + e.ToString());
                    return;
                }
                finally
                {
                    SQLConnectClass.connectionClose();
                }
            }
        }
        #endregion

    }
}
