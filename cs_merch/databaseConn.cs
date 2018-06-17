using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace cs_merch
{
    internal class DatabaseConn
    {
        private DataTable _holder;
        private MySqlCommand _cmd;

        private string _sql = "";
        private bool _flag;

        public DatabaseConn() => RefreshCmd();

        public string lastID => _cmd.LastInsertedId.ToString();

        private string tableNamer(string tableName)
        {
            string[] naming = tableName.Split('=');
            if (naming.Length > 1)
                return naming[0] + " AS " + naming[1];
            return tableName;
        }

        private void RefreshCmd()
        {
            _cmd = new MySqlCommand();
            _holder = new DataTable();
        }

        public dynamic GetQueryData()
        {
            using (var databasecon = new MySqlConnection("Server=localhost;Database=cs_merch_test;Uid=root;Pwd=;"))
            {
                databasecon.Open();
                _cmd.CommandText = _sql;
                _cmd.Connection = databasecon;
                if (_flag)
                {
                    _cmd.ExecuteNonQuery();
                    return _flag;
                }
                _holder.Load(_cmd.ExecuteReader());
                return _holder;
            }
        }

        public DataTable GetData() => _holder;

        public MySqlCommand getCmd() => _cmd;

        public DatabaseConn Select(string table, params string[] fields)
        {
            _flag = false;
            table = tableNamer(table);
            RefreshCmd();
            _sql = "SELECT " + string.Join(", ", fields) + " FROM " + table + " ";
            return this;
        }

        public DatabaseConn Select(string table, string[] fields, string[] wheres) => Select(table, fields).Where(wheres);

        public DatabaseConn Update(string table, params string[] fields)
        {
            if (fields.Length % 2 == 0)
            {
                RefreshCmd();
                _flag = true;
                table = tableNamer(table);
                _sql = "UPDATE " + table + " SET ";
                for (var i = 0; i < fields.Length; i++)
                {
                    _sql += fields[i] + " = @" + fields[i] + (i + 2 < fields.Length ? " ,    " : " ");
                    _cmd.Parameters.AddWithValue("@" + fields[i], fields[++i]);
                    //Every other parameter added as parameterized value
                }
                return this;
            }
            throw new Exception("Lack of fields/values arguements passed:" + string.Join(",", fields));
        }

        public DatabaseConn Update(string table, string[] fields, string[] wheres) => Update(table, fields).Where(wheres);

        public DatabaseConn Insert(string table, params string[] fields)
        {
            if (fields.Length % 2 == 0)
            {
                RefreshCmd();
                table = tableNamer(table);
                _flag = true;
                _sql = "INSERT INTO " + table + " (";
                var values = "VALUES (";
                for (var i = 0; i < fields.Length; i++)
                {
                    _sql += fields[i] + (i + 2 < fields.Length ? " ,    " : ") ");
                    values += "@" + fields[i] + (i + 2 < fields.Length ? " ,    " : ") ");
                    _cmd.Parameters.AddWithValue("@" + fields[i], fields[++i]);
                    //Every other parameter added as parameterized value
                }
                _sql += values;
                return this;
            }
            throw new Exception("Lack of fields/values arguements passed:" + string.Join(",", fields));
        }

        public DatabaseConn Where(params string[] fields)
        {
            if (fields.Length % 2 != 0)
                throw new Exception("Lack of fields/values arguements passed:" + string.Join(",", fields));
            _sql += "WHERE ";
            for (var i = 0; i < fields.Length; i++)
            {
                _sql += fields[i] + " = @" + fields[i] + (i + 2 < fields.Length ? " AND " : " ");
                _cmd.Parameters.AddWithValue("@" + fields[i], fields[++i]);
                //Every other parameter added as parameterized value
            }
            return this;
        }
        public DatabaseConn Like(params string[] fields)
        {
            if (fields.Length % 2 != 0)
                throw new Exception("Lack of fields/values arguements passed:" + string.Join(",", fields));
            _sql += "WHERE ";
            for (var i = 0; i < fields.Length; i++)
            {
                _sql += fields[i] + " LIKE @" + fields[i] + (i + 2 < fields.Length ? " AND " : " ");
                _cmd.Parameters.AddWithValue("@" + fields[i], fields[++i] + "%");
                //Every other parameter added as parameterized value
            }
            return this;
        }

        private DatabaseConn Join(string table, string field1, string field2, string type)
        {
            table = tableNamer(table);
            _sql += type + " JOIN " + table + " ON " + field1 + " = " + field2 + " ";
            return this;
        }

        public DatabaseConn IJoin(string table, string field1, string field2) => Join(table, field1, field2, "INNER");

        public DatabaseConn RJoin(string table, string field1, string field2) => Join(table, field1, field2, "RIGHT");

        public DatabaseConn LJoin(string table, string field1, string field2) => Join(table, field1, field2, "LEFT");

        public DatabaseConn NJoin(string table)
        {
            table = tableNamer(table);
            _sql += "NATURAL JOIN " + table + " ";
            return this;

        }

        public DatabaseConn Group(params string[] fields)
        {
            _sql += "GROUP BY (";
            foreach (var field in fields)
            {
                _sql += field + ", ";
            }
            _sql = _sql.Remove(_sql.Length - 2, 2) + ") ";
            return this;
        }
    }

}
