using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Categories : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // man skal have administrations rettigheder for at være på denne side
        if (Session["role_access"] == null || Convert.ToInt32(Session["role_access"]) < 100)
        {
            Response.Redirect("../Login.aspx");
        }

        // præsenter den ønskede del af kategori administrationen, baseret på action i URL
        switch (Request.QueryString["action"])
        {
            case "add":
                Panel_Form.Visible = true;
                break;

            case "edit":
                if (Request.QueryString["category_id"] != null)
                {
                    Panel_Form.Visible = true;
                    GetItem(Request.QueryString["category_id"]);
                }
                break;

            case "delete":
                if (Request.QueryString["category_id"] != null)
                {
                    DeleteItem(Request.QueryString["category_id"]);
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
            SELECT category_id, category_title, category_description 
            FROM categories 
            ORDER BY category_title ASC", conn);
        DataTable items = new DataTable();
        adapter.Fill(items);

        Repeater_List.DataSource = items;
        Repeater_List.DataBind();
    }

    private void DeleteItem(string category_id)
    {
        try
        {
            SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM categories WHERE category_id = @id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = category_id;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Session["Message"] = "Kategorien blev slettet";
            Response.Redirect("Categories.aspx");
        }
        catch (Exception ex)
        {
            Panel_Error.Visible = true;
            Label_Error.Text = ex.Message + " <strong>DeleteItem(), Categories.aspx.cs</strong>";
        }
    }

    private void GetItem(string category_id)
    {
        if (!IsPostBack)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
                SqlCommand cmd = new SqlCommand(@"SELECT category_title, category_description FROM categories WHERE category_id = @id", conn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = category_id;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    TextBox_Title.Text = reader["category_title"].ToString();
                    TextBox_Description.Text = reader["category_description"].ToString();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Panel_Error.Visible = true;
                Label_Error.Text = ex.Message + " <strong>GetItem(), Categories.aspx.cs</strong>";
            }
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
                    cmd.CommandText = "INSERT INTO categories (category_title, category_description) VALUES (@category_title, @category_description)";
                    break;

                case "edit":
                    cmd.CommandText = "UPDATE categories SET category_title = @category_title, category_description = @category_description WHERE category_id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = Request.QueryString["category_id"];
                    break;
            }

            cmd.Parameters.Add("@category_title", SqlDbType.VarChar, 32).Value = TextBox_Title.Text;
            cmd.Parameters.Add("@category_description", SqlDbType.VarChar, 200).Value = TextBox_Description.Text;


            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Session["Message"] = "Kategorien blev gemt";
            Response.Redirect("Categories.aspx");
        }
        catch (Exception ex)
        {
            Panel_Error.Visible = true;
            Label_Error.Text = ex.Message + " <strong>Button_Save_Click(), Categories.aspx.cs</strong>";
        }
    }
}