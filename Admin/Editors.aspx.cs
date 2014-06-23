using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Editors : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((int)Session["role_access"] < 100)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void CatSelectBtn_Click(object sender, EventArgs e)
    {
        nonEditors();
        Editors();
    }

    private void Editors()
    {
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = @" 
            SELECT user_id, user_name  
            FROM users 
            INNER JOIN category_editors ON fk_users_id =user_id 
            WHERE fk_categories_id = @Category 
            AND fk_roles_id = 3";
        cmd.Parameters.Add("@Category", SqlDbType.Int).Value = CatDdl.SelectedValue;

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable DataTable_Editors = new DataTable();
        ad.Fill(DataTable_Editors);
        ListBox_Editors.DataSource = DataTable_Editors;
        ListBox_Editors.DataBind();
    }

    private void nonEditors()
    {
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = @" 
            SELECT user_id, user_name  
            FROM users 
            WHERE user_id NOT IN ( 
            SELECT DISTINCT fk_users_id 
            FROM category_editors 
            WHERE fk_categories_id = @category ) 
            AND fk_roles_id = 3";
        cmd.Parameters.Add("@category", SqlDbType.Int).Value = CatDdl.SelectedValue;
        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataTable DataTable_NotEditors = new DataTable();
        ad.Fill(DataTable_NotEditors);
        ListBox_NotEditors.DataSource = DataTable_NotEditors;
        ListBox_NotEditors.DataBind();
    }


    private void removeEditor()
    {
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = @" 
            DELETE FROM category_editors  
            WHERE fk_categories_id = @category  
            AND fk_users_id = @user";

        cmd.Parameters.Add("@category", SqlDbType.Int).Value = CatDdl.SelectedValue;
        cmd.Parameters.Add("@user", SqlDbType.Int).Value = 0;

        conn.Open();
        foreach (ListItem user in ListBox_Editors.Items)
        {
            if (user.Selected)
            {
                cmd.Parameters["@user"].Value = user.Value;
                cmd.ExecuteNonQuery();
            }
        }
        conn.Close();
    }

    private void addEditor()
    {
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = @"INSERT INTO category_editors VALUES(@user, @category)";

        cmd.Parameters.Add("@category", SqlDbType.Int).Value = CatDdl.SelectedValue;
        cmd.Parameters.Add("@user", SqlDbType.Int).Value = 0;

        conn.Open();
        foreach (ListItem user in ListBox_NotEditors.Items)
        {
            if (user.Selected)
            {
                cmd.Parameters["@user"].Value = user.Value;
                cmd.ExecuteNonQuery();
            }
        }
        conn.Close(); 
    }

    protected void DeselectBtn_Click(object sender, EventArgs e)
    {
        ListBox_Editors.ClearSelection();
        ListBox_NotEditors.ClearSelection();
    }
    protected void MoveBtn_Click(object sender, EventArgs e)
    {
        addEditor();
        removeEditor();
        nonEditors();
        Editors();
        Helpers.EditorCatList();

    }
}