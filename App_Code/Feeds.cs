using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for Feeds
/// </summary>
public class Feeds
{
    public Feeds()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //Static data. Just practicing
    public static void CreatePersonXml()
    {
        XmlDocument Dom = new XmlDocument();
        XmlProcessingInstruction Xpi = Dom.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
        Dom.AppendChild(Xpi);

        //Create Root element
        XmlElement Rod = Dom.CreateElement("Rod");
        Rod.SetAttribute("Attribut", "Værdi");

        //Create Person element
        XmlElement Person = Dom.CreateElement("Person");

        //Create Person child-elements
        XmlElement Navn = Dom.CreateElement("Navn");
        Navn.AppendChild(Dom.CreateTextNode("Cookie Monster"));
        Person.AppendChild(Navn);

        XmlElement Status = Dom.CreateElement("Status");
        Status.AppendChild(Dom.CreateTextNode("Sulten"));
        Person.AppendChild(Status);

        Rod.AppendChild(Person);
        Dom.AppendChild(Rod);
        Dom.Save(HttpContext.Current.Server.MapPath("~/") + "Person.xml");
    }

    public static void CreateCatXml(int catId)
    {

        //Create document
        XmlDocument Dom = new XmlDocument();
        //Create version and encoding and add as child to document (Dom)
        XmlProcessingInstruction Xpi = Dom.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
        Dom.AppendChild(Xpi);

        //XmlProcessingInstruction Xpi = Dom.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
        //Dom.AppendChild(Xpi);

        //Create Root element to document and set Root attributes
        XmlElement Rss = Dom.CreateElement("rss");
        Rss.SetAttribute("version", "2.0");
        XmlElement Channel = Dom.CreateElement("channel");
        Rss.AppendChild(Channel);


        //SQL
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand(@"
                                        SELECT TOP(5)
                                            categories.category_id AS CatId, 
                                            categories.category_title AS ChannelName, 
                                            categories.category_description AS ChannelDescription, 
                                            news.news_id AS ItemId, 
                                            news.news_title AS ItemTitle, 
                                            news.news_content AS ItemContent,
                                            news.news_postdate AS PubDate, 
                                            users.user_name AS Author
                                        FROM categories
                                            INNER JOIN news
	                                            ON news.fk_categories_id = categories.category_id
                                            INNER JOIN users
	                                            ON users.user_id = news.fk_users_id
                                        WHERE category_id = @CatId
                                        ORDER BY PubDate DESC", conn);

        cmd.Parameters.Add("@CatId", SqlDbType.Int).Value = catId;

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        XmlElement ChannelTitle = Dom.CreateElement("title");
        Channel.AppendChild(ChannelTitle);

        XmlElement ChannelLink = Dom.CreateElement("link");
        Channel.AppendChild(ChannelLink);

        XmlElement ChannelDesc = Dom.CreateElement("description");
        Channel.AppendChild(ChannelDesc);

        XmlElement ChannelPubDate = Dom.CreateElement("pubDate");
        Channel.AppendChild(ChannelPubDate);

        while (reader.Read())
        {
            HttpContext.Current.Session["CatTitle"] = reader["ChannelName"].ToString();


            HttpContext.Current.Session["ChannelName"] = reader["ChannelName"].ToString();
            HttpContext.Current.Session["ChannelId"] = reader["CatId"].ToString();
            HttpContext.Current.Session["ChannelDesc"] = reader["ChannelDescription"].ToString();
            //HttpContext.Current.Session["PubDate"] = reader["PubDate"].ToString();

            XmlElement ChannelItem = Dom.CreateElement("item");
            Channel.AppendChild(ChannelItem);

            XmlElement ItemTitle = Dom.CreateElement("title");
            ItemTitle.AppendChild(Dom.CreateTextNode(reader["ItemTitle"].ToString()));
            ChannelItem.AppendChild(ItemTitle);

            //XmlElement ItemLink = Dom.CreateElement("link");
            //ItemLink.AppendChild(Dom.CreateTextNode("http://localhost:58302/cmk_asp_nyhedssite/News.aspx?category_id=" + reader["CatId"].ToString() + HttpContext.Current.Server.UrlEncode("&#x26;") + "news_id=" + reader["ItemId"].ToString()));
            //ChannelItem.AppendChild(ItemLink);

            XmlElement ItemLink = Dom.CreateElement("link");
            ItemLink.AppendChild(Dom.CreateCDataSection("http://localhost:58302/cmk_asp_nyhedssite/News.aspx?category_id=" + reader["CatId"].ToString() +  "&news_id=" + reader["ItemId"].ToString()));
            ChannelItem.AppendChild(ItemLink);

            XmlElement ItemDesc = Dom.CreateElement("description");
            ItemDesc.AppendChild(Dom.CreateCDataSection(reader["ItemContent"].ToString()));
            ChannelItem.AppendChild(ItemDesc);

            XmlElement ItemPubDate = Dom.CreateElement("pubDate");
            ItemPubDate.AppendChild(Dom.CreateTextNode(reader["PubDate"].ToString()));
            ChannelItem.AppendChild(ItemPubDate);

            XmlElement ItemAuthor = Dom.CreateElement("author");
            ItemAuthor.AppendChild(Dom.CreateTextNode(reader["Author"].ToString()));
            ChannelItem.AppendChild(ItemAuthor);

        }

        //Channel data. Only 1 item, which is why it is placed outside the loop
        ChannelTitle.AppendChild(Dom.CreateTextNode(HttpContext.Current.Session["ChannelName"].ToString()));
        ChannelLink.AppendChild(Dom.CreateTextNode("http://localhost:58302/cmk_asp_nyhedssite/Categories.aspx?category_id=" + HttpContext.Current.Session["ChannelId"].ToString()));
        ChannelDesc.AppendChild(Dom.CreateTextNode(HttpContext.Current.Session["ChannelDesc"].ToString()));
        ChannelPubDate.AppendChild(Dom.CreateTextNode("Last updated " + DateTime.Now.ToString()));


        conn.Close();
        Dom.AppendChild(Rss);
        Dom.Save(HttpContext.Current.Server.MapPath("~/Assets/Rss/") + (string)HttpContext.Current.Session["CatTitle"] + ".xml");
    }


}