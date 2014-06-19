using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_id"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (Convert.ToInt32(Session["role_access"]) < 10)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}