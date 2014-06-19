using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Categories : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
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
                // hvis vi har en category_id af typen INT i URL, så henter vi kategoriens nyheder
                GetCategoryNews(category_id);
            }
            else
            {
                // hvis det går galt med konverteringen, sendes brugeren tilbage til forsiden
                Response.Redirect("Default.aspx");
            }
        }
        BuildBreadCrumbs();
    }

    private void GetCategoryNews(int category_id)
    {
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = @"
            SELECT 
                news_id
                , news_title
                , news_content
                , news_postdate
                , user_name
                , category_id
            FROM news
            INNER JOIN 
                categories ON category_id = news.fk_categories_id
            INNER JOIN 
                users ON user_id = news.fk_users_id 
            WHERE 
                news.fk_categories_id = @category_id
            ORDER BY 
                news_postdate DESC
            OFFSET 0 ROWS
            FETCH NEXT 5 ROWS ONLY";

        cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = category_id;
        conn.Open();
        Repeater_Category.DataSource = cmd.ExecuteReader();
        Repeater_Category.DataBind();
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
                    Literal_BreadCrumb_CategoryTitle.Text = reader["category_title"].ToString();
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