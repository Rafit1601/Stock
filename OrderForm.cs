using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace StockMenagement
{
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
            LoadProducts();
            LoadOrders();
        }
        private void LoadProducts()
        {
            using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection())
            {
                OracleCommand cmd = new OracleCommand("PKG_PRODUCT.GET_PRODUCTS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cboProduct.DataSource = dt;
                cboProduct.DisplayMember = "PRODUCT_NAME";
                cboProduct.ValueMember = "PRODUCT_ID";
                txtPrice.DataBindings.Add("Text", dt, "PRICE");
            }
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            decimal qty = Convert.ToDecimal(txtQty.Text);
            decimal price = Convert.ToDecimal(txtPrice.Text);
            decimal total = qty * price;
            txtTotal.Text = total.ToString("C");
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection())
            {
                OracleCommand cmd = new OracleCommand("PKG_ORDER.ADD_ORDER", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("P_PRODUCT_ID", OracleDbType.Int32).Value = cboProduct.SelectedValue;
                cmd.Parameters.Add("P_QTY", OracleDbType.Int32).Value = int.Parse(txtQty.Text);
                cmd.Parameters.Add("P_TOTAL", OracleDbType.Decimal).Value = decimal.Parse(txtTotal.Text, System.Globalization.NumberStyles.Currency);

                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Order placed successfully!");
            LoadOrders();
        }
        private void LoadOrders()
        {
            using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection())
            {
                OracleCommand cmd = new OracleCommand("PKG_ORDER.GET_ORDERS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvOrders.DataSource = dt;
            }
        }
    }
}
