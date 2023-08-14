using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

public partial class _Default : System.Web.UI.Page
{
    private static string cnnstrSource = "";

    protected void GenerateSQL()
    {
        string WHERE = "";
        string ORDERBY = "";


        //Generate SQL
        txtSQL.Text = "SELECT ";

        //Get number of fields in availabe mode
        //Checking fields are available or not!
        if (gdwConditions.Rows.Count != 0)
        {
            //chbSequential is enabled
            if (chbSequential.Checked)
            {
                txtSQL.Text += "(ROW_NUMBER() OVER(ORDER BY " + ddlColumns2.SelectedItem.Text + ") ) as [ردیف], ";
            }

            for (int j = 0; j < gdwConditions.Rows.Count; j++)
            {
                CheckBox ch = (CheckBox)gdwConditions.Rows[j].FindControl("chbExport");
                if (ch.Checked)
                {
                    //ALIAS
                    TextBox t = (TextBox)gdwConditions.Rows[j].FindControl("txtAlias");
                    if (t.Text != string.Empty)
                    {
                        txtSQL.Text += gdwConditions.Rows[j].Cells[4].Text + " AS [" + t.Text + "], ";
                    }
                    else
                    {
                        txtSQL.Text += gdwConditions.Rows[j].Cells[4].Text + ", ";
                    }

                    //ORDERBY
                    DropDownList d = (DropDownList)gdwConditions.Rows[j].FindControl("ddlSortType");
                    if (d.SelectedValue != "Unsorted")
                    {
                        if (t.Text != string.Empty)
                        {
                            if (d.SelectedValue == "Descending")
                            {
                                ORDERBY += t.Text + " " + d.SelectedValue + ", ";
                            }
                            else
                            {
                                ORDERBY += t.Text + ", ";
                            }
                        }
                        else
                        {
                            if (d.SelectedValue == "Descending")
                            {
                                ORDERBY += gdwConditions.Rows[j].Cells[4].Text + " " + d.SelectedValue + ", ";
                            }
                            else
                            {
                                ORDERBY += gdwConditions.Rows[j].Cells[4].Text + ", ";
                            }
                        }
                    }
                }
            }

            //WHERE
            for (int i = 0; i < chbFilters.Items.Count; i++)
            {
                if (chbFilters.Items[i].Selected)
                {
                    WHERE += chbFilters.Items[i].Value;
                }
            }


            //Remove extra charecters
            txtSQL.Text = txtSQL.Text.Substring(0, txtSQL.Text.Length - 2);
            txtSQL.Text += " FROM ";

            //Assign selected tbl or qry
            if (lsbTables.SelectedItem != null)
            {
                txtSQL.Text += lsbTables.SelectedItem.Text;
            }
            else
            {
                txtSQL.Text += lsbQueries.SelectedItem.Text;
            }

            //WHERE
            if (WHERE != "")
            {
                if (WHERE.EndsWith("AND "))
                {
                    WHERE = WHERE.Substring(0, WHERE.Length - 5);
                }
                else
                {
                    WHERE = WHERE.Substring(0, WHERE.Length - 4);
                }
                txtSQL.Text += " WHERE " + WHERE;
            }

            //ORDER BY
            if (ORDERBY != "")
            {
                ORDERBY = ORDERBY.Substring(0, ORDERBY.Length - 2);
                txtSQL.Text += " ORDER BY " + ORDERBY;
            }
        }
        else
        {
            lblMSG.Text = "لطفا فیلدهای گزارش را انتخاب نمایید";
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

                Panel1.Visible = false;
                Panel2.Visible = false;
                Panel5.Visible = false;
            }

            lblSRCd.Text = cnnstrSource = ConfigurationManager.ConnectionStrings[ddlConnectionStringSource.Text].ConnectionString.Replace("/", "//");
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
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

    protected void lsbTables_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (lsbQueries.SelectedItem != null)
            {
                lsbQueries.SelectedItem.Selected = false;
            }

            chbFields.Items.Clear();
            lblFields.Visible = true;
            lbtnSelectAll.Visible = true;

            SqlConnection cnn = new SqlConnection(cnnstrSource);
            cnn.Open();

            DataTable schemaTable = cnn.GetSchema("Columns", new string[] { null, null, lsbTables.SelectedValue, null });
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                for (int k = 0; k < schemaTable.Rows.Count; k++)
                {
                    if (schemaTable.Rows[k][4].Equals(i + 1))
                    {
                        chbFields.Items.Insert(i, new ListItem(schemaTable.Rows[k][3].ToString()));
                    }
                }
            }

