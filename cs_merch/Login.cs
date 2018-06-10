using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cs_merch
{
    public partial class Login : Form
    {

        private DatabaseConn conn = new DatabaseConn();
        public string username, fn, ln, userid;

        public Login()
        {
            InitializeComponent();
        }

        private void exit_login_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            username = txtUsername.Text;
            string password = txtPassword.Text;
            var users_dt = conn.Select("users", "*")
                        .Where("username", username, "password", password)
                        .GetQueryData();

            if (users_dt.Rows.Count == 1)
            {
                userid = users_dt.Rows[0][0].ToString();
                fn = users_dt.Rows[0][3].ToString();
                ln = users_dt.Rows[0][4].ToString();
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
