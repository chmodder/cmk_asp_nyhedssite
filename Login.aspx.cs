using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label_Errors.Visible = false;
        if (Request.QueryString["action"] != null)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
    protected void Button_Login_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = @"
            SELECT user_id, user_name, user_email, role_access
            FROM users 
            INNER JOIN roles ON role_id = fk_roles_id
            WHERE 
                user_email = @user_email 
            AND 
                user_password = @user_password";
        cmd.Parameters.Add("@user_email", SqlDbType.VarChar, 32).Value = TextBox_Email.Text;
        cmd.Parameters.Add("@user_password", SqlDbType.VarChar, 200).Value = TextBox_Password.Text;
        
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            Session["user_id"] = reader["user_id"];
            Session["user_name"] = reader["user_name"];
            Session["user_email"] = reader["user_email"];
            Session["role_access"] = reader["role_access"];
            Response.Redirect("Default.aspx");
        }
        else
        {
            Label_Errors.Text = "Forkert Email eller Password";
            Label_Errors.Visible = true;
        }
        conn.Close();
    }
}