using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Knack_Sales_Portal;

namespace Knack_Sales_Portal
{

    public partial class Form_login : Form
    {
        public Form_login()
        {
            InitializeComponent();
        }
        private void tb_Login_UserName_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Login_UserName.Visible = false;
        }

        private void tb_Login_password_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Login_password.Visible = false;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;"))
            {
                SqlCommand cmd = new SqlCommand("select * from Users where UserName like'" + tb_Login_UserName.Text.ToString() + "'and Password like'" + tb_Login_password.Text.ToString() + "'", con);
                con.Open();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                bool loginSuccessful = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
                if (loginSuccessful)
                {
                    GetUserDetails.UserName = ds.Tables[0].Rows[0][0].ToString();
                    GetUserDetails.FName = ds.Tables[0].Rows[0][2].ToString();
                    GetUserDetails.UserRole = ds.Tables[0].Rows[0][3].ToString();
                    this.Hide();
                    new Home().ShowDialog();
                    this.Close();
                }
                else
                lb_Login_WrongInfo.Show();

            }
            
        }

        private void lb_Login_UserName_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Login_UserName.Visible = false;
            tb_Login_UserName.Focus();
        }

        private void lb_Login_password_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Login_password.Visible = false;
            tb_Login_password.Focus();
        }
        private void tb_Login_password_Enter(object sender, EventArgs e)
        {
            lb_Login_password.Visible = false;
            lb_Login_WrongInfo.Hide();
        }

        private void tb_Login_UserName_Enter(object sender, EventArgs e)
        {
            lb_Login_UserName.Visible = false;
            lb_Login_WrongInfo.Hide();
        }

        private void Form_login_Load(object sender, EventArgs e)
        {
            lb_Login_WrongInfo.Hide();
        }
    }
}
