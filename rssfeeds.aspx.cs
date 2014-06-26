using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rssfeeds : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //Add method to RSS class (CreateRssList)
        foreach (var files in Directory.GetFiles(Server.MapPath("~/Assets/Rss/")))
        {
            //Gets complete serverpath and filename including extentions (unused)
            FileInfo info = new FileInfo(files);

            //Get filename incl. extention (no path or directory)
            var fileName = Path.GetFileName(info.FullName);

            //Get filename excl. extention (no path or directory)
            var fileNameNoExt = Path.GetFileNameWithoutExtension(info.FullName);


            RssListLtl.Text += "<a href='Assets/Rss/" + fileName + "'><span class='label label-info'>" + fileNameNoExt + "</span></a><br/>";
        }

        #region Testcode draft
        //foreach (string LinkToFeed in Directory.GetFiles(Server.MapPath("/App_Data/Rss/"), "*.xml")).Select
        //{
        //    RssListLtl.Text = "\"<li><a href='\" + LinkToFeed + \"'><span class='label label-info'></span></a></li>";
        //}

        //foreach (string LinkToFeed in Directory.GetFiles(Server.MapPath("/App_Data/Rss/"), "*.xml")).Select
        //{
        //    RssListLtl.Text = "\"<li><a href='\" + LinkToFeed + \"'><span class='label label-info'></span></a></li>";
        //}

        //string[] filePaths = Directory.GetFiles(Server.MapPath("~/App_Data/Rss/"), "*.xml", SearchOption.TopDirectoryOnly);
        #endregion
    }
}