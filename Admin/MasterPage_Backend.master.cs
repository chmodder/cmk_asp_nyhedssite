using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MasterPage_Backend : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["user_id"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["message"] != null)
        {
            Panel_Message.Visible = true;
            Label_Message.Text = Session["message"].ToString();
            Session.Remove("message");
        }
    }



    public bool IsAdmin()
    {
        bool isAdmin = (int)Session["role_access"] == 100;

        return isAdmin;
    }

    public string Active(string Target)
    {
        return (Request.RawUrl.Contains(Target + " ") ? "class='active'" : "");
    }

    public string MenuItem(string Target, string Text)
    {


        string active = (Request.RawUrl.Contains(Target) ? " class='active'" : "");

        return String.Format("<li{2}><a href='{0}'>{1}</a></li>", Target, Text, active);

    }
    public string MenuItem(string Target, string Text, string OnClick)
    {
        string active = (Request.RawUrl.Contains(Target) ? " class='active'" : "");
        return String.Format("<li{2}><a href='{0}' onclick='{3}'>{1}</a></li>", Target, Text, active, OnClick);

    }
    public string BuildEditorNavigation()
    {

        string MenuItems = "";

        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand(@"SELECT category_title, category_id FROM categories ORDER BY category_title ASC", conn);

        string category_id = "0";
        if (Request.QueryString["category_id"] != null)
        {
            category_id = Request.QueryString["category_id"].ToString();
        }

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {

            string active = (Request.RawUrl.Contains("News.aspx") && category_id == reader["category_id"].ToString() ? " class='active'" : "");

            if ((int)Session["role_access"] == 100 || Helpers.EditorForThisCat(reader["category_id"]))
            {
                MenuItems += String.Format("<li{2}><a href='{0}'>{1}</a></li>", "News.aspx?category_id=" + reader["category_id"], reader["category_title"], active);

            }

        }
        conn.Close();

        return MenuItems;
    }
}
