using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Pagination
/// </summary>
public class Pagination
{
    #region Fields

    private int _currentPageNo;
    private int _newsPerPage;
    private int _newsInCategory;
    private int _categoryId;
    private string _links;


    #endregion

    #region constructors
    //public Pagination()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}

    /// <summary>
    /// Static CommandText
    /// </summary>
    /// <param name="CatId"></param>
    public Pagination(int CatId)
    {
        CategoryId = CatId;
        UpdateCurrentPageNo();

        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand("SELECT COUNT(NewsId) AS antal FROM news WHERE fk_categories_id = @category_id", conn);

        cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = CatId;

        cmd.Connection = conn;
        conn.Open();
        NewsInCategory = Convert.ToInt32(cmd.ExecuteScalar());
        conn.Close();

    }

    /// <summary>
    /// Universal constructor
    /// </summary>
    /// <param name="CatId"></param>
    /// <param name="CommandText"></param>
    public Pagination(int CatId, string CommandText)
    {
        CategoryId = CatId;
        UpdateCurrentPageNo();

        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand(CommandText, conn);

        cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = CatId;

        cmd.Connection = conn;
        conn.Open();
        NewsInCategory = Convert.ToInt32(cmd.ExecuteScalar());
        conn.Close();

    }

    #endregion


    #region Methods

    public void UpdateCurrentPageNo()
    {
        //string QueryStringName = 
        int TempPageId = 1;
        CurrentPageNo = TempPageId;
        if (HttpContext.Current.Request.QueryString["PageNo"] != null)
        {
            if (!int.TryParse(HttpContext.Current.Request.QueryString["PageNo"].ToString(), out TempPageId))
            {
                TempPageId = 1;
            }
        }
        CurrentPageNo = TempPageId;
    }

    public void BuildPager()
    {
        Links = "<ul class='pagination'>";
        for (int i = 1; i <= TotalPages; i++)
        {
            Links += "<li " + (CurrentPageNo == i ? "class='active'" : "") + ">";
            Links += "<a href='Categories.aspx?category_id=" + CategoryId + "&PageNo=" + i + "'>" + i + "</a>";
            Links += "</li>";
        }
        Links += "</ul>";

    }

    public DataTable GetRows(string CommandText)
    {
        int Offset = (CurrentPageNo - 1) * NewsPerPage;

        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand(CommandText, conn);

        cmd.Parameters.Add("@category_id", SqlDbType.Int).Value = CategoryId;
        cmd.Parameters.Add("@news_per_page", SqlDbType.Int).Value = NewsPerPage;
        cmd.Parameters.Add("@offset", SqlDbType.Int).Value = Offset;

        cmd.Connection = conn;

        DataTable Dt = new DataTable();
        conn.Open();

        SqlDataAdapter Da = new SqlDataAdapter(cmd);
        Da.Fill(Dt);

        return Dt;

        #region CommandTextExample
        ////CommandText Example:
        //SELECT news_id, news_title, news_content, news_postdate, user_name, category_id
        //                FROM news
        //                INNER JOIN categories ON category_id = fk_categories_id
        //                INNER JOIN users ON user_id = fk_users_id
        //                WHERE fk_categories_id = @category_id
        //                ORDER BY news_postdate DESC
        //                OFFSET @offset ROWS
        //                FETCH NEXT @news_per_page ROWS ONLY
        #endregion
    }

    public void SetPagerNav(Literal Pager)
    {
        BuildPager();
        Pager.Text = Links;
    }
    #endregion

    #region Properties

    public int TotalPages
    {
        get { return (int)Math.Ceiling((double)NewsInCategory / (double)NewsPerPage); }
    }

    public int NewsPerPage
    {
        get { return _newsPerPage; }
        set { _newsPerPage = value; }
    }

    public int NewsInCategory
    {
        get { return _newsInCategory; }
        set { _newsInCategory = value; }
    }


    public int CategoryId
    {
        get { return _categoryId; }
        set { _categoryId = value; }
    }

    public string Links
    {
        get { return _links; }
        set { _links = value; }
    }

    public int CurrentPageNo
    {
        get { return _currentPageNo; }
        set { _currentPageNo = value; }
    }


    #endregion
}