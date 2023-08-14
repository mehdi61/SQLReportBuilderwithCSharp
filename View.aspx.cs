using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Count != 0)
            {
                StreamReader sr = new StreamReader(Server.MapPath("~/App_Data/Reports/" + Request.QueryString["ReportName"] + ".txt"));
                string m = sr.ReadToEnd();
                string[] spt = m.Split('*');


                SqlDataSource1.ConnectionString = spt[0];
                SqlDataSource1.SelectCommand = spt[1];
                SqlDataSource1.ConflictDetection = ConflictOptions.CompareAllValues;
                SqlDataSource1.DeleteCommand = "";
                SqlDataSource1.DataBind(); 
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}
