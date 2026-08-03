using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace StockMenagement
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
            LoadProducts();
            //LoadCategory();
            //LoadBrand();
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
                dgvProducts.DataSource = dt;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection())
            {
                OracleCommand cmd = new OracleCommand("PKG_PRODUCT.UPDATE_PRODUCT", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = int.Parse(txtProductId.Text);
                cmd.Parameters.Add("P_NAME", OracleDbType.Varchar2).Value = txtProductName.Text;
                cmd.Parameters.Add("P_CATEGORY", OracleDbType.Int32).Value = cboCategory.SelectedValue;
                cmd.Parameters.Add("P_BRAND", OracleDbType.Int32).Value = cboBrand.SelectedValue;
                cmd.Parameters.Add("P_PRICE", OracleDbType.Decimal).Value = decimal.Parse(txtPrice.Text);
                cmd.Parameters.Add("P_QTY", OracleDbType.Int32).Value = int.Parse(txtQty.Text);

                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Product Updated!");
            LoadProducts();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection())
            {
                OracleCommand cmd = new OracleCommand("PKG_PRODUCT.DELETE_PRODUCT", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = int.Parse(txtProductId.Text);

                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Product Deleted!");
            LoadProducts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection())
            {
                OracleCommand cmd = new OracleCommand("PKG_PRODUCT.INSERT_PRODUCT", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("P_NAME", OracleDbType.Varchar2).Value = txtProductName.Text;
                cmd.Parameters.Add("P_CATEGORY", OracleDbType.Int32).Value = cboCategory.SelectedValue;
                cmd.Parameters.Add("P_BRAND", OracleDbType.Int32).Value = cboBrand.SelectedValue;
                cmd.Parameters.Add("P_PRICE", OracleDbType.Decimal).Value = decimal.Parse(txtPrice.Text);
                cmd.Parameters.Add("P_QTY", OracleDbType.Int32).Value = int.Parse(txtQty.Text);

                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Product Added!");
            LoadProducts();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection();
            OracleCommand cmd = new OracleCommand("PKG_PRODUCT.GET_PRODUCTS", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvProducts.DataSource = dt;

        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new MenuForm().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection())
            {
                using (var cmd = new OracleCommand("PKG_PRODUCT.GET_PRODUCTS", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        dgvProducts.DataSource = dt;
                    }
                }
            }
        }
    }
}
