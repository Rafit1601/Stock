using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;


namespace StockMenagement
{
    public partial class Dashboard : Form
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;

        public object ConfigurationManager { get; private set; }

        public Dashboard()
        {
            InitializeComponent();
        }
        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        private void DisableButton()
        {
            foreach(Control previouBtn in panel1.Controls)
            {
                if(previouBtn.GetType() == typeof(Button))
                {
                    previouBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previouBtn.ForeColor = Color.Gainsboro;
                    previouBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.Connection["OracleDb"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(connString))
            {
                MessageBox.Show("Database connection string 'OracleDb' is not configured. Please add it to App.config.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var conn = new OracleConnection(connString))
            {
                try
                {
                    conn.Open();

                    button1.Text = GetValue(conn, "PKG_DASHBOARD.GET_TOTAL_PRODUCT");
                    button2.Text = GetValue(conn, "PKG_DASHBOARD.GET_TOTAL_CUSTOMER");
                    button3.Text = GetValue(conn, "PKG_DASHBOARD.GET_TOTAL_SUPPLIER");
                    button4.Text = GetValue(conn, "PKG_DASHBOARD.GET_TOTAL_SALES");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading dashboard values: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string GetValue(OracleConnection conn, string procedure)
        {
            using (var cmd = new OracleCommand(procedure, conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add("P_TOTAL",
                    OracleDbType.Int32).Direction =
                    System.Data.ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                return cmd.Parameters["P_TOTAL"].Value?.ToString() ?? "0";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            new Login().Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }
    }
}
