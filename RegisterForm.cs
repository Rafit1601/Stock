using Oracle.ManagedDataAccess.Client;
using System;
using System.Windows.Forms;

namespace StockMenagement
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection())
            {
                OracleCommand cmd = new OracleCommand("PKG_USER.REGISTER_USER", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("P_EMAIL", OracleDbType.Varchar2).Value = txtEmail.Text;
                cmd.Parameters.Add("P_USERNAME", OracleDbType.Varchar2).Value = txtUsername.Text;
                cmd.Parameters.Add("P_PASSWORD", OracleDbType.Varchar2).Value = txtPassword.Text;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Registration successful!");
                new Login().Show();
                this.Hide();
            }
        }
    }
}
