using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

using System.Windows.Forms;

namespace StockMenagement
{

    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            using (Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection())
            {
                OracleCommand cmd = new OracleCommand("PKG_ADMIN.LOGIN_ADMIN", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;
                cmd.Parameters.Add("P_USERNAME", OracleDbType.Varchar2).Value = textBox1.Text;
                cmd.Parameters.Add("P_PASSWORD", OracleDbType.Varchar2).Value = textBox2.Text;
                cmd.Parameters.Add("P_ROLE", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;

                
                string role = Convert.ToString(cmd.Parameters["P_ROLE"].Value)?.Trim();

                if (string.Equals(role, "ADMIN", StringComparison.OrdinalIgnoreCase))
                    new Dashboard().Show();
                else
                    new ProductForm().Show();

                this.Hide();
                MessageBox.Show("Role returned: [" + role + "]");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new RegisterForm().Show();
            this.Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {


        }
    }
}
