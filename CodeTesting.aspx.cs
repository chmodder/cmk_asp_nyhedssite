using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CodeTesting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var fileName = "sports";
        FeedsRefactored.CreateRssFeed("SELECT " +
                                      "categories.category_id AS CatId, " +
                                      "categories.category_title AS ChannelName, " +
                                      "categories.category_description AS ChannelDescription " +
                                      "FROM categories " +
                                      "WHERE category_id = 2",
                                      "SELECT TOP(5)" +
                                      "news.news_id AS ItemId, " +
                                      "news.news_title AS ItemTitle, " +
                                      "news.news_content AS ItemContent, " +
                                      "news.news_postdate AS PubDate, " +
                                      "users.user_name AS Author " +
                                      "FROM categories " +
                                      "INNER JOIN news " +
                                      "ON news.fk_categories_id = categories.category_id " +
                                      "INNER JOIN users " +
                                      "ON users.user_id = news.fk_users_id " +
                                      "WHERE category_id = 2 " +
                                      "ORDER BY PubDate DESC", 
                                      fileName);


        WriteItems();
    }

    private void WriteItems()
    {
       foreach (List<RssNamespace.KeyValuePair> Item in FeedsRefactored.Items)
       {
           Response.Write(Item[0] + " " + Item[0].Value + "<br />");
           Response.Write(Item[1].Key + " " + Item[1].Value + "<br />");
           Response.Write(Item[2].Key + " " + Item[2].Value + "<br />");
           Response.Write(Item[3].Key + " " + Item[3].Value + "<br />");
           Response.Write(Item[4].Key + " " + Item[4].Value + "<hr />");
       }
    }
}