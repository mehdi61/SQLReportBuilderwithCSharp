using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Reports_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string[] ff = Directory.GetFiles(Server.MapPath("~/App_Data/Reports/"), "*.txt");
            for (int i = 0; i < ff.Length; i++)
            {
                ddlReports.Items.Add(ff[i].ToString()); 
            } 
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
}
