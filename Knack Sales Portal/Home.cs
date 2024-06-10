using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Management.Instrumentation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knack_Sales_Portal
{
   
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            if (GetUserDetails.UserRole.ToString() != "Admin")
            {
                panel_Home_lb_ManageSalesRep.Hide();
                panel_Home_lb_ManageTeams.Hide();
                panel_Home_lb_ManageSales.Hide();
                tb_Home_SalesRecord_Search.Hide();
                lb_Home_SalesRecord_Search.Hide();
                lb_Home_SalesRecord_By.Hide();
                cb_Home_SalesRecord_SearchOptions.Hide();
                lb_Home_SalesRecord_SelectAny.Hide();
                btn_Home_SalesRecord_Search.Hide();
                DGV_Home_SalesRecord.Location = new Point(9 ,3);
                DGV_Home_SalesRecord.Size = new Size(836, 443);
            }
            
            Home_body_SalesRecord_Enable();
            panel_Home_body_TopTeam.Hide();
            panel_Home_body_TopSeller.Hide();
            panel_Home_body_ManageSales.Hide();
            panel_Home_body_ManageTeams.Hide();
            panel_Home_body_ManageSalesRep.Hide();

            panel_Home_AccountSettings.Hide();

            lb_LoggedInName.Text = GetUserDetails.FName.ToString();


        }
        private void DGV_SalesRecord_Fetch_Data(String query)
        {
            try
            {
                lb_DGV_Home_SalesRecord_NoData.Visible = false;
                DGV_Home_SalesRecord.Rows.Clear();
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
                if (RecordFound)
                {
                    lb_DGV_Home_SalesRecord_NoData.Visible = false;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DGV_Home_SalesRecord.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][5].ToString(), ds.Tables[0].Rows[i][6].ToString());
                    }

                }
                else
                {
                    lb_DGV_Home_SalesRecord_NoData.Visible = true;
                }
            }
            catch(Exception ex)
            {
                lb_DGV_Home_SalesRecord_NoData.Visible = true;
            }
        }
        private void Home_body_SalesRecord_Disable()
        {
            panel_Home_lb_SalesRecord.Size = new System.Drawing.Size(140, 35);
            panel_Home_lb_SalesRecord.BackColor = System.Drawing.Color.White;
            lb_Home_SalesRecord.ForeColor = System.Drawing.Color.Purple;
            panel_Home_body_SalesRecord.Hide();
            DGV_Home_SalesRecord.Rows.Clear();
            tb_Home_SalesRecord_Search.Clear();
            lb_Home_SalesRecord_Search.Show();
            cb_Home_SalesRecord_SearchOptions.SelectedIndex = -1;
            lb_Home_SalesRecord_SelectAny.Show();
        }
        private void Home_body_SalesRecord_Enable()
        {
            panel_Home_lb_SalesRecord.Size = new System.Drawing.Size(140, 40);
            panel_Home_lb_SalesRecord.BackColor = System.Drawing.Color.Gray;
            lb_Home_SalesRecord.ForeColor = System.Drawing.Color.White;
            panel_Home_body_SalesRecord.Show();
            if (GetUserDetails.UserRole.ToString() != "Admin")
            {
                String query = "select * from Sales_Record where SalesRep = '" + GetUserDetails.FName.ToString() + "'";
                DGV_SalesRecord_Fetch_Data(query);
            }
            else
                DGV_SalesRecord_Fetch_Data("select * from Sales_Record");
        }
        private void Home_body_TopTeam_Disable()
        {
            panel_Home_lb_TopTeam.Size = new System.Drawing.Size(140, 35);
            panel_Home_lb_TopTeam.BackColor = System.Drawing.Color.White;
            lb_Home_TopTeam.ForeColor = System.Drawing.Color.Purple;
            panel_Home_body_TopTeam.Hide();
        }
        private void Home_body_TopTeam_Enable()
        {
            panel_Home_lb_TopTeam.Size = new System.Drawing.Size(140, 40);
            panel_Home_lb_TopTeam.BackColor = System.Drawing.Color.Gray;
            lb_Home_TopTeam.ForeColor = System.Drawing.Color.White;
            panel_Home_body_TopTeam.Show();
            DGV_Home_TopTeam.Rows.Clear();
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("SELECT Team , COUNT(*) FROM Sales_Record GROUP BY Team order by COUNT(*) desc", con);
            con.Open();

            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);


            bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
            if (RecordFound)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cmd = new SqlCommand("SELECT TeamLead FROM Teams where TeamName = '" + ds.Tables[0].Rows[i][0].ToString() + "'", con);
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds2);
                    DGV_Home_TopTeam.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds2.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString());
                }

            }
            con.Close();
        }
        private void Home_body_TopSeller_Disable()
        {
            panel_Home_lb_TopSeller.Size = new System.Drawing.Size(140, 35);
            panel_Home_lb_TopSeller.BackColor = System.Drawing.Color.White;
            lb_Home_TopSeller.ForeColor = System.Drawing.Color.Purple;
            panel_Home_body_TopSeller.Hide();
        }
        private void Home_body_TopSeller_Enable()
        {
            panel_Home_lb_TopSeller.Size = new System.Drawing.Size(140, 40);
            panel_Home_lb_TopSeller.BackColor = System.Drawing.Color.Gray;
            lb_Home_TopSeller.ForeColor = System.Drawing.Color.White;
            panel_Home_body_TopSeller.Show();

            DGV_Home_TopSeller.Rows.Clear();
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("SELECT SalesRep, COUNT(*) FROM Sales_Record GROUP BY SalesRep order by COUNT(*) desc", con);
            con.Open();

            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);


            bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
            if (RecordFound)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cmd = new SqlCommand("SELECT Team FROM SalesReps where SalesRepName = '" + ds.Tables[0].Rows[i][0].ToString() + "'", con);
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds2);
                    DGV_Home_TopSeller.Rows.Add(ds.Tables[0].Rows[i][0].ToString(), ds2.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString());
                }

            }
            con.Close();
        }
        private void Home_body_ManageSales_Disable()
        {
            panel_Home_lb_ManageSales.BackColor = System.Drawing.Color.White;
            lb_Home_ManageSales.ForeColor = System.Drawing.Color.Purple;
            panel_Home_body_ManageSales.Hide();
        }
        private void Home_body_ManageSales_Enable()
        {
            panel_Home_lb_ManageSales.BackColor = System.Drawing.Color.Gray;
            lb_Home_ManageSales.ForeColor = System.Drawing.Color.White;
            panel_Home_body_ManageSales.Show();
            lb_Home_ManageSales_Add_ActualDate.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            MenuStrip_Home_ManageSales_Add_Click();
        }
        private void Home_body_ManageTeams_Disable()
        {
            panel_Home_lb_ManageTeams.BackColor = System.Drawing.Color.White;
            lb_Home_ManageTeams.ForeColor = System.Drawing.Color.Purple;
            panel_Home_body_ManageTeams.Hide();
        }
        private void Home_body_ManageTeams_Enable()
        {
            panel_Home_lb_ManageTeams.BackColor = System.Drawing.Color.Gray;
            lb_Home_ManageTeams.ForeColor = System.Drawing.Color.White;
            panel_Home_body_ManageTeams.Show();
            lb_Home_ManageTeams_Add_ActualDate.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            MenuStrip_Home_ManageTeams_Add_Click();
        }
        private void Home_body_ManageSalesRep_Disable()
        {
            panel_Home_lb_ManageSalesRep.BackColor = System.Drawing.Color.White;
            lb_Home_ManageSalesRep.ForeColor = System.Drawing.Color.Purple;
            panel_Home_body_ManageSalesRep.Hide();
        }
        private void Home_body_ManageSalesRep_Enable()
        {
            panel_Home_lb_ManageSalesRep.BackColor = System.Drawing.Color.Gray;
            lb_Home_ManageSalesRep.ForeColor = System.Drawing.Color.White;
            panel_Home_body_ManageSalesRep.Show();
            lb_Home_ManageSalesRep_Add_ActualDate.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            MenuStrip_Home_ManageSalesRep_Add_Click();
        }
        
        private void panel_Home_lb_SalesRecord_MouseClick(object sender, MouseEventArgs e)
        {
            Home_body_SalesRecord_Enable();
            Home_body_TopTeam_Disable();
            Home_body_TopSeller_Disable();
            Home_body_ManageSales_Disable();
            Home_body_ManageTeams_Disable();
            Home_body_ManageSalesRep_Disable();
        }
        private void lb_Home_SalesRecord_MouseClick(object sender, MouseEventArgs e)
        {
            Home_body_SalesRecord_Enable();
            Home_body_TopTeam_Disable();
            Home_body_TopSeller_Disable();
            Home_body_ManageSales_Disable();
            Home_body_ManageTeams_Disable();
            Home_body_ManageSalesRep_Disable();
        }

        private void panel_Home_lb_TopTeam_MouseClick(object sender, MouseEventArgs e)
        {
            Home_body_TopTeam_Enable();
            Home_body_SalesRecord_Disable();
            Home_body_TopSeller_Disable();
            Home_body_ManageSales_Disable();
            Home_body_ManageTeams_Disable();
            Home_body_ManageSalesRep_Disable();
        }

        private void lb_Home_TopTeam_MouseClick(object sender, MouseEventArgs e)
        {
            Home_body_TopTeam_Enable();
            Home_body_SalesRecord_Disable();
            Home_body_TopSeller_Disable();
            Home_body_ManageSales_Disable();
            Home_body_ManageTeams_Disable();
            Home_body_ManageSalesRep_Disable();
        }

        private void panel_Home_lb_TopSeller_MouseClick(object sender, MouseEventArgs e)
        {
            Home_body_TopSeller_Enable();
            Home_body_SalesRecord_Disable();
            Home_body_TopTeam_Disable();
            Home_body_ManageSales_Disable();
            Home_body_ManageTeams_Disable();
            Home_body_ManageSalesRep_Disable();
        }

        private void lb_Home_TopSeller_MouseClick(object sender, MouseEventArgs e)
        {
            Home_body_TopSeller_Enable();
            Home_body_SalesRecord_Disable();
            Home_body_TopTeam_Disable();
            Home_body_ManageSales_Disable();
            Home_body_ManageTeams_Disable();
            Home_body_ManageSalesRep_Disable();
        }

        private void panel_Home_lb_ManageSales_MouseClick(object sender, MouseEventArgs e)
        {
            Home_body_ManageSales_Enable();
            Home_body_SalesRecord_Disable();
            Home_body_TopTeam_Disable();
            Home_body_TopSeller_Disable();
            Home_body_ManageTeams_Disable();
            Home_body_ManageSalesRep_Disable();
        }

        private void lb_Home_ManageSales_MouseClick(object sender, MouseEventArgs e)
        {
            Home_body_ManageSales_Enable();
            Home_body_SalesRecord_Disable();
            Home_body_TopTeam_Disable();
            Home_body_TopSeller_Disable();
            Home_body_ManageTeams_Disable();
            Home_body_ManageSalesRep_Disable();
        }

        private void panel_Home_lb_ManageTeams_MouseClick(object sender, MouseEventArgs e)
        {
            Home_body_ManageTeams_Enable();
            Home_body_SalesRecord_Disable();
            Home_body_TopTeam_Disable();
            Home_body_TopSeller_Disable();
            Home_body_ManageSales_Disable();
            Home_body_ManageSalesRep_Disable();
        }

        private void lb_Home_ManageTeams_MouseClick(object sender, MouseEventArgs e)
        {
            Home_body_ManageTeams_Enable();
            Home_body_SalesRecord_Disable();
            Home_body_TopTeam_Disable();
            Home_body_TopSeller_Disable();
            Home_body_ManageSales_Disable();
            Home_body_ManageSalesRep_Disable();

        }

        private void panel_Home_lb_ManageSalesRep_MouseClick(object sender, MouseEventArgs e)
        {
            Home_body_ManageSalesRep_Enable();
            Home_body_SalesRecord_Disable();
            Home_body_TopTeam_Disable();
            Home_body_TopSeller_Disable();
            Home_body_ManageSales_Disable();
            Home_body_ManageTeams_Disable();
        }

        private void lb_Home_ManageSalesRep_MouseClick(object sender, MouseEventArgs e)
        {
            Home_body_ManageSalesRep_Enable();
            Home_body_SalesRecord_Disable();
            Home_body_TopTeam_Disable();
            Home_body_TopSeller_Disable();
            Home_body_ManageSales_Disable();
            Home_body_ManageTeams_Disable();
        }





        private void lb__Home_SalesRecord_Search_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_SalesRecord_Search.Visible = false;
            tb_Home_SalesRecord_Search.Focus();

        }

        private void tb_Home_SalesRecord_Search_Enter(object sender, EventArgs e)
        {
            lb_Home_SalesRecord_Search.Visible = false;
        }

        private void lb_Home_SalesRecord_SelectAny_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_SalesRecord_SelectAny.Visible = false;
            cb_Home_SalesRecord_SearchOptions.DroppedDown = true;
        }

        private void cb_Home_SalesRecord_SearchOptions_Enter(object sender, EventArgs e)
        {
            lb_Home_SalesRecord_SelectAny.Visible = false;
            cb_Home_SalesRecord_SearchOptions.DroppedDown = true;
        }




        private void MenuStrip_Home_ManageSales_Add_Click()
        {
            lb_Home_ManageSales_Add_ActualDate.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            MenuStrip_Home_ManageSales_Add.BackColor = Color.Gray;
            MenuStrip_Home_ManageSales_Update.BackColor = Color.Purple;
            MenuStrip_Home_ManageSales_Delete.BackColor = Color.Purple;
            panel_Home_ManageSales_Add_Body.Show();
            panel_Home_ManageSales_Update_Body.Hide();
            panel_Home_ManageSales_Delete_Body.Hide();
            tb_Home_ManageSales_Add_CustomerName.Clear();
            tb_Home_ManageSales_Add_PhoneNo.Clear();
            cb_Home_ManageSales_Add_State.SelectedIndex = -1;
            cb_Home_ManageSales_Add_SalesRepID.SelectedIndex = -1;
            lb_Home_ManageSales_Add_CustomerName.Visible = true;
            lb_Home_ManageSales_Add_PhoneNo.Visible = true;
            lb_Home_ManageSales_Add_State.Visible = true;
            lb_Home_ManageSales_Add_SalesRepID.Visible = true;
            cb_Home_ManageSales_Add_SalesRepID.Items.Clear();
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('Sales_Record')", con);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            lb_Home_ManageSales_Add_ActualLeadID.Text = (Convert.ToInt32(ds.Tables[0].Rows[0][0]) + 1).ToString();

            cmd = new SqlCommand("Select SalesRepID from SalesReps", con);
            ds.Clear();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
            if (RecordFound)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cb_Home_ManageSales_Add_SalesRepID.Items.Add(ds.Tables[0].Rows[i][1].ToString());
                }

            }
        }
        private void MenuStrip_Home_ManageSales_Add_Click(object sender, EventArgs e)
        {
            MenuStrip_Home_ManageSales_Add_Click();
        }

        private void MenuStrip_Home_ManageSales_Update_Click()
        {
            MenuStrip_Home_ManageSales_Update.BackColor = Color.Gray;
            MenuStrip_Home_ManageSales_Add.BackColor = Color.Purple;
            MenuStrip_Home_ManageSales_Delete.BackColor = Color.Purple;
            panel_Home_ManageSales_Add_Body.Hide();
            panel_Home_ManageSales_Update_Body.Show();
            panel_Home_ManageSales_Delete_Body.Hide();
            cb_Home_ManageSales_Update_SalesRepID.Items.Clear();
            tb_Home_ManageSales_Update_CustomerName.Clear();
            tb_Home_ManageSales_Update_PhoneNo.Clear();
            cb_Home_ManageSales_Update_State.SelectedIndex = -1;
            cb_Home_ManageSales_Update_SalesRepID.SelectedIndex = -1;
            lb_Home_ManageSales_Update_CustomerName.Visible = true;
            lb_Home_ManageSales_Update_PhoneNo.Visible = true;
            lb_Home_ManageSales_Update_State.Visible = true;
            lb_Home_ManageSales_Update_SalesRepID.Visible = true;
            lb_Home_ManageSales_Update_ActualDate.Text = "";
            lb_Home_ManageSales_Update_InvalidLeadID.Visible = false;

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("Select SalesRepID from SalesReps", con);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
            if (RecordFound)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cb_Home_ManageSales_Update_SalesRepID.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
        }
        private void MenuStrip_Home_ManageSales_Update_Click(object sender, EventArgs e)
        {
            MenuStrip_Home_ManageSales_Update_Click();
            lb_Home_ManageSales_Update_LeadID.Visible = true;
            tb_Home_ManageSales_Update_LeadID.Clear();
        }

        private void MenuStrip_Home_ManageSales_Delete_Click()
        {
            MenuStrip_Home_ManageSales_Delete.BackColor = Color.Gray;
            MenuStrip_Home_ManageSales_Add.BackColor = Color.Purple;
            MenuStrip_Home_ManageSales_Update.BackColor = Color.Purple;
            panel_Home_ManageSales_Add_Body.Hide();
            panel_Home_ManageSales_Update_Body.Hide();
            panel_Home_ManageSales_Delete_Body.Show();
            lb_Home_ManageSales_Delete_InvalidLeadID.Visible = false;
            lb_Home_ManageSales_Delete_LeadID.Visible = true;
            tb_Home_ManageSales_Delete_LeadID.Clear();
            lb_Home_ManageSales_Delete_ActualDate.Text = "";
        }
        private void MenuStrip_Home_ManageSales_Delete_Click(object sender, EventArgs e)
        {
            MenuStrip_Home_ManageSales_Delete_Click();
        }

        private void tb_Home_ManageSales_Add_CustomerName_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Add_CustomerName.Visible = false;
        }

        private void lb_Home_ManageSales_Add_CustomerName_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSales_Add_CustomerName.Visible = false;
            tb_Home_ManageSales_Add_CustomerName.Focus();
        }

        private void tb_Home_ManageSales_Add_PhoneNo_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Add_PhoneNo.Visible = false;
        }

        private void lb_Home_ManageSales_Add_PhoneNo_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSales_Add_PhoneNo.Visible = false;
            tb_Home_ManageSales_Add_PhoneNo.Focus();
        }
        private void cb_Home_ManageSales_Add_State_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Add_State.Visible = false;
        }

        private void lb_Home_ManageSales_Add_State_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSales_Add_State.Visible = false;
            cb_Home_ManageSales_Add_State.Focus();
        }
        private void cb_Home_ManageSales_Add_SalesRepID_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Add_SalesRepID.Visible = false;
        }

        private void lb_Home_ManageSales_Add_SalesRepID_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSales_Add_SalesRepID.Visible = false;
            cb_Home_ManageSales_Add_SalesRepID.Focus();
        }




        private void tb_Home_ManageSales_Update_LeadID_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Update_LeadID.Visible = false;
        }

        private void lb_Home_ManageSales_Update_LeadID_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSales_Update_LeadID.Visible = false;
            tb_Home_ManageSales_Update_LeadID.Focus();
        }
        private void tb_Home_ManageSales_Update_CustomerName_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Update_CustomerName.Visible = false;
        }

        private void lb_Home_ManageSales_Update_CustomerName_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSales_Update_CustomerName.Visible = false;
            tb_Home_ManageSales_Update_CustomerName.Focus();
        }

        private void tb_Home_ManageSales_Update_PhoneNo_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Update_PhoneNo.Visible = false;
        }

        private void lb_Home_ManageSales_Update_PhoneNo_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSales_Update_PhoneNo.Visible = false;
            tb_Home_ManageSales_Update_PhoneNo.Focus();
        }
        private void cb_Home_ManageSales_Update_State_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Update_State.Visible = false;
        }

        private void lb_Home_ManageSales_Update_State_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSales_Update_State.Visible = false;
            cb_Home_ManageSales_Update_State.Focus();
        }
        private void cb_Home_ManageSales_Update_SalesRepID_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Update_SalesRepID.Visible = false;
        }

        private void lb_Home_ManageSales_Update_SalesRepID_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSales_Update_SalesRepID.Visible = false;
            cb_Home_ManageSales_Update_SalesRepID.Focus();
        }
        private void tb_Home_ManageSales_Update_CustomerName_TextChanged(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Update_CustomerName.Visible = false;
        }
        private void tb_Home_ManageSales_Update_PhoneNo_TextChanged(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Update_PhoneNo.Visible = false;
        }
        private void cb_Home_ManageSales_Update_State_TextChanged(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Update_State.Visible = false;
        }
        private void cb_Home_ManageSales_Update_SalesRepID_TextChanged(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Update_SalesRepID.Visible = false;
        }
        private void tb_Home_ManageSales_Update_LeadID_TextChanged(object sender, EventArgs e)
        {
            MenuStrip_Home_ManageSales_Update_Click();
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                SqlCommand cmd = new SqlCommand("Select CustomerName,PhoneNo,State,SalesRep,Date from Sales_Record where LeadID = " + tb_Home_ManageSales_Update_LeadID.Text, con);
                con.Open();
                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd = new SqlCommand("Select SalesRepID from SalesReps where SalesRepName = '" + ds.Tables[0].Rows[0][3].ToString() + "'", con);
                da = new SqlDataAdapter(cmd);
                da.Fill(ds2);
                bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
                if (RecordFound)
                {
                    tb_Home_ManageSales_Update_CustomerName.Text = ds.Tables[0].Rows[0][0].ToString();
                    tb_Home_ManageSales_Update_PhoneNo.Text = ds.Tables[0].Rows[0][1].ToString();
                    cb_Home_ManageSales_Update_State.SelectedItem = ds.Tables[0].Rows[0][2].ToString();
                    cb_Home_ManageSales_Update_SalesRepID.SelectedItem = ds2.Tables[0].Rows[0][0].ToString();
                    lb_Home_ManageSales_Update_ActualDate.Text = ds.Tables[0].Rows[0][4].ToString();
                }
                else
                {
                    lb_Home_ManageSales_Update_InvalidLeadID.Visible = true;
                }
            }
            catch(Exception ex)
            {
                if (tb_Home_ManageSales_Update_LeadID.Text.Equals(String.Empty))
                {

                    lb_Home_ManageSales_Update_InvalidLeadID.Visible = false;
                }
                else
                    lb_Home_ManageSales_Update_InvalidLeadID.Visible = true;
            }
        }




        private void tb_Home_ManageSales_Delete_LeadID_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSales_Delete_LeadID.Visible = false;
        }

        private void lb_Home_ManageSales_Delete_LeadID_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSales_Delete_LeadID.Visible = false;
            tb_Home_ManageSales_Delete_LeadID.Focus();
        }

        private void tb_Home_ManageSales_Delete_LeadID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(tb_Home_ManageSales_Delete_LeadID.Text.Equals(string.Empty))
                {
                    lb_Home_ManageSales_Delete_ActualDate.Text = "";
                }
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                SqlCommand cmd = new SqlCommand("Select Date from Sales_Record where LeadID = " + tb_Home_ManageSales_Delete_LeadID.Text, con);
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
                if (RecordFound)
                {
                    lb_Home_ManageSales_Delete_ActualDate.Text = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    lb_Home_ManageSales_Delete_ActualDate.Text = "";
                    lb_Home_ManageSales_Delete_InvalidLeadID.Visible = true;
                }
            }
            catch (Exception ex)
            {
                if (tb_Home_ManageSales_Delete_LeadID.Text.Equals(string.Empty))
                {
                    lb_Home_ManageSales_Delete_InvalidLeadID.Visible = false;
                }
                else
                    lb_Home_ManageSales_Delete_InvalidLeadID.Visible = true;
            }
        }




        private void MenuStrip_Home_ManageTeams_Add_Click()
        {
            lb_Home_ManageTeams_Add_ActualDate.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            MenuStrip_Home_ManageTeams_Add.BackColor = Color.Gray;
            MenuStrip_Home_ManageTeams_Update.BackColor = Color.Purple;
            MenuStrip_Home_ManageTeams_Delete.BackColor = Color.Purple;
            panel_Home_ManageTeams_Add_Body.Show();
            panel_Home_ManageTeams_Update_Body.Hide();
            panel_Home_ManageTeams_Delete_Body.Hide();
            tb_Home_ManageTeams_Add_TeamName.Clear();
            cb_Home_ManageTeams_Add_TeamLead.SelectedIndex = -1;
            lb_Home_ManageTeams_Add_TeamName.Show();
            lb_Home_ManageTeams_Add_TeamLead.Show();

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("Select SalesRepName from SalesReps", con);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
            if (RecordFound)
            {
                CLB_Home_ManageTeams_Add_SelectSalesReps.Items.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    CLB_Home_ManageTeams_Add_SelectSalesReps.Items.Add(ds.Tables[0].Rows[i][0].ToString(), CheckState.Unchecked);
                }
            }
        }
        private void MenuStrip_Home_ManageTeams_Add_Click(object sender, EventArgs e)
        {
            MenuStrip_Home_ManageTeams_Add_Click();
        }
        private void MenuStrip_Home_ManageTeams_Update_Click()
        {
            MenuStrip_Home_ManageTeams_Update.BackColor = Color.Gray;
            MenuStrip_Home_ManageTeams_Add.BackColor = Color.Purple;
            MenuStrip_Home_ManageTeams_Delete.BackColor = Color.Purple;
            panel_Home_ManageTeams_Add_Body.Hide();
            panel_Home_ManageTeams_Update_Body.Show();
            panel_Home_ManageTeams_Delete_Body.Hide();
            cb_Home_ManageTeams_Update_TeamName.Items.Clear();
            cb_Home_ManageTeams_Update_TeamLead.SelectedIndex = -1;
            lb_Home_ManageTeams_Update_TeamName.Visible = true;
            lb_Home_ManageTeams_Update_TeamLead.Visible = true;
            lb_Home_ManageTeams_Update_ActualDate.Text = "";
            CLB_Home_ManageTeams_Update_SelectSalesReps.Items.Clear();
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("Select TeamName from Teams", con);
            con.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
            if (RecordFound)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cb_Home_ManageTeams_Update_TeamName.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
        }
        private void MenuStrip_Home_ManageTeams_Update_Click(object sender, EventArgs e)
        {
            MenuStrip_Home_ManageTeams_Update_Click();
        }

        private void MenuStrip_Home_ManageTeams_Delete_Click()
        {
            MenuStrip_Home_ManageTeams_Delete.BackColor = Color.Gray;
            MenuStrip_Home_ManageTeams_Add.BackColor = Color.Purple;
            MenuStrip_Home_ManageTeams_Update.BackColor = Color.Purple;
            panel_Home_ManageTeams_Add_Body.Hide();
            panel_Home_ManageTeams_Update_Body.Hide();
            panel_Home_ManageTeams_Delete_Body.Show();
            cb_Home_ManageTeams_Delete_TeamName.Items.Clear();
            lb_Home_ManageTeams_Delete_TeamName.Visible = true;
            lb_Home_ManageTeams_Delete_ActualDate.Text = "";
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("Select TeamName from Teams", con);
            con.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
            if (RecordFound)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][0].ToString() != "UNDEFINED")
                        cb_Home_ManageTeams_Delete_TeamName.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
        }
        private void MenuStrip_Home_ManageTeams_Delete_Click(object sender, EventArgs e)
        {
            MenuStrip_Home_ManageTeams_Delete_Click();
        }



        private void tb_Home_ManageTeams_Add_TeamName_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageTeams_Add_TeamName.Visible = false;
        }

        private void lb_Home_ManageTeams_Add_TeamName_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageTeams_Add_TeamName.Visible = false;
            tb_Home_ManageTeams_Add_TeamName.Focus();
        }
        private void cb_Home_ManageTeams_Add_TeamLead_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageTeams_Add_TeamLead.Visible = false;
        }

        private void lb_Home_ManageTeams_Add_TeamLead_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageTeams_Add_TeamLead.Visible = false;
            cb_Home_ManageTeams_Add_TeamLead.Focus();
        }



        private void cb_Home_ManageTeams_Update_TeamName_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageTeams_Update_TeamName.Visible = false;
        }
        private void lb_Home_ManageTeams_Update_TeamName_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageTeams_Update_TeamName.Visible = false;
            cb_Home_ManageTeams_Update_TeamName.Focus();
        }
        private void cb_Home_ManageTeams_Update_TeamLead_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageTeams_Update_TeamLead.Visible = false;
        }
        private void lb_Home_ManageTeams_Update_TeamLead_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageTeams_Update_TeamLead.Visible = false;
            cb_Home_ManageTeams_Update_TeamLead.Focus();
        }
        private void cb_Home_ManageTeams_Update_TeamLead_TextChanged(object sender, EventArgs e)
        {
            lb_Home_ManageTeams_Update_TeamLead.Visible = false;
        }
        private void cb_Home_ManageTeams_Update_TeamName_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("Select TeamLead,Date from Teams where TeamName = '" + cb_Home_ManageTeams_Update_TeamName.SelectedItem.ToString() + "'", con);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            cb_Home_ManageTeams_Update_TeamLead.SelectedItem = ds.Tables[0].Rows[0][0].ToString();
            lb_Home_ManageTeams_Update_ActualDate.Text = ds.Tables[0].Rows[0][1].ToString();

            cmd = new SqlCommand("Select SalesRepName,Team from SalesReps", con);
            con.Open();
            ds.Clear();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
            if (RecordFound)
            {
                CLB_Home_ManageTeams_Update_SelectSalesReps.Items.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if(cb_Home_ManageTeams_Update_TeamName.SelectedItem.ToString() == ds.Tables[0].Rows[i][3].ToString())
                    {
                        CLB_Home_ManageTeams_Update_SelectSalesReps.Items.Add(ds.Tables[0].Rows[i][2].ToString(),CheckState.Checked);
                    }
                    else
                        CLB_Home_ManageTeams_Update_SelectSalesReps.Items.Add(ds.Tables[0].Rows[i][2].ToString(), CheckState.Unchecked);
                }
            }
        }



        private void cb_Home_ManageTeams_Delete_TeamName_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageTeams_Delete_TeamName.Visible = false;
        }
        private void lb_Home_ManageTeams_Delete_TeamName_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageTeams_Delete_TeamName.Visible = false;
            cb_Home_ManageTeams_Delete_TeamName.Focus();
        }

        private void cb_Home_ManageTeams_Delete_TeamName_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("Select Date from Teams where TeamName = '" + cb_Home_ManageTeams_Delete_TeamName.SelectedItem.ToString() + "'", con);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            lb_Home_ManageTeams_Delete_ActualDate.Text = ds.Tables[0].Rows[0][0].ToString();
        }




        private void MenuStrip_Home_ManageSalesRep_Add_Click()
        {
            lb_Home_ManageSalesRep_Add_ActualDate.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            MenuStrip_Home_ManageSalesRep_Add.BackColor = Color.Gray;
            MenuStrip_Home_ManageSalesRep_Update.BackColor = Color.Purple;
            MenuStrip_Home_ManageSalesRep_Delete.BackColor = Color.Purple;
            panel_Home_ManageSalesRep_Add_Body.Show();
            panel_Home_ManageSalesRep_Update_Body.Hide();
            panel_Home_ManageSalesRep_Delete_Body.Hide();
            tb_Home_ManageSalesRep_Add_SaleRepName.Clear();
            cb_Home_ManageSalesRep_Add_Team.SelectedIndex = -1;
            lb_Home_ManageSalesRep_Add_SalesRepName.Visible = true;
            lb_Home_ManageSalesRep_Add_Team.Visible = true;
            cb_Home_ManageSalesRep_Add_Team.Items.Clear();

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("Select TeamName from Teams", con);
            con.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
            if (RecordFound)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cb_Home_ManageSalesRep_Add_Team.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            cmd = new SqlCommand("SELECT IDENT_CURRENT('SalesReps')", con);
            ds.Clear();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            lb_Home_ManageSalesRep_Add_ActualSalesRepID.Text = (Convert.ToInt32(ds.Tables[0].Rows[0][1]) + 1).ToString();
        }
        private void MenuStrip_Home_ManageSalesRep_Add_Click(object sender, EventArgs e)
        {
            MenuStrip_Home_ManageSalesRep_Add_Click();
        }
        private void MenuStrip_Home_ManageSalesRep_Update_Click()
        {
            MenuStrip_Home_ManageSalesRep_Update.BackColor = Color.Gray;
            MenuStrip_Home_ManageSalesRep_Add.BackColor = Color.Purple;
            MenuStrip_Home_ManageSalesRep_Delete.BackColor = Color.Purple;
            panel_Home_ManageSalesRep_Add_Body.Hide();
            panel_Home_ManageSalesRep_Update_Body.Show();
            panel_Home_ManageSalesRep_Delete_Body.Hide();
            lb_Home_ManageSalesRep_Update_ActualDate.Text = "";

            cb_Home_ManageSalesRep_Update_Team.Items.Clear();
            cb_Home_ManageSalesRep_Update_Team.SelectedIndex = -1;
            cb_Home_ManageSalesRep_Update_SalesRepID.Items.Clear();
            cb_Home_ManageSalesRep_Update_SalesRepID.SelectedIndex = -1;
            tb_Home_ManageSalesRep_Update_SaleRepName.Clear();
            lb_Home_ManageSalesRep_Update_Team.Visible = true;
            lb_Home_ManageSalesRep_Update_SalesRepID.Visible = true;
            lb_Home_ManageSalesRep_Update_SalesRepName.Visible = true;
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("Select SalesRepID from SalesReps", con);
            con.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            

            bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
            if (RecordFound)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cb_Home_ManageSalesRep_Update_SalesRepID.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }

            cmd = new SqlCommand("Select TeamName from Teams", con);
            ds.Clear();
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);


            RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
            if (RecordFound)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cb_Home_ManageSalesRep_Update_Team.Items.Add(ds.Tables[0].Rows[i][1].ToString());
                }
            }
            con.Close();

        }
        private void MenuStrip_Home_ManageSalesRep_Update_Click(object sender, EventArgs e)
        {
            MenuStrip_Home_ManageSalesRep_Update_Click();
        }

        private void MenuStrip_Home_ManageSalesRep_Delete_Click()
        {
            MenuStrip_Home_ManageSalesRep_Delete.BackColor = Color.Gray;
            MenuStrip_Home_ManageSalesRep_Add.BackColor = Color.Purple;
            MenuStrip_Home_ManageSalesRep_Update.BackColor = Color.Purple;
            panel_Home_ManageSalesRep_Add_Body.Hide();
            panel_Home_ManageSalesRep_Update_Body.Hide();
            panel_Home_ManageSalesRep_Delete_Body.Show();
            cb_Home_ManageSalesRep_Delete_SalesRepID.Items.Clear();
            cb_Home_ManageSalesRep_Delete_SalesRepID.SelectedIndex = -1;
            lb_Home_ManageSalesRep_Delete_SalesRepID.Visible = true;
            lb_Home_ManageSalesRep_Delete_ActualDate.Text = "";
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("Select SalesRepID, Date from SalesReps", con);
            con.Open();

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);


            bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
            if (RecordFound)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cb_Home_ManageSalesRep_Delete_SalesRepID.Items.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
        }
        private void MenuStrip_Home_ManageSalesRep_Delete_Click(object sender, EventArgs e)
        {
            MenuStrip_Home_ManageSalesRep_Delete_Click();
        }



        private void tb_Home_ManageSalesRep_Add_SaleRepName_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSalesRep_Add_SalesRepName.Visible = false;
        }

        private void lb_Home_ManageSalesRep_Add_SalesRepName_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSalesRep_Add_SalesRepName.Visible = false;
            tb_Home_ManageSalesRep_Add_SaleRepName.Focus();
        }
        private void cb_Home_ManageSalesRep_Add_Team_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSalesRep_Add_Team.Visible = false;
        }

        private void lb_Home_ManageSalesRep_Add_Team_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSalesRep_Add_Team.Visible = false;
            cb_Home_ManageSalesRep_Add_Team.Focus();
        }




        private void cb_Home_ManageSalesRep_Update_SaleRepID_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSalesRep_Update_SalesRepID.Visible = false;
        }

        private void lb_Home_ManageSalesRep_Update_SalesRepID_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSalesRep_Update_SalesRepID.Visible = false;
            cb_Home_ManageSalesRep_Update_SalesRepID.Focus();
        }
        private void tb_Home_ManageSalesRep_Update_SaleRepName_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSalesRep_Update_SalesRepName.Visible = false;
        }

        private void lb_Home_ManageSalesRep_Update_SalesRepName_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSalesRep_Update_SalesRepName.Visible = false;
            tb_Home_ManageSalesRep_Update_SaleRepName.Focus();
        }
        private void cb_Home_ManageSalesRep_Update_Team_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSalesRep_Update_Team.Visible = false;
        }

        private void lb_Home_ManageSalesRep_Update_Team_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSalesRep_Update_Team.Visible = false;
            cb_Home_ManageSalesRep_Update_Team.Focus();
        }

        private void tb_Home_ManageSalesRep_Update_SaleRepName_TextChanged(object sender, EventArgs e)
        {
            lb_Home_ManageSalesRep_Update_SalesRepName.Visible = false;
        }

        private void cb_Home_ManageSalesRep_Update_Team_TextChanged(object sender, EventArgs e)
        {
            lb_Home_ManageSalesRep_Update_Team.Visible = false;
        }
        private void cb_Home_ManageSalesRep_Update_SalesRepID_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("Select SalesRepName,Team,Date from SalesReps where SalesRepID = '" + cb_Home_ManageSalesRep_Update_SalesRepID.SelectedItem.ToString() + "'", con);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            tb_Home_ManageSalesRep_Update_SaleRepName.Text = ds.Tables[0].Rows[0][0].ToString();
            cb_Home_ManageSalesRep_Update_Team.SelectedItem = ds.Tables[0].Rows[0][1].ToString();
            lb_Home_ManageSalesRep_Update_ActualDate.Text = ds.Tables[0].Rows[0][2].ToString();
        }




        private void cb_Home_ManageSalesRep_Delete_SaleRepID_Enter(object sender, EventArgs e)
        {
            lb_Home_ManageSalesRep_Delete_SalesRepID.Visible = false;
        }

        private void lb_Home_ManageSalesRep_Delete_SalesRepID_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ManageSalesRep_Delete_SalesRepID.Visible = false;
            cb_Home_ManageSalesRep_Delete_SalesRepID.Focus();
        }
        private void cb_Home_ManageSalesRep_Delete_SalesRepID_TextChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
            SqlCommand cmd = new SqlCommand("Select Date from SalesReps where SalesRepID = '" + cb_Home_ManageSalesRep_Delete_SalesRepID.SelectedItem.ToString() + "'", con);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            lb_Home_ManageSalesRep_Delete_ActualDate.Text = ds.Tables[0].Rows[0][0].ToString();
        }







        private void tb_Home_AccountSettings_FName_Enter(object sender, EventArgs e)
        {
            lb_Home_AccountSettings_FName.Visible = false;
            lb_Home_AccountSettings_Updated.Hide();
        }

        private void lb_Home_AccountSettings_FName_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_AccountSettings_FName.Visible = false;
            tb_Home_AccountSettings_FName.Focus();
            lb_Home_AccountSettings_Updated.Hide();
        }
        private void tb_Home_AccountSettings_UserName_Enter(object sender, EventArgs e)
        {
            lb_Home_AccountSettings_UserName.Visible = false;
            lb_Home_AccountSettings_Updated.Hide();
        }

        private void lb_Home_AccountSettings_UserName_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_AccountSettings_UserName.Visible = false;
            tb_Home_AccountSettings_UserName.Focus();
            lb_Home_AccountSettings_Updated.Hide();
        }
        private void tb_Home_ChangePassword_OldPassword_Enter(object sender, EventArgs e)
        {
            lb_Home_ChangePassword_NewPassword.Visible = false;
            lb_Home_ChangePassword_Updated.Text = "";
        }
        private void lb_Home_ChangePassword_OldPassword_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ChangePassword_NewPassword.Visible = false;
            tb_Home_ChangePassword_NewPassword.Focus();
            lb_Home_ChangePassword_Updated.Text = "";
        }
        private void tb_Home_ChangePassword_NewPassword_Enter(object sender, EventArgs e)
        {
            lb_Home_ChangePassword_ConfirmNewPassword.Visible = false;
            lb_Home_ChangePassword_Updated.Text = "";
        }
        private void lb_Home_ChangePassword_NewPassword_MouseClick(object sender, MouseEventArgs e)
        {
            lb_Home_ChangePassword_ConfirmNewPassword.Visible = false;
            tb_Home_ChangePassword_ConfirmNewPassword.Focus();
            lb_Home_ChangePassword_Updated.Text = "";
        }
        private void tb_Home_AccountSettings_FName_TextChanged(object sender, EventArgs e)
        {
            lb_Home_AccountSettings_FName.Visible = false;
        }
        private void tb_Home_AccountSettings_UserName_TextChanged(object sender, EventArgs e)
        {
            lb_Home_AccountSettings_UserName.Visible = false;
        }
        private void tb_Home_ChangePassword_OldPassword_TextChanged(object sender, EventArgs e)
        {
            lb_Home_ChangePassword_NewPassword.Visible = false;
        }
        private void tb_Home_ChangePassword_NewPassword_TextChanged(object sender, EventArgs e)
        {
            lb_Home_ChangePassword_ConfirmNewPassword.Visible = false;
        }




        private void lb_Home_AccountSettings_MouseHover(object sender, EventArgs e)
        {
            lb_Home_AccountSettings.ForeColor = Color.Gray;
        }

        private void lb_Home_AccountSettings_MouseLeave(object sender, EventArgs e)
        {
            lb_Home_AccountSettings.ForeColor = Color.Purple;
        }

        private void lb_Home_Logout_MouseHover(object sender, EventArgs e)
        {
            lb_Home_Logout.ForeColor = Color.Gray;
        }

        private void lb_Home_Logout_MouseLeave(object sender, EventArgs e)
        {
            lb_Home_Logout.ForeColor = Color.Purple;
        }

        private void lb_Home_Logout_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            new Form_login().ShowDialog();
        }

        private void lb_Home_AccountSettings_MouseClick(object sender, MouseEventArgs e)
        {
            panel_Home_AllTabsData.Hide();
            panel_Home_AccountSettings.Show();
            tb_Home_AccountSettings_UserName.Text = GetUserDetails.UserName.ToString();
            tb_Home_AccountSettings_FName.Text = GetUserDetails.FName.ToString();
            tb_Home_ChangePassword_ConfirmNewPassword.Clear();
            lb_Home_ChangePassword_ConfirmNewPassword.Show();
            tb_Home_ChangePassword_NewPassword.Clear();
            lb_Home_ChangePassword_NewPassword.Show();
            lb_Home_AccountSettings_Updated.Hide();
            lb_Home_ChangePassword_Updated.Text = "";
        }

        private void btn_Home_AccountSettings_Close_MouseClick(object sender, MouseEventArgs e)
        {
            panel_Home_AccountSettings.Hide();
            panel_Home_AllTabsData.Show();
        }

        private void btn_Home_SalesRecord_Search_Click(object sender, EventArgs e)
        {
            String query = null;
            if (tb_Home_SalesRecord_Search.Text.Equals(string.Empty))
            {
                tb_Home_SalesRecord_Search.Focus();
            }
            else if (cb_Home_SalesRecord_SearchOptions.SelectedItem == null)
            {
                cb_Home_SalesRecord_SearchOptions.Focus();
            }
            else
            {
                if (cb_Home_SalesRecord_SearchOptions.SelectedItem.ToString() == "Lead ID")
                {
                    
                    query = "select * from Sales_Record where LeadID =" + tb_Home_SalesRecord_Search.Text.ToString();
                    DGV_SalesRecord_Fetch_Data(query);
                }
                else if (cb_Home_SalesRecord_SearchOptions.SelectedItem.ToString() == "Customer Name")
                {
                    query = "select * from Sales_Record where CustomerName = '" + tb_Home_SalesRecord_Search.Text.ToString() + "'";
                    DGV_SalesRecord_Fetch_Data(query);
                }
                else if (cb_Home_SalesRecord_SearchOptions.SelectedItem.ToString() == "Phone No")
                {
                    query = "select * from Sales_Record where PhoneNo = '" + tb_Home_SalesRecord_Search.Text.ToString() + "'";
                    DGV_SalesRecord_Fetch_Data(query);
                }
                else if (cb_Home_SalesRecord_SearchOptions.SelectedItem.ToString() == "State")
                {
                    query = "select * from Sales_Record where State = '" + tb_Home_SalesRecord_Search.Text.ToString() + "'";
                    DGV_SalesRecord_Fetch_Data(query);
                }
                else if (cb_Home_SalesRecord_SearchOptions.SelectedItem.ToString() == "Team")
                {
                    query = "select * from Sales_Record where Team = '" + tb_Home_SalesRecord_Search.Text.ToString() + "'";
                    DGV_SalesRecord_Fetch_Data(query);
                }
                else
                {
                    query = "select * from Sales_Record where SalesRep = '" + tb_Home_SalesRecord_Search.Text.ToString() + "'";
                    DGV_SalesRecord_Fetch_Data(query);
                }
            }
        }

        private void btn_Home_ManageTeams_Add_Click(object sender, EventArgs e)
        {
            if (tb_Home_ManageTeams_Add_TeamName.Text.Equals(string.Empty))
            {
                tb_Home_ManageTeams_Add_TeamName.Focus();
            }
            else if (cb_Home_ManageTeams_Add_TeamLead.SelectedItem == null)
            {
                cb_Home_ManageTeams_Add_TeamLead.Focus();
            }
            else
            {
                try{
                    String Date = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString(); ;
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                    SqlCommand cmd = new SqlCommand("Insert into Teams Values('"+ Date + "','" + tb_Home_ManageTeams_Add_TeamName.Text + "','" + cb_Home_ManageTeams_Add_TeamLead.SelectedItem.ToString() + "')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < CLB_Home_ManageTeams_Add_SelectSalesReps.CheckedItems.Count; i++)
                    {
                        cmd = new SqlCommand("Update SalesReps Set Team = '" + tb_Home_ManageTeams_Add_TeamName.Text + "' where SalesRepName = '" + CLB_Home_ManageTeams_Add_SelectSalesReps.CheckedItems[i].ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    MenuStrip_Home_ManageTeams_Add_Click();
                }
                catch(Exception ex){
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btn_Home_ManageTeams_Update_Click(object sender, EventArgs e)
        {
            if (cb_Home_ManageTeams_Update_TeamName.SelectedItem == null)
            {
                cb_Home_ManageTeams_Update_TeamName.Focus();
            }
            else if (cb_Home_ManageTeams_Update_TeamLead.SelectedItem == null)
            {
                cb_Home_ManageTeams_Update_TeamLead.Focus();
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                    SqlCommand cmd = new SqlCommand("Update Teams Set TeamLead = '" + cb_Home_ManageTeams_Update_TeamLead.SelectedItem.ToString() + "' where TeamName = '" + cb_Home_ManageTeams_Update_TeamName.SelectedItem.ToString() + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    for(int i = 0; i < CLB_Home_ManageTeams_Update_SelectSalesReps.CheckedItems.Count; i++)
                    {
                        cmd = new SqlCommand("Update SalesReps Set Team = '" + cb_Home_ManageTeams_Update_TeamName.SelectedItem.ToString() + "' where SalesRepName = '" + CLB_Home_ManageTeams_Update_SelectSalesReps.CheckedItems[i].ToString() + "'",con);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                MenuStrip_Home_ManageTeams_Update_Click();
            }
        }

        private void btn_Home_ManageTeams_Delete_Click(object sender, EventArgs e)
        {
            if (cb_Home_ManageTeams_Delete_TeamName.SelectedItem == null)
            {
                cb_Home_ManageTeams_Delete_TeamName.Focus();
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                    SqlCommand cmd = new SqlCommand("delete from Teams where TeamName = '" + cb_Home_ManageTeams_Delete_TeamName.SelectedItem.ToString() + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Update SalesReps Set Team = 'UNDEFINED' where Team = '" + cb_Home_ManageTeams_Delete_TeamName.SelectedItem.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                MenuStrip_Home_ManageTeams_Delete_Click();
            }

        }

        private void btn_Home_ManageSalesRep_Add_Click(object sender, EventArgs e)
        {
            if (tb_Home_ManageSalesRep_Add_SaleRepName.Text.Equals(string.Empty))
            {
                tb_Home_ManageSalesRep_Add_SaleRepName.Focus();
            }
            else if (cb_Home_ManageSalesRep_Add_Team.SelectedItem == null)
            {
                cb_Home_ManageSalesRep_Add_Team.Focus();
            }
            else
            {
                try
                {
                    int space1 = tb_Home_ManageSalesRep_Add_SaleRepName.Text.IndexOf(' ');
                    String Date = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString(); ;
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                    SqlCommand cmd = new SqlCommand("Insert into SalesReps Values('" + Date + "','" + tb_Home_ManageSalesRep_Add_SaleRepName.Text + "','" + cb_Home_ManageSalesRep_Add_Team.SelectedItem.ToString() + "')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("SELECT IDENT_CURRENT('SalesReps')", con);
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    String UserName = tb_Home_ManageSalesRep_Add_SaleRepName.Text.Substring(0, space1) + "."+  ds.Tables[0].Rows[0][0].ToString() + "@w4work";
                    String Password = tb_Home_ManageSalesRep_Add_SaleRepName.Text.Substring(0, space1) + "12345";
                    cmd = new SqlCommand("Insert into Users Values('" + UserName + "','" + Password + "','" + tb_Home_ManageSalesRep_Add_SaleRepName.Text + "','Sales Rep')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                MenuStrip_Home_ManageSalesRep_Add_Click();
            }
        }

        private void btn_Home_ManageSalesRep_Update_Click(object sender, EventArgs e)
        {
            if (cb_Home_ManageSalesRep_Update_SalesRepID.SelectedItem == null)
            {
                cb_Home_ManageSalesRep_Update_SalesRepID.Focus();
            }
            else if (tb_Home_ManageSalesRep_Update_SaleRepName.Text.Equals(string.Empty))
            {
                tb_Home_ManageSalesRep_Update_SaleRepName.Focus();
            }
            else if (cb_Home_ManageSalesRep_Update_Team.SelectedItem == null)
            {
                cb_Home_ManageSalesRep_Update_Team.Focus();
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                    SqlCommand cmd = new SqlCommand("Select SalesRepName from SalesReps where SalesRepID = '"+ cb_Home_ManageSalesRep_Update_SalesRepID.SelectedItem.ToString() + "'", con);
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    cmd = new SqlCommand("Update SalesReps Set SalesRepName = '" + tb_Home_ManageSalesRep_Update_SaleRepName.Text.ToString() + "', Team = '" + cb_Home_ManageSalesRep_Update_Team.SelectedItem.ToString() + "' where SalesRepID = '" + cb_Home_ManageSalesRep_Update_SalesRepID.SelectedItem.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Update Users Set FullName = '" + tb_Home_ManageSalesRep_Update_SaleRepName.Text.ToString() +  "' where FullName = '" + ds.Tables[0].Rows[0][0].ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Update Sales_Record Set SalesRep = '" + tb_Home_ManageSalesRep_Update_SaleRepName.Text.ToString() + "' where SalesRep = '" + ds.Tables[0].Rows[0][0].ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                MenuStrip_Home_ManageSalesRep_Update_Click();
            }
        }

        private void btn_Home_ManageSalesRep_Delete_Click(object sender, EventArgs e)
        {
            if (cb_Home_ManageSalesRep_Delete_SalesRepID.SelectedItem == null)
            {
                cb_Home_ManageSalesRep_Delete_SalesRepID.Focus();
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                    SqlCommand cmd = new SqlCommand("Select SalesRepName from SalesReps where SalesRepID = '" + cb_Home_ManageSalesRep_Delete_SalesRepID.SelectedItem.ToString() + "'", con);
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    cmd = new SqlCommand("delete from SalesReps where SalesRepID = '" + cb_Home_ManageSalesRep_Delete_SalesRepID.SelectedItem.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("delete from Users where FullName = '" + ds.Tables[0].Rows[0][0].ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                MenuStrip_Home_ManageSalesRep_Delete_Click();
            }
        }

        private void btn_Home_ManageSales_Add_Click(object sender, EventArgs e)
        {

            if (tb_Home_ManageSales_Add_CustomerName.Text.Equals(string.Empty))
            {
                tb_Home_ManageSales_Add_CustomerName.Focus();
            }
            else if (tb_Home_ManageSales_Add_PhoneNo.Text.Equals(string.Empty))
            {
                tb_Home_ManageSales_Add_PhoneNo.Focus();
            }
            else if (cb_Home_ManageSales_Add_State.SelectedItem == null)
            {
                cb_Home_ManageSales_Add_State.Focus();
            }
            else if (cb_Home_ManageSales_Add_SalesRepID.SelectedItem == null)
            {
                cb_Home_ManageSales_Add_SalesRepID.Focus();
            }
            else
            {
                try
                {
                    String Date = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString(); ;
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                    SqlCommand cmd = new SqlCommand("Select SalesRepName, Team from SalesReps where SalesRepID = " + cb_Home_ManageSales_Add_SalesRepID.SelectedItem.ToString(), con);
                    DataSet ds = new DataSet();
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    cmd = new SqlCommand("Insert into Sales_Record Values('" + Date + "','" + tb_Home_ManageSales_Add_CustomerName.Text + "','" + tb_Home_ManageSales_Add_PhoneNo.Text + "','" + cb_Home_ManageSales_Add_State.SelectedItem.ToString() + "','" + ds.Tables[0].Rows[0][1] + "','" + ds.Tables[0].Rows[0][0] + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                MenuStrip_Home_ManageSales_Add_Click();
            }
        }

        private void btn_Home_ManageSales_Update_Click(object sender, EventArgs e)
        {
            if (tb_Home_ManageSales_Update_LeadID.Text.Equals(string.Empty))
            {
                tb_Home_ManageSales_Update_LeadID.Focus();
            }
            else if (tb_Home_ManageSales_Update_CustomerName.Text.Equals(string.Empty))
            {
                tb_Home_ManageSales_Update_CustomerName.Focus();
            }
            else if (tb_Home_ManageSales_Update_PhoneNo.Text.Equals(string.Empty))
            {
                tb_Home_ManageSales_Update_PhoneNo.Focus();
            }
            else if (cb_Home_ManageSales_Update_State.SelectedItem == null)
            {
                cb_Home_ManageSales_Update_State.Focus();
            }
            else if (cb_Home_ManageSales_Update_SalesRepID.SelectedItem == null)
            {
                cb_Home_ManageSales_Update_SalesRepID.Focus();
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                    SqlCommand cmd = new SqlCommand("Select SalesRepName, Team from SalesReps where SalesRepID = " + cb_Home_ManageSales_Update_SalesRepID.SelectedItem.ToString(), con);
                    DataSet ds = new DataSet();
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    cmd = new SqlCommand("update Sales_Record Set CustomerName ='" + tb_Home_ManageSales_Update_CustomerName.Text + "', PhoneNo = '" + tb_Home_ManageSales_Update_PhoneNo.Text + "', State = '" + cb_Home_ManageSales_Update_State.SelectedItem.ToString() + "', Team ='" + ds.Tables[0].Rows[0][1] + "', SalesRep = '" + ds.Tables[0].Rows[0][0] + "' where LeadID = '" + tb_Home_ManageSales_Update_LeadID.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                MenuStrip_Home_ManageSales_Update_Click();
                tb_Home_ManageSales_Update_LeadID.Clear();
                lb_Home_ManageSales_Update_LeadID.Visible = true;
            }
        }

        private void btn_Home_ManageSales_Delete_Click(object sender, EventArgs e)
        {
            if (tb_Home_ManageSales_Delete_LeadID.Text.Equals(string.Empty))
            {
                tb_Home_ManageSales_Delete_LeadID.Focus();
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                    SqlCommand cmd = new SqlCommand("Select * from Sales_Record where LeadID = '" + tb_Home_ManageSales_Delete_LeadID.Text + "'", con);
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    bool RecordFound = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));
                    if (RecordFound)
                    {
                        cmd = new SqlCommand("delete from Sales_Record where LeadID = '" + tb_Home_ManageSales_Delete_LeadID.Text + "'", con);
                        cmd.ExecuteNonQuery();

                        MenuStrip_Home_ManageSales_Delete_Click();
                    }
                    else
                    {
                        lb_Home_ManageSales_Delete_InvalidLeadID.Visible = true;
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void btn_Home_AccountSettings_Save_Click(object sender, EventArgs e)
        {
            if (tb_Home_AccountSettings_UserName.Text.Equals(string.Empty)){
                tb_Home_AccountSettings_UserName.Focus();
            }
            else if (tb_Home_AccountSettings_FName.Text.Equals(string.Empty))
            {
                tb_Home_AccountSettings_FName.Focus();
            }
            else
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                SqlCommand cmd = new SqlCommand("Update Users Set UserName = '" + tb_Home_AccountSettings_UserName.Text + "', FullName = '" + tb_Home_AccountSettings_FName.Text + "' where UserName = '" + GetUserDetails.UserName.ToString() + "'", con);
                GetUserDetails.UserName = tb_Home_AccountSettings_UserName.Text;
                GetUserDetails.FName = tb_Home_AccountSettings_FName.Text;
                lb_LoggedInName.Text = tb_Home_AccountSettings_FName.Text; ;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                lb_Home_AccountSettings_Updated.Show();
            }
        }

        private void btn_Home_ChangePassword_Save_Click(object sender, EventArgs e)
        {
            if (tb_Home_ChangePassword_NewPassword.Text.Equals(string.Empty))
            {
                tb_Home_ChangePassword_NewPassword.Focus();
            }
            else if (tb_Home_ChangePassword_ConfirmNewPassword.Text.Equals(string.Empty))
            {
                tb_Home_ChangePassword_ConfirmNewPassword.Focus();
            }
            else
            {
                if (tb_Home_ChangePassword_NewPassword.Text.Equals(tb_Home_ChangePassword_ConfirmNewPassword.Text))
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-02SDTMJ\SQLEXPRESS;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Knack Sales Portal;");
                    SqlCommand cmd = new SqlCommand("Update Users Set Password = '" + tb_Home_ChangePassword_NewPassword.Text + "' where UserName = '" + GetUserDetails.UserName + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    lb_Home_ChangePassword_Updated.Text = "Password Updated";
                    tb_Home_ChangePassword_NewPassword.Clear();
                    lb_Home_ChangePassword_NewPassword.Show();
                    tb_Home_ChangePassword_ConfirmNewPassword.Clear();
                    lb_Home_ChangePassword_ConfirmNewPassword.Show();
                }
                else
                    lb_Home_ChangePassword_Updated.Text = "Mismatched Password";
            }
        }

        //private void tb_Home_SalesRecord_Search_TextChanged(object sender, EventArgs e)
        //{
        //    String query = "select * from Sales_Record where SalesRep like '" + tb_Home_SalesRecord_Search.Text.ToString() + "%'" ;
        //    DGV_SalesRecord_Fetch_Data(query);
        //}
    }
}
