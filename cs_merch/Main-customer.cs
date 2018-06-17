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
    public partial class Main_customer : Form
    {
        public Form main { get; set; }
        private DatabaseConn conn = new DatabaseConn();
        private Main mainMenu;
        public Main_customer(Main x)
        {
            mainMenu = x;
            InitializeComponent();
            customer_cluster.SelectedIndex = 0;
        }

        private void exit_customer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customer_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customer_save_Click(object sender, EventArgs e)
        {
            int parse;
            if (customer_fname.Text == "" || customer_lname.Text == "" || customer_cluster.Text == "" || (customer_contact.Text == "" || !int.TryParse(customer_contact.Text, out parse)))
            {
                MessageBox.Show("Please make sure all fields are filled in correctly.");

            }
            else
            {
                string firstname = customer_fname.Text;
                string lastname = customer_lname.Text;

                var tempcust_dt = conn.Select("customer", "*")
                            .Where("firstname", firstname, "lastname", lastname)
                            .GetQueryData();

                if (tempcust_dt.Rows.Count == 1)
                {
                    MessageBox.Show("Customer already exists!");
                }
                else
                {
                    conn.Insert("customer", "firstname", customer_fname.Text, "lastname", customer_lname.Text, "contact", customer_contact.Text, "cluster", customer_cluster.Text).GetQueryData();
                }
                mainMenu.setCustomerlist();
                
            }
        }

        private void exit_main_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
