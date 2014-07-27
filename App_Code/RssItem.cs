using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RssNamespace;

namespace RssNamespace
{
    /// <summary>
    /// Summary description for RssItem
    /// </summary>
    public class RssItem
    {
        #region fields

        private List<RssNamespace.KeyValuePair> _rssItems;

        #region OldAndUnused
        ////More info http://cyber.law.harvard.edu/rss/rss.html
        ////---------------------------------------------------
        //private string _title;
        ////Item title.
        //private string _link;
        ////Url of the item.
        //private string _description;
        ////Item synopsis.
        //private string _category;
        ////Includes the item in one or more categories.
        //private string _author;
        ////Email address of the author of the item.
        //private DateTime _pubDate;
        ////Indicates when the item was published.
        //private string _source;
        ////The RSS channel that the item came from.
        //private string _enclosure;
        ////Describes a media object that is attached to the item.
        #endregion
        #endregion

        #region Constructors
        public RssItem()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region Properties

        public List<KeyValuePair> RssItems
        {
            get { return _rssItems; }
            set { _rssItems = value; }
        }

        #region OldAndUnused
        //public string Title
        //{
        //    get { return _title; }
        //    set { _title = value; }
        //}

        //public string Link
        //{
        //    get { return _link; }
        //    set { _link = value; }
        //}

        //public string Description
        //{
        //    get { return _description; }
        //    set { _description = value; }
        //}

        //public string Category
        //{
        //    get { return _category; }
        //    set { _category = value; }
        //}

        //public string Author
        //{
        //    get { return _author; }
        //    set { _author = value; }
        //}

        //public DateTime PubDate
        //{
        //    get { return _pubDate; }
        //    set { _pubDate = value; }
        //}

        //public string Source
        //{
        //    get { return _source; }
        //    set { _source = value; }
        //}

        //public string Enclosure
        //{
        //    get { return _enclosure; }
        //    set { _enclosure = value; }
        //}
        #endregion

        #endregion

    }
}