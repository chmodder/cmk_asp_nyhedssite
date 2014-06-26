using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestFeed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Feeds.CreateCatXml(3);


        ////Updates RSS feed
        //Feeds.CreateCatXml(Convert.ToInt32(Request.QueryString["category_id"]));
    }
}