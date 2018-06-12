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
            setMerchandise();
            setCustomerlist();
            getOrderId();
        }

        public Login Login { get; internal set; }


        decimal payment;
        int order_status;
        decimal change;
        int customer_id;
        string paystatus;
        string temp_merchname;
        int temp_qty = 0;
        int temp_merchid = 0;
        decimal temp_price;
        decimal merch_total;
        decimal total_price;
        int currCustomerNo = 0;
        string custfn;
        string custln;

        public void checkoutOrder(decimal payment, decimal change, string paystatus)
        {

            DateTime date = DateTime.Now;
            var order_date = date.Date;

            order_status = 1;
            customer_id = int.Parse(selectedCustIDTxt.Text);
            this.paystatus = paystatus;
            this.payment = payment;
            this.change = change;

            conn.Insert("orders", "order_date", DateTime.Now.ToString("yyyy-MM-dd"), "order_status", "1", "customer_id", customer_id.ToString(), "payment_status", paystatus).GetQueryData();
            conn.Insert("order_payment", "order_id", conn.lastID, "payment", payment.ToString(), "payment_date",
                DateTime.Now.ToString("yyyy-MM-dd")).GetQueryData();
            //orderline.ColumnCount = 5;
            string temp_orderid;
            temp_orderid = conn.lastID;
            foreach (DataGridViewRow rows in orderline.Rows)
            {
                MessageBox.Show(rows.Cells[0].Value.ToString());
                conn.Insert("orderline", "order_id", temp_orderid, "merch_id", rows.Cells[3].Value.ToString(), "quantity", rows.Cells[1].Value.ToString(), "total_price", rows.Cells[2].Value.ToString()).GetQueryData();
            }


        }

        private void getOrderId()
        {
            order_no.Enabled = false;
            price_total.Enabled = false;
            var orders_dt = conn.Select("orders", "order_id").GetQueryData();

            if (orders_dt.Rows.Count == 0)
            {
                //Should not preemptively insert into the database
                //conn.Insert("orders", "order_id", "1").GetQueryData();
                order_no.Text = "1";
            }
            else
            {
                int order_id = orders_dt.Rows[orders_dt.Rows.Count - 1][0] + 1;
                order_no.Text = order_id.ToString();
            }

        }

        private void setMerchandise()
        {

            var merch_dt = conn.Select("merchandise", "*")
                        .GetQueryData();

            sell_merchandise.DataSource = merch_dt;
            sell_merchandise.Columns["merch_id"].Visible = false;
            sell_merchandise.Columns["merch_name"].HeaderText = "Description";
            sell_merchandise.Columns["merch_price"].HeaderText = "Price";

            //Default Selected
            temp_merchname = sell_merchandise.Rows[0].Cells["merch_name"].Value.ToString();
            temp_price = Convert.ToDecimal(sell_merchandise.Rows[0].Cells["merch_price"].Value);
            merch_total = Convert.ToDecimal(temp_price) * Convert.ToDecimal(temp_qty);
        }

        private void setOrderNo()
        {
            //get latest and increment but is temporary
        }

        public void setCustomerlist()
        {
            var cust_dt = conn.Select("customer", "*")
                        .GetQueryData();

            customer_list.DataSource = cust_dt;
            customer_list.Columns["customer_id"].HeaderText = "ID";
            customer_list.Columns["firstname"].HeaderText = "First Name";
            customer_list.Columns["lastname"].HeaderText = "Last Name";
            customer_list.Columns["contact"].HeaderText = "Contact";
            customer_list.Columns["cluster"].HeaderText = "Cluster";

            currCustomerNo = Convert.ToInt32(customer_list.Rows[0].Cells["customer_id"].Value);
            custfn = customer_list.Rows[0].Cells["firstname"].Value.ToString();
            custln = customer_list.Rows[0].Cells["lastname"].Value.ToString();
            selectedCustIDTxt.Text = currCustomerNo.ToString();
            selectedCustNameTxt.Text = custfn + " " + custln;


        }
        private void main_close_Click(object sender, EventArgs e)
        {
            Program.looper = false;
            this.Close();
            // Prompt to Logout //
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

        private void sell_additem_Click(object sender, EventArgs e)
        {
            temp_qty = Convert.ToInt32(item_quantity.Text);
            // Create a new row first as it will include the columns you've created at design-time.
            bool Found = false;
            if (orderline.Rows.Count > 0)
            {
                foreach (DataGridViewRow row1 in orderline.Rows)
                {
                    if (Convert.ToString(row1.Cells[0].Value) == temp_merchname)
                    {
                        //Update the Quantity of the found row
                        row1.Cells[1].Value = temp_qty;
                        row1.Cells[2].Value = temp_price * temp_qty;
                        Found = true;
                    }

                }
                if (!Found)
                {

                    int rowId = orderline.Rows.Add();

                    // Grab the new row!
                    DataGridViewRow row = orderline.Rows[rowId];

                    // Add the data
                    row.Cells["merch_name"].Value = temp_merchname;
                    row.Cells["merch_quantity"].Value = temp_qty;
                    row.Cells["merch_price"].Value = temp_price * temp_qty;
                    row.Cells["merch_id"].Value = temp_merchid;
                }
            }
            else
            {

                int rowId = orderline.Rows.Add();

                // Grab the new row!
                DataGridViewRow row = orderline.Rows[rowId];

                // Add the data
                row.Cells["merch_name"].Value = temp_merchname;
                row.Cells["merch_quantity"].Value = temp_qty;
                row.Cells["merch_price"].Value = temp_price * temp_qty;
                row.Cells["merch_id"].Value = temp_merchid;
            }

            price_total.Text = (from DataGridViewRow row in orderline.Rows
                                where row.Cells[2].FormattedValue.ToString() != string.Empty
                                select Convert.ToDecimal(row.Cells[2].FormattedValue)).Sum().ToString();
            total_price = (from DataGridViewRow row in orderline.Rows
                           where row.Cells[2].FormattedValue.ToString() != string.Empty
                           select Convert.ToDecimal(row.Cells[2].FormattedValue)).Sum();

            sell_additem.Text = "Update";
        }

        private void sell_removeitem_Click(object sender, EventArgs e)
        {
            if (orderline.Rows.Count > 0)
            {
                orderline.Rows.RemoveAt(orderline.CurrentRow.Index);
            }
            else
            {
                MessageBox.Show("No Item in Orderline");
            }

            price_total.Text = (from DataGridViewRow row in orderline.Rows
                                where row.Cells[2].FormattedValue.ToString() != string.Empty
                                select Convert.ToDecimal(row.Cells[2].FormattedValue)).Sum().ToString();
            total_price = (from DataGridViewRow row in orderline.Rows
                           where row.Cells[2].FormattedValue.ToString() != string.Empty
                           select Convert.ToDecimal(row.Cells[2].FormattedValue)).Sum();
        }

        private void sell_removeall_Click(object sender, EventArgs e)
        {
            sell_additem.Text = "Add";
            orderline.Rows.Clear();
            orderline.Refresh();
            price_total.Text = "0.00";
        }

        private void customer_select_Click(object sender, EventArgs e)
        {
            currCustomerNo = Convert.ToInt32(customer_list.CurrentRow.Cells["customer_id"].Value);
            custfn = customer_list.CurrentRow.Cells["firstname"].Value.ToString();
            custln = customer_list.CurrentRow.Cells["lastname"].Value.ToString();
            selectedCustIDTxt.Text = currCustomerNo.ToString();
            selectedCustNameTxt.Text = custfn + " " + custln;
        }

        private void customer_new_Click(object sender, EventArgs e)
        {
            using (Unfocus p = new Unfocus(this))
            {
                //p.Location = new Point(48, 15);
                //p.Location = new Point(0, 0);
                p.Size = this.ClientRectangle.Size;
                p.StartPosition = FormStartPosition.CenterScreen;
                p.BringToFront();

                Main_customer addcust = new Main_customer(this);
                addcust.main = this;
                addcust.ShowDialog(this);
            }
        }

        public void claimOrder(string olID, string quantity)
        {
            var quantityClaimed = conn.Select("orderline", "quantity_claimed")
                .Where("orderlineID", olID)
                .GetQueryData();
            quantityClaimed += int.Parse(quantity);
            conn.Update("orderline", "quantity_claimed", quantityClaimed)
                .Where("orderline_id", olID)
                .GetQueryData();
            conn.Insert("order_claim", "orderline_id", olID, "quantity_no", quantity, "date_claimed",
                    DateTime.Now.ToString("yyyy-MM-dd"))
                .GetQueryData();

        }

        public void payOrder(string oID, string payment) => payOrder(oID, Convert.ToInt32(payment));

        public void payOrder(string oID, int payment)
        {
            // toDo: 1 insert for payment
            var present_pay = conn.Select("order_payment", "SUM(payment)")
                                    .NJoin("orders")
                                    .Where("order_id", oID)
                                    .Group("order_id")
                                    .GetQueryData()
                                    .Rows[0][0];
            int total_pay = Convert.ToInt32(present_pay) + payment;
            MessageBox.Show(total_pay.ToString());
            conn.Insert("order_payment", "order_id", oID, "payment", payment.ToString(), "payment_date",
                DateTime.Now.ToString("yyyy-MM-dd"))
                .GetQueryData();
            var total_price = conn.Select("orderline", "SUM(total_price)")
                .Where("order_id", oID)
                .Group("order_id")
                .GetQueryData()
                .Rows[0][0];
            if (total_pay >= total_price)
                conn.Update("orders", "payment_status", "1")
                    .Where("order_id", oID)
                    .GetQueryData();
        }

        private void sell_merchandise_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                temp_merchname = sell_merchandise.Rows[e.RowIndex].Cells["merch_name"].Value.ToString();
                temp_price = Convert.ToDecimal(sell_merchandise.Rows[e.RowIndex].Cells["merch_price"].Value);
                merch_total = Convert.ToDecimal(temp_price) * Convert.ToDecimal(temp_qty);
                temp_merchid = Convert.ToInt32(sell_merchandise.Rows[e.RowIndex].Cells["merch_id"].Value);
                item_quantity.Text = "1";
            }
            bool Found = false;
            if (orderline.Rows.Count > 0)
            {
                foreach (DataGridViewRow row1 in orderline.Rows)
                {
                    if (Convert.ToString(row1.Cells[0].Value) == temp_merchname)
                    {
                        Found = true;
                        sell_additem.Text = "Update";
                    }

                }
                if (!Found)
                {
                    sell_additem.Text = "Add";
                }
            }
            else
            {
                sell_additem.Text = "Add";
            }
        }

        private void sell_merchandise_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            temp_qty = Convert.ToInt32(item_quantity.Text);
            // Create a new row first as it will include the columns you've created at design-time.
            bool Found = false;
            if (orderline.Rows.Count > 0)
            {
                foreach (DataGridViewRow row1 in orderline.Rows)
                {
                    if (Convert.ToString(row1.Cells[0].Value) == temp_merchname)
                    {
                        //Update the Quantity of the found row
                        row1.Cells[1].Value = temp_qty;
                        row1.Cells[2].Value = temp_price * temp_qty;
                        Found = true;
                    }

                }
                if (!Found)
                {

                    int rowId = orderline.Rows.Add();

                    // Grab the new row!
                    DataGridViewRow row = orderline.Rows[rowId];

                    // Add the data
                    row.Cells["merch_name"].Value = temp_merchname;
                    row.Cells["merch_quantity"].Value = temp_qty;
                    row.Cells["merch_price"].Value = temp_price * temp_qty;
                    row.Cells["merch_id"].Value = temp_merchid;
                }
            }
            else
            {

                int rowId = orderline.Rows.Add();

                // Grab the new row!
                DataGridViewRow row = orderline.Rows[rowId];

                // Add the data
                row.Cells["merch_name"].Value = temp_merchname;
                row.Cells["merch_quantity"].Value = temp_qty;
                row.Cells["merch_price"].Value = temp_price * temp_qty;
                row.Cells["merch_id"].Value = temp_merchid;
            }

            price_total.Text = (from DataGridViewRow row in orderline.Rows
                                where row.Cells[2].FormattedValue.ToString() != string.Empty
                                select Convert.ToDecimal(row.Cells[2].FormattedValue)).Sum().ToString();
            total_price = (from DataGridViewRow row in orderline.Rows
                           where row.Cells[2].FormattedValue.ToString() != string.Empty
                           select Convert.ToDecimal(row.Cells[2].FormattedValue)).Sum();

            sell_additem.Text = "Update";
        }
    }
}
