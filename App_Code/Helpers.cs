using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Security;

/// <summary>
/// Samling af små hjælpe funktioner
/// </summary>
public class Helpers
{
    /// <summary>
    /// Her sættes den "globale" connection string
    /// </summary>
    public static string ConnectionString = ConfigurationManager.ConnectionStrings["cmk_asp_nyhedssite"].ToString();



    /// <summary>
    /// Trim en string ned til en bestemt maksimum længde
    /// </summary>
    /// <param name="FullText">Den fulde tekst der skal klippes af</param>
    /// <param name="MaxLength">Antallet af tegn der skal trimmes ned til</param>
    /// <returns></returns>
    public static string EvalTrimmed(string FullText, int MaxLength)
    {
        // Teksten kan indeholde HTML tags, som ikke bør blive klippet over,
        // derfor fjernes alt der ligger imellem to <>
        FullText = Regex.Replace(FullText, "<.*?>", string.Empty);
        // hvis teksten stadig er længere end MaxLength
        if (FullText.Length > MaxLength)
        {
            // så returneres det ønskede antal tegn, plus tre ...
            return FullText.Substring(0, MaxLength - 3) + "..."; ;
        }
        // hvis teksten ikke er længere end MaxLength, returneres den den HTML frie tekst
        return FullText;
    }

    private static string Salt = "IbsensGibsGebis";

    public static string HashNSalt(string UserEmail, string UserPassWord)
    {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(UserPassWord + UserEmail + Salt, "MD5");
    }


    public static bool EditorForThisCat(object catId)
    {
        bool IsEditor = false;
        ArrayList ThisEditorCatIdList = (ArrayList)HttpContext.Current.Session["cat_id"];

        if (ThisEditorCatIdList.Contains(catId))
        {
            IsEditor = true;
        }
        return IsEditor;
    }

    public static bool Test(object catId)
    {
        bool IsEditor = false;
        ArrayList ThisEditorCatIdList = (ArrayList)HttpContext.Current.Session["cat_id"];

        if (ThisEditorCatIdList.Contains(catId))
        {
            IsEditor = true;
        }
        return IsEditor;
    }

    public static void EditorCatList()
    {
        SqlConnection Conn = new SqlConnection(ConnectionString);
        SqlCommand Cmd = new SqlCommand("SELECT fk_categories_id FROM category_editors WHERE fk_users_id = @UserId");

        Cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = HttpContext.Current.Session["user_id"];

        Cmd.Connection = Conn;

        ArrayList CatIdList = new ArrayList();

        Conn.Open();
        SqlDataReader Reader = Cmd.ExecuteReader();

        while (Reader.Read())
        {
            CatIdList.Add(Reader["fk_categories_id"]);
        }
        Conn.Close();
        HttpContext.Current.Session["cat_id"] = CatIdList;
    }

    /// <summary>
    /// CKEditor htmldecoder.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string ReverseHtmlEncoding(string text)
    {
        return text.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
    }
}