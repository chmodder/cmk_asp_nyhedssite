using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class News : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // denne side er afhængig af at have en news_id i URL
        if (Request.QueryString["news_id"] == null)
        {
            // findes den ikke, sendes brugeren tilbage til forsiden
            Response.Redirect("Default.aspx");
        }
        else
        {
            // når der er en news_id i URL, tjekkes på om den er en INT
            int news_id;
            if (int.TryParse(Request.QueryString["news_id"], out news_id))
            {
                GetNewsContent(news_id);
            }
            else
            {
                // hvis det går galt med konverteringen, sendes brugeren tilbage til forsiden
                Response.Redirect("Default.aspx");
            }
        }
        BuildBreadCrumbs();
    }

    private void GetNewsContent(int news_id)
    {
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = @"
            SELECT news.*, user_name 
            FROM news
            INNER JOIN users ON user_id = news.fk_users_id 
            WHERE news_id = @news_id";

        cmd.Parameters.Add("@news_id", SqlDbType.Int).Value = news_id;
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            Literal_NewsTitle.Text = reader["news_title"].ToString();
            Literal_BreadCrumb_NewsTitle.Text = reader["news_title"].ToString();
            Literal_NewsContent.Text = reader["news_content"].ToString();
            Label_UserName.Text = reader["user_name"].ToString();
            Label_NewsPostdate.Text = reader["news_postdate"].ToString();
        }
        conn.Close();
    }

    private void BuildBreadCrumbs()
    {
        // denne side er afhængig af at have en category_id i URL
        if (Request.QueryString["category_id"] == null)
        {
            // findes den ikke, sendes brugeren tilbage til forsiden
            Response.Redirect("Default.aspx");
        }
        else
        {
            // når der er en category_id i URL, tjekkes på om den er en INT
            int category_id;
            if (int.TryParse(Request.QueryString["category_id"], out category_id))
            {
                SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT category_id, category_title FROM categories WHERE category_id = @category_id";

                cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = Request.QueryString["category_id"];
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    HyperLink_BreadCrumb_Category.NavigateUrl = "~/Categories.aspx?category_id=" + reader["category_id"];
                    HyperLink_BreadCrumb_Category.Text = reader["category_title"].ToString();
                }
                conn.Close();
            }
            else
            {
                // hvis det går galt med konverteringen, sendes brugeren tilbage til forsiden
                Response.Redirect("Default.aspx");
            }
        }
    }
}