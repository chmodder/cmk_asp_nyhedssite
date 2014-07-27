using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RssNamespace
{
    /// <summary>
    /// 2 fields/properties of datatype string
    /// </summary>
    public class KeyValuePair
    {
        #region Fields
        private string _key;
        private string _value;
        #endregion

        #region Constructors
        public KeyValuePair()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region Properties
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion
    }
}

