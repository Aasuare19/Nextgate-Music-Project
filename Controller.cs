using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NextGateMusic
{
    public class Controller
    {
        private string _sql;
        private string _connString;
        private MySqlConnection _conn;
        private MySqlCommand _cmd;
        private MySqlDataReader _dr;
        private MySqlDataAdapter _da;

        public Controller()
        {
            _connString = "Server=localhost;Database=ng_music;Uid=ngmusic;Pwd=ngmusic;";
            _conn = new MySqlConnection(_connString);
        }
        public bool OpenConn()
        {
            bool isOpen;

            try
            {
                _conn.Open();
                isOpen = true;
            }
            catch (MySqlException ex)
            {
                //message box
                isOpen = false;
            }

            return isOpen;
        }
        public void CloseConn()
        {
            try
            {
                _conn.Close();
            }
            catch (MySqlException ex)
            {
                //message box
            }
        }
        public bool isValidUser(string username, string password)
        {
            bool isValid = false;

            _sql = "SELECT username, password FROM ng_users where username = @username";

            _cmd = new MySqlCommand(_sql, _conn);
            _cmd.Parameters.AddWithValue("@username", username);

            _dr = _cmd.ExecuteReader();

            if (_dr.Read())
            {
                string dbUsername = _dr.GetString(0);
                string dbPassword = _dr.GetString(1);

                if (username.Equals(dbUsername) && password.Equals(dbPassword))
                {
                    isValid = true;
                }
            }

            return isValid;
        }
        public DataTable searchDatabase(string searchText)
        {
            DataTable dt = new DataTable();

            _sql = "select s.name as 'Name' , a.album_name as 'Album Name' , " +
                    "a.release_year as 'Release Year', a.record_company as 'Record Company' " +
                    "from ng_singers s " +
                    "inner join ng_albums a on s.ng_singers_id = a.ng_singers_id " +
                    "where s.name LIKE @searchText " +
                    "or a.album_name LIKE @searchText";

            _cmd = new MySqlCommand(_sql, _conn);
            _cmd.Parameters.AddWithValue("@searchText", searchText);

            _da = new MySqlDataAdapter(_sql, _conn);
            _da.SelectCommand = _cmd;
            _da.Fill(dt);

            return dt;
        }
        public bool usernameExists(string username)
        {
            bool exists = false;

            _sql = "SELECT username FROM ng_users WHERE username = @username";

            _cmd = new MySqlCommand(_sql, _conn);
            _cmd.Parameters.AddWithValue("@username", username);

            _dr = _cmd.ExecuteReader();

            if (_dr.HasRows)
            {
                exists = true;
            }

            _dr.Close();
            return exists;
        }
        public void createNewUser(string username, string password)
        {
            _sql = "INSERT INTO ng_users (username, password) VALUES (@username, @password)";

            _cmd = new MySqlCommand(_sql, _conn);
            _cmd.Parameters.AddWithValue("@username", username);
            _cmd.Parameters.AddWithValue("@password", password);

            _cmd.ExecuteNonQuery();
        }
        public DataTable bindSingerButtons()
        {
            _sql = "Select name FROM ng_singers";

            _cmd = new MySqlCommand(_sql, _conn);

            _da = new MySqlDataAdapter(_sql, _conn);
            _da.SelectCommand = _cmd;

            DataTable dt = new DataTable();
            _da.Fill(dt);

            return dt;
        }
        public void addNewArtist(string name, string dob, string sex)
        {
            _sql = "INSERT INTO ng_singers (name, dob, sex) VALUES(@name, @dob, @sex)";

            _cmd = new MySqlCommand(_sql, _conn);
            _cmd.Parameters.AddWithValue("@name", name);
            _cmd.Parameters.AddWithValue("@dob", dob);
            _cmd.Parameters.AddWithValue("@sex", sex);

            _cmd.ExecuteNonQuery();
        }
        public bool artistExists(string name, string dob, string sex)
        {
            bool exists = false;

            _sql = "SELECT name, dob, sex FROM ng_singers WHERE name = @name AND dob = @dob AND sex = @sex";

            _cmd = new MySqlCommand(_sql, _conn);
            _cmd.Parameters.AddWithValue("@name", name);
            _cmd.Parameters.AddWithValue("@dob", dob);
            _cmd.Parameters.AddWithValue("@sex", sex);

            _dr = _cmd.ExecuteReader();

            if (_dr.HasRows)
            {
                exists = true;
            }

            _dr.Close();

            return exists;
        }
        public int getSingerIdByName(string name)
        {
            int id = 0;

            _sql = "SELECT ng_singers_id FROM ng_singers WHERE name = @name";

            _cmd = new MySqlCommand(_sql, _conn);
            _cmd.Parameters.AddWithValue("@name", name);

            _dr = _cmd.ExecuteReader();

            if (_dr.HasRows)
            {
                _dr.Read();
                id = _dr.GetInt32(0);
            }

            _dr.Close();

            return id;
        }
        public bool albumbExists(int singerid, string albumname, string releaseyear, string recordcompany)
        {
            bool exists = false;

            _sql = "SELECT ng_albums_id FROM ng_albums WHERE ng_singers_id = @singerid AND album_name = @albumname " +
                    "AND release_year = @releaseyear AND record_company = @recordcompany";

            _cmd = new MySqlCommand(_sql, _conn);
            _cmd.Parameters.AddWithValue("@singerid", singerid);
            _cmd.Parameters.AddWithValue("@albumname", albumname);
            _cmd.Parameters.AddWithValue("@releaseyear", releaseyear);
            _cmd.Parameters.AddWithValue("@recordcompany", recordcompany);

            _dr = _cmd.ExecuteReader();

            if (_dr.HasRows)
            {
                exists = true;
            }

            _dr.Close();

            return exists;
        }
        public void addAlbum(int singerid, string albumname, string releaseyear, string recordcompany)
        {
            _sql = "INSERT INTO ng_albums (ng_singers_id, album_name, release_year, record_company) " +
                    "VALUES(@singerid, @albumname, @releaseyear, @recordcompany)";

            _cmd = new MySqlCommand(_sql, _conn);
            _cmd.Parameters.AddWithValue("@singerid", singerid);
            _cmd.Parameters.AddWithValue("@albumname", albumname);
            _cmd.Parameters.AddWithValue("@releaseyear", releaseyear);
            _cmd.Parameters.AddWithValue("@recordcompany", recordcompany);

            _cmd.ExecuteNonQuery();
        }
        public string cleanseUserInput(string input)
        {
            //trim
            input = input.Trim();

            //reduces white space to just one
            input = Regex.Replace(input, "[ ]{2,}", " ", RegexOptions.None);

            return input;
        }
    }
}