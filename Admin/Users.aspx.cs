using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // man skal have administrations rettigheder for at være på denne side
        if (Session["role_access"] == null || Convert.ToInt32(Session["role_access"]) < 100)
        {
            Response.Redirect("../Login.aspx");
        }

        // præsenter den ønskede del af bruger administrationen, baseret på action i URL
        switch (Request.QueryString["action"])
        {
            case "add":
                Panel_Form.Visible = true;
                GetRoles();
                break;

            case "edit":
                if (Request.QueryString["user_id"] != null)
                {
                    Panel_Form.Visible = true;
                    GetItem(Request.QueryString["user_id"]);
                }
                break;

            case "delete":
                if (Request.QueryString["user_id"] != null)
                {
                    DeleteItem(Request.QueryString["user_id"]);
                }
                break;

            default:
                GetItems();
                break;
        }
    }

    private void GetItems()
    {
        Panel_List.Visible = true;
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlDataAdapter adapter = new SqlDataAdapter(@"
            SELECT user_id, user_name, user_email, role_title
            FROM users
            INNER JOIN roles ON role_id = fk_roles_id
            ORDER BY role_access DESC, user_name ASC", conn);
        DataTable items = new DataTable();
        adapter.Fill(items);

        Repeater_List.DataSource = items;
        Repeater_List.DataBind();


    }

    private void DeleteItem(string user_id)
    {
        try
        {
            SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE user_id= @id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = user_id;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Session["Message"] = "Brugeren blev slettet";
            Response.Redirect("Users.aspx");
        }
        catch (Exception ex)
        {
            Panel_Error.Visible = true;
            Label_Error.Text = ex.Message + " <strong>DeleteItem(), Users.aspx.cs</strong>";
        }
    }

    private void GetItem(string user_id)
    {
        if (!IsPostBack)
        {
            try
            {
                GetRoles();
                SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
                SqlCommand cmd = new SqlCommand(@"SELECT user_name, user_email, fk_roles_id FROM users WHERE user_id = @id", conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = user_id;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    TextBox_Name.Text = reader["user_name"].ToString();
                    TextBox_Email.Text = reader["user_email"].ToString();
                    DropDownList_Role.SelectedValue = reader["fk_roles_id"].ToString();
                }
                conn.Close();

            }
            catch (Exception ex)
            {
                Panel_Error.Visible = true;
                Label_Error.Text = ex.Message + " <strong>GetItem(), Users.aspx.cs</strong>";
            }
        }
    }

    private void GetRoles()
    {
        if (!IsPostBack)
        {
            SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(@"
            SELECT role_id, role_title
            FROM roles
            ORDER BY role_access DESC", conn);
            DataTable items = new DataTable();
            adapter.Fill(items);

            DropDownList_Role.DataTextField = "role_title";
            DropDownList_Role.DataValueField = "role_id";

            DropDownList_Role.DataSource = items;
            DropDownList_Role.DataBind();
            DropDownList_Role.Items.Insert(0, new ListItem("Vælg Rolle", "0"));
        }
    }

    protected void Button_Save_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            switch (Request.QueryString["action"])
            {
                case "add":
                    if (TextBox_Password.Text == "" || TextBox_Password_Repeat.Text == "")
                    {
                        throw new FormatException("Udfyld Brugernavn");
                    }
                    cmd.CommandText = "INSERT INTO users (user_name, user_email, user_password, fk_roles_id) VALUES (@user_name, @user_email, @user_password, @role_id)";
                    cmd.Parameters.Add("@user_password", SqlDbType.VarChar, 32).Value = Helpers.HashNSalt(TextBox_Email.Text, TextBox_Password.Text);
                    break;

                case "edit":
                    cmd.CommandText = "UPDATE users SET user_name = @user_name, user_email = @user_email, fk_roles_id = @role_id, user_password = @user_password WHERE user_id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["user_id"];
                    cmd.Parameters.Add("@user_password", SqlDbType.VarChar, 32).Value = Helpers.HashNSalt(TextBox_Email.Text, TextBox_Password.Text);
                    break;
            }

            cmd.Parameters.Add("@user_name", SqlDbType.VarChar, 32).Value = TextBox_Name.Text;
            cmd.Parameters.Add("@user_email", SqlDbType.VarChar, 200).Value = TextBox_Email.Text;
            cmd.Parameters.Add("@role_id", SqlDbType.Int).Value = DropDownList_Role.SelectedValue;


            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Session["Message"] = "Brugeren blev gemt";
            Response.Redirect("Users.aspx");
        }
        catch (FormatException ex)
        {
            Panel_Error.Visible = true;
            Label_Error.Text = "Udfyld kodeord";
        }
        catch (Exception ex)
        {
            Panel_Error.Visible = true;
            Label_Error.Text = ex.Message + " <strong>Button_Save_Click(), Users.aspx.cs</strong>";
        }
    }
}