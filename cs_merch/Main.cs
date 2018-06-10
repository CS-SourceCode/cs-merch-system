using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace cs_merch
{
    public partial class Main : Form
    {

        DatabaseConn conn = new DatabaseConn();

        public Main()
        {
            InitializeComponent();
        }

        //SIDE BAR CONTROLS//
            private void btnDashboard_Click(object sender, EventArgs e)
            {
                main_browser.SelectTab("dashboard");
                btnDashboard.BackColor = Color.LightGray;
            }
            private void btnSales_Click(object sender, EventArgs e)
            {
                main_browser.SelectTab("sales");
            }

            private void btnSell_Click(object sender, EventArgs e)
            {
                sales_browser.SelectTab("sell");
            }

            private void btnOrders_Click(object sender, EventArgs e)
            {
                sales_browser.SelectTab("orders");
            }

            private void btnMerch_Click(object sender, EventArgs e)
            {
                main_browser.SelectTab("merchandise");
            }

            private void btnReports_Click(object sender, EventArgs e)
            {
                main_browser.SelectTab("reports");
            }
            private void btnUsers_Click(object sender, EventArgs e)
            {

            }

            private void exit_login_Click(object sender, EventArgs e)
            {
                Program.looper = false;
                this.Close();
            }
    }
}
