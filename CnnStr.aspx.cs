using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Xml;

public partial class CnnStr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Get all of the sql server services in LAN
                string[] servers = DBGrep.SqlLocator.GetServers();
                ddlServers.DataSource = servers;
                ddlServers.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }


    protected void btnConnect_Click(object sender, EventArgs e)
    {
        try
        {
            // write a new value for the Test1 setting
            ConfigSettings.WriteSetting(ddlServers.Text + "_" + ddlDatabase.Text, "Data Source=" + ddlServers.Text + ";Initial Catalog=" + ddlDatabase.Text + ";User ID=" + txtUsername.Text + ";Password=" + txtPassword.Text + "");
            lblMSG.Text = "کانکشن مورد نظر با موفقیت ایجاد شد";
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    protected void lbtnNewServer_Click(object sender, EventArgs e)
    {
        try
        {
            txtNewServer.Visible = true;
            btnAddServer.Visible = true;
            Label5.Visible = true;
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    protected void btnAddServer_Click(object sender, EventArgs e)
    {
        try
        {
            ddlServers.Items.Add(new ListItem(txtNewServer.Text));
            txtNewServer.Visible = false;
            btnAddServer.Visible = false;
            Label5.Visible = false;
            ddlServers.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    protected void BtnShowDBs_Click(object sender, EventArgs e)
    {
        try
        {
            OleDbConnection cnn = null;

            //Connect to db and get dbs
            cnn = new OleDbConnection("Provider=SQLOLEDB;Data Source=" + ddlServers.Text + ";User ID=" + txtUsername.Text + ";Password=" + txtPassword.Text + ";");
            cnn.Open();
            OleDbCommand cmd = new OleDbCommand("sp_databases", cnn);
            OleDbDataReader dr = cmd.ExecuteReader();

            //Connect to db & get number of dbs
            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd.CommandText, cnn);
            da.Fill(ds);

            ddlDatabase.Items.Clear();

            //read records and bind to list
            while (dr.Read())
            {
                ddlDatabase.Items.Add(new ListItem(dr[0].ToString()));
            }
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }
}
