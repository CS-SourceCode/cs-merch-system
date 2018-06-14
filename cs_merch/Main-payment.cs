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
    public partial class Main_payment : Form
    {
        Main parent = new Main();
        decimal totalprice;
        decimal cash;
        decimal cashchange;
        int payment_status = 0;
        Boolean _flag;

        public Main_payment(Main main, decimal total, decimal amtpaid, Boolean flag = false)
        {
            _flag = flag;
            InitializeComponent();
            parent = main;
            totalprice = Convert.ToDecimal(total);
            payment_total.Text = totalprice.ToString();
            payment_paid.Text = amtpaid.ToString();
        }

        public Main checkoutform { get; internal set; }

        private void exit_payment_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void payment_record_Click(object sender, EventArgs e)
        {
            cash = Convert.ToDecimal(payment_cash.Text);
            if (_flag)
            {
                string selectedOrder = parent.orders_list.SelectedRows[0].Cells[0].Value.ToString();
                parent.payOrder(selectedOrder, cash);
            }
            else
            {
                if (totalprice > cash)
                {
                    cashchange = Convert.ToDecimal(0.00);
                    payment_status = 2;
                    MessageBox.Show("Insufficient Cash; this transaction will be recorded as DOWN Payment.");
                    parent.order_change.Text = cashchange.ToString();
                }
                else if (totalprice <= cash)
                {
                    cashchange = cash - totalprice;
                    payment_status = 1;
                    MessageBox.Show("This transaction will be recorded as FULLY PAID.");
                    parent.order_change.Text = cashchange.ToString();
                }
                parent.recordOrder(cash, cashchange, payment_status);
            }
            this.Close();
            parent.sell_additem.Enabled = false;
            parent.sell_removeitem.Enabled = false;
            parent.sell_removeall.Enabled = false;
            parent.order_checkout.Enabled = false;
        }
    }
}
