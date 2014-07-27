using System;
using System.Activities.Statements;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Xml;
using RssNamespace;

/// <summary>
/// Summary description for FeedsRefactored
/// </summary>
public class FeedsRefactored
{
    //NotUsed
    #region Fields
    #region UnusedForNow
    //private string _SqlCommandText;
    //private string _rssVersion;
    //private string _encoding;
    //private string _channelTitle;
    //private string _channelLink;
    //private string _channelDescription;

    //private string _channelSqlCmd;
    //private string _itemSqlCmd;
    #endregion
    #endregion

    //NotUsed
    public FeedsRefactored()
    {
        //ChannelSqlCmd = channelSql;
        //ItemSqlCmd = itemSql;
    }

    #region Properties

    public static List<RssNamespace.KeyValuePair> ChannelData { get; set; }

    public static List<RssNamespace.KeyValuePair> ItemData { get; set; }

    public static List<RssItem> Items { get; set; }

    #region UnusedForNow

    //public string ChannelSqlCmd
    //{
    //    get { return _channelSqlCmd; }
    //    set { _channelSqlCmd = value; }
    //}

    //public string ItemSqlCmd
    //{
    //    get { return _itemSqlCmd; }
    //    set { _itemSqlCmd = value; }
    //}
    #endregion
    #endregion

    /// <summary>
    /// Important: Read info for inputparameters.
    /// Easiest way to achieve this, is to give each column an alias, that match Rss element naming rules.
    /// If inputparameter rules are not upheld, the method will break.
    /// </summary>
    /// <param name="catId"></param>
    /// <param name="channelSql">Database output MUST ONLY contain valid RSS elementnames</param>
    /// <param name="itemSql">Database output MUST ONLY contain valid RSS elementnames</param>
    public static void CreateRssFeed(string channelSql, string itemSql)
    {
        //path/filename needs fixing...
        string PathAndFileName = HttpContext.Current.Server.MapPath("~/Assets/Rss/") + (string)HttpContext.Current.Session["CatTitle"] + ".xml";

        //Create document
        XmlDocument Dom = new XmlDocument();
        //Create version and encoding and add as child to document (Dom)
        XmlProcessingInstruction Xpi = Dom.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
        Dom.AppendChild(Xpi);

        //Create Root element to document and set Root("Rss") attributes
        XmlElement Rss = Dom.CreateElement("rss");
        Rss.SetAttribute("version", "2.0");
        //Creates Channel element
        XmlElement Channel = Dom.CreateElement("channel");
        Rss.AppendChild(Channel);

        //Get SQL data and add to list in a field
        GetChannelData(channelSql);
        GetItemsData(itemSql);

        //Add channelData
        foreach (var Item in ChannelData)
        {
            XmlElement Temp = Dom.CreateElement(Item.Key);
            Temp.AppendChild(Dom.CreateCDataSection(Item.Value));
            Channel.AppendChild(Temp);
        }

        //Add Items/itemdata
        foreach (var Item in Items)
        {
            XmlElement ChannelItem = Dom.CreateElement("item");
            Channel.AppendChild(ChannelItem);
            foreach (var item in ItemData)
            {
                XmlElement Temp = Dom.CreateElement(item.Key);
                Temp.AppendChild(Dom.CreateCDataSection(item.Value));
                ChannelItem.AppendChild(Temp);
            }
        }
        Dom.AppendChild(Rss);
        Dom.Save(PathAndFileName);
    }

    private static void GetChannelData(string channelSql)
    {
        #region channelSql-Example
        //"SELECT
        //    categories.category_id AS CatId,
        //    categories.category_title AS ChannelName,
        //    categories.category_description AS ChannelDescription
        //FROM categories
        //WHERE category_id = 2"
        #endregion

        #region SqlDbQuery
        //call db
        //SQL (this can be added to dataAccessLayer)
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand(channelSql, conn);
        SqlDataAdapter Da = new SqlDataAdapter(cmd);
        DataTable Dt = new DataTable();
        Da.Fill(Dt);
        #endregion

        //Convert sqlData to ChannelData (property) with correct Rss-compatiple naming for each element in ArrayList

        //loop through every RssItem in the DataTable (Dt)

        foreach (DataRow row in Dt.Rows)
        {
            //Create Item (in a list<ItemData>)
            foreach (DataColumn column in Dt.Columns)
            {
                //create temporary KeyValuePair to hold data from each RowField/ColumnName

                KeyValuePair TempPair = new KeyValuePair();
                TempPair.Key = column.ColumnName;
                TempPair.Value = row[column].ToString();
                ChannelData.Add(TempPair);
            }

        }

    }

    private static void GetItemsData(string itemSql)
    {

        #region itemSql-Example
        //ExampleText:
        //
        //"SELECT TOP(5)
        //    news.news_id AS ItemId, 
        //    news.news_title AS ItemTitle, 
        //    news.news_content AS ItemContent,
        //    news.news_postdate AS PubDate, 
        //    users.user_name AS Author
        // FROM categories
        //    INNER JOIN news
        //        ON news.fk_categories_id = categories.category_id
        //    INNER JOIN users
        //        ON users.user_id = news.fk_users_id
        // WHERE category_id = 2
        //ORDER BY PubDate DESC"
        #endregion

        #region SqlDbQuery
        //call db
        //SQL (this can be converted to a method in a dataAccessLayer)
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand(itemSql, conn);
        SqlDataAdapter Da = new SqlDataAdapter(cmd);
        DataTable Dt = new DataTable();
        Da.Fill(Dt);
        #endregion

        //add sqlData to ItemData (property) with correct Rss-compatiple naming for each element in ArrayList
        //loops through every RssItem in the DataTable (Dt)

        foreach (DataRow row in Dt.Rows)
        {
            //Create Item (in a list<ItemData>)
            foreach (DataColumn Column in Dt.Columns)
            {
                //create temporary KeyValuePair to hold data from each RowField/ColumnName
                KeyValuePair TempPair = new KeyValuePair();
                TempPair.Key = Column.ColumnName;
                TempPair.Value = row[Column].ToString();

                //Add KeyValuePair to ItemData (List<KeyValuePair>)
                ItemData.Add(TempPair);
            }


            //Add ItemData, which is an RssItem (equialent to a DataTableRow) to ItemData,
            //which is a list of RssItems or List<RssItem> (equialent to a the whole DataTable).
            //It actually adds a List<KeyValuePair> to a new list List to create a list of lists.
            Items.Add(ItemData);

            //public static List<RssNamespace.KeyValuePair> ChannelData { get; set; }
            //public static List<RssNamespace.KeyValuePair> ItemData { get; set; }
            //public static List<RssItem> Items { get; set; }

            //Now we have the complete List of RssItems (converted from the DataTable) in the "Items" Property,
            //and we are ready to add the RssItems to the Xml-Document.
        }
    }

}
