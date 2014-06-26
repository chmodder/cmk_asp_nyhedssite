using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
                CreateRssLink(category_id);
            }
            else
            {
                // hvis det går galt med konverteringen, sendes brugeren tilbage til forsiden
                Response.Redirect("Default.aspx");
            }
        }
        BuildBreadCrumbs();
    }

    //Add this Method to Rss class (CreateLinkToExistingFeed)
    private void CreateRssLink(int CatId)
    {
        string CatTitle = GetCatTitle(CatId);


        foreach (var files in Directory.GetFiles(Server.MapPath("~/Assets/Rss/")))
        {
            //Gets complete serverpath and filename including extentions (unused)
            FileInfo info = new FileInfo(files);

            //Get filename incl. extention (no path or directory)
            var fileName = Path.GetFileName(info.FullName);

            //Get filename excl. extention (no path or directory)
            var fileNameNoExt = Path.GetFileNameWithoutExtension(info.FullName);

            if (fileNameNoExt == CatTitle)
            {
                CatLtl.Text = "<a href='Assets/Rss/" + fileName + "'><span class='label label-info'>" + "RSS: " + fileNameNoExt + "</span></a>";
            }

            #region Test


            //string RssLink = "";

            //if (fileNameNoExt != CatTitle)
            //{
            //    Feeds.CreateCatXml(CatId);
            //    RssLink = "<a href='Assets/Rss/" + fileName + "'><span class='label label-info'>" + "RSS: " + fileNameNoExt + "</span></a>";

            //    if (fileNameNoExt == CatTitle)
            //    {
            //        CatLtl.Text = RssLink;
            //    }
            //}
            #endregion
        }

    }

    //Add to Helper or Rss class to assist the method above (CreateRssLink)
    private string GetCatTitle(int CatId)
    {
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = @"
                        SELECT 
                            category_title
                        FROM categories
                           WHERE categories.category_id = @category_id
                        ";

        cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = CatId;
        conn.Open();
        string CatTitle = (string)cmd.ExecuteScalar();
        conn.Close();

        return CatTitle;
    }

    #region Pager



    private void GetCategoryNews(int category_id)
    {
        Pagination PageHandler = new Pagination(category_id, "SELECT COUNT(news_id) AS antal FROM news WHERE fk_categories_id = @category_id");

        PageHandler.NewsPerPage = 5;
        PageHandler.UpdateCurrentPageNo();

        //bind rowlist to repeater
        Repeater_Category.DataSource = PageHandler.GetRows(@"
                        SELECT news_id, news_title, news_content, news_postdate, user_name, category_id
                        FROM news
                        INNER JOIN categories ON category_id = fk_categories_id
                        INNER JOIN users ON user_id = fk_users_id
                        WHERE fk_categories_id = @category_id
                        ORDER BY news_postdate DESC
                        OFFSET @offset ROWS
                        FETCH NEXT @news_per_page ROWS ONLY");
        Repeater_Category.DataBind();


        PageHandler.SetPagerNav(PagerLtl);

        #region Old SQL
        //        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = conn;
        //        cmd.CommandText = @"
        //            SELECT 
        //                news_id
        //                , news_title
        //                , news_content
        //                , news_postdate
        //                , user_name
        //                , category_id
        //            FROM news
        //            INNER JOIN 
        //                categories ON category_id = news.fk_categories_id
        //            INNER JOIN 
        //                users ON user_id = news.fk_users_id 
        //            WHERE 
        //                news.fk_categories_id = @category_id
        //            ORDER BY 
        //                news_postdate DESC
        //            OFFSET 0 ROWS
        //            FETCH NEXT 5 ROWS ONLY";

        //        cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = category_id;
        //        conn.Open();
        //        Repeater_Category.DataSource = cmd.ExecuteReader();
        //        Repeater_Category.DataBind();
        //        conn.Close();
        #endregion
    }


    #endregion

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