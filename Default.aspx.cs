using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = @"
            SELECT TOP(5) news_id, news_title, news_content, news_postdate, category_id, category_title, user_name 
	        FROM news
	        INNER JOIN categories ON categories.category_id = news.fk_categories_id
	        INNER JOIN users ON users.user_id = news.fk_users_id
	        ORDER BY news_postdate DESC";

        conn.Open();
        Repeater_Frontpage.DataSource = cmd.ExecuteReader();
        Repeater_Frontpage.DataBind();
        conn.Close();
    }
}