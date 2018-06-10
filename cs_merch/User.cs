using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cs_merch
{
    public sealed class User
    {
        private static User name = null;
        public readonly string username, fn, ln, userid;
        private User(string username, string fn, string ln, string userid)
        {
            this.username = username;
            this.fn = fn;
            this.ln = ln;
            this.userid = userid;
        }

        public static User Name
        {
            get
            {
                if (name == null)
                    using (var login = new Login())
                        if (login.ShowDialog() == DialogResult.OK)
                            name = new User(login.username, login.fn, login.ln, login.userid);
                return name;
            }
        }
        public bool Logout()
        {
            name = null;
            return true;
        }
    }
}
