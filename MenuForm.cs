using System;
using System.Windows.Forms;

namespace StockMenagement
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            new ProductForm().Show();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            new OrderForm().Show();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            //new ReportForm().Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }
    }
}
