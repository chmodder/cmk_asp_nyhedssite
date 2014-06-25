using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_News : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UrlCatId = Request.QueryString["category_id"];

        bool RoleUser = RoleIsUser();
        bool RoleNull = RoleIsNull();
        bool CIdNoAccess = CatIdNoAccess(UrlCatId);

        // man skal have administrations rettigheder for at være på denne side
        if (RoleNull || RoleUser || (CIdNoAccess && (int)Session["role_access"] < 100))
        {
            Response.Redirect("../Login.aspx");
        }

        //old code
        //// man skal have administrations rettigheder for at være på denne side
        //if (Session["role_access"] == null || Convert.ToInt32(Session["role_access"]) < 10 || Helpers.Test(UrlCatId) != true)
        //{
        //    Response.Redirect("../Login.aspx");
        //}

        // præsenter den ønskede del af kategori administrationen, baseret på action i URL
        switch (Request.QueryString["action"])
        {
            case "add":
                Panel_Form.Visible = true;
                GetCategories();
                HyperLink_Cancel.NavigateUrl = "News.aspx?category_id=" + Request.QueryString["category_id"];
                break;

            case "edit":
                if (Request.QueryString["news_id"] != null)
                {
                    Panel_Form.Visible = true;
                    HyperLink_Cancel.NavigateUrl = "News.aspx?category_id=" + Request.QueryString["category_id"];
                    GetCategories();
                    GetItem(Request.QueryString["news_id"]);
                }
                break;

            case "delete":
                if (Request.QueryString["news_id"] != null)
                {
                    DeleteItem(Request.QueryString["news_id"]);
                }
                break;

            default:
                if (Request.QueryString["category_id"] != null)
                {
                    GetItems(Request.QueryString["category_id"]);
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
                break;
        }
    }

    private bool CatIdNoAccess(string urlCatId)
    {
        bool CIdNoAccess = true;
        if (Helpers.Test(Convert.ToInt32(urlCatId)) == true)
        {
            CIdNoAccess = false;
        }
        return CIdNoAccess;
    }

    private bool RoleIsNull()
    {
        bool SessionNull = true;
        if (Session["role_access"] != null)
        {
            SessionNull = false;
        }
        return SessionNull;
    }

    private bool RoleIsUser()
    {
        bool RoleIsUser = true;
        if (!(Convert.ToInt32(Session["role_access"]) < 10))
        {
            RoleIsUser = false;
        }
        return RoleIsUser;
    }

    private void GetCategories()
    {
        if (!IsPostBack)
        {
            SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(@"SELECT category_id, category_title FROM categories ORDER BY category_title ASC", conn);
            DataTable items = new DataTable();
            adapter.Fill(items);

            DropDownList_Category.DataTextField = "category_title";
            DropDownList_Category.DataValueField = "category_id";

            DropDownList_Category.DataSource = items;
            DropDownList_Category.DataBind();
            DropDownList_Category.Items.Insert(0, new ListItem("Vælg Kategori", "0"));
            DropDownList_Category.SelectedValue = Request.QueryString["category_id"];
        }
    }
    private void GetItems(string category_id)
    {
        Panel_List.Visible = true;
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand(@"
            SELECT news_id, news_title, news_content, news_postdate, fk_categories_id, user_name
            FROM news 
            INNER JOIN users ON user_id = fk_users_id
            WHERE fk_categories_id = @category_id
            ORDER BY news_postdate DESC", conn);
        cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = category_id;

        conn.Open();
        Repeater_List.DataSource = cmd.ExecuteReader();
        Repeater_List.DataBind();
        conn.Close();
        HyperLink_Add_Footer.NavigateUrl = "News.aspx?action=add&category_id=" + category_id;
        HyperLink_Add_Header.NavigateUrl = "News.aspx?action=add&category_id=" + category_id;
    }

    private void DeleteItem(string news_id)
    {
        try
        {
            SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM news WHERE news_id = @id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = news_id;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Session["Message"] = "Nyheden blev slettet";
            Response.Redirect("News.aspx");
        }
        catch (Exception ex)
        {
            Panel_Error.Visible = true;
            Label_Error.Text = ex.Message + " <strong>DeleteItem(), News.aspx.cs</strong>";
        }
    }

    private void GetItem(string news_id)
    {
        if (!IsPostBack)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
                SqlCommand cmd = new SqlCommand(@"SELECT news_title, news_content, fk_categories_id FROM news WHERE news_id = @id", conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = news_id;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    TextBox_Title.Text = reader["news_title"].ToString();
                    editor1.Text = reader["news_content"].ToString();
                    DropDownList_Category.SelectedValue = reader["fk_categories_id"].ToString();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Panel_Error.Visible = true;
                Label_Error.Text = ex.Message + " <strong>GetItem(), News.aspx.cs</strong>";
            }
        }
    }

    protected void Button_Save_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            switch (Request.QueryString["action"])
            {
                case "add":
                    cmd.CommandText = "INSERT INTO news (news_title, news_content, news_postdate, fk_users_id, fk_categories_id) VALUES (@news_title, @news_content, GETDATE(), @user_id, @category_id)";
                    cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = Session["user_id"];
                    cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = Request.QueryString["category_id"];
                    break;

                case "edit":
                    cmd.CommandText = "UPDATE news SET news_title = @news_title, news_content = @news_content WHERE news_id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["news_id"];
                    break;
            }

            cmd.Parameters.Add("@news_title", SqlDbType.VarChar, 32).Value = TextBox_Title.Text;
            cmd.Parameters.Add("@news_content", SqlDbType.Text).Value = Helpers.ReverseHtmlEncoding(editor1.Text);
            //cmd.Parameters.Add("@news_content", SqlDbType.Text).Value = editor1.Text;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Session["Message"] = "Nyheden blev gemt";
            Response.Redirect("News.aspx?category_id=" + Request.QueryString["category_id"]);
        }
        catch (Exception ex)
        {
            Panel_Error.Visible = true;
            Label_Error.Text = ex.Message + " <strong>Button_Save_Click(), News.aspx.cs</strong>";
        }
    }
}