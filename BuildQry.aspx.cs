using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;

public partial class BuildQry : System.Web.UI.Page
{
    private static string cnnstrSource = "";
    private static bool InnerJoin = false;
    private static bool AddJoin = false;

    protected void GenerateSQL()
    {
        string[] myArray = new string[lsbFields.Items.Count];

        for (int i = 0; i < lsbFields.Items.Count; i++)
        {
            string[] spl = lsbFields.Items[i].Text.Split('.');
            myArray[i] = spl[0];
        }

        for (int i = 0; i < lsbFields.Items.Count - 1; i++)
        {
            if (myArray[i].Equals(myArray[i + 1]))
            {
            }
            else
            {
                InnerJoin = true;
            }
        }

        //Generate SQL
        txtSQL.Text = "SELECT ";

        //Checking fields are available or not!
        if (lsbFields.Items.Count != 0)
        {
            for (int j = 0; j < lsbFields.Items.Count; j++)
            {
                txtSQL.Text += lsbFields.Items[j].Text + ", ";
            }

            //Remove extra charecters
            txtSQL.Text = txtSQL.Text.Substring(0, txtSQL.Text.Length - 2);
            txtSQL.Text += " FROM";


            if (InnerJoin == false)
            {
                string[] spl = lsbFields.Items[0].Text.Split('.');
                txtSQL.Text += " " + spl[0];
            }
            else
            {

                for (int i = 0; i < chbKeys.Items.Count; i++)
                {
                    if (chbKeys.Items[i].Selected)
                    {
                        AddJoin = true;
                    }
                }

                string[] flds = new string[lsbFields.Items.Count];
                for (int i = 0; i < lsbFields.Items.Count; i++)
                {
                    string[] spl = lsbFields.Items[i].Text.Split('.');
                    flds[i] = spl[0];
                }


                ArrayList al1 = new ArrayList(flds);
                ArrayList al2 = new ArrayList();
                foreach (string str in al1)
                {
                    if (!al2.Contains(str))
                    {
                        al2.Add(str);
                    }
                }

                if (AddJoin)
                {
                    for (int i = 0; i < al2.Count - 1; i++)
                    {
                        txtSQL.Text += " " + al2[i] + " INNER JOIN " + al2[i + 1];
                    }

                    txtSQL.Text += " ON " + ddlFK.SelectedItem.Text + " = " + ddlPK.SelectedItem.Text;
                }
                else
                {
                    for (int i = 0; i < al2.Count - 1; i++)
                    {
                        txtSQL.Text += " " + al2[i] + " CROSS JOIN " + al2[i + 1];
                    }
                    lblMSG.Text = "شما فیلدهایی از دو جدول را انتخاب نموده اید ولی ارتباط را مشخص نکرده اید. در واقع خروجی شما CROSS JOIN می باشد";
                }
            }
        }
        else
        {
            lblMSG.Text = "لطفا فیلدهای پرس و جو را انتخاب نمایید";
        }

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMSG.Text = "";

            if (!IsPostBack)
            {
                //Get themes
                //////string[] themes = Directory.GetDirectories(Server.MapPath("~/App_Themes"));
                //////for (int i = 0; i < themes.Length; i++)
                //////{
                //////    ddlThemes.Items.Insert(i, new ListItem(Path.GetFileName(themes[i]), themes[i]));
                //////}

                //Get connectionString from web.config
                ConnectionStringSettingsCollection connectionStrings =
                                    ConfigurationManager.ConnectionStrings;

                foreach (ConnectionStringSettings connection in connectionStrings)
                {
                    ddlConnectionStringSource.Items.Add(connection.Name);
                }
            }

            lblSRCd.Text = cnnstrSource = ConfigurationManager.ConnectionStrings[ddlConnectionStringSource.Text].ConnectionString.Replace("/", "//");
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }

    }

    protected void Loadtbl()
    {
        SqlConnection cnn = null;

        //Connect to db and get dbs
        cnn = new SqlConnection(cnnstrSource);
        cnn.Open();
        SqlCommand cmd = new SqlCommand("select * from sys.tables order by NAME", cnn);
        SqlDataReader dr = cmd.ExecuteReader();

        twTables.Nodes.Clear();

        //read records and bind to list
        while (dr.Read())
        {
            twTables.Nodes.Add(new TreeNode(dr[0].ToString()));
        }
        dr.Dispose();
        dr.Close();

        //Assign fields
        for (int k = 0; k < Convert.ToInt32(twTables.Nodes.Count); k++)
        {
            DataTable schemaTable = cnn.GetSchema("Columns", new string[] { null, null, twTables.Nodes[k].Text, null });
            twTables.Nodes[k].SelectAction = TreeNodeSelectAction.SelectExpand;
            for (int j = 0; j < schemaTable.Rows.Count; j++)
            {
                twTables.Nodes[k].ChildNodes.AddAt(j, new TreeNode(schemaTable.Rows[j][3].ToString()));
                twTables.Nodes[k].ChildNodes[j].SelectAction = TreeNodeSelectAction.None;
            }
        }
        cnn.Close();
    }

    protected void Loadqry()
    {
        SqlConnection cnn = null;

        //Connect to db and get dbs
        cnn = new SqlConnection(cnnstrSource);
        cnn.Open();
        SqlCommand cmd = new SqlCommand("select * from sys.views order by NAME", cnn);
        SqlDataReader dr = cmd.ExecuteReader();

        twViews.Nodes.Clear();

        //read records and bind to list
        while (dr.Read())
        {
            twViews.Nodes.Add(new TreeNode(dr[0].ToString()));
        }
        dr.Dispose();
        dr.Close();

        //Assign fields
        for (int k = 0; k < Convert.ToInt32(twViews.Nodes.Count); k++)
        {
            DataTable schemaTable = cnn.GetSchema("Columns", new string[] { null, null, twViews.Nodes[k].Text, null });
            twViews.Nodes[k].SelectAction = TreeNodeSelectAction.SelectExpand;
            for (int j = 0; j < schemaTable.Rows.Count; j++)
            {
                twViews.Nodes[k].ChildNodes.AddAt(j, new TreeNode(schemaTable.Rows[j][3].ToString()));
                twViews.Nodes[k].ChildNodes[j].SelectAction = TreeNodeSelectAction.None;
            }
        }
        cnn.Close();
    }

    protected void btnStep1_Click(object sender, EventArgs e)
    {
        try
        {
            Loadtbl();
            Loadqry();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void ddlConnectionStringSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblSRCd.Text = cnnstrSource;
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }
    protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlConnectionStringSource.Items.Clear();

            //Get connectionString from web.config
            ConnectionStringSettingsCollection connectionStrings =
                                ConfigurationManager.ConnectionStrings;

            foreach (ConnectionStringSettings connection in connectionStrings)
            {
                ddlConnectionStringSource.Items.Add(connection.Name);
            }
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    protected void btnAddFields_Click(object sender, EventArgs e)
    {
        try
        {
            ddlPK.Items.Clear();
            ddlFK.Items.Clear();
            lsbFields.Items.Clear();

            //TBL
            for (int i = 0; i < twTables.Nodes.Count; i++)
            {
                for (int j = 0; j < twTables.Nodes[i].ChildNodes.Count; j++)
                {
                    if (twTables.Nodes[i].ChildNodes[j].Checked)
                    {
                        ddlPK.Items.Add(new ListItem(twTables.Nodes[i].Value + "." + twTables.Nodes[i].ChildNodes[j].Text, twTables.Nodes[i].Value + "." + twTables.Nodes[i].ChildNodes[j].Value));
                        ddlFK.Items.Add(new ListItem(twTables.Nodes[i].Value + "." + twTables.Nodes[i].ChildNodes[j].Text, twTables.Nodes[i].Value + "." + twTables.Nodes[i].ChildNodes[j].Value));
                        lsbFields.Items.Add(new ListItem(twTables.Nodes[i].Value + "." + twTables.Nodes[i].ChildNodes[j].Text, twTables.Nodes[i].Value + "." + twTables.Nodes[i].ChildNodes[j].Value));
                    }
                }
            }

            //QRY
            for (int i = 0; i < twViews.Nodes.Count; i++)
            {
                for (int j = 0; j < twViews.Nodes[i].ChildNodes.Count; j++)
                {
                    if (twViews.Nodes[i].ChildNodes[j].Checked)
                    {
                        ddlPK.Items.Add(new ListItem(twViews.Nodes[i].Value + "." + twViews.Nodes[i].ChildNodes[j].Text, twViews.Nodes[i].Value + "." + twViews.Nodes[i].ChildNodes[j].Value));
                        ddlFK.Items.Add(new ListItem(twViews.Nodes[i].Value + "." + twViews.Nodes[i].ChildNodes[j].Text, twViews.Nodes[i].Value + "." + twViews.Nodes[i].ChildNodes[j].Value));
                        lsbFields.Items.Add(new ListItem(twViews.Nodes[i].Value + "." + twViews.Nodes[i].ChildNodes[j].Text, twViews.Nodes[i].Value + "." + twViews.Nodes[i].ChildNodes[j].Value));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    protected void imgbUP_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (lsbFields.SelectedIndex != 0)
            {
                int i = lsbFields.SelectedIndex;
                string s = lsbFields.Items[i].Text;
                string s1 = lsbFields.Items[i - 1].Text;
                lsbFields.Items[i - 1].Text = s;
                lsbFields.Items[i].Text = s1;
            }
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }
    protected void imgbDOWN_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (lsbFields.SelectedIndex != lsbFields.Items.Count)
            {
                int i = lsbFields.SelectedIndex;
                string s = lsbFields.Items[i].Text;
                string s1 = lsbFields.Items[i + 1].Text;
                lsbFields.Items[i + 1].Text = s;
                lsbFields.Items[i].Text = s1;
            }
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    protected void btnSaveChange_Click(object sender, EventArgs e)
    {
        try
        {
            GenerateSQL();
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    protected void btnAddKey_Click(object sender, EventArgs e)
    {
        try
        {
            string[] spl = ddlPK.SelectedItem.Text.Split('.');
            chbKeys.Items.Add(new ListItem(ddlFK.SelectedItem.Text + " = " + ddlPK.SelectedItem.Text, spl[0] + " ON " + ddlFK.SelectedItem.Text + " = " + ddlPK.SelectedItem.Text));

            for (int i = 0; i < chbKeys.Items.Count; i++)
            {
                chbKeys.Items[i].Selected = true;
            }

            GenerateSQL();
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cnn = new SqlConnection(cnnstrSource);
            cnn.Open();
            DataSet ds = new DataSet("Result");
            SqlDataAdapter da = new SqlDataAdapter(txtSQL.Text, cnn);
            da.Fill(ds, "Result");
            gdwExport.DataSource = ds;
            gdwExport.DataMember = "Result";
            gdwExport.DataBind();
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }
    protected void btnSaveView_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cnn = new SqlConnection(cnnstrSource);
            cnn.Open();
            string sql = "CREATE VIEW " + txtViewName.Text +
                            " AS " + txtSQL.Text;
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }
}