            cnn.Close();
            txtReportName.Text = lsbTables.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    protected void lsbQueries_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (lsbTables.SelectedItem != null)
            {
                lsbTables.SelectedItem.Selected = false;
            }


            chbFields.Items.Clear();
            lblFields.Visible = true;
            lbtnSelectAll.Visible = true;

            SqlConnection cnn = new SqlConnection(cnnstrSource);
            cnn.Open();

            DataTable schemaTable = cnn.GetSchema("Columns", new string[] { null, null, lsbQueries.SelectedValue, null });
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                for (int k = 0; k < schemaTable.Rows.Count; k++)
                {
                    if (schemaTable.Rows[k][4].Equals(i + 1))
                    {
                        chbFields.Items.Insert(i, new ListItem(schemaTable.Rows[k][3].ToString()));
                    }
                }
            }

            cnn.Close();
            txtReportName.Text = lsbQueries.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    protected void btnStep1_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cnn = new SqlConnection(cnnstrSource);
            cnn.Open();

            //TABLES

            SqlCommand cmd = new SqlCommand("select * from sys.tables order by NAME", cnn);
            SqlDataReader dr = cmd.ExecuteReader();

            lsbTables.Items.Clear();

            //Bind dbs to listbox
            while (dr.Read())
            {
                lsbTables.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
            }
            dr.Dispose();
            dr.Close();

            //VIEWS

            cmd.CommandText = "select * from sys.views order by NAME";
            dr = cmd.ExecuteReader();

            lsbQueries.Items.Clear();

            //Bind dbs to listbox            
            while (dr.Read())
            {
                lsbQueries.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
            }
            dr.Dispose();
            dr.Close();
            cnn.Close();

            Panel0.Visible = false;
            Panel1.Visible = true;
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    protected void btnStep2_Click(object sender, EventArgs e)
    {
        try
        {
            ddlColumns.Items.Clear();
            ddlColumns2.Items.Clear();
            ddlColumns3.Items.Clear();

            System.Collections.Specialized.StringCollection sc = new System.Collections.Specialized.StringCollection();

            for (int i = 0; i < chbFields.Items.Count; i++)
            {
                if (chbFields.Items[i].Selected)
                {
                    sc.Add(chbFields.Items[i].Text);
                }
            }

            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("نام ستون", Type.GetType("System.String"));

            dt.Columns.Add(dc1);

            for (int j = 0; j < sc.Count; j++)
            {
                DataRow dr = dt.NewRow();

                dr["نام ستون"] = sc[j];
                dt.Rows.Add(dr);
                ddlColumns.Items.Add(dr["نام ستون"].ToString());
                ddlColumns2.Items.Add(dr["نام ستون"].ToString());
                ddlColumns3.Items.Add(new ListItem(dr["نام ستون"].ToString(), j.ToString()));
            }

            gdwConditions.DataSource = dt;
            gdwConditions.DataBind();

            Panel1.Visible = false;
            Panel2.Visible = true;
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    protected void btnAddFilter_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlConditionType.SelectedItem.Value.Equals("BETWEEN"))
            {
                //BETWEEN
                ListItem li = new ListItem(ddlColumns.SelectedItem.Text + " " + ddlConditionType.SelectedItem.Text + " " + txtValue.Text + " " + rblAndOr.SelectedItem.Text, ddlColumns.SelectedItem.Value + " " + ddlConditionType.SelectedItem.Value + " " + "'" + txtValue.Text + "'" + " " + "AND" + " " + "'" + txtValue.Text + "'" + " " + rblAndOr.SelectedValue + " ");
                chbFilters.Items.Add(li);
            }
            else if (ddlConditionType.SelectedItem.Value.Equals("NOT BETWEEN "))
            {
                //NOT BETWEEN 
                ListItem li = new ListItem(ddlColumns.SelectedItem.Text + " " + ddlConditionType.SelectedItem.Text + " " + txtValue.Text + " " + rblAndOr.SelectedItem.Text, ddlColumns.SelectedItem.Value + " " + ddlConditionType.SelectedItem.Value + " " + "'" + txtValue.Text + "'" + " " + "AND" + " " + "'" + txtValue.Text + "'" + " " + rblAndOr.SelectedValue + " ");
                chbFilters.Items.Add(li);
            }
            else if (ddlConditionType.SelectedItem.Value.Equals("LIKE"))
            {
                //LIKE '%%'
                ListItem li = new ListItem(ddlColumns.SelectedItem.Text + " " + ddlConditionType.SelectedItem.Text + " " + txtValue.Text + " " + rblAndOr.SelectedItem.Text, ddlColumns.SelectedItem.Value + " " + ddlConditionType.SelectedItem.Value + " " + "'%" + txtValue.Text + "%'" + " " + rblAndOr.SelectedValue + " ");
                chbFilters.Items.Add(li);
            }
            else if (ddlConditionType.SelectedItem.Value.Equals("NOT LIKE"))
            {
                //NOT LIKE '%%'
                ListItem li = new ListItem(ddlColumns.SelectedItem.Text + " " + ddlConditionType.SelectedItem.Text + " " + txtValue.Text + " " + rblAndOr.SelectedItem.Text, ddlColumns.SelectedItem.Value + " " + ddlConditionType.SelectedItem.Value + " " + "'%" + txtValue.Text + "%'" + " " + rblAndOr.SelectedValue + " ");
                chbFilters.Items.Add(li);
            }
            else if (ddlConditionType.SelectedItem.Value.Equals("LIKE'%"))
            {
                //LIKE '%
                ListItem li = new ListItem(ddlColumns.SelectedItem.Text + " " + ddlConditionType.SelectedItem.Text + " " + txtValue.Text + " " + rblAndOr.SelectedItem.Text, ddlColumns.SelectedItem.Value + " " + ddlConditionType.SelectedItem.Value + " " + "'%" + txtValue.Text + "'" + " " + rblAndOr.SelectedValue + " ");
                chbFilters.Items.Add(li);
            }
            else if (ddlConditionType.SelectedItem.Value.Equals("LIKE%'"))
            {
                //LIKE %'
                ListItem li = new ListItem(ddlColumns.SelectedItem.Text + " " + ddlConditionType.SelectedItem.Text + " " + txtValue.Text + " " + rblAndOr.SelectedItem.Text, ddlColumns.SelectedItem.Value + " " + ddlConditionType.SelectedItem.Value + " " + "'" + txtValue.Text + "%'" + " " + rblAndOr.SelectedValue + " ");
                chbFilters.Items.Add(li);
            }
            else if (ddlConditionType.SelectedItem.Value.Equals("IS NULL"))
            {
                //IS NULL
                ListItem li = new ListItem(ddlColumns.SelectedItem.Text + " " + ddlConditionType.SelectedItem.Text + " " + txtValue.Text + " " + rblAndOr.SelectedItem.Text, ddlColumns.SelectedItem.Value + " " + ddlConditionType.SelectedItem.Value + " " + rblAndOr.SelectedValue + " ");
                chbFilters.Items.Add(li);
            }
            else if (ddlConditionType.SelectedItem.Value.Equals("IS NOT NULL"))
            {
                //IS NOT NULL
                ListItem li = new ListItem(ddlColumns.SelectedItem.Text + " " + ddlConditionType.SelectedItem.Text + " " + txtValue.Text + " " + rblAndOr.SelectedItem.Text, ddlColumns.SelectedItem.Value + " " + ddlConditionType.SelectedItem.Value + " " + rblAndOr.SelectedValue + " ");
                chbFilters.Items.Add(li);
            }
            else
            {
                //= > >= < <=
                ListItem li = new ListItem(ddlColumns.SelectedItem.Text + " " + ddlConditionType.SelectedItem.Text + " " + txtValue.Text + " " + rblAndOr.SelectedItem.Text, ddlColumns.SelectedItem.Value + " " + ddlConditionType.SelectedItem.Value + " " + "'" + txtValue.Text + "'" + " " + rblAndOr.SelectedValue + " ");
                chbFilters.Items.Add(li);
            }


            //for (int i = 0; i < chbFilters.Items.Count; i++)
            //{
            //    chbFilters.Items[i].Selected = true;
            //}
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }
    protected void lbtnSelectAll_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < chbFields.Items.Count; i++)
            {
                chbFields.Items[i].Selected = true;
            }
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }
    protected void gdwFilters_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }
    protected void lbtnSelectAll0_Click(object sender, EventArgs e)
    {
        try
        {

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

            SqlConnection cnn = new SqlConnection(cnnstrSource);
            cnn.Open();
            DataSet ds = new DataSet("Result");
            SqlDataAdapter da = new SqlDataAdapter(txtSQL.Text, cnn);
            da.Fill(ds, "Result");
            gdwExport.DataSource = ds;
            gdwExport.DataMember = "Result";
            gdwExport.DataBind();

            Panel2.Visible = false;
            Panel5.Visible = true;
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    int j = 0;
    protected void ddlSortType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            for (int k = 0; k < gdwConditions.Rows.Count; k++)
            {
                TextBox txt = (TextBox)gdwConditions.Rows[k].FindControl("txtSortRank");
                if (txt.Text != "")
                {
                    j = j + 1;
                }
            }
            for (int m = 0; m < gdwConditions.Rows.Count; m++)
            {
                DropDownList ddl = (DropDownList)gdwConditions.Rows[m].FindControl("ddlSortType");
                if (ddl.SelectedValue != "Unsorted")
                {
                    TextBox txt = (TextBox)gdwConditions.Rows[m].FindControl("txtSortRank");
                    if (txt.Text == string.Empty)
                    {
                        txt.Text = j.ToString();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    private decimal x = (decimal)0.0;
    protected void gdwExport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (chbAgreegate.Checked)
            {
                if (rblFunctions.SelectedValue.Equals("SUM"))
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        x += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, ddlColumns3.SelectedItem.Text));
                    }
                    else if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        e.Row.Cells[Convert.ToInt32(ddlColumns3.SelectedValue)].Text = "جمع: " + x.ToString();
                    }
                }
                else if (rblFunctions.SelectedValue.Equals("AVG"))
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        x += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, ddlColumns3.SelectedItem.Text));
                    }
                    else if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        x = x / gdwExport.Rows.Count;
                        e.Row.Cells[Convert.ToInt32(ddlColumns3.SelectedValue)].Text = "میانگین: " + x.ToString();
                    }
                }
                else if (rblFunctions.SelectedValue.Equals("COUNT"))
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        x = gdwExport.Rows.Count;
                    }
                    else if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        x++;
                        e.Row.Cells[Convert.ToInt32(ddlColumns3.SelectedValue)].Text = "تعداد: " + x.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }

    protected void lbtnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (lsbTables.SelectedItem.Selected)
            {
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + lsbTables.SelectedItem.Text + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite =
                new HtmlTextWriter(stringWrite);
                gdwExport.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
            }
            else if (lsbQueries.SelectedItem.Selected)
            {
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + lsbQueries.SelectedItem.Text + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite =
                new HtmlTextWriter(stringWrite);
                gdwExport.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
            }

        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }

        //// try
        //// {
        ////     //Export the GridView to Excel
        ////     Response.Clear();
        ////     Response.Buffer = true;
        ////     if (lsbTables.SelectedItem != null)
        ////     {
        ////         Response.AddHeader("content-disposition",
        //// "attachment;filename=" + lsbTables.SelectedItem.Text + ".xls");
        ////     }
        ////     else
        ////     {
        ////         Response.AddHeader("content-disposition",
        ////"attachment;filename=" + lsbQueries.SelectedItem.Text + ".xls");
        ////     }
        ////     Response.Charset = "UTF8";
        ////     Response.ContentType = "application/vnd.ms-excel";

        ////     StringWriter sw = new StringWriter();
        ////     HtmlTextWriter hw = new HtmlTextWriter(sw);
        ////     gdwExport.AllowPaging = false;
        ////     gdwExport.DataBind();

        ////     //Change the Header Row back to white color
        ////     gdwExport.HeaderRow.Style.Add("background-color", "Gray");

        ////     //Apply style to Individual Cells
        ////     for (int i = 0; i < gdwExport.HeaderRow.Cells.Count; i++)
        ////     {
        ////         gdwExport.HeaderRow.Cells[i].Style.Add("background-color", "Gray");
        ////     }

        ////     for (int i = 0; i < gdwExport.Rows.Count; i++)
        ////     {
        ////         GridViewRow row = gdwExport.Rows[i];

        ////         //Change Color back to white
        ////         row.BackColor = System.Drawing.Color.White;

        ////         //Apply text style to each Row
        ////         row.Attributes.Add("class", "textmode");

        ////         //Apply style to Individual Cells of Alternating Row
        ////         if (i % 2 != 0)
        ////         {
        ////             for (int j = 0; j < gdwExport.HeaderRow.Cells.Count; j++)
        ////             {
        ////                 row.Cells[j].Style.Add("background-color", "Gray");
        ////             }
        ////         }
        ////     }

        ////     gdwExport.RenderControl(hw);

        ////     //style to format numbers to string
        ////     string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        ////     Response.Write(style);
        ////     Response.Output.Write(sw.ToString());
        ////     Response.Flush();
        ////     Response.End();
        //// }
        //// catch (Exception ex)
        //// {
        ////     lblMSG.Text = ex.Message;
        //// }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void lbtnSaveQry_Click(object sender, EventArgs e)
    {
        try
        {
            StreamWriter sw = new StreamWriter(Server.MapPath("~/App_Data/Reports/" + txtReportName.Text + ".txt"));
            sw.Write(lblSRCd.Text + "*" + txtSQL.Text + "*");
            sw.Flush();
            sw.Close();
            lblMSG.Text = "گزارش با موفقیت ذخیره شد";
        }
        catch (Exception ex)
        {
            lblMSG.Text = ex.Message;
        }
    }
}